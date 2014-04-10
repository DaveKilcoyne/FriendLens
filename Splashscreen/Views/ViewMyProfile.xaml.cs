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
using Telerik.Everlive.Sdk.Core;

namespace Splashscreen.Views
{
    public partial class ViewMyProfile : PhoneApplicationPage
    {
        public ViewMyProfile()
        {
            InitializeComponent();
        }
        protected override async void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (GlobalLocation.longitude != "" && GlobalLocation.latitude != "")
            {
                syncstory.Begin();
            }

            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.New)
            {
                Guid guid = new Guid(CloudProvider.Current.CurrentUser.GetId().ToString());
                EverliveApp everliveApp = CloudProvider.Current.NativeConnection as EverliveApp;
                this.currentUser = await everliveApp.WorkWith().Data<CustomUser>().GetById(guid).ExecuteAsync();
                this.DataContext = this.currentUser;
            }
        }

        #region Private Fields and Constants

        private CustomUser currentUser;

        #endregion
    }
}