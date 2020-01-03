using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Timers;

namespace TwitterBot
{
    class Program
    {

        private static string customerKey = "exaO4equ7vze7WfpoeuiPqdBs";
        private static string customerKeySecret = "iUKTHsK4oycV2vFgXoNO8qIU75ugBRWzUkEUW2zpeYnfgCz9xb";
        private static string accessToken = "1577588460-jv5Q9byGgpVKsNclKLqTA7BQzSEyTLrsnuWofIM";
        private static string accessTokenSecret = "5GshyiBeOrMXJvZXSaULeh6vESQzQzgGHnZiXJfDWobXd";

        private static TwitterService service = new TwitterService(customerKey, customerKeySecret, accessToken, accessTokenSecret);

        static void Main(string[] args)
        {

            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            SendTweet("HelloWorld!");
            Console.Read();

        }

        private static void SendTweet(string _status)
        {

            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR>" + response.Error.Message);
                    Console.ResetColor();
                }
            });

        }
    }
}
