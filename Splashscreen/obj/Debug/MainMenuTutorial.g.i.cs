﻿#pragma checksum "C:\Users\Dave\Documents\Visual Studio 2012\Projects\Splashscreen\Splashscreen\MainMenuTutorial.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FF440A308440A41EFB6E80BEAE73F704"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Splashscreen {
    
    
    public partial class MainMenuTutorial : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Media.Animation.Storyboard MeStoryBoard;
        
        internal System.Windows.Controls.TextBlock Me;
        
        internal System.Windows.Media.Animation.Storyboard RotateStoryBoard;
        
        internal System.Windows.Controls.TextBlock Rotate;
        
        internal System.Windows.Media.Animation.Storyboard ArStoryBoard;
        
        internal System.Windows.Controls.TextBlock Ar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Splashscreen;component/MainMenuTutorial.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.MeStoryBoard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("MeStoryBoard")));
            this.Me = ((System.Windows.Controls.TextBlock)(this.FindName("Me")));
            this.RotateStoryBoard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("RotateStoryBoard")));
            this.Rotate = ((System.Windows.Controls.TextBlock)(this.FindName("Rotate")));
            this.ArStoryBoard = ((System.Windows.Media.Animation.Storyboard)(this.FindName("ArStoryBoard")));
            this.Ar = ((System.Windows.Controls.TextBlock)(this.FindName("Ar")));
        }
    }
}

