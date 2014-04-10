using Splashscreen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Everlive.Sdk.Core;
using Telerik.Windows.Cloud;
using Windows.Devices.Geolocation;

namespace Splashscreen
{
    class GlobalLocation
    {
        public static String longitude = "";
        public static String latitude = "";
        public static bool track1Mins = false;
        public static bool track15Seconds = true;
        public static Geolocator geolocator = new Geolocator();
        public static bool monitor = false;
        public static int j = 0;

        public static async void getLocation()
        {
            geolocator.DesiredAccuracyInMeters = 1;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                maximumAge: TimeSpan.FromMinutes(5),
                timeout: TimeSpan.FromSeconds(10)
                );

                latitude = geoposition.Coordinate.Latitude.ToString("0.0000000000");
                longitude = geoposition.Coordinate.Longitude.ToString("0.0000000000");
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    MessageBox.Show("location is disabled in phone settings, please enable and restart the app",
                    "Location",
                    MessageBoxButton.OK);
                }
            }
        }

        public static async void trackLocation()
        {
            //bool locationOn = true;
            Guid guid = new Guid(CloudProvider.Current.CurrentUser.GetId().ToString());
            EverliveApp everliveApp = CloudProvider.Current.NativeConnection as EverliveApp;
            CustomUser user = await everliveApp.WorkWith().Data<CustomUser>().GetById(guid).ExecuteAsync();

            //while (locationOn)
            //{
                if (track1Mins)
                {
                    MainMenu.getLocTimer.Interval = TimeSpan.FromMinutes(1);
                    getLocation();
                    user.About = longitude + ", " + latitude;
                    j++;
                    if (user.About.Equals(", "))
                    {
                        
                    }
                    else
                    {
                        await (CloudProvider.Current as ICloudProvider).UpdateExistingUserAsync(user);
                    }
                }
                if (track15Seconds)
                {
                    MainMenu.getLocTimer.Interval = TimeSpan.FromSeconds(15);
                    monitor = true;
                    getLocation();
                    user.About = longitude + ", " + latitude;
                    j++;
                    if (user.About.Equals(", ")) 
                    {
                        
                    }
                    else
                    {
                        await (CloudProvider.Current as ICloudProvider).UpdateExistingUserAsync(user);
                    }
                }
            //}
        }


    }
}
