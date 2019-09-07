using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;

namespace Eset
{
    public partial class Main_form : Form
    {
        bool what = true;
        bool googlePing = true;

        public Main_form()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;

          // Пингую гугл чтобы убедиться в налчии интернета
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("www.google.com", 1000);
                //MessageBox.Show(reply.Status.ToString());
         
            }
            catch { googlePing = false;}
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void Main_form_Load(object sender, EventArgs e)
        {
            try
            {
                string path = "D:\\ESET KEY\\Log.txt";
                string user = "";

             // Вывод приветственной надписи

                if (File.Exists("D:\\ESET KEY\\Settings.txt"))
                {
                    using (StreamReader streamReader = new StreamReader("D:\\ESET KEY\\Settings.txt"))
                    {
                        user = streamReader.ReadLine();
                    }

                    textBox3.Text = "WELCOME " + user; 
                }
                else
                {
                    textBox3.Text = "WELCOME USERNAME";
                }


             //  Получаю html код, произвожу парсинг и записываю ключи и датами в списки

                WebClient wc = new WebClient();
                string web = wc.DownloadString("https://4fornod.net/keys-nod32/#");

                Regex regex = new Regex(@"[A-Z0-9]\w+\-[A-Z0-9]\w+\-[A-Z0-9]\w+\-[A-Z0-9]\w+\-[A-Z0-9]\w+");
                Regex regex_date = new Regex(@"\d+\.\d*\.\d*");

                List<string> key = new List<string>();
                List<string> date = new List<string>();

                int count = 0;
                foreach (Match match in regex.Matches(web))
                {
                    if (count % 2 == 0 && count > 10 && count < 24)
                    {
                        key.Add(match.Value);
                    }
                    count++;
                }
                Random random = new Random();
                int selected_key = random.Next(0, key.Count);
                count = 0;
                foreach (Match match in regex_date.Matches(web))
                {
                    if (count > 16 && count < 23)
                    {
                        date.Add(match.Value);
                    }
                    count++;
                }

                
             // Создание папок, файлов и запись в них

                if (Directory.Exists("D:\\ESET KEY")) { }
                else
                {
                    Directory.CreateDirectory("D:\\ESET KEY");
                }

             // Создаю файл Log.txt и записываю туда ключ и дату

                using (FileStream file = new FileStream(path, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.Write(key[selected_key] + " ");
                    writer.Write(date[selected_key] + " ");
                }

                Clipboard.SetText(key[selected_key]);
                pictureBox1.Visible = true;
                textBox1.Text = "Ваши ключи готовы";
                textBox2.Text = "Осталось только вставить";
                
            }

            catch
            {
                if (!googlePing)
                {
                    pictureBox2.Visible = true;
                    textBox1.Text = "Кто-то отключил меня от Интернета";
                    textBox2.Text = "Возможно Они уже идут за вами";
                }
                else
                {
                    pictureBox2.Visible = true;
                    textBox1.Text = "Программа не может получить доступ к необходимым ресурсам";
                    textBox2.Text = "Отключите антивирус и перезапустите программу";
                }
                
            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            if (what)
            {
                settings.Show();
                what = false;
            }
            

            // После показа окна проверяю наличие файа \ создаю файл

            if (File.Exists("D:\\ESET KEY\\Settings.txt")) { }
            else
            {
                using (FileStream fileStream = new FileStream("D:\\ESET KEY\\Settings.txt", FileMode.Create))
                {
                    using (StreamWriter stream = new StreamWriter(fileStream))
                    {
                        stream.Write("USERNAME");
                    } 
                }
            }  
        }
    }
}
