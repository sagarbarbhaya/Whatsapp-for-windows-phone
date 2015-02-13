///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:Login.xaml.cs
/// Description: It will help user to register to use this app
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
using System.Windows;
using Windows.UI;
using System.Windows.Input;
using Windows.UI.Popups;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        String url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";

        public Login()
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

        
        /// <summary>
        /// This fucntion will be used to validate the user details which is entered during registration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (tb1.Text.Trim().Length == 0)
            {
                var dialog = new MessageDialog("Please Enter the Email-ID");
                dialog.ShowAsync();

            }
            if (tb2.Password.Trim().Length == 0)
            {
                var dialog1 = new MessageDialog("Please Enter the password");
                dialog1.ShowAsync();
                
            }
            if (tb3.Text.Trim().Length == 0)
            {
                var dialog2 = new MessageDialog("Please Enter the First Name");
                dialog2.ShowAsync();
            }
            if (tb4.Text.Trim().Length == 0)
            {
                var dialog3 = new MessageDialog("Please Enter the Last Name");
                dialog3.ShowAsync();

            }
            if (tb1.Text.Trim().Length != 0 && tb2.Password.Trim().Length != 0 && tb3.Text.Trim().Length != 0 && tb4.Text.Trim().Length != 0)
            {
                createAccount();
                var dialog4 = new MessageDialog("Account Created Successfully! Now it will redirect to Main Page");
                dialog4.ShowAsync();
                this.Frame.Navigate(typeof(MainPage));
            }
        }
        /// <summary>
        /// This function interacts with server to create an account for new user
        /// </summary>
        private async void createAccount()
        {
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 3000000;
            var uri = new Uri(url + "command=createAccount" + "&"
                + "email="+tb1.Text + "&" + "password="+tb2.Password + "&"
                + "first_name="+tb3.Text + "&" + "last_name="+tb4.Text);
          String message = await client.GetStringAsync(uri);
              
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
      
       
    }
}
