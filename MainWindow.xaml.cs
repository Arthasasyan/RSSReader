using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using RSSReader.RSS_Parsers;

namespace RSSReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window
    {
        public static RSSFeed CurrentFeed { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Settings.Initialize();
            Downloader.Download();

            RSSFeed.InitializeList();
            CurrentFeed = RSSFeed.Feeds[0];
            DisplayFeed();

        }

        /// <summary>
        /// Displays Current Feed in ItemPanel
        /// </summary>
        private void DisplayFeed()
        {
            ItemPanel.Children.Clear();
            ItemPanel.Children.Add(CurrentFeed.Display());
        }
        /// <summary>
        /// Handler for choice menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem.Header.ToString()=="Выбрать ленту")
            {
                FeedAddWindow window = new FeedAddWindow();
                window.Owner = this;
                window.Show();
                window.ShowChannels();
                window.Closed += (o, args) => { DisplayFeed();};

            }
        }
        /// <summary>
        /// Handler for SenderButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SenderButton_OnClick(object sender, RoutedEventArgs e)
        {
            Downloader.Download(LinkBox.Text);
            Settings.UpdateSettings();
            ((sender as Button).Parent as MenuItem).IsSubmenuOpen = false;
            CurrentFeed = RSSFeed.Feeds[RSSFeed.Feeds.Count - 1];
            DisplayFeed();
        }

/*        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Settings.CurrentSettings.UpdateTimeInSeconds = Int32.Parse(CurrentUpdateTime.Text);
                if(Settings.NeedsUpdate)
                    Downloader.Download();

                Settings.UpdateSettings();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            ((sender as Button).Parent as MenuItem).IsSubmenuOpen = false;
        }
        */
    }
}