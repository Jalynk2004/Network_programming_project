using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessUI.Models
{
    [FirestoreData]

    internal class History
    {
        [FirestoreProperty]
        public string UserId { get; set; }
        [FirestoreProperty]
        public string Username { get; set; }

        [FirestoreProperty]
        public string OpponentId { get; set; }

        [FirestoreProperty]
        public string OpponentName { get; set; }

        [FirestoreProperty]
        public string Result { get; set; }

        [FirestoreProperty]
        public DateTime Date { get; set; }

        [FirestoreProperty]
        public string GameId { get; set; }


    }
}
