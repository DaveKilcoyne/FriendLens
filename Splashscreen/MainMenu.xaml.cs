using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Data;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Windows.Input;
using Telerik.QSF.WP;
using System.IO.IsolatedStorage;
using Telerik.Windows.Controls.Cloud.Sample.Helpers;
using Telerik.Windows.Cloud;
using Telerik.Windows.Controls.Cloud;
using GART.BaseControls;
using Splashscreen.Model;
using Telerik.Everlive.Sdk.Core;
using System.Threading;
using System.IO;
using System.Text;
using System.Windows.Threading;
using System.Device.Location;

namespace Splashscreen
{
    public partial class MainMenu : PhoneApplicationPage
    {
        private CustomUser user;
        public static DispatcherTimer getLocTimer = new DispatcherTimer();
        public static int currentCount = 0;
        public static int previousCount = 0;
        public static int globalTileCount = 0;

        public MainMenu()
        {
            InitializeComponent();
            //tile.Message = "Who's around";
            // timer interval specified as 10 seconds
            getLocTimer.Interval = TimeSpan.FromSeconds(10);
            // Sub-routine OnTimerTick will be called at every 10 second
            getLocTimer.Tick += OnTimerTick;
            //ThreadPool.QueueUserWorkItem(new WaitCallback(callTimer));  
        }

        private async void getMe() 
        {
            Guid guid = new Guid(CloudProvider.Current.CurrentUser.GetId().ToString());
            EverliveApp everliveApp = CloudProvider.Current.NativeConnection as EverliveApp;
            user = await everliveApp.WorkWith().Data<CustomUser>().GetById(guid).ExecuteAsync();
        }

        void OnTimerTick(Object sender, EventArgs args)
        {
            GlobalLocation.trackLocation();
            //MessageBox.Show("tick ",
                            //"tick",
                            //MessageBoxButton.OK);
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GartARView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void addMosaicImages()
        {
            if (GlobalARPrep.usersList.Count != 0)
            {

            }
        }

        async void getmyPicture() 
        {
            Guid guid = new Guid(CloudProvider.Current.CurrentUser.GetId().ToString());
            EverliveApp everliveApp = CloudProvider.Current.NativeConnection as EverliveApp;
            user = await everliveApp.WorkWith().Data<CustomUser>().GetById(guid).ExecuteAsync();

            String userPic = user.PictureFileUri;
            var bi = new BitmapImage(new Uri(userPic));
            ProfileImage.Source = bi;
            //BitmapImage image = new BitmapImage();
            //image.SetSource(pictureStream);
            //ProfileImage.Source = image;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //getLocTimer.Stop();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            getmyPicture();
            addMosaicImages();
            tile.Count = globalTileCount;

            if (IsolatedStorageSettings.ApplicationSettings.Contains("Intro"))
            {
                if (IsolatedStorageSettings.ApplicationSettings["Intro"].Equals(true))
                {
                    checkUserLocPermission();
                }
            }
            else
            {
                IsolatedStorageSettings.ApplicationSettings["Intro"] = true;
                IsolatedStorageSettings.ApplicationSettings.Save();
                playIntro();
            }

        }

        private void checkUserLocPermission() 
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains("LocationConsent"))
            {
                if (IsolatedStorageSettings.ApplicationSettings["LocationConsent"].Equals(false))
                {
                    request();
                }
                else if (IsolatedStorageSettings.ApplicationSettings["LocationConsent"].Equals(true))
                {
                    updateLocation();
                }
            }
            else
            {
                request();

                if (IsolatedStorageSettings.ApplicationSettings["LocationConsent"].Equals(true))
                {
                    updateLocation();
                }
            }
        }

        void request()
        {
            MessageBoxResult result =
                    MessageBox.Show("This app accesses your phone's location. Is that ok?",
                    "Location",
                    MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = true;
            }
            else
            {
                MessageBox.Show("This app requires access to your location for function.",
                "Location",
                MessageBoxButton.OK);
                IsolatedStorageSettings.ApplicationSettings["LocationConsent"] = false;
                NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
            }
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void updateLocation() 
        {
            GlobalLocation.trackLocation();
            getLocTimer.Start();
        }

        private void playIntro() 
        {
            NavigationService.Navigate(new Uri("/Views/Intro.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RunMeTransition(object sender, MouseButtonEventArgs e)
        {
            var user = 0;
            NavigationService.Navigate("/Views/ViewMyProfile.xaml", user);
            //Guid itemId = (CloudProvider.Current.CurrentUser.GetId());
            //this.NavigationService.Navigate(new Uri(string.Format("/Views/ViewMemory.xaml?id={0}", itemId), UriKind.RelativeOrAbsolute));
            //NavigationService.Navigate("/Views/ViewProfilePage.xaml", UriKind.RelativeOrAbsolute);
            //Guid itemId = (CloudProvider.Current.CurrentUser as CustomUser).Id;
            //this.NavigationService.Navigate(new Uri(string.Format("/Views/ViewMemory.xaml?id={0}", itemId), UriKind.RelativeOrAbsolute));
        }

        private void LoadFriends(object sender, MouseButtonEventArgs e)
        {
            //this.SetValue(RadTileAnimation.ContainerToAnimateProperty, this.Panorama);
            NavigationService.Navigate(new Uri("/Views/FriendsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void navAbout(object sender, MouseButtonEventArgs e)
        {
            //this.SetValue(RadTileAnimation.ContainerToAnimateProperty, this.Panorama);
            NavigationService.Navigate(new Uri("/about.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void OnLogout_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i <= GlobalARPrep.usersList.Count; i++)
            {
                //MessageBox.Show("Location stats " + GlobalLocation.longitude + " " + GlobalLocation.latitude,
                //"Name",
                //MessageBoxButton.OK);

                MessageBox.Show("Users " + GlobalARPrep.userNameList[i],
                "Name",
                MessageBoxButton.OK);
            }*/

            //MessageBox.Show("User in 0 position " + GlobalARPrep.userNameList[0],
            //"Name",
            //MessageBoxButton.OK);

            //MessageBox.Show("Hello your location now is long: " + GlobalLocation.Longitude + " and lat: " + GlobalLocation.Latitude,
            //"Hello",
            //MessageBoxButton.OK);

            //MessageBox.Show("count " + count,
            //"Hello",
            //MessageBoxButton.OK);

            bool logoutSuccess = await RadCloudLogin.LogoutAsync();
            if (logoutSuccess)
            {
                this.NavigationService.GoBack();
            }
        }

        private void RunContactsTransition(object sender, MouseButtonEventArgs e)
        {
            this.SetValue(RadTileAnimation.ContainerToAnimateProperty, this.Panorama);
            NavigationService.Navigate(new Uri("/Views/Me.xaml", UriKind.RelativeOrAbsolute));
        }

    }


}