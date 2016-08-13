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
namespace Socket_file_transfer
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            int port = int.Parse(metroTextBox1.Text);
            Form3 f3 = new Form3(port);
            f3.Show();
            
        }
    }
}
