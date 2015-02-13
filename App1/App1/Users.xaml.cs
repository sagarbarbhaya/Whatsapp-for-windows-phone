///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:Users.xaml.cs
///Description: Users.xaml.cs Page is designed for the purpose of sending messages to new user
///And if User is new he/she can register to use this Application
///</summary>
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;
using Windows.System.Threading;
using Windows.UI.Core;
using System.Collections.ObjectModel;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Users : Page
    {
        string url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";

        ObservableCollection<Friends> myFriends = new ObservableCollection<Friends>();
        string use;
        string Gmessage;
        string name = "";
       string  f_name = "";
       string l_name = "";
        string email = "";
     
        string fromMessage = "";
        string messages = "";
        string ts = "";
      

        string selectedemail="";
       



        public Users()
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
            Updateu();
            com.ItemsSource = myFriends;
        }
        /// <summary>
        /// This Combo Box is functioned when there is click on any item in combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem c = (ComboBoxItem)com.SelectedItem;
            selectedemail = c.Name;
            if(selectedemail!=null)
            {
                send.Opacity = 1;
                Sendbtn.Opacity = 1;
               
                Updategetmessage(selectedemail);
                mes.Opacity = 1;
               

            }
            
        }
        ModelView m1 = new ModelView();
        /// <summary>
        /// This function is implemented to search for the user from List of users
        /// </summary>
        private async void Updateu()
        {

            use = await m1.getModel(0);
            var obj = JArray.Parse(use);
            foreach (JObject item in obj)
            {
               
                foreach (KeyValuePair<String, JToken> app in item)
                {
                    if (app.Key == "first_name")
                    {
                         f_name = (String)app.Value.ToString();
                    }
                    if (app.Key == "last_name")
                    {
                        l_name = (String)app.Value.ToString();
                    }
                    if (app.Key == "email")
                    {
                        email = (String)app.Value.ToString();
                    }
                }
                Friends f = new Friends();
                f.fName = f_name + " " + l_name;
                f.femail = email;
                //ComboBoxItem m = new ComboBoxItem();
                //m.Content = f_name + " " + l_name;
                //m.Name = email;
                //com.Items.Add(m);
                myFriends.Add(f);
            }
        }



        /// <summary>
        /// This async function is made to send messages to new users
        /// IT interacts with the server by appending the url and all the commands
        /// </summary>
        /// <returns>
        /// await client.GetStringAsync(uri)
        /// </returns>
        private async Task<string>SendMessage()
        {
            var client = new HttpClient();
           client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.MaxResponseContentBufferSize = 3000000;
            var uri = new Uri(url + "command=sendMessage" + "&"
                + "email=" + ApplicationData.Current.LocalSettings.Values["email"].ToString() + "&"
                + "password=" + ApplicationData.Current.LocalSettings.Values["password"].ToString() + "&"
                + "to=" + name + "&" + "message=" + send.Text);
           
          return await client.GetStringAsync(uri);

        }
        /// <summary>
        /// This function is created to update the UI of textbox by the list of messages of Previous 
        /// conversation.
        /// </summary>
        /// <param name="str"></param>
        private async void Updategetmessage(string str)
        {
            mes.Text = "";
            
            Gmessage = await m1.getModel(1);
            var obj = JArray.Parse(Gmessage);
            foreach (JObject item in obj)
            {
                bool b = false;
                foreach (KeyValuePair<String, JToken> app in item)
                {
                    if ( app.Key == "msg_type" )
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
                    if(app.Key == "email")
                    {
                       
                        if (name == app.Value.ToString())
                        {
                            b = true;
                        }
                    }
                }

              
                if (b)
                {
                    if (fromMessage == "to")
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
      /// This function is executed when the user will click on send button
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void Sendbtn_Click(object sender, RoutedEventArgs e)
        {
            sendButtonClick();
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
                        this.Updategetmessage(name);

                   
                    });
            }, updatetime);
        }
        /// <summary>
        /// This function will be executed when there is click on send button
        /// </summary>
        private async void sendButtonClick()
        {
            if(name!=null)
            {
                if (send.Text != "")
                {   
                    await SendMessage();

                    Updategetmessage(name);                 
                    //mes.Opacity = 1;
                   
                    send.Text = "";
                }
              

            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Sendbtn_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void mes_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock m = (TextBlock)sender;
            name = m.Tag.ToString();
            if (name != null)
            {
                send.Opacity = 1;
                Sendbtn.Opacity = 1;

                Updategetmessage(name);
                mes.Opacity = 1;


            }

        }
        
    }
}
