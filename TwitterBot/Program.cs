﻿using System;
using System.Linq;
using System.Net;
using TweetSharp;

namespace TwitterBot
{
    class Program
    {
        //TODO 
        // - Automate to update every hour while running on raspi. 

        private static string lastTweet = "";
        private static string customerKey = "gQFaDYO5a59nf3AGWo6ZCCIaJ";
        private static string customerKeySecret = "bbmjE1aU1fQeyvNUR1MpghhyqaUwzWiNlNG4u6HIAcCdD3pJRY";
        private static string accessToken = "1212970147939381250-7aj2fMVVd5yq5HElbElncVOVJkUVJp";
        private static string accessTokenSecret = "joz6Ruap7aV2URSSp7CvGiD6wCs1aZeiCj1iDr5PHcDrr";
        private static TwitterService service = new TwitterService(customerKey, customerKeySecret, accessToken, accessTokenSecret);

        static void Main(string[] args)
        {

            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            
            //Get tweet to send as string 
            var tweet = "RT @EPA_Victoria: " + Program.GetTweet("@EPA_Victoria #AirQuality forecast for today:");

            //Check to make sure we're not sending a duplicate tweet
            if (tweet != lastTweet)
            {
                //Send it
                //SendTweet(tweet);
                //Program.lastTweet = tweet;
                Console.WriteLine(tweet);
                Console.Read();
            }
            else
            {
                Console.WriteLine("Error: duplicate tweet");
                Console.Read();
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
