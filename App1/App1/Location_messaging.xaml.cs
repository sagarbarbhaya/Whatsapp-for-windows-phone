///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:Location_messaging.xaml.cs
///Description: Location_messaging is designed for the purpose of tracking a user location nd sending messages 
///to that respective selected users.
///And if User is new he/she can register to use this Application
///</summary>

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Location_messaging : Page
    {
        string url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";
        string selectedemail = "";
        string use = "";
        string f_name = "";
        string l_name = "";
        string email = "";
        string Gmessage = "";
        string messages = "";
        string ts = "";
        string fromMessage = "";
     
        string tommm= (string)ApplicationData.Current.LocalSettings.Values["lmail"];

        public Location_messaging()
        {
            this.InitializeComponent();
            label.Text = tommm;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Updategetmessage(tommm);
            
        }
       
        /// <summary>
        /// This async function is made to send messages to new users
        /// IT interacts with the server by appending the url and all the commands
        /// </summary>
        /// <returns>
        /// await client.GetStringAsync(uri)
        /// </returns>
        private async Task<string> SendMessage()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.MaxResponseContentBufferSize = 3000000;
            var uri = new Uri(url + "command=sendMessage" + "&"
                + "email=" + ApplicationData.Current.LocalSettings.Values["email"].ToString() + "&"
                + "password=" + ApplicationData.Current.LocalSettings.Values["password"].ToString() + "&"
                + "to=" + tommm + "&" + "message=" + tb1.Text);

            return await client.GetStringAsync(uri);

        }
        /// <summary>
        /// This async function is made to get messages from the friends
        /// It works by interacting with the server and by passing commands
        /// </summary>
        /// <returns></returns>
        private async Task<string>  getMessages()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.MaxResponseContentBufferSize = 3000000;
            var uri = new Uri(url + "command=getMessages" + "&"
                + "email=" + ApplicationData.Current.LocalSettings.Values["email"].ToString() + "&"
                + "password=" + ApplicationData.Current.LocalSettings.Values["password"].ToString());
           
           return await client.GetStringAsync(uri) ;
        }
       /// <summary>
       /// This function is created to update the UI of textbox by the list of messages of Previous 
       /// conversation.
       /// </summary>
       /// <param name="str"></param>
        private async void Updategetmessage(string str)
        {
            mesg.Text = "";

            Gmessage = await this.getMessages();
            var obj = JArray.Parse(Gmessage);
            foreach (JObject item in obj)
            {
                bool b = false;
                foreach (KeyValuePair<String, JToken> app in item)
                {
                    if (app.Key == "msg_type")
                    {
                        fromMessage = (String)app.Value.ToString();

                    }

                    if (app.Key == "message")
                    {
                        messages = (String)app.Value.ToString();
                    }
                    if (app.Key == "ts")
                    {
                        ts = (String)app.Value.ToString();
                    }
                    if (app.Key == "email")
                    {
                        
                        if (tommm== (String)app.Value.ToString())
                        {
                            b = true;
                        }
                    }
                }

              
                if (b)
                {
                    if (fromMessage == "to")
                    {

                        mesg.Text = " \n\t\t\t" + messages + "\n\t\t\t" + ts + "\n" + mesg.Text;
                    }
                    else
                    {

                        mesg.Text = " \n" + messages + "\n" + ts + "\n" + mesg.Text;

                    }  
                }
            }

        }
        /// <summary>
        /// This function is for the selecting textbox with the eail id on it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if(label.Text!=null)
            {
                mesg.Opacity = 1;
                Updategetmessage(tommm);
            }
        }
        /// <summary>
        /// This function is created for updating UI with thread
        /// UI will be updated after every 7 seconds
        /// </summary>
        private void Timer()
        {
            TimeSpan updatetime = TimeSpan.FromSeconds(7);
            ThreadPoolTimer timer1 = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                //  this.Updategetmessage(selectedemail);
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    this.Updategetmessage(selectedemail);


                });
            }, updatetime);
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            sendButtonClick();
        }
        /// <summary>
        /// This function is executed when there is click on send button
        /// It shoots out the send message function
        /// </summary>
        private async void sendButtonClick()
        {
            if (tommm != null)
            {
                if (tb1.Text != "")
                {
                    await SendMessage();

                    Updategetmessage(tommm);
    

                   tb1.Text = "";
                }


            }
        }

        private void mesg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
