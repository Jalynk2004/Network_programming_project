using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessUI.Models;
using System.IO;
using Newtonsoft.Json;

namespace ChessUI.DashboardForm
{
    public partial class Account : Form
    {
        private FirestoreDb _firestoreDb;
        private CollectionReference _userCollection;
        private User currentUser;

        public Account()
        {
            InitializeComponent();
            InitializeFirestore();
            currentUser = LoadUserFromLocal(); // Load user first
            if (currentUser != null)
            {
                InitializeUserData(); // Initialize user data only if user is not null
            }
            else
            {
                MessageBox.Show("Failed to load user data.");
            }
        }
        public User LoadUserFromLocal()
        {
            var filePath = Path.Combine(Application.LocalUserAppDataPath, "user.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<User>(json);
            }
            return null;
        }

        private void InitializeFirestore()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\jalynk\\Downloads\\ChessProject\\ChessUI\\chessgame-426408-07acea961dc4.json");
            _firestoreDb = FirestoreDb.Create("chessgame-426408");
            _userCollection = _firestoreDb.Collection("users");
        }
        private void InitializeUserData()
        {
            textBox1.Text = currentUser.Username;
            textBox2.Text = "********";
            winLb.Text = $"{currentUser.Wins}";
            loseLb.Text = $"{currentUser.Losses}";
            drawLb.Text = $"{currentUser.Draws}";
            eloLb.Text = $"{currentUser.ELO}";

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.ReadOnly)
            {
                textBox1.ReadOnly = false;
            }
            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Username cannot be empty!");
                    return;
                }
                currentUser.Username = textBox1.Text;
                UpdateUserInFirestore(currentUser);
                textBox1.ReadOnly = true;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.ReadOnly)
            {
                textBox2.ReadOnly = false;
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Password cannot be empty!");
                    return;
                }
                currentUser.Password = BCrypt.Net.BCrypt.HashPassword(textBox2.Text);
                UpdateUserInFirestore(currentUser);
                textBox2.ReadOnly = true;
            }
        }
        private async Task UpdateUserInFirestore(User user)
        {
            try
            {
                DocumentReference docRef = _userCollection.Document(user.UserId);
                Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "Username", user.Username },
            { "Password", user.Password },
            { "Wins", user.Wins },
            { "Draws", user.Draws },
            { "Losses", user.Losses },
            { "ELO", user.ELO },
            { "LastLogin", user.LastLogin }
        };
                await docRef.UpdateAsync(updates);
                MessageBox.Show("Account details updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MatchHistory matchHistory = new MatchHistory();
            matchHistory.Show();
            this.Hide();

        }
    }
}
