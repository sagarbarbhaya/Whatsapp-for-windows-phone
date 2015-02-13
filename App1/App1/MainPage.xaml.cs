///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:MainPage.xaml.cs
///Description: Main Page is designed for the purpose of Authetication/Login 
///And if User is new he/she can register to use this Application
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
using System.Threading.Tasks;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Windows;
using Windows.UI;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.Storage;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        String url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";
        string messageu;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        /// <summary>
        /// This HyperlinkButton will help to navigate to postlogin page 
        /// where user will get option of selecting messages or maps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            loginID();
            
        }
        /// <summary>
        /// Making an object of class ModelView
        /// </summary>
        ModelView m = new ModelView();
        /// <summary>
        /// This funtion is created for the purpose of validation
        /// </summary>
        private async void loginID()
        {
          if (mtb1.Text.Trim().Length == 0)
            {
                var dialog = new MessageDialog("Please Enter the Email-ID");
                dialog.ShowAsync();
              
            }
            else if (s.Password.Trim().Length == 0)
            {
                var dialog1 = new MessageDialog("Please Enter the password");
                dialog1.ShowAsync();
               
            }
            else if (mtb1.Text.Trim().Length != 0 && s.Password.Trim().Length != 0)
            {

                messageu = await this.getUsers ();
                if (messageu == "Invalid user")
                {
                    var dialogi = new MessageDialog("Please enter the valid credentials");
                    dialogi.ShowAsync();
                }
                else
                {
                    var dialog2 = new MessageDialog("Caution: This Page would Like to access your location");
                    dialog2.ShowAsync();
                    ApplicationData.Current.LocalSettings.Values["email"] = mtb1.Text;
                    ApplicationData.Current.LocalSettings.Values["password"] = s.Password;
                    this.Frame.Navigate(typeof(Options));
                }

            }
        }
      
      /// <summary>
      /// This function is used to retrieve users list from server
      /// </summary>
      /// <returns>
      /// Returns JSON string
      /// </returns>
       
       private async Task<string> getUsers()
        {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.MaxResponseContentBufferSize = 300000;
                var uri = new Uri(url + "command=getUsers" + "&"
                    + "email=" + mtb1.Text + "&"
                    + "password=" + s.Password);

                return await client.GetStringAsync(uri);
        }

        /// <summary>
        /// This hyperlink button will be used to navigate to pge where user can register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void HyperlinkButton_Click1(object sender, RoutedEventArgs e)
       {
           this.Frame.Navigate(typeof(Login));
     
       }

       private void mtb1_TextChanged(object sender, TextChangedEventArgs e)
       {

       }

        }
        
        }
           
        
      