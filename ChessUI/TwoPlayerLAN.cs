using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ChessSharp;
using ChessSharp.Pieces;
using ChessSharp.SquareData;
using System.Media;
using Stockfish.NET;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ChessUI.DashboardForm;
using Google.Cloud.Firestore;
using System.IO;
using Newtonsoft.Json;
using ChessUI.Models;
using System.Net.Http;
namespace ChessUI
{
    public partial class TwoPlayerLAN : Form
    {
        private bool isRemoteMove = false;
        private TcpClient client;
        private TcpListener server;
        private NetworkStream clientStream;
        private NetworkStream stream;
        private byte[] message = new byte[4096];
        private TimeSpan gameTime;
        private Player initialPlayer;
        private string difficulty;
        private Label[] _squareLabels;
        private Dictionary<string, Point> _whiteLocations;
        private Dictionary<string, Point> _blackLocations;
        private Square _selectedSourceSquare;
        private ChessGame _gameBoard = new ChessGame();
        private System.Windows.Forms.Timer timer;
        private TimeSpan whiteTimeRemaining;
        private TimeSpan blackTimeRemaining;
        private byte[] buffer = new byte[4096];
        private bool isHost;
        private string ip;
        private FirestoreDb _firestoreDb;
        private CollectionReference _userCollection;
        private User currentUser;
        private TcpClient chatClient;
        private NetworkStream chatStream;
        private byte[] chatMessage = new byte[4096];
        private string _bestMove;

        private static string InvertSquare(string sq)
        {
            var f = (char)('A' + 'H' - sq[4]);
            var r = '9' - sq[5];
            return "lbl_" + f + r;
        }

