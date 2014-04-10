using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Splashscreen.Resources;
using GART.BaseControls;
using GART;
using Location = System.Device.Location.GeoCoordinate;
using GART.Data;
using GART.Controls;
using Splashscreen.Model;
using Telerik.Everlive.Sdk.Core;
using Telerik.Windows.Cloud;
using System.Threading;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Device.Location;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Examples.WP.MessageBox;

namespace Splashscreen
{
    public partial class GartARView : PhoneApplicationPage
    {
        #region Member Variables
        private Random rand = new Random();
        public static bool updateLocationsOn = true;
        public static DispatcherTimer newTimer = new DispatcherTimer();
        public BitmapImage usersPicture;
        public static String currentUserSearch = "";
        public static int Count = 0;
        #endregion // Member Variables

        public GartARView()
        {
            InitializeComponent();
            // timer interval specified as 20  seconds
            newTimer.Interval = TimeSpan.FromSeconds(20);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += OnTimerTick;
        }

        void OnTimerTick(Object sender, EventArgs args)
        {
            // text box property is set to current system date.
            // ToString() converts the datetime value into text
            refreshUsers();
        }

        #region Internal Methods
        private void AddLabel(Location location, CustomUser user)
        {
            //getUserPic(user);
            String userPic = user.PictureFileUri;
            var bi = new BitmapImage(new Uri(userPic));

            // We'll use the specified text for the content and we'll let 
            // the system automatically project the item into world space
            // for us based on the Geo location.
            ARItem item = new UserThumbnail()
            {
                Name = user.DisplayName,
                GeoLocation = location,
                Distance = calculateDistance(location) + "m",
                Rating = user.Rating,   //Get users current rating here
                UserPic = bi,
            };

            ARDisplay.ARItems.Add(item);
        }

