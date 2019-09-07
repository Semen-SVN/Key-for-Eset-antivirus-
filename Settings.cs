using System;
using System.Windows.Forms;
using System.IO;

namespace Eset
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        string settings_path = "D:\\ESET KEY\\Settings.txt";

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool check = true;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // По нажатию клавиши Enter записываю Username пользователя

            if (e.KeyCode == Keys.Enter && check)
            {
                string uname = textBox1.Text;
                if (uname.Length <= 16)
                {
                    using (StreamWriter writer = new StreamWriter(settings_path))
                        writer.Write(uname);
                }
                else { MessageBox.Show("Введенное Вами имя слишком длинное (Лимит 16 символов)"); }
                
                check = false;
                   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            download download = new download();
            download.Show();
            this.Close();
        }
    }
}
