using ChessSharp.SquareData;
using ChessSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessSharp.Pieces;
using System.Media;
using System.Text.RegularExpressions;
using ChessUI.DashboardForm;
using System.Threading;

namespace ChessUI
{
    public partial class TwoPlayerOnline : Form
    {
        private bool isRemoteMove = false;
        private TcpClient client;
        private TcpListener server;
        private NetworkStream clientStream;
        private NetworkStream stream;
        private byte[] message = new byte[4096];
        private TimeSpan gameTime;
        private Player initialPlayer;
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

        public TwoPlayerOnline(TimeSpan selectedTime)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            gameTime = selectedTime;
            InitializeTimer();
            InitializeListView();
            InitializeLabels();
            InitializeBoard();
            DrawBoard();
        }

        private static string InvertSquare(string sq)
        {
            var f = (char)('A' + 'H' - sq[4]);
            var r = '9' - sq[5];
            return "lbl_" + f + r;
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
                    _selectedSourceSquare = ParseSquare(selectedLabel.Name.Substring(4)); // Get square from label name
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
                Square destinationSquare = ParseSquare(selectedLabel.Name.Substring(4)); // Get square from label name
                MakeMove(_selectedSourceSquare, destinationSquare);
            }
        }


        private void DrawBoard(Player? playerInCheck = null)
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    var file = (File)i;
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
                .Select((p, i) => new { Piece = p, Square = new Square((File)(i % 8), (Rank)(i / 8)) })
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
                    return;
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
            client = new TcpClient();
            client.Connect("chessServer-1499465774.ap-southeast-1.elb.amazonaws.com", 8888);
            clientStream = client.GetStream();
            stream = client.GetStream();
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            
        }

        private void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            clientStream.Write(messageBytes, 0, messageBytes.Length);
           
        }

        private void ReceiveMessages()
        {
            int bytesRead;
            while (true)
            {
                bytesRead = clientStream.Read(message, 0, 4096);
                string receivedMessage = Encoding.ASCII.GetString(message, 0, bytesRead);
                richTextBox1.AppendText(receivedMessage + "\n");
                HandleReceivedMove(receivedMessage);
                if (receivedMessage.StartsWith("isHost:"))
                {
                    isHost = receivedMessage.Split(':')[1] == "true";
                    if (!isHost)
                    {
                        FlipUi(Player.Black);
                    }
                }
                else if (receivedMessage == "resign")
                {
                    MessageBox.Show($"{GetOpponent(_gameBoard.WhoseTurn)} has resigned. {_gameBoard.WhoseTurn} wins!", "Resign", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EndGame();
                    return;
                }
               

            }
        }

        private Square ParseSquare(string squareString)
        {
            File file = (File)(squareString[0] - 'a');
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

        private void button1_Click(object sender, EventArgs e)
        {
            Player currentPlayer = _gameBoard.WhoseTurn;
            MessageBox.Show($"{currentPlayer} has resigned. {GetOpponent(currentPlayer)} wins!", "Resign", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EndGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Stop();
            if (MessageBox.Show("Are you sure to quit?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                var menu = new MainMenu();
                menu.Show();
            }
            else
            {
                timer.Start();
            }
        }

        private void TwoPlayerOnline_Load(object sender, EventArgs e)
        {
            ConnectToOpponent();
        }
    }
}
