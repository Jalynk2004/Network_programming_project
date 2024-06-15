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
using Newtonsoft.Json;
namespace ChessUI.DashboardForm
{
    public partial class Home : Form
    {
        public event EventHandler ChildPvEButton_Click;
        public event EventHandler ChildPvPButton_Click;

        public Home()
        {
            InitializeComponent();
            var user = LoadUserFromLocal();
            if (user != null)
            {
                label3.Text = $"Welcome back , {user.Username}!";
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PvEButton_Click(object sender, EventArgs e)
        {
            ChildPvEButton_Click?.Invoke(this, e);

        }

        private void PvPButton_Click(object sender, EventArgs e)
        {
            ChildPvPButton_Click?.Invoke(this, e);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
