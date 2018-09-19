using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RSSReader.RSS_Parsers;

namespace RSSReader
{
    /// <summary>
    /// Логика взаимодействия для FeedAddWindow.xaml
    /// </summary>
    public partial class FeedAddWindow : Window
    {
        public FeedAddWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Shows all channels from feed list
        /// </summary>
        public void ShowChannels()
        {
            foreach (RSSFeed feed in RSSFeed.Feeds)
            {
                Button button = new Button();
                button.Content = feed.ChannelInfo.Title;
                button.Click += (sender, args) =>
                {
                    MainWindow.CurrentFeed = feed;
                    Close();
                };
                ChannelPanel.Children.Add(button);
            }
        }
    }

}
