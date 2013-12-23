using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Linq;

namespace Twitter_Search
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        private void btnLookupUser_Click(object sender, RoutedEventArgs e)
        {
            WebClient twitter = new WebClient();
            twitter.DownloadStringCompleted += new DownloadStringCompletedEventHandler(twitter_DownloadStringCompleted);
            twitter.DownloadStringAsync(new Uri("http://search.twitter.com/search.atom?q=%40twitterapi"));
        }


        void twitter_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                return;

            XElement xmlTweets = XElement.Parse(e.Result);

            lstTwitter.ItemsSource = from tweet in xmlTweets.Descendants("status")
                                     select new TwitterItem
                                     {
                                         ImageSource = tweet.Element("user").Element("profile_image_url").Value,
                                         Message = tweet.Element("text").Value
                                     };
        }
    }
}