        private String calculateDistance(Location location)
        {
            var sCoord = location;
            var eCoord = ARDisplay.Location;

            double distance = sCoord.GetDistanceTo(eCoord);
            double roundedNum = Math.Round(distance);
            return roundedNum.ToString();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        public void AddNearbyUsers()
        {
            if (GlobalARPrep.usersList.Count == 0)
            {
                MessageBox.Show("You need to Add some Friends to find! Go to Menu -> Friends ",
                "Friends",
                 MessageBoxButton.OK);
            }
            else
                if (GlobalARPrep.usersList.Count > 1)
                {
                    newTimer.Interval = TimeSpan.FromSeconds(30);
                }
            textStatus.Text = "Mapping Users";
            newTimer.Start();
            // We'll add Labels
            //addLabels();

            /*while (updateLocationsOn)
            {
                MessageBox.Show("Helloooo",
                "Friends",
                 MessageBoxButton.OK);
                Thread.Sleep(TimeSpan.FromSeconds(10));
                MessageBox.Show("Helloooo",
                "Friends",
                 MessageBoxButton.OK);
                turnOffLoc();
            }*/

        }
        #endregion // Internal Methods

        public async void refreshUsers()
        {
            for (int j = 0; j < GlobalARPrep.usersList.Count; j++)
            {
                //Thread.Sleep(TimeSpan.FromSeconds(20));
                //refresh the list every few seconds
                Guid guid = new Guid(GlobalARPrep.usersList[j].GetId().ToString());
                EverliveApp everliveApp = CloudProvider.Current.NativeConnection as EverliveApp;
                CustomUser currentUser = await everliveApp.WorkWith().Data<CustomUser>().GetById(guid).ExecuteAsync();

                //Put the users back in the list they existed in
                GlobalARPrep.usersList.RemoveAt(j);
                GlobalARPrep.userNameList.RemoveAt(j);

                GlobalARPrep.userNameList.Insert(j, currentUser.DisplayName);
                GlobalARPrep.usersList.Insert(j, currentUser);

                if (Count == 0)
                {
                    if (j == (GlobalARPrep.usersList.Count - 1))
                    {
                        textStatus.Text = "Ready";
                        PIFadeOut.Begin();
                        Count++;
                    }
                }
            }
            ARDisplay.ARItems.Clear();
            addLabels();
        }

        public void addLabels()
        {
            // We'll add Labels
            for (int i = 0; i < GlobalARPrep.usersList.Count; i++)
            {
                CustomUser currentUser = GlobalARPrep.usersList[i];

                string locationStringAbout = currentUser.About;
                // Split string on commas. This will separate all the words in the string
                string[] words = locationStringAbout.Split(',');

                String longitudeUser = words[0];
                String latitudeUser = words[1];

                Location offset = new Location()
                {
                    Latitude = Convert.ToDouble(latitudeUser),
                    Longitude = Convert.ToDouble(longitudeUser),
                    Altitude = Double.NaN // NaN will keep it on the horizon
                };

                AddLabel(offset, currentUser);
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            Count--;
            // Stop AR services
            // Does this work, will rotate screen superceed this command, testing says it does
            newTimer.Stop();
            GlobalLocation.track15Seconds = false;
            GlobalLocation.track1Mins = true;
            ARDisplay.StopServices();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ARDisplay.StartServices();
            GlobalLocation.track1Mins = false;
            GlobalLocation.track15Seconds = true;

            if (e.NavigationMode == NavigationMode.Back)
            {
                Application.Current.Terminate();
            }

            ControlOrientation orientation = ControlOrientation.Default;

            if (this.Orientation.Equals(PageOrientation.LandscapeLeft))
            {
                orientation = ControlOrientation.Clockwise270Degrees;
            }
            else
                orientation = ControlOrientation.Clockwise90Degrees;

            ARDisplay.Orientation = orientation;

            base.OnNavigatedTo(e);

            AddNearbyUsers();
        }

        private void ClearLocationsMenu_Click(object sender, EventArgs e)
        {
            ARDisplay.ARItems.Clear();
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            ControlOrientation orientation = ControlOrientation.Default;

            switch (e.Orientation)
            {
                case PageOrientation.LandscapeLeft:
                    orientation = ControlOrientation.Clockwise270Degrees;
                    break;
                case PageOrientation.LandscapeRight:
                    orientation = ControlOrientation.Clockwise90Degrees;
                    break;
                case PageOrientation.PortraitUp:
                    NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
                    break;
            }

            ARDisplay.Orientation = orientation;
        }

        public bool findUserOnList(String userName)
        {
            if (userName == currentUserSearch)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void ParseButton_Click(object sender, RoutedEventArgs e)
        {
            {
                TextBlock userBlock = e.OriginalSource as TextBlock;
                currentUserSearch = userBlock.Text;

                int userlistLoc = GlobalARPrep.userNameList.FindIndex(findUserOnList);

                CustomUser currentUser = GlobalARPrep.usersList[userlistLoc];

                MessageBoxClosedEventArgs args = await RadMessageBox.ShowAsync(new CustomHeaderedContentControl[] 
            { 
                new CustomHeaderedContentControl() { Title = "Unfollow", Message = "Stop following the user " + currentUser.DisplayName },
                new CustomHeaderedContentControl() { Title = "Like", Message = "Give this user a like      " },
            },
                currentUser.DisplayName,
                "Select an option off the list below");

                CustomHeaderedContentControl option = (CustomHeaderedContentControl)args.ClickedButton.Content;
                if (option.Title == "Unfollow")
                {
                    textStatus.Text = "User Removed";
                    GlobalARPrep.usersList.RemoveAt(userlistLoc);
                    GlobalARPrep.userNameList.RemoveAt(userlistLoc);
                    ARDisplay.ARItems.RemoveAt(userlistLoc);
                }
                if (option.Title == "Like")
                {
                    textStatus.Text = "User Liked";

                    currentUser.Likes = currentUser.Likes + 1.0;
                    //Compute new rating
                    double total = currentUser.Likes + currentUser.Dislikes;
                    double sum = (currentUser.Likes + currentUser.Dislikes) / 2;
                    double totalpackage = sum / total;
                    double actualRating = 0;
                    if (currentUser.Dislikes.Equals(0.0))
                    {
                        actualRating = 5.0;
                    }
                    else
                    {
                        actualRating = 5 * totalpackage;
                    }

                    currentUser.Rating = actualRating;

                    bool x = await (CloudProvider.Current as ICloudProvider).UpdateExistingUserAsync(currentUser);
                }
            }
        }
    }
}