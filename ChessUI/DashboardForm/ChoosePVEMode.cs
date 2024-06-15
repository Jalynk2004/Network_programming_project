using ChessSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessUI.DashboardForm
{
    public partial class ChoosePVEMode : Form
    {
        public TimeSpan SelectedTime { get;  set; }
        public string SelectedDifficulty { get;  set; }
        public ChoosePVEMode()
        {
            InitializeComponent();
            PopulateDifficultyOptions();
            PopulateTimeOptions();
        }
        private void PopulateTimeOptions()
        {
            comboBox1.Items.Add("5 minutes");
            comboBox1.Items.Add("10 minutes");
            comboBox1.Items.Add("15 minutes");
            comboBox1.SelectedIndex = 1; // Default to 10 minutes
        }
        private void PopulateDifficultyOptions()
        {
            comboBox2.Items.Add("Easy");
            comboBox2.Items.Add("Medium");
            comboBox2.Items.Add("Hard");
            comboBox2.Items.Add("Very Hard");

            comboBox2.SelectedIndex = 1; // Default to Medium
        }

        private void ChoosePVEMode_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedTimeText = comboBox1.SelectedItem.ToString();
            SelectedTime = selectedTimeText switch
            {
                "5 minutes" => TimeSpan.FromMinutes(5),
                "10 minutes" => TimeSpan.FromMinutes(10),
                "15 minutes" => TimeSpan.FromMinutes(15),
                _ => TimeSpan.FromMinutes(10),
            };

            SelectedDifficulty = comboBox2.SelectedItem.ToString();

            var singleplayer= new SinglePlayer(SelectedTime, SelectedDifficulty);
            singleplayer.Show();
            this.Hide();


          

        }
    }
}
