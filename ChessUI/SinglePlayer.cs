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
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using ChessUI.Models;
using System.IO;
using System.Net.Http;
using System.Text;



namespace ChessUI
{
    public partial class SinglePlayer : Form
    {
        private IStockfish _stockfish;
        private TimeSpan gameTime;
        private Player initialPlayer;
        private Player humanPlayer;
        private Player stockfishPlayer;
        private string difficulty;
        private Label[] _squareLabels;
        private Dictionary<string, Point> _whiteLocations;
        private Dictionary<string, Point> _blackLocations;
        private Square _selectedSourceSquare;
        private ChessGame _gameBoard = new ChessGame();
        private Timer timer;
        private TimeSpan whiteTimeRemaining;
        private TimeSpan blackTimeRemaining;
        private FirestoreDb _firestoreDb;
        private CollectionReference _userCollection;
        private User currentUser;
        private string _bestMove;

        private static string InvertSquare(string sq)
        {
            var f = (char)('A' + 'H' - sq[4]);
            var r = '9' - sq[5];
            return "lbl_" + f + r;
        }

        public SinglePlayer(TimeSpan selectedTime, string selectedDifficulty)
        {
            InitializeComponent();
            gameTime = selectedTime;

            difficulty = selectedDifficulty;

            InitializeTimer();
            InitializeListView();
            InitializeLabels();
            InitializeBoard();

            DrawBoard();
            FlipUi(initialPlayer);
            SetStockfishDifficulty();
            currentUser = LoadUserFromLocal();
            InitializeFirestore();
        }

        private void LoadModel(string modelPath)
        {

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
            timer = new Timer
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

                if (selectedLabel.Tag!.ToString() != _gameBoard.WhoseTurn.ToString()) return;
                _selectedSourceSquare = selectedLabel.Name.AsSpan().Slice("lbl_".Length); // implicit conversion
                var validDestinations = ChessUtilities.GetValidMovesOfSourceSquare(_selectedSourceSquare, _gameBoard).Select(m => m.Destination).ToArray();
                if (validDestinations.Length == 0) return;
                selectedLabel.BackColor = Color.Cyan;
                Array.ForEach(validDestinations, square =>
                {
                    _squareLabels.First(lbl => lbl.Name == $"lbl_{square}").BackColor = Color.DarkCyan;
                });
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
                    MessageBox.Show("Invalid Move!", "Chess", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    var history = new History
                    {
                        GameId = Guid.NewGuid().ToString(),
                        UserId = currentUser.UserId,
                        Username = currentUser.Username,
                        OpponentId = "AI-001",
                        OpponentName = "Stockfish",
                        Result = _gameBoard.GameState.ToString(),
                        Date = DateTime.UtcNow
                    };
                    await _userCollection.Document(history.GameId).SetAsync(history);

                    return;
                }
                SoundPlayer moveSoundPlayer = new SoundPlayer(Properties.Resources.Move);
                moveSoundPlayer.Play();
                Player whoseTurn = _gameBoard.WhoseTurn;

                if (whoseTurn == Player.Black)
                {
                    await StockfishMove();
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
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error\n{exception.Message}\n\n{exception.StackTrace}", "Chess", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async Task StockfishMove()
        {
            string fen = _gameBoard.GetFen();
            _stockfish.SetFenPosition(fen);
            string bestMove = await Task.Run(() => _stockfish.GetBestMove());
            if (bestMove != null)
            {
                Square src = ParseSquare(bestMove.Substring(0, 2));
                Square dest = ParseSquare(bestMove.Substring(2, 2));
                MakeMove(src, dest);
            }
        }
        private string SquareToNotation(Square square)
        {
            return $"{(char)('a' + (int)square.File)}{(int)square.Rank + 1}";
        }

        private string MoveToNotation(Move move)
        {
            return SquareToNotation(move.Source) + SquareToNotation(move.Destination);
        }

        private Square ParseSquare(string squareString)
        {
            ChessSharp.SquareData.File file = (ChessSharp.SquareData.File)(squareString[0] - 'a');
            Rank rank = (Rank)(squareString[1] - '1');
            return new Square(file, rank);
        }

        private void SinglePlayer_Load(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Player currentPlayer = _gameBoard.WhoseTurn;
            MessageBox.Show($"{currentPlayer} has resigned. {GetOpponent(currentPlayer)} wins!", "Resign", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EndGame();
            // Add match history to Firestore
            var history = new History
            {
                GameId = Guid.NewGuid().ToString(),
                UserId = currentUser.UserId,
                Username = currentUser.Username,
                OpponentId = "AI-001",
                OpponentName = "Stockfish",
                Result = "Loss",
                Date = DateTime.UtcNow


            };
            await _userCollection.Document(history.GameId).SetAsync(history);




        }

        private void SetStockfishDifficulty()
        {
            _stockfish = new Stockfish.NET.Stockfish(@"F:\ChessProject2\ChessUI\stockfish\stockfish_20090216_x64.exe");

            switch (difficulty)
            {
                case "Easy":
                    _stockfish.SkillLevel = 1; // Adjust this value as per Stockfish documentation
                    break;
                case "Medium":
                    _stockfish.SkillLevel = 5;
                    break;
                case "Hard":
                    _stockfish.SkillLevel = 10;
                    break;
                case "Very Hard":
                    _stockfish.SkillLevel = 20;
                    break;
            }
        }

        private void blackLabel_Click(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
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
