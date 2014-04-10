using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace Splashscreen
{
    public partial class MainMenuTutorial : PhoneApplicationPage
    {
        public MainMenuTutorial()
        {
            InitializeComponent();

            MeStoryBoard.Begin();

            RotateStoryBoard.Begin();

            ArStoryBoard.Begin();

            //Create AR item online

            ArStoryBoard.Completed += new EventHandler(changepage);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                Application.Current.Terminate();
            }
        }

        private void changepage(Object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }

        private void skipIntro(Object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }
    }
}