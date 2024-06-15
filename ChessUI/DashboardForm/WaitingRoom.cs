using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessUI.DashboardForm;
namespace ChessUI.DashboardForm
{
    public partial class WaitingRoom : Form
    {
        MainMenu parentForm;

        public WaitingRoom()
        {
            InitializeComponent();
        }

        private void WaitingRoom_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var temp = new DashboardForm.ChoosePVPMode(parentForm);
            parentForm.LoadForm(temp);
           
            this.Close();
        }
    }
}
