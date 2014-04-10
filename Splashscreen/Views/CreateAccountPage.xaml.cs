using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls.DataForm;
using Telerik.Windows.Cloud;
using System.Threading.Tasks;
using Splashscreen.UserControls;
using Telerik.Windows.Controls;
using Splashscreen.Model;
using Telerik.Everlive.Sdk.Core.Model.System;

namespace Splashscreen.Views
{
    public partial class CreateAccountPage : PhoneApplicationPage
    {
        public CreateAccountPage()
        {
            InitializeComponent();

            this.Loaded += (x, y) =>
            {
                SystemTray.IsVisible = true;
                SystemTray.Opacity = 0.0;
            };      
        }

        /// <summary>
        /// Called when a page becomes the active page in a frame.
        /// </summary>
        /// <param name="e">An object that contains the event data.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Refresh || e.NavigationMode ==
                NavigationMode.Back)
                return;

                this.PageTitle.Text = "registration";
                CustomUser newUser = new CustomUser();
                //newUser.About = GlobalLocation.longitude + ", " + GlobalLocation.latitude; //Get user location
                this.urControl.CurrentItem = newUser;
        }

        private async void commitButton_Click(object sender, EventArgs e)
        {
            await this.urControl.CommitAsync();
        }

        private void OnSuccess()
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void OnFailed()
        {
            System.Windows.MessageBox.Show("Could not create profile.");

            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void urControl_Success(object sender, EventArgs e)
        {
            this.OnSuccess();
        }

        private void urControl_Failed(object sender, EventArgs e)
        {
            this.OnFailed();
        }

        private async Task urControl_CommittingDataFieldAsync(object sender, CommittingDataFieldEventArgs e)
        {
            if (e.TargetField.PropertyKey == "PictureFileId")
            {
                CustomDataField field = e.TargetField as CustomDataField;
                PictureSelector pSelector = field.EditorElement as PictureSelector;
                if (pSelector.ChoosenPhotoStream != null)
                {
                    pSelector.ChoosenPhotoStream.Position = 0;
                    try
                    {
                        Guid id = await (CloudProvider.Current as ICloudProvider).UploadFileAsync(pSelector.FileName, pSelector.ChoosenPhotoStream);
                        e.Value = id;
                    }
                    catch (Exception ex)
                    {
                        e.Cancel = true;
                        System.Windows.MessageBox.Show("Could not upload your profile picture. Error: " + ex.Message);
                    }
                }
            }
            if (e.TargetField.PropertyKey == "BirthDate")
            {
                DateTime value = (DateTime)e.Value;

                e.Value = new DateTime(value.Ticks, DateTimeKind.Utc);
            }
        }

        private void urControl_DataFieldCreating(object sender, DataFieldCreatingEventArgs e)
        {
            if (CloudProvider.Current.IsLoggedIn)
            {
                if (e.PropertyName == "Password")
                {
                    e.Cancel = true;
                }
            }
        }
    }
}