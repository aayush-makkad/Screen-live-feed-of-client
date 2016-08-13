using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using MetroFramework.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Socket_file_transfer
{
    public partial class Form1 : MetroForm
    {
        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainstream;
        private int portnum;

        private static Image GrabDesktop()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width,bounds.Height);
            Graphics graphics = Graphics.FromImage(screenshot);
            graphics.CopyFromScreen(bounds.X, bounds.Y,0,0,bounds.Size,CopyPixelOperation.SourceCopy);
            return screenshot;

        }
        private void sendesktopimage()
        {
            BinaryFormatter binformatter = new BinaryFormatter();
            mainstream = client.GetStream();
            binformatter.Serialize(mainstream, GrabDesktop());
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            portnum = int.Parse(metroTextBox2.Text);
            try
            {
                client.Connect(metroTextBox1.Text,portnum);
                MessageBox.Show("Connected!");

            }
            catch
            {
                MessageBox.Show("failed to connect!");
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if(metroButton2.Text.StartsWith("Share"))
            {
                timer1.Start();
                metroButton2.Text = "Stop Sharing";

            }
            else
            {
                timer1.Start();
                metroButton2.Text = "Share Screen";

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sendesktopimage();

       


        }
    }
}
