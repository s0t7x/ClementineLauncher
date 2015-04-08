using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace ClementineLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("PsuIlluminus.exe");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 settingsForm = new Form2();
            settingsForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("http://psu-clementine.net/index.php");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            credits settingsForm = new credits();
            settingsForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("PsuIlluminusOff.exe");
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TcpClient tcpportScan = new TcpClient();
            string IP = "psu-clementine.ddns.net";
            try
            {
                tcpportScan.Connect(IP, 11230);
                this.Invoke((MethodInvoker)delegate
                {
                    this.statText.Text = "Online";
                    this.statText.ForeColor = Color.Lime;
                    tcpportScan.Close();
                    timer1.Stop();
                    timer1.Enabled = false;
                });
            }
            catch
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.statText.Text = "Offline";
                    this.statText.ForeColor = Color.Red;
                    //          tcpportScan.Close();
                });
            }

        }




	}

}
