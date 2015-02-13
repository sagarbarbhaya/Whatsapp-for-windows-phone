///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:Message.xaml.cs
///Description: Message.xaml.cs Page is designed for the purpose of sending messages to new user
///And if User is new he/she can register to use this Application
///</summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Message : Page
    {
        String url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";
        string messagem;
        public Message()
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
            Update();
           
        }
        /// <summary>
        /// This function is used to get messages and store in string
        /// </summary>
        private async void Update()
        {
            messagem = await this.getMessages();

        }
        private void message_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        /// <summary>
        /// This function is used to getmessages by appending to the url
        /// </summary>
        /// <returns></returns>
        private async Task<string> getMessages()
     {
         var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
           
         client.MaxResponseContentBufferSize = 3000000;

         var uri = new Uri(url + "command=getMessages" + "&"
             + "email=" + ApplicationData.Current.LocalSettings.Values["email"].ToString()+ "&"
             + "password=" + ApplicationData.Current.LocalSettings.Values["password"].ToString());
         return await client.GetStringAsync(uri);
           
     }
    }
}
