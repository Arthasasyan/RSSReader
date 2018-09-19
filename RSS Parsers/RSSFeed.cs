using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RSSReader.RSS_Parsers
{
    public class RSSFeed :XmlDocument
    {
        public RSSChannelInfo ChannelInfo { get;}
        public List<RSSItem> ItemList { get; private set; }

        public static List<RSSFeed> Feeds { get; }

        static RSSFeed()
        {
            Feeds = new List<RSSFeed>();
        }
        /// <summary>
        /// Reads feed from rss file
        /// </summary>
        /// <param name="file">File path</param>
        /// <param name="rssFileCorrect"> Will be false if failed to read file</param>
        public RSSFeed(string file, out bool rssFileCorrect)
        {
            Load(file);
            rssFileCorrect = true;
            ItemList = new List<RSSItem>();
            foreach (XmlNode rss in ChildNodes)
            {
                if (rss.LocalName == "rss")
                {
                    bool foundItemNode = false;
                    XmlNode channel =null;
                    foreach (XmlNode node in rss.ChildNodes)
                    {
                        if (node.LocalName == "channel")
                        {
                            ChannelInfo = new RSSChannelInfo(node);
                            channel = node;
                        }

                        if (node.LocalName == "item")
                        {
                            ItemList.Add(new RSSItem(node));
                            foundItemNode = true;
                        }

                    }

                    if (!foundItemNode) //if items are in channel
                    {
                        foreach (XmlNode node in channel.ChildNodes)
                        {
                            if (node.LocalName == "item")
                            {
                                ItemList.Add(new RSSItem(node));
                                foundItemNode = true;
                            }
                        }
                    }

                    if (!foundItemNode) //if still didn't find any item nodes
                    {
                        MessageBox.Show("RSS File is incorrect");
                        rssFileCorrect = false;
                    }

                    break;
                }
            }
        }
        /// <summary>
        /// Makes panel from items
        /// </summary>
        /// <returns>ItemPanel to display</returns>
        public StackPanel Display()
        {
            StackPanel view = new StackPanel();
            TextBlock channelInfo = new TextBlock();
            channelInfo.Text = ChannelInfo.Title;
            channelInfo.FontSize = 40;
            view.Children.Add(channelInfo);
            foreach (RSSItem item in ItemList)
            {
                view.Children.Add(item.Display());
            }
            return view;
        }
        /// <summary>
        /// Reeds files in working directory with "rss" extension
        /// </summary>
        public static void InitializeList()
        {
            foreach (string fileName in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Extension == ".rss")
                {
                    bool isCorrect;
                    RSSFeed feed = new RSSFeed(fileName, out isCorrect);
                    if (isCorrect)
                    {
                        Feeds.Add(feed);
                    }
                }
            }
        }
    }
}