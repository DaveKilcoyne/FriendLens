using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls.Cloud.Sample.Helpers;
using Telerik.Windows.Cloud;
using Splashscreen.Model;

namespace Splashscreen.Views
{
    public partial class FriendsPage : PhoneApplicationPage
    {
        public FriendsPage()
        {
            InitializeComponent();
            this.friends.CloudDataService = new EverliveCloudDataService<CustomUser>();

            this.Loaded += (x, y) =>
            {
                SystemTray.IsVisible = true;
                SystemTray.Opacity = 0.0;
            };      
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var user = (CustomUser)((FrameworkElement)e.OriginalSource).DataContext;
            NavigationService.Navigate("/Views/ViewProfilePage.xaml", user);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            friends.CloudDataService.GetItemsAsync();
        }
    }
}