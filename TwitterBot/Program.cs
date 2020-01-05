using System;
using System.Linq;
using System.Net;
using System.Threading;
using TweetSharp;

namespace TwitterBot
{
    class Program
    {
        //TODO 
        // - transfer to raspi. 

        private static string lastTweet = "";
        private static string customerKey = "gQFaDYO5a59nf3AGWo6ZCCIaJ";
        private static string customerKeySecret = "bbmjE1aU1fQeyvNUR1MpghhyqaUwzWiNlNG4u6HIAcCdD3pJRY";
        private static string accessToken = "1212970147939381250-7aj2fMVVd5yq5HElbElncVOVJkUVJp";
        private static string accessTokenSecret = "joz6Ruap7aV2URSSp7CvGiD6wCs1aZeiCj1iDr5PHcDrr";
        private static TwitterService service = new TwitterService(customerKey, customerKeySecret, accessToken, accessTokenSecret);

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine($"<{DateTime.Now}> - Bot Cycling");

                //Get tweet to send as string 
                var tweet = Program.GetTweet("@EPA_Victoria #AirQuality forecast for today:");

                Console.WriteLine("Tweet loaded before duplicate check: " + tweet);

                //Check to make sure we're not sending a duplicate tweet
                if (tweet != lastTweet && tweet[0] != 'R')
                {
                    //Send it
                    SendTweet("RT @EPA_Victoria: " + tweet);
                    Program.lastTweet = tweet;
                }
                else
                {
                    Console.WriteLine("Not Sent: duplicate tweet");
                }

                //Cycle every thirty minutes
                Thread.Sleep(1800000);
            }
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

        private static String GetTweet(string target)
        {
            var tweets_search = service.Search(new SearchOptions { Q = target, Resulttype = TwitterSearchResultType.Recent });

            return tweets_search.Statuses.First().Text;
        }
    }
}
