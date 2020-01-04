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
        //TODO 
        // - Search only EPA_Victoria rather than the whole hashtag

        private static string customerKey = "gQFaDYO5a59nf3AGWo6ZCCIaJ";
        private static string customerKeySecret = "bbmjE1aU1fQeyvNUR1MpghhyqaUwzWiNlNG4u6HIAcCdD3pJRY";
        private static string accessToken = "1212970147939381250-7aj2fMVVd5yq5HElbElncVOVJkUVJp";
        private static string accessTokenSecret = "joz6Ruap7aV2URSSp7CvGiD6wCs1aZeiCj1iDr5PHcDrr";

        private static TwitterService service = new TwitterService(customerKey, customerKeySecret, accessToken, accessTokenSecret);

        static void Main(string[] args)
        {

            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            var tweet = "RT @EPA_Victoria:" + Program.GetTweet("#AirQuality");
            SendTweet(tweet);
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


        private static String GetTweet(string hashtagTarget)
        {
            var tweets_search = service.Search(new SearchOptions { Q = hashtagTarget, Resulttype = TwitterSearchResultType.Recent });
            
            return tweets_search.Statuses.First().Text;
        }
    }
}
