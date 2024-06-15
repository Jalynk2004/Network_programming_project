using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessUI.Models;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
namespace ChessUI.DashboardForm
{
    public partial class MatchHistory : Form
    {

        private FirestoreDb _firestoreDb;
        private CollectionReference _userCollection;
        private User currentUser;
        public MatchHistory()
        {
            InitializeComponent();
            InitializeFirestore();
            currentUser = LoadUserFromLocal();

        }
        private void InitializeFirestore()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "F:\\ChessProject2\\ChessUI\\chessgame-426408-07acea961dc4.json");
            _firestoreDb = FirestoreDb.Create("chessgame-426408");
            _userCollection = _firestoreDb.Collection("users");
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
        public void LoadHistory()
        {
            var historyCollection = _firestoreDb.Collection("history");
            var query = historyCollection.WhereEqualTo("UserId", currentUser.UserId);
            var querySnapshot = query.GetSnapshotAsync().Result;
            if (querySnapshot.Count > 0)
            {
                foreach (var document in querySnapshot.Documents)
                {
                    var history = document.ConvertTo<History>();
                    var row = new string[] { history.OpponentName, history.Result, history.Date.ToString() };
                    var lvi = new ListViewItem(row);
                    listView1.Items.Add(lvi);
                }
            }
            else
            {
                MessageBox.Show("No history found.");
            }
          
            


        }
    }
}
