using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckIp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OnButton2_Click(object sender, EventArgs e)
        {
            ResetColorLabel();

            if (VerificationIPv4AndPort(textBox1.Text))
                label3.BackColor = Color.ForestGreen;
            else label2.BackColor = Color.OrangeRed;
        }

        private void OnButton1_Click(object sender, EventArgs e)
        {
            ResetColorLabel();

            if (VerificationIPv4(textBox1.Text))
            {
                label3.BackColor = Color.ForestGreen;

                GetData();
            }
            else
            {
                label2.BackColor = Color.OrangeRed;
            }
        }

        private bool VerificationIPv4(string line)
        {
            string ipv4 = @"^((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|1[0-9]|1|0[0-9][0-9]|0[0-9]|0|[0-9][0-9]|[0-9])\.){3}(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|1[0-9]|1|0[0-9][0-9]|0[0-9]|0|[0-9][0-9]|[0-9])$";
            

            Match match = Regex.Match(line, ipv4);

            if (match.Success)
                return true;
            return false;
        }
        private bool VerificationIPv4AndPort(string line)
        {
            //65535 65529 65499 64999 59999 9 99 999 9999
            string ipv4AndPort = @"^((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|1[0-9]|1|0[0-9][0-9]|0[0-9]|0|[0-9][0-9]|[0-9])\.){3}(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|1[0-9]|1|0[0-9][0-9]|0[0-9]|0|[0-9][0-9]|[0-9]):(6553[0-5]|655[0-2][0-9]|65[4][0-9][0-9]|6[0-4][0-9][0-9][0-9]|[0-5][0-9][0-9][0-9][0-9]|\d\d\d\d|\d\d\d|\d\d|\d)$";

            Match match = Regex.Match(line, ipv4AndPort);

            if (match.Success)
                return true;
            return false;
        }

        private void ResetColorLabel()
        {
            label2.BackColor = Color.Gainsboro;
            label3.BackColor = Color.Gainsboro;
        }

        private void ResetLabel()
        {
            label4.Text = "Country: ";
            label5.Text = "City: ";
        }

        private void GetData()
        {
            ResetLabel();

            string country = "<country>(.*?)</country>";
            string city = "<city>(.*?)</city>";

            string line = new WebClient().DownloadString($"http://ip-api.com/xml/{textBox1.Text}?lang=en");

            label4.Text += Regex.Match(line, country).Groups[1].Value;
            label5.Text += Regex.Match(line, city).Groups[1].Value;
        }

        private void ResetForm(object sender, EventArgs e)
        {
            ResetColorLabel();
            ResetLabel();
            textBox1.Text = "";
        }
    }
}
