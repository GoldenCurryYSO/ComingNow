using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace MastodonClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(new ConsoleUpdater());
            while (true)
            {
                string str = Console.ReadLine();
                Regex reg_a = new Regex("^(.*?)(<a.*?</a>)(.*)$");
                Regex reg_br = new Regex("<br.*?/>");

                Match m;
                Console.WriteLine(reg_br.Replace(str, "\n"));
                /*
                string after = str;
                while((m = reg_a.Match(after)).Success)
                {
                    Console.WriteLine(m.Groups[2]);
                    after = m.Groups[3].Value;
                }
                */
                /*
                Console.WriteLine("li:log in / ltl:get ltl");
                string cmd = Console.ReadLine();
                switch(cmd)
                {
                    case ("li"):
                        client.Authorization();
                        Console.WriteLine("Input code:");
                        string code = Console.ReadLine();
                        client.GetAccessToken(code).Wait();
                        break;
                    case ("ltl"):
                        var updater = new ConsoleUpdater();
                        client.StreamingLocalTimeline(updater).Wait();
                        break;
                }
                */
            }
        }
    }
}