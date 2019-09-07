using System;
using System.Windows.Forms;
using System.Net;

namespace Eset
{
    public partial class download : Form
    {
        public download()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void download_Load(object sender, EventArgs e)
        {
            string user = Environment.UserName;
            using (WebClient wc = new WebClient())
            {
                
                wc.DownloadFileAsync(new Uri(@"https://download.esetnod32.ru/home/trial/11/eav_trial_rus.exe"), $@"C:\Users\{Environment.UserName}\Downloads\eav_trial_rus.exe");
                wc.DownloadProgressChanged += (s, d) =>
                {
                    progressBar1.Value = d.ProgressPercentage;
                    textBox1.Text = d.ProgressPercentage.ToString() + " %";
                };
                
                //MessageBox.Show($@"C:\Users\{Environment.UserName}\Desktop\eav_trial_rus.exe");

            }
        }
    }
}
