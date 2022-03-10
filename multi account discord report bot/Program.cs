using System;
using System.IO;
using System.Net;
using System.Threading;
using Leaf.xNet;

namespace multi_account_discord_report_bot
{
    class Program
    {
        static void Main()
        {

            string[] tokens;
            retry:
            try
            {
            
            
            Console.WriteLine("example of file:\ntoken1\ntoken2");

            Console.Write("Drag the .txt file with tokens: ");

            string path = Console.ReadLine();
            tokens = File.ReadAllLines(path);


            }
            catch
            {
                Console.WriteLine("Looks like there is something wrong to the file path, an valid example: D:\\Desktop\\tokens.txt ");
                Thread.Sleep(3500);
                goto retry;

            }


            Console.WriteLine("Server id:");
            var guildid = Console.ReadLine();


            Console.WriteLine("Channel id:");
            var channelid = Console.ReadLine();


            Console.WriteLine("Message id:");
            var messageid = Console.ReadLine();


            Console.WriteLine("Reason:");
            var reason = Console.ReadLine();

            int hits = 0;
            int tries = 0;
            foreach (var token in tokens)
            {

                tries++;
                bool Worked = Check(token, channelid, guildid, messageid, reason);

                if (Worked)
                {
                    hits++;
                    Console.WriteLine("Report send from " + token);
                    
                }
                else {
                    Console.WriteLine("did not manage to sent a report, Maybe the account doesn't have permission to view the message. " + token);

                }

                Console.Title = "Tries: " + tries + " Reports sent: " + hits;

            }


        

        }


        private static bool Check(string token, string channelid, string guildid, string messageid, string reason)
        {

            try
            {


            
            WebClient client = new WebClient();

            client.Headers.Add("Authorization", token);

            string url = "https://discord.com/api/v6/report";

            string jsonData = "{\"channel_id\": \"" + channelid + "\", \"guild_id\": \"" + guildid + "\", \"message_id\": \"" + messageid + "\", \"reason\": \"" + reason + "\" }";

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            string HtmlResult = client.UploadString(url, jsonData);
            
                
                return true;
            }
            catch (Exception ex)
            {
           
                return false;

            }
            

    }


    }
}
