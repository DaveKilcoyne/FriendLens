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

namespace Splashscreen.Views
{
    public partial class Intro : PhoneApplicationPage
    {
        public Intro()
        {
            InitializeComponent();

            QuickStoryBoard.Begin();

            TutorialStoryBoard.Begin();

            TutorialStoryBoard.Completed += new EventHandler(changepage);
        }

        private void changepage(Object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenuTutorial.xaml", UriKind.Relative));
        }
    }
}