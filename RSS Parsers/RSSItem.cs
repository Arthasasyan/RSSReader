using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;

namespace RSSReader.RSS_Parsers
{
    public class RSSItem
    {
        public string Title { get; }
        public string Link { get; }
        public string Description { get; }
        public string PubDate { get; }

        public RSSItem(XmlNode itemNode)
        {
            foreach (XmlNode node in itemNode.ChildNodes)
            {
                switch (node.LocalName)
                {
                    case "title":
                        Title = node.InnerText;
                        break;
                    case "link":
                        Link = node.InnerText;
                        break;
                    case "description":
                        Description = node.InnerText;
                        break;
                    case "pubDate":
                        string[] tempArr = node.InnerText.Split(',')[1].Split(' ');
                        PubDate = $"{tempArr[1]} {tempArr[2]} {tempArr[3]}"; //date: DAY MONTH YEAR

                        break;
                }
            }
        }
        /// <summary>
        /// Makes panel for item
        /// </summary>
        /// <returns>Panel to displat=y</returns>
        public StackPanel Display()
        {
            StackPanel itemPanel = new StackPanel();
            TextBlock title =new TextBlock();
            Hyperlink link = new Hyperlink();
            link.NavigateUri = new Uri(Link);
            link.Inlines.Add(Title);
            title.Inlines.Add(link);
            link.RequestNavigate += (sender, args) =>
            {
                Process.Start(args.Uri.AbsoluteUri);
                args.Handled = true;
            };
            Button describtion = new Button();
            describtion.Content = "Открыть описание";
            describtion.Click += (sender, args) =>
            {
                Button button = sender as Button;
                StackPanel parent = button.Parent as StackPanel;
                if (button.Content as string == "Открыть описание")
                {
                    TextBlock desc = new TextBlock();
                    desc.Text = Description;
                    parent.Children.Add(desc);
                    button.Content = "Закрыть описание";
                }
                else
                {
                    parent.Children.RemoveAt(parent.Children.Count-1); //Removing describtion
                    button.Content = "Открыть описание";
                }
            };

            TextBlock time = new TextBlock();
            time.Text = PubDate;
            itemPanel.Children.Add(time);
            itemPanel.Children.Add(title);
            itemPanel.Children.Add(describtion);
            return itemPanel;
        }

        //public static readonly DependencyProperty LinkProperty = DependencyProperty.Register("Link", typeof(Hyperlink),typeof(TextBlock));
    }
}