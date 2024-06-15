using System;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using ChessUI.Models;
using BCrypt.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ChessUI
{
    public partial class LoginForm : Form
    {
        private FirestoreDb _firestoreDb;
        private CollectionReference _userCollection;

        public LoginForm()
        {
            InitializeComponent();
            InitializeFirestore();
            pwLogin.PasswordChar = '*';
            textBox2.PasswordChar = '*';
        }

        private void InitializeFirestore()
        {
            // Replace "path/to/your/credentials.json" with the actual path to your Firestore credentials file.
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\jalynk\\Downloads\\ChessProject\\ChessUI\\chessgame-426408-07acea961dc4.json");
            _firestoreDb = FirestoreDb.Create("chessgame-426408");
            _userCollection = _firestoreDb.Collection("users");
        }

        private void SaveUserToLocal(User user)
        {
            var filePath = Path.Combine(Application.LocalUserAppDataPath, "user.json");
            var json = JsonConvert.SerializeObject(user);
            File.WriteAllText(filePath, json);
        }

        private async void loginBtn_Click(object sender, EventArgs e)
        {
            string username = emailLogin.Text;
            string password = pwLogin.Text;

            Query query = _userCollection.WhereEqualTo("Username", username);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            var user = querySnapshot.Documents.Count > 0 ? querySnapshot.Documents[0].ConvertTo<User>() : null;

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Save user information to local file
                SaveUserToLocal(user);

                MessageBox.Show("Login successful!");
                MainMenu menu = new MainMenu();
                menu.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Invalid email or password!");
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            Query query = _userCollection.WhereEqualTo("Username", username);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            var user = querySnapshot.Documents.Count > 0 ? querySnapshot.Documents[0].ConvertTo<User>() : null;

            if (user != null)
            {
                MessageBox.Show("Username already exists! Please choose a different one.");
                return;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = username,
                Password = hashedPassword,
                ELO = 1200,
                Wins = 0,
                Draws = 0,
                Losses = 0,
                LastLogin = DateTime.UtcNow
            };

            await _userCollection.Document(newUser.UserId).SetAsync(newUser);

            MessageBox.Show("Sign up successful!");
        }
    }
}