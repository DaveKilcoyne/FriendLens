using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Splashscreen.Model;
using Telerik.Windows.Controls.Cloud.Sample.Helpers;
using Telerik.Windows.Cloud;
using System.Device.Location;

namespace Splashscreen.Views
{
    public partial class ViewProfilePage : PhoneApplicationPage
    {

        public ViewProfilePage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.New)
            {
                var user = (CustomUser)this.NavigationContext.GetData();
                if (user != null)
                {
                    this.DataContext = user;
                    this.currentUser = user;
                }
                else
                {
                    this.DataContext = CloudProvider.Current.CurrentUser;
                    this.currentUser = CloudProvider.Current.CurrentUser as CustomUser;
                }
                setToggleButton();
            }
        }

        private void setToggleButton() 
        {
            if (GlobalARPrep.userNameList.Contains(currentUser.DisplayName))
            {
                this.toggle.IsChecked = true;
                this.toggle.Unchecked += new EventHandler<RoutedEventArgs>(toggle_Unchecked);
            }
            else
            {
                this.toggle.IsChecked = false;
                this.toggle.Checked += new EventHandler<RoutedEventArgs>(toggle_Checked);
            }
        }

        void toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.toggle.Content = "Off";
            //GlobalARPrep.userNameList.RemoveAt(GlobalARPrep.iterator);
            //GlobalARPrep.usersList.RemoveAt(GlobalARPrep.iterator);
            //GlobalARPrep.iterator--;
            int userlistLoc = GlobalARPrep.usersList.FindIndex(findUserOnList);
            GlobalARPrep.usersList.RemoveAt(userlistLoc);
            GlobalARPrep.userNameList.RemoveAt(userlistLoc);
            if (MainMenu.globalTileCount != 0)
            {
                MainMenu.globalTileCount--;
            }
        }

        private async void dislikeUser(object sender, RoutedEventArgs e)
        {
            currentUser.Dislikes = currentUser.Dislikes + 1.0;
            //Compute new rating
            double total = currentUser.Likes + currentUser.Dislikes;
            double sum = (currentUser.Likes + currentUser.Dislikes)/2;
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

            MessageBox.Show("User Disliked",
            currentUser.DisplayName,
            MessageBoxButton.OK);
        }

        private async void likeUser(object sender, RoutedEventArgs e)
        {
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

            MessageBox.Show("User Liked",
                currentUser.DisplayName,
                MessageBoxButton.OK);
        }

        public bool findUserOnList(CustomUser user)
        {
            if (user.DisplayName == currentUser.DisplayName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void toggle_Checked(object sender, RoutedEventArgs e)
        {
            //GlobalARPrep.iterator++;
            this.toggle.Content = "On";
            GlobalARPrep.userNameList.Add(currentUser.DisplayName);
            GlobalARPrep.usersList.Add(currentUser);

            getCloseUser();
        }


        private async void getCloseUser()
        {
            if (GlobalLocation.longitude != "" && GlobalLocation.latitude != "")
            {
                String longitudeMe = GlobalLocation.longitude;
                String latitudeMe = GlobalLocation.latitude;

                GeoCoordinate mylocation = new GeoCoordinate()
                {
                    Latitude = Convert.ToDouble(latitudeMe),
                    Longitude = Convert.ToDouble(longitudeMe),
                    //Altitude = Double.NaN // NaN will keep it on the horizon
                };

                for (int k = 0; k <= GlobalARPrep.usersList.Count - 1; k++)
                {
                    String about = currentUser.About;

                    string[] words = about.Split(',');

                    String longitudeOtherUser = words[0];
                    String latitudeOtherUser = words[1];

                    GeoCoordinate youlocation = new GeoCoordinate()
                    {
                        Latitude = Convert.ToDouble(latitudeOtherUser),
                        Longitude = Convert.ToDouble(longitudeOtherUser),
                    };


                    double dist = calculateDistance(mylocation, youlocation);

                    if (dist < 50.00)
                    {
                        MainMenu.globalTileCount++;
                    }

                }
            }
        }

        

        private Double calculateDistance(GeoCoordinate myloc, GeoCoordinate youloc)
        {
            var scoor = myloc;
            var ncoor = youloc;

            double distance = myloc.GetDistanceTo(youloc);
            double roundedNum = Math.Round(distance);
            return distance;
        }



        private void EditButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate("/Views/CreateAccountPage.xaml", this.currentUser);
        }

        #region Private Fields and Constants

        private CustomUser currentUser;

        #endregion
    }
}