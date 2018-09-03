using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace transkart_captcha_downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\thedc\Desktop\TranskartCaptcha";
            string captchaPageUrl = "http://81.23.146.8/default.aspx";
            string captchaXPath = "//img";
            string captchaImageFormat = ".jpg";
            int imagesNumber;

            Console.Write("Count: ");
            imagesNumber = Convert.ToInt32(Console.ReadLine());
            GetCaptcha(folderPath, captchaPageUrl, captchaXPath, imagesNumber, captchaImageFormat);
            Console.WriteLine("Ok");
            Console.ReadKey(true);
        }

        static void GetCaptcha(string folderPath, string captchaPageUrl, string captchaXPath, int imagesNumber, string captchaImageFormat)
        {
            Console.WriteLine("Downloading started...");
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = new HtmlDocument();
            HtmlNode node;
            WebClient client = new WebClient();
            string downloadUrl, localPath;
            for (int i = 0; i < imagesNumber; i++)
            {
                Console.WriteLine("Downloading captcha #" + (i + 1));
                document = web.Load(captchaPageUrl);
                node = document.DocumentNode.SelectSingleNode(captchaXPath);
                downloadUrl = new Uri(captchaPageUrl).GetLeftPart(UriPartial.Authority) + '/' + node.Attributes["src"].Value;
                localPath = Path.Combine(folderPath, ((i + 1).ToString()) + captchaImageFormat);
                client.DownloadFile(downloadUrl, localPath);
                Console.WriteLine((i + 1) + " downloaded");
            }
        }
    }
}
