using System;
using System.Net;
using System.Windows;
using RSSReader.RSS_Parsers;

namespace RSSReader
{
    public static class Downloader
    {
        private static readonly WebClient _webClient;

        static Downloader()
        {
            _webClient=new WebClient();
        }

        /// <summary>
        /// Downloads each file from Current Settings
        /// </summary>
        public static void Download()
        {

            int feedCollectionSize = Settings.CurrentSettings.RSSFeeds.Count;
            for(int i=0;i<feedCollectionSize;i++)
            {
                string url = Settings.CurrentSettings.RSSFeeds[i];
                string fileName;
                if (url.StartsWith("http"))
                {
                    fileName = $"{url.Split('/')[2].Split('.')[0]}.rss";
                }
                else
                {
                    fileName = $"{url.Split('/')[0].Split('.')[0]}.rss";
                }
                _webClient.DownloadFile(url,fileName);
               /* bool isCorrect;
                RSSFeed feed = new RSSFeed(fileName,out isCorrect);
                if (isCorrect)
                {
                    Settings.CurrentSettings.RSSFeeds.Add(url);
                    RSSFeed.Feeds.Add(feed);
                }*/
            }
        }
        /// <summary>
        /// Downloads file
        /// </summary>
        /// <param name="url">URL-adress of file</param>
        public static void Download(string url)
        {
            string fileName;
            if (url.StartsWith("http"))
            {
                fileName = $"{url.Split('/')[2].Split('.')[0]}.rss";
            }
            else
            {
                fileName = $"{url.Split('/')[0].Split('.')[0]}.rss";
            }

            _webClient.DownloadFile(url,fileName);
            bool isCorrect;
            RSSFeed feed = new RSSFeed(fileName,out isCorrect);
            if (isCorrect)
            {
                Settings.CurrentSettings.RSSFeeds.Add(url);
                RSSFeed.Feeds.Add(feed);
            }
        }
    }
}