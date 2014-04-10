using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;

namespace Splashscreen
{
    public partial class PleaseWaitControl : PhoneApplicationPage
    {
        public PleaseWaitControl()
        {
            InitializeComponent();
   
            PleaseStoryboard.Begin();

            WaitStoryboard.Begin();

            DotOneStoryBoard.Begin();

            DotTwoStoryBoard.Begin();

            DotThreeStoryBoard.Begin();

            DotThreeStoryBoard.Completed += new EventHandler(changepage);
        }

        private void changepage (Object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
        }
    }
}