using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTMLdecode;
using System.Net;


namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            HTMLdecode.HTMLdecode htmlDecode = new HTMLdecode.HTMLdecode();
            WebClient webClient = new WebClient();
            String html = webClient.DownloadString("http://kionier.com/about/");
            htmlDecode.Startprocess(html);
            
            Console.ReadKey();
        }
    }
}
