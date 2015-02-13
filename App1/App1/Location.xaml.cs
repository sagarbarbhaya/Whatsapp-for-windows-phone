///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:Location.xaml.cs
///Description: This page will help to access the users Location from user
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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Device.Location;
using Windows.UI.Popups;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Location : Page
    {
        string url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";
        string location = "";
        string latitude1 = "";
        string longitude1 = "";
        string accuracy = "";
        
        string locationss = "";
        double DLatitude;
        double DLongtitude;
     

        private Windows.UI.Xaml.Shapes.Rectangle fence;
        private BasicGeoposition fencePos;
        private MapControl mapy;
  
        
        
        public Location()
        {
            this.InitializeComponent();
            updateLocation();
        }
       

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            updateLocation();
        }
        /// <summary>
        /// This function is used to update a location of the users 
        /// In this function JSON string is parsed using JARRAY
        /// </summary>
        private async void updateLocation()
        {
            location = await m.getModel(2);
            var obj = JArray.Parse(location);

            foreach (JObject item in obj)
            {

                foreach (KeyValuePair<String, JToken> app in item)
                {
                    if (app.Key == "latitude")
                    {
                        latitude1 = (String)app.Value.ToString();
                    }
                    if (app.Key == "longitude")
                    {
                        longitude1 = (String)app.Value.ToString();
                    }
                    if (app.Key == "accuracy")
                    {
                       accuracy = (String)app.Value.ToString();
                    }
                }

                DLatitude = Convert.ToDouble(latitude1);
                DLongtitude = Convert.ToDouble(longitude1);
               
            }
         
        }
     
        ModelView m = new ModelView();
        /// <summary>
        /// This function is executed when the map is loaded on windows phone
        /// This function is taking latitude and lonitude of all the users during runtime.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            
            mapy = (MapControl)sender;
            mapy.Center = new Geopoint(new BasicGeoposition() { Altitude = 643, Latitude = 43.089863, Longitude = -77.669609 });
            mapy.ZoomLevel = 14;
            locationss = await m.getModel(2);
            var objects = JArray.Parse(locationss);
            Geopoint point;
            BitmapImage img;
           
            foreach(JObject root in objects)
            {
                if ((root["email"].ToString() != "isb4190@rit.edu"))
                {
                  fencePos=new BasicGeoposition() { Latitude=Convert.ToDouble(root["latitude"].ToString()),Longitude=Convert.ToDouble(root["longitude"].ToString())};

                  point = new Geopoint(fencePos);
            fence = new Rectangle();
            fence.Width = 30;
            fence.Height = 30;
            img = new BitmapImage(new Uri("ms-appx:///Assets/redpin.png"));
            fence.Fill = new ImageBrush() { ImageSource = img };
            MapControl.SetLocation(fence, point);
            MapControl.SetNormalizedAnchorPoint(fence, new Point(1.0, 0.5));
            mapy.Children.Add(fence);
            fence.Tapped += (o, args) =>
            {
               
                MessageDialog myDialog = new MessageDialog(root["email"].ToString() +"\n"+ root["first_name"].ToString() +"\n" +root["last_name"].ToString());
                myDialog.ShowAsync();
                var trying = root["email"].ToString();
                ApplicationData.Current.LocalSettings.Values["lmail"] = trying;
                this.Frame.Navigate(typeof(Location_messaging));
            };
                }
                 
            }
            
        }
    }
}
