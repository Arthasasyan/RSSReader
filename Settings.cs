using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RSSReader
{
    /// <summary>
    /// Class for Settings
    /// </summary>
    [Serializable]
    public class Settings
    {

        public DateTime LastUpdate { get; set; }
        public int UpdateTimeInSeconds { get; set; }
        [NonSerialized]
        public List<string> RSSFeeds;

        private string[] rssFeeds;

        public Settings()
        {

        }

        public Settings(string[] rssFeeds, DateTime lastUpdate, int updateTime)
        {
            RSSFeeds = new List<string>(rssFeeds);
            LastUpdate = lastUpdate;
            UpdateTimeInSeconds = updateTime;
            this.rssFeeds = rssFeeds;
        }

        /// <summary>
        /// Initializes settings.xml or reads information from it and sets it to CurrentSettings
        /// </summary>
        public static void Initialize()
        {
            string fileName = "settings.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));

            if (!File.Exists(fileName))
            {
                CurrentSettings = new Settings(new string[] {@"https://habr.com/rss/interesting/"}, DateTime.Now, 3600);
                using (FileStream xmlFile = new FileStream(fileName, FileMode.CreateNew))
                {
                    serializer.Serialize(xmlFile, CurrentSettings);
                }
            }
            else
            {
                using (FileStream xmlFile = new FileStream(fileName, FileMode.Open))
                {
                    CurrentSettings = serializer.Deserialize(xmlFile) as Settings;
                }
            }
        }

        /// <summary>
        /// Updates Settings file
        /// </summary>
        public static void UpdateSettings()
        {
            string fileName = "settings.xml";
            CurrentSettings.LastUpdate = DateTime.Now;
            XmlSerializer serializer=new XmlSerializer(typeof(Settings));
            using (FileStream xmlFile = new FileStream(fileName, FileMode.Open))
            {
             serializer.Serialize(xmlFile, CurrentSettings);
            }
        }
        public static Settings CurrentSettings { get; set; }
        public static bool NeedsUpdate {
            get
            {
                return DateTime.Now.CompareTo(
                           CurrentSettings.LastUpdate.AddSeconds(CurrentSettings.UpdateTimeInSeconds)) <= 0;
            }
        }

    }
}