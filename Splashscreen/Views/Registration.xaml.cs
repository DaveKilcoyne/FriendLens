using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls.Cloud;
using Telerik.Windows.Controls;

namespace Splashscreen.Views
{
    public partial class Registration : PhoneApplicationPage
    {
        public Registration()
        {
            InitializeComponent();
            this.Loaded += (x, y) =>
            {
                SystemTray.IsVisible = true;
                SystemTray.Opacity = 0.0;
            };      
        }

        private void OnRegistrationSuccess(object sender, EventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private async void OnRegistrationFailed(object sender, RegistrationFailedEventArgs e)
        {
            await RadMessageBox.ShowAsync("Registration failed.");
        }
    }
}