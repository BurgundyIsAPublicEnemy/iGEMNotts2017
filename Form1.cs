using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization;


namespace Game8
{
   public partial class Main_Form : Form
    {
        public Main_Form()
        {
            InitializeComponent();
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Form());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Write to preferences.txt
            string preference;
            preference = Preference_Form.Text;
            if (preference != "")
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "/Preferences.txt", true))
                    file.WriteLine(preference + " 0");
            } else
            {
                //Error! 
                MessageBox.Show("You need to input something");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Reset debug 
            richTextBox1.Text = "";
            //Read line by line from preferences.txt
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(Directory.GetCurrentDirectory() + "/Preferences.txt");
            while ((line = file.ReadLine()) != null)
            {
                //Search happens here
                search_api(line);
                richTextBox1.Text = richTextBox1.Text + " " + line; 
            }

            file.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            search_api();
        }

        void search_api() { 
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://newsapi.org/v1/articles?source=the-next-web&sortBy=latest&apiKey=98a6433c929341e89a4b237abe1248f1");

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(responseStream))
                {
                    var fzResult = Newtonsoft.Json.JsonConvert.DeserializeObject<FZResult>(reader.ReadToEnd());
                    try
                    {
                    
                        fzResult.Articles.ForEach(a => Console.WriteLine("{0} {1}", a.Title, a.description));
                        notifyIcon1.Visible = true;
                        notifyIcon1.Icon = SystemIcons.Exclamation;
                        fzResult.Articles.ForEach(a =>  notifyIcon1.BalloonTipTitle = a.Title);
                        fzResult.Articles.ForEach(a =>   notifyIcon1.BalloonTipText = a.description);
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                        notifyIcon1.ShowBalloonTip(1000);

                    } catch {
                        Console.WriteLine("No reports found");
                        notifyIcon1.Visible = true;
                        notifyIcon1.Icon = SystemIcons.Exclamation;
                        notifyIcon1.BalloonTipTitle = "No new news today!";
                        notifyIcon1.BalloonTipText = "Have a nice day";
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                        notifyIcon1.ShowBalloonTip(1000);

                    }
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {

        }

        void search_api(string preferences)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://newsapi.org/v1/sources?category=" + preferences);

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(responseStream))
                {
                    var fzResult = Newtonsoft.Json.JsonConvert.DeserializeObject<FZResult>(reader.ReadToEnd());
                    try
                    {

                        fzResult.Articles.ForEach(a => Console.WriteLine("{0} {1}", a.Title, a.description));
                        notifyIcon1.Visible = true;
                        notifyIcon1.Icon = SystemIcons.Exclamation;
                        fzResult.Articles.ForEach(a => notifyIcon1.BalloonTipTitle = a.Title);
                        fzResult.Articles.ForEach(a => notifyIcon1.BalloonTipText = a.description);
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                        notifyIcon1.ShowBalloonTip(1000);

                    }
                    catch
                    {
                        Console.WriteLine("No reports found");
                        notifyIcon1.Visible = true;
                        notifyIcon1.Icon = SystemIcons.Exclamation;
                        notifyIcon1.BalloonTipTitle = "No new news today!";
                        notifyIcon1.BalloonTipText = "Have a nice day";
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                        notifyIcon1.ShowBalloonTip(1000);

                    }
                }
            }
        }
        [DataContract]
        public class FZResult
        {      
        //    [DataMember(Name = "sources")]
            [DataMember(Name = "articles")]
            public List<Article> Articles { get; set; }
        }

        public class Article
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "description")]
            public string description { get; set; }
        }
    }
}
