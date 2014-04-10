using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using Splashscreen.Resources;
using GART.BaseControls;
using GART;
using Location = System.Device.Location.GeoCoordinate;
using GART.Data;
using GART.Controls;
using Splashscreen.Model;
using System.Windows.Media.Imaging;

namespace Splashscreen.Views
{
    public partial class MeTile : PhoneApplicationPage
    {
        Geolocator geolocator = null;
        bool tracking = false;
        public ProgressIndicator pi = SystemTray.ProgressIndicator;

        public MeTile()
        {
            InitializeComponent();
            BackKeyPress += PageBackKeyPress;
            
            this.Loaded += (x, y) =>
            {
                SystemTray.IsVisible = true;
                if (pi == null)
                {
                    pi = new ProgressIndicator();
                    SystemTray.SetProgressIndicator(this, pi);
                    SystemTray.ProgressIndicator.IsIndeterminate = true;
                    SystemTray.Opacity = 0.0;
                    SystemTray.ProgressIndicator.IsVisible = true;
                    SystemTray.ProgressIndicator.Text = "Initializing";
                }
            };        
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (GlobalARPrep.usersList.Count == 0)
            {
                //Lets track the friends (Not in this page) (when they are added to the list we have to update the tile/toast/livetile) and we'll add them to the background process to alert the user if they are near and update the live tile/in app tile
                //Box with friends page link
                MessageBox.Show("You need to Add some Friends to find! Go to Menu -> Friends ",
                "Friends",
                 MessageBoxButton.OK);
            }
            else
            //Start AR services and set the map to Rotate the map relative to users direction
            ARDisplay.StartServices();
            ARDisplay.ARItems.Clear();
            addLabels();
            HeadingIndicator.RotationSource = RotationSource.North;
            OverheadMap.RotationSource = RotationSource.AttitudeHeading;
            TrackLocation();
        }

        void PageBackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ARDisplay.ARItems.Clear();
            ARDisplay.StopServices();
            tracking = false;
            geolocator = null;
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.RelativeOrAbsolute));
        }

        private void TrackLocation()
        {
            if ((bool)IsolatedStorageSettings.ApplicationSettings["LocationConsent"] != true)
            {
                // The user has opted out of Location.
                return;
            }

            if (!tracking)
            {
                geolocator = new Geolocator();
                geolocator.DesiredAccuracy = PositionAccuracy.High;
                geolocator.MovementThreshold = 1; // The units are meters.

                geolocator.StatusChanged += geolocator_StatusChanged;
                geolocator.PositionChanged += geolocator_PositionChanged;

                tracking = true;
            }
            else
            {
                geolocator.PositionChanged -= geolocator_PositionChanged;
                geolocator.StatusChanged -= geolocator_StatusChanged;
                geolocator = null;

                tracking = false;
            }
        }

        void geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            string status = "";

                if (pi == null)
                {
                    pi = new ProgressIndicator();
                    SystemTray.SetProgressIndicator(this, pi);
                    SystemTray.ProgressIndicator.IsIndeterminate = true;
                    SystemTray.Opacity = 0.0;
                    SystemTray.ProgressIndicator.IsVisible = false;
                }

            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    // the application does not have the right capability or the location master switch is off
                    status = "Location is disabled in phone settings";
                    break;
                case PositionStatus.Initializing:
                    // the geolocator started the tracking operation
                    status = "Initializing";
                    break;
                case PositionStatus.NoData:
                    // the location service was not able to acquire the location
                    status = "No data";
                    break;
                case PositionStatus.Ready:
                    // the location service is generating geopositions as specified by the tracking parameters
                    status = "Ready";
                    break;
                case PositionStatus.NotAvailable:
                    status = "Not available";
                    // not used in WindowsPhone, Windows desktop uses this value to signal that there is no hardware capable to acquire location information
                    break;
                case PositionStatus.NotInitialized:
                    // the initial state of the geolocator, once the tracking operation is stopped by the user the geolocator moves back to this state
                    break;
            }

            Dispatcher.BeginInvoke(() =>
            {
                if (status.Equals("Ready"))
                {
                    pi.Text = status;
                    pi.IsVisible = false;
                    ARDisplay.StopServices();
                    ARDisplay.StartServices(); //Refresh
                }
                else
                    pi.IsVisible = true;
                    pi.Text = status;     
            });
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
                    //Altitude = Double.NaN // NaN will keep it on the horizon
                };

                AddLabel(offset, currentUser);

            }
        }

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
                Rating = user.Rating,   //Get users current rating here
                UserPic = bi,
                RadButton = user.DisplayName,
            };

            ARDisplay.ARItems.Add(item);
        }

        void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Dispatcher.BeginInvoke(() =>
            {
                LatitudeTextBlock.Text = args.Position.Coordinate.Latitude.ToString("0.0000000000");
                LongitudeTextBlock.Text = args.Position.Coordinate.Longitude.ToString("0.0000000000");
            });
        }

        private void ClearLocationsMenu_Click(object sender, EventArgs e)
        {
            ARDisplay.ARItems.Clear();
        }

        private void RefreshButton_Click(object sender, System.EventArgs e)
        {
            ARDisplay.StopServices();
            ARDisplay.ARItems.Clear();
            ARDisplay.StartServices(); //Refresh
            addLabels();
        }

    }
}