///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:GetMessage.xaml.cs
///Description: Users.xaml.cs Page is designed for the purpose of reading/getting messages from the 
///friends.
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
    public sealed partial class GetMessage : Page
    {
        string url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";
        string Gmessage = "";
        string frommessage="";
        string messages="";
        string ts="";
        string emails="";
        string emaillist = "";
        private Dictionary<string,int> Dictionary = new Dictionary<string,int>();
       
        

        
        public GetMessage()
        {
            this.InitializeComponent();
            this.Timer();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            display();
        }
        /// <summary>
        /// This method is used to implement send message command 
        /// </summary>
        /// <returns></returns>
       
        private async Task<string> SendMessage()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.MaxResponseContentBufferSize = 3000000;
            var uri = new Uri(url + "command=sendMessage" + "&"
                + "email=" + ApplicationData.Current.LocalSettings.Values["email"].ToString() + "&"
                + "password=" + ApplicationData.Current.LocalSettings.Values["password"].ToString() + "&"
                + "to=" + emaillist + "&" + "message=" + send.Text);

            return await client.GetStringAsync(uri);

        }
        ModelView m = new ModelView();
        /// <summary>
        /// This method is implemented to display the email list of only those friends who
        /// have sent a message
        /// </summary>
        public async void display()
        {
            Gmessage = await m.getModel(1);
            var obj = JArray.Parse(Gmessage);
            foreach (JObject item in obj)
            {
               
                foreach (KeyValuePair<String, JToken> app in item)
                {
                    if (app.Key == "msg_type")
                    {
                        frommessage = (String)app.Value.ToString();

                    }
                   
                    if (app.Key == "ts")
                    {
                        ts = (String)app.Value.ToString();
                    }
                    if (app.Key == "email")
                    {
                        emails = (String)app.Value.ToString();
                    }


                    if(!(Dictionary.ContainsKey(emails)) && emails!="")
                    {
                        Dictionary.Add(emails, 100);
                    }
                    
                }
               
                
            }
           foreach(var temp in Dictionary)
           {
               combo.Items.Add(temp.Key);

           }
        }
        /// <summary>
        /// This method is implemented to update the messages in textbox
        /// </summary>
        /// <param name="str"></param>
        private async void Updategetmessage(string str)
        {
            mes.Text = "";
            Gmessage = await m.getModel(1);
            var obj = JArray.Parse(Gmessage);
            foreach (JObject item in obj)
            {
                bool b = false;
                foreach (KeyValuePair<String, JToken> app in item)
                {
                    if (app.Key == "msg_type")
                    {
                        frommessage = (String)app.Value.ToString();

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
                       
                        if (emaillist== (String)app.Value.ToString())
                        {
                            b = true;
                        }
                    }
                }

               
                if (b)
                {
                    if (frommessage == "to")
                    {

                        mes.Text = " \n\t\t\t" + messages + "\n\t\t\t" + ts + "\n" + mes.Text;
                    }
                    else
                    {

                        mes.Text = " \n" + messages + "\n" + ts + "\n" + mes.Text;

                    }  
                }
            }

        }
        /// <summary>
        /// This function is implemented to change the combo items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           emaillist = (String)combo.SelectedItem;
            if(emaillist!=null)
            {
                send.Opacity = 1;
                Sendbtn.Opacity = 1;

                Updategetmessage(emaillist);
                mes.Opacity = 1;
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
                Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    this.Updategetmessage(emaillist);


                });
            }, updatetime);
        }
        private void Sendbtn_Click(object sender, RoutedEventArgs e)
        {
            sendButtonClick();
        }
        /// <summary>
        /// This function will be executed when there is click on send button
        /// </summary>
        private async void sendButtonClick()
        {
            if (emaillist != null)
            {
                if (send.Text != "")
                {
                    await SendMessage();

                    Updategetmessage(emaillist);
                    //mes.Opacity = 1;

                    send.Text = "";
                }


            }
        }





    }
}
