using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading;

namespace Gamer_Spammer
{
    class Http
    {
        public static byte[] Post(string url, NameValueCollection pairs)
        {
            using (WebClient webClient = new WebClient())
                return webClient.UploadValues(url, pairs);
        }
    }
    class Program
    {
        public static string WebhookUrl = "";
        public static string WebhookUsername = "";
        public static string WebhookMessage = "";

        public static int SendNumberOfTimes = 0;
        public static double WaitInterval = 0;
        public static int WebhookSentNumber = 0;

        public static double ToMiliseconds(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalMilliseconds;
        }

        static void Main(string[] args)
        {
            Console.Title = ("coasts' Spammer");

            Console.WriteLine("Enter the Discord webhook url:");
            WebhookUrl = Console.ReadLine();

            Console.WriteLine("Enter a username for the webhook:");
            WebhookUsername = Console.ReadLine();

            Console.WriteLine("Enter the message to send:");
            WebhookMessage = Console.ReadLine();

            Console.WriteLine("Enter the amount of how many times you want it to send:");
            SendNumberOfTimes = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Wait interval (In seconds and to prevent getting rate limited.)");
            WaitInterval = Int32.Parse(Console.ReadLine());

            Console.WriteLine("\nWebhook url: " + WebhookUrl + ", Webhook message: " + WebhookMessage + ", Amount to send: " + SendNumberOfTimes + ", and Wait interval: " + WaitInterval);
            Console.WriteLine("When you are ready, press any key to start.");
            Console.ReadKey(true);

            Console.WriteLine("");

            while (SendNumberOfTimes > WebhookSentNumber)
            {
                WebhookSentNumber += 1;

                Http.Post(WebhookUrl, new NameValueCollection()
                {
                    {
                        "username",
                        WebhookUsername
                    },
                    {
                        "content",
                        WebhookMessage
                    }
                });

                Console.WriteLine("[Info]: Sent webhook post number " + WebhookSentNumber + " of " + SendNumberOfTimes + ".");

                Thread.Sleep((int)ToMiliseconds(WaitInterval));
            }

            Console.WriteLine("\nFinished webhook spam, press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
