using Google.Cloud.Firestore;
using System;

namespace ChessUI.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public string Username { get; set; }

        [FirestoreProperty]
        public string Password { get; set; }

        [FirestoreProperty]
        public int ELO { get; set; }

        [FirestoreProperty]
        public int Wins { get; set; }

        [FirestoreProperty]
        public int Draws { get; set; }

        [FirestoreProperty]
        public int Losses { get; set; }

        [FirestoreProperty]
        public DateTime LastLogin { get; set; }
    }
}
