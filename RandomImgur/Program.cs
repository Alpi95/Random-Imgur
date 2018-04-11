using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using HtmlAgilityPack;
using System.Net;

namespace RandomImgur
{
    class Program
    {
        private static readonly Random rnd = new Random();
        private static string url = "https://i.imgur.com/";
        static private string[] charList = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        
        static private string RandomCharOrNmb()
        {
            return charList[rnd.Next(0, charList.Length)];
        }

        static private string GenerateIndentifier()
        {
            string id = "";

            for (int i = 0; i < 5; i++)
            {
                int lowUp = rnd.Next(0, 2);

                switch (lowUp)
                {
                    case 0:
                        id += RandomCharOrNmb().ToLower();
                        break;
                    case 1:
                        id += RandomCharOrNmb();
                        break;
                }
            }
            return id;
        }

        private static void Search()
        {
            //  string path = @"C:\Users\Aleksander\Desktop\imgur.txt";
            string imgPath = @"C:\Users\Aleksander\Desktop\pics\";
            string fullUrl;

          //  if (!File.Exists(path))
          //  {
          //      string createText = "URL's:" + Environment.NewLine;
          //      File.WriteAllText(path, createText);
          //  }

            for (int i = 0; i < 10000; i++)
            {
                string identifier = GenerateIndentifier();
                string fullUrl = url + identifier;

                var webGet = new HtmlWeb();
                var document = webGet.Load(fullUrl);
                var title = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;

                if(title != "imgur: the simple 404 page")
                {
                  //  string appendText = i.ToString() + " " + URL + Environment.NewLine;
                  //  File.AppendAllText(path, appendText);

                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(fullUrl + ".jpg", imgPath + identifier + ".jpg");
                }
            }
            Console.WriteLine(Thread.CurrentThread.Name + " Finished");
            Thread.CurrentThread.Abort();
        }

        static void Main(string[] args)
        {
            Thread thr = new Thread(Search);        
            thr.Start();
            Console.ReadKey();
        }
    }
}
