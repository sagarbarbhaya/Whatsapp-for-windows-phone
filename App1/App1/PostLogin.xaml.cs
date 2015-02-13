///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:PostLogin.xaml.cs
///Description: This page will help to navigate the user to othe page
///</summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using System.Net.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostLogin : Page
    {
        //String url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";
        public PostLogin()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }
        MainPage mp = new MainPage();
        /// <summary>
        /// This hyperlink button is used to navigate to read message page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
          
            this.Frame.Navigate(typeof(GetMessage));
        }
        /// <summary>
        /// This hyperlink button is used to navigate to send message page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Users));
        }
        /// <summary>
        /// This hyperlink button is used to navigate to use map location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyperlinkButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Location));

        }

        private void HyperlinkButton_Click_3(object sender, RoutedEventArgs e)
        {

        }
       
    }
}
