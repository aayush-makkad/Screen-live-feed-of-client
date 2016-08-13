using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using MetroFramework.Forms;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Socket_file_transfer
{
    public partial class Form3 : MetroForm
    {
        public int port;
        private TcpClient client;
        private TcpListener server;
        private NetworkStream mainstream;
        private readonly Thread listening;
        private readonly Thread getimge;
        public Form3(int Port)
        {
            client = new TcpClient();
            listening = new Thread(startlistening);
            getimge = new Thread(recieveimage);
            port = Port;
            InitializeComponent();
        }
        private void startlistening()
        {
            while(!client.Connected)
            {
                server.Start();
                client = server.AcceptTcpClient();

            }
            getimge.Start();
        }
        private void stoplistening()
        {
            server.Stop();
            client = null;
            if (listening.IsAlive) listening.Abort();
            if (getimge.IsAlive) getimge.Abort();

        }
        private void recieveimage()
        {
            BinaryFormatter binformatter = new BinaryFormatter();
            while(client.Connected)
            {
                mainstream = client.GetStream();
                pictureBox1.Image = (Image)binformatter.Deserialize(mainstream);

            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            server = new TcpListener(IPAddress.Any, port);
            listening.Start();

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            stoplistening();
        }
    }
}
