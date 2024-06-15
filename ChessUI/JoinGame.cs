using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessUI.DashboardForm
{
    public partial class JoinGame : Form
    {
        MainMenu parentForm;
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        public JoinGame(MainMenu parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("13.212.8.228", 8888); // Change this to your server IP and port
                stream = client.GetStream();
                receiveThread = new Thread(ReceiveData);
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to the server: " + ex.Message);
            }
        }

        private void ReceiveData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;
            string message = "";

            while (true)
            {
                bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                HandleMessage(message);
            }
            
        }

        private void HandleMessage(string message)
        {
            if (message.Trim() == "MatchFound")
            {
                // Check if we need to invoke (are we on a different thread?)
                if (this.InvokeRequired)
                {
                    // Marshal the call to the UI thread
                    this.Invoke(new Action(() => ShowTwoPlayerOnlineForm()));
                }
                else
                {
                    // We're already on the UI thread, so call directly
                    ShowTwoPlayerOnlineForm();
                }
            }
        }



        private void ShowTwoPlayerOnlineForm()
        {
            TwoPlayerOnline twoPlayerOnlineForm = new TwoPlayerOnline(TimeSpan.FromMinutes(10));
            twoPlayerOnlineForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentForm.LoadForm(new ChoosePVPMode(parentForm));
        }

      
    }
}
