///<summary>
///@author Sagar Barbhaya
///@version:1.0
///@revision:1.0
///File:Model.cs 
///This file will give you 
///</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace App1
{
    public class ModelView
    {
       
        string url = "http://www.cs.rit.edu/~jsb/2135/ProgSkills/Labs/Messenger/api.php?";
        int n=0;
        string c = "";

        
        public async Task<string> getModel(int t)
        {
            
            if(t==0)
            {
                c = "getUsers";
             }
            else if(t==1)
            {
                c = "getMessages";
            }
            else if(t==2)
            {
                c = "getLocations";
            }
            
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.MaxResponseContentBufferSize = 3000000;
            var uri = new Uri(url + "command=" + c + "&"
                + "email=" + ApplicationData.Current.LocalSettings.Values["email"].ToString() + "&"
                + "password=" + ApplicationData.Current.LocalSettings.Values["password"].ToString());

           return await client.GetStringAsync(uri);


        }

                       
            


    }
}
