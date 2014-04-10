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
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
            GlobalLocation.getLocation();
            this.Loaded += (x, y) =>
            {
                SystemTray.IsVisible = true;
                SystemTray.Opacity = 0.0;
            
            };        
        }

        private async void OnLogin_Failed(object sender, LoginFailedEventArgs e)
        {
            await RadMessageBox.ShowAsync("Login failed.");
        }
    }
}