        public TwoPlayerLAN(TimeSpan selectedTime, string selectedDifficulty, bool isHost, string ip = null)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            gameTime = selectedTime;
            difficulty = selectedDifficulty;
            InitializeTimer();
            InitializeListView();
            InitializeLabels();
            InitializeBoard();
            DrawBoard();
            this.isHost = isHost;
            this.ip = ip;
            InitializeFirestore();
            currentUser = LoadUserFromLocal();
            InitializeChat();

        }
        private void InitializeFirestore()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\jalynk\\Downloads\\ChessProject\\ChessUI\\chessgame-426408-07acea961dc4.json");
            _firestoreDb = FirestoreDb.Create("chessgame-426408");
            _userCollection = _firestoreDb.Collection("history");
        }
        public User LoadUserFromLocal()
        {
            var filePath = Path.Combine(Application.LocalUserAppDataPath, "user.json");
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<User>(json);
            }
            return null;
        }
        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer
            {
                Interval = 1000 // 1 second
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            whiteTimeRemaining = gameTime;
            blackTimeRemaining = gameTime;
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Move #", 50);
            listView1.Columns.Add("White", 150);
            listView1.Columns.Add("Black", 150);
        }

        private void InitializeLabels()
        {
            whiteLabel.Text = $"{whiteTimeRemaining:mm\\:ss}";
            blackLabel.Text = $"{blackTimeRemaining:mm\\:ss}";
        }

        private void InitializeBoard()
        {
            _squareLabels = Controls.OfType<Label>()
                                    .Where(m => Regex.IsMatch(m.Name, "lbl_[A-H][1-8]")).ToArray();

            Array.ForEach(_squareLabels, lbl =>
            {
                lbl.BackgroundImageLayout = ImageLayout.Zoom;
                lbl.Click += SquaresLabels_Click;
            });

            _whiteLocations = _squareLabels.ToDictionary(lbl => lbl.Name, lbl => lbl.Location);
            _blackLocations = _squareLabels.ToDictionary(lbl => InvertSquare(lbl.Name), lbl => lbl.Location);

            var duplicateLabels = _squareLabels.GroupBy(lbl => lbl.Name)
                                               .Where(g => g.Count() > 1)
                                               .Select(g => g.Key)
                                               .ToList();
            if (duplicateLabels.Any())
            {
                throw new InvalidOperationException($"Duplicate label names found: {string.Join(", ", duplicateLabels)}");
            }
        }

        private Player GetOpponent(Player player)
        {
            return player == Player.White ? Player.Black : Player.White;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_gameBoard.WhoseTurn == Player.White)
            {
                whiteTimeRemaining -= TimeSpan.FromSeconds(1);
                whiteLabel.Text = $"{whiteTimeRemaining:mm\\:ss}";
                if (whiteTimeRemaining <= TimeSpan.Zero)
                {
                    timer.Stop();
                    MessageBox.Show("White's time is up. Black wins!", "Time Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EndGame();
                }
            }
            else if (_gameBoard.WhoseTurn == Player.Black)
            {
                blackTimeRemaining -= TimeSpan.FromSeconds(1);
                blackLabel.Text = $"{blackTimeRemaining:mm\\:ss}";
                if (blackTimeRemaining <= TimeSpan.Zero)
                {
                    timer.Stop();
                    MessageBox.Show("Black's time is up. White wins!", "Time Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EndGame();
                }
            }
        }

        private void FlipUi(Player player)
        {
            if (checkBox1.Checked) return;
            var locationsDictionary = player == Player.White ? _whiteLocations : _blackLocations;
            Array.ForEach(_squareLabels, lbl => lbl.Location = locationsDictionary[lbl.Name]);
        }

        private Player? GetPlayerInCheck()
        {
            if (_gameBoard.GameState == GameState.BlackInCheck || _gameBoard.GameState == GameState.WhiteWinner)
            {
                return Player.Black;
            }
            if (_gameBoard.GameState == GameState.WhiteInCheck || _gameBoard.GameState == GameState.BlackWinner)
            {
                return Player.White;
            }
            return null;
        }

        private void SquaresLabels_Click(object? sender, EventArgs e)
        {
            Label selectedLabel = (Label)sender!;
            if (selectedLabel.BackColor != Color.DarkCyan)
            {
                // Re-draw to remove previously colored labels.
                DrawBoard(GetPlayerInCheck());

                // Check if it's the current player's turn and allow them to move their respective pieces
                if ((isHost && _gameBoard.WhoseTurn == Player.White && selectedLabel.Tag?.ToString() == Player.White.ToString()) ||
                    (!isHost && _gameBoard.WhoseTurn == Player.Black && selectedLabel.Tag?.ToString() == Player.Black.ToString()))
                {
                    _selectedSourceSquare = selectedLabel.Name.AsSpan().Slice("lbl_".Length); // implicit conversion
                    var validDestinations = ChessUtilities.GetValidMovesOfSourceSquare(_selectedSourceSquare, _gameBoard).Select(m => m.Destination).ToArray();
                    if (validDestinations.Length == 0) return;
                    selectedLabel.BackColor = Color.Cyan;
                    Array.ForEach(validDestinations, square =>
                    {
                        _squareLabels.First(lbl => lbl.Name == $"lbl_{square}").BackColor = Color.DarkCyan;
                    });
                }
            }
            else
            {
                MakeMove(_selectedSourceSquare, selectedLabel.Name.AsSpan().Slice("lbl_".Length));
            }
        }

        private void DrawBoard(Player? playerInCheck = null)
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    var file = (ChessSharp.SquareData.File)i;
                    var rank = (Rank)j;
                    Label lbl = _squareLabels.First(m => m.Name == "lbl_" + file.ToString() + ((int)rank + 1));
                    Piece? piece = _gameBoard[file, rank];
                    lbl.BackColor = ((i + j) % 2 == 0) ? Color.FromArgb(181, 136, 99) : Color.FromArgb(240, 217, 181);
                    if (piece == null)
                    {
                        lbl.BackgroundImage = null;
                        lbl.Tag = "empty";
                        continue;
                    }
                    lbl.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject($"{piece.Owner}{piece.GetType().Name}")!;
                    lbl.Tag = piece.Owner.ToString();
                }
            }

            if (playerInCheck == null) return;

            Square checkedKingSquare = _gameBoard.Board.SelectMany(x => x)
                .Select((p, i) => new { Piece = p, Square = new Square((ChessSharp.SquareData.File)(i % 8), (Rank)(i / 8)) })
                .First(m => m.Piece is King && m.Piece.Owner == playerInCheck).Square;
            _squareLabels.First(lbl => lbl.Name == "lbl_" + checkedKingSquare).BackColor = Color.Red;
        }

        private void EndGame()
        {
            timer.Stop();
            // Additional end game logic here (e.g., disable moves, show final message, etc.)
        }

        private string SquareToNotation(Square square)
        {
            return $"{(char)('a' + (int)square.File)}{(int)square.Rank + 1}";
        }

        private async void MakeMove(Square source, Square destination)
        {
            try
            {
                Player player = _gameBoard.WhoseTurn;
                PawnPromotion? pawnPromotion = null;
                if (_gameBoard[source.File, source.Rank] is Pawn)
                {
                    if ((player == Player.White && destination.Rank == Rank.Eighth) ||
                        (player == Player.Black && destination.Rank == Rank.First))
                    {
                        string? promotion;
                        using (var inputBox = new InputBox())
                        {
                            inputBox.ShowDialog();
                            promotion = inputBox.UserInput;
                        }
                        pawnPromotion = (PawnPromotion)Enum.Parse(typeof(PawnPromotion), promotion!, true);
                    }
                }

                var move = new Move(source, destination, player, pawnPromotion);
                if (!_gameBoard.IsValidMove(move))
                {
                    return;
                }
                _gameBoard.MakeMove(move, isMoveValidated: true);

                DrawBoard(GetPlayerInCheck());

                // Log move to ListView
                string moveNotation = SquareToNotation(source) + SquareToNotation(destination);
                if (player == Player.White)
                {
                    listView1.Items.Add(new ListViewItem(new[] { (listView1.Items.Count + 1).ToString(), moveNotation, "" }));
                }
                else
                {
                    listView1.Items[listView1.Items.Count - 1].SubItems[2].Text = moveNotation;
                }

                if (_gameBoard.GameState == GameState.Draw || _gameBoard.GameState == GameState.Stalemate ||
                    _gameBoard.GameState == GameState.BlackWinner || _gameBoard.GameState == GameState.WhiteWinner)
                {
                    MessageBox.Show(_gameBoard.GameState.ToString());
                    EndGame();
                    return;
                }
                SoundPlayer moveSoundPlayer = new SoundPlayer(Properties.Resources.Move);
                moveSoundPlayer.Play();
                if (isRemoteMove)
                {
                    isRemoteMove = false;
                    string fen = _gameBoard.GetFen();
                    predictLb.Text = "Predicting next move for you...";

                    string message = await FetchBestMoveFromAPIAsync(fen);

                    // Update the label with the fetched message
                    if (!string.IsNullOrEmpty(message))
                    {
                        predictLb.Text = message;
                    }
                }
                else
                {
                    predictLb.Text = ""; // Clear prediction for white's turn
                }
                SendMessage(moveNotation);
               
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error\n{exception.Message}\n\n{exception.StackTrace}", "Chess", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConnectToOpponent()
        {
            if (isHost)
            {
                server = new TcpListener(System.Net.IPAddress.Any, 8080);
                server.Start();
                Thread acceptThread = new Thread(AcceptClient);
                acceptThread.Start();
            }
            else
            {
                client = new TcpClient();
                client.Connect(ip, 8080);
                clientStream = client.GetStream();
                stream = client.GetStream();
                Thread receiveThread = new Thread(ReceiveMessages);
                receiveThread.Start();
                if (_gameBoard.WhoseTurn == Player.Black)
                {
                    FlipUi(Player.Black);
                }
            }
        }

        private void AcceptClient()
        {
            client = server.AcceptTcpClient();
            clientStream = client.GetStream();
            stream = client.GetStream();
            Invoke((MethodInvoker)delegate
            {
                // Close the waiting form if it is open
                WaitingRoom waitingForm = Application.OpenForms.OfType<WaitingRoom>().FirstOrDefault();
                if (waitingForm != null)
                {
                    waitingForm.Close();
                    timer.Start();
                }
            });
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();
        }

        private void SendMessage(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }

        private void ReceiveMessages()
        {
            int bytesRead;
            while (true)
            {
                bytesRead = clientStream.Read(message, 0, 4096);
                string receivedMessage = Encoding.ASCII.GetString(message, 0, bytesRead);
                if (receivedMessage == "resign")
                {
                    MessageBox.Show($"{GetOpponent(_gameBoard.WhoseTurn)} has resigned. {_gameBoard.WhoseTurn} wins!", "Resign", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EndGame();
                    return;
                }
                HandleReceivedMove(receivedMessage);
            }
        }

        private Square ParseSquare(string squareString)
        {
            ChessSharp.SquareData.File file = (ChessSharp.SquareData.File)(squareString[0] - 'a');
            Rank rank = (Rank)(squareString[1] - '1');
            return new Square(file, rank);
        }

        private void HandleReceivedMove(string receivedMove)
        {
            Square source = ParseSquare(receivedMove.Substring(0, 2));
            Square destination = ParseSquare(receivedMove.Substring(2, 2));
            isRemoteMove = true;
            MakeMove(source, destination);
        }

        private async void TwoPlayerLAN_Load(object sender, EventArgs e)
        {
            if (isHost)
            {
                WaitingRoom waitingForm = new WaitingRoom();
                waitingForm.Show(this);
                timer.Stop();
            }
            else
            {
                FlipUi(Player.Black);
            }
            ConnectToOpponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (MessageBox.Show("Are you sure to quit?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();

            }
            else
            {
                timer.Start();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Player currentPlayer = _gameBoard.WhoseTurn;
            SendMessage("resign");
            MessageBox.Show($"{currentPlayer} has resigned. {GetOpponent(currentPlayer)} wins!", "Resign", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EndGame();
            var history = new History
            {
                GameId = Guid.NewGuid().ToString(),
                UserId = currentUser.UserId,
                Username = currentUser.Username,
                OpponentId = "",
                OpponentName = "",
                Result = "Loss",
                Date = DateTime.Now,
            };

            await _userCollection.Document(history.GameId).SetAsync(history);
        }
        private void ReceiveChatMessages()
        {
            int bytesRead;
            while (true)
            {
                bytesRead = chatStream.Read(chatMessage, 0, 4096);
                if (bytesRead == 0)
                {
                    // Handle client disconnection if necessary
                    continue;
                }

                string receivedMessage = Encoding.ASCII.GetString(chatMessage, 0, bytesRead);

                // Check if the message starts with the local identifier
                if (receivedMessage.StartsWith("LOCAL:"))
                {
                    receivedMessage = receivedMessage.Substring(6); // Remove the "LOCAL:" prefix
                }

                // Update the UI to display the received message
                Invoke((MethodInvoker)delegate
                {
                    richTextBox1.Text += receivedMessage + Environment.NewLine;
                });
            }
        }


        private void InitializeChat()
        {
            chatClient = new TcpClient();
            chatClient.Connect("chessServer-1499465774.ap-southeast-1.elb.amazonaws.com", 9000);
            chatStream = chatClient.GetStream();
            Thread receiveThread = new Thread(ReceiveChatMessages);
            receiveThread.Start();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox2.Text)) return;

            string message = $"{currentUser.Username}: {richTextBox2.Text}";
            string localIdentifier = "LOCAL:";
            byte[] buffer = Encoding.ASCII.GetBytes(localIdentifier + message);

            chatStream.Write(buffer, 0, buffer.Length);
            richTextBox1.Text += message + Environment.NewLine;
            richTextBox2.Clear();




        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private async Task<string> FetchBestMoveFromAPIAsync(string fen)
        {
            string apiUrl = "http://54.179.159.97:7000/"; // Update with your actual API URL

            try
            {
                var payload = new { fen = fen };
                string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode(); // Throw on error code.
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseBody);
                    string bestMove = responseObject.best_move;
                    _bestMove = bestMove;

                    string message = $"I propose the move {bestMove}";

                    return message;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Error fetching data from API: {e.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_bestMove))
            {
                button3.Enabled = false;

            }

            Square src = ParseSquare(_bestMove.Substring(0, 2));
            Square dest = ParseSquare(_bestMove.Substring(2, 2));
            MakeMove(src, dest);
        }
    }
}
