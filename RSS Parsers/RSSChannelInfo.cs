using System.Xml;

namespace RSSReader.RSS_Parsers
{
    public class RSSChannelInfo
    {
        public string Title { get; }
        //public string Link { get; }
        //public string Description { get; }
        //private XmlNode node;

        public RSSChannelInfo(XmlNode channelNode)
        {
            //node = channelNode;
            foreach (XmlNode n in channelNode.ChildNodes)
            {
                if (n.LocalName == "title")
                {
                    Title = n.InnerText;
                }
               /* else if (n.LocalName == "link")
                {
                    Link = n.Value;
                }
                else if (n.LocalName == "describtion")
                {
                    Description = n.Value;
                }*/
            }
        }

    }
}