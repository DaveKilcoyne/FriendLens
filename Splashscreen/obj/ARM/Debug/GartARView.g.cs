﻿#pragma checksum "C:\Users\Dave\Documents\Visual Studio 2012\Projects\Splashscreen\Splashscreen\GartARView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "471ADA55BF3CD37466E95259FF57B0EA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GART.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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
    
    
    public partial class GartARView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard PIFadeOut;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem ClearLocationsMenu;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal GART.Controls.ARDisplay ARDisplay;
        
        internal GART.Controls.VideoPreview VideoPreview;
        
        internal GART.Controls.WorldView WorldView;
        
        internal System.Windows.Shapes.Rectangle rectangle;
        
        internal System.Windows.Controls.TextBlock textStatus;
        
        internal System.Windows.Controls.ProgressBar progressBar;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Splashscreen;component/GartARView.xaml", System.UriKind.Relative));
            this.PIFadeOut = ((System.Windows.Media.Animation.Storyboard)(this.FindName("PIFadeOut")));
            this.ClearLocationsMenu = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("ClearLocationsMenu")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ARDisplay = ((GART.Controls.ARDisplay)(this.FindName("ARDisplay")));
            this.VideoPreview = ((GART.Controls.VideoPreview)(this.FindName("VideoPreview")));
            this.WorldView = ((GART.Controls.WorldView)(this.FindName("WorldView")));
            this.rectangle = ((System.Windows.Shapes.Rectangle)(this.FindName("rectangle")));
            this.textStatus = ((System.Windows.Controls.TextBlock)(this.FindName("textStatus")));
            this.progressBar = ((System.Windows.Controls.ProgressBar)(this.FindName("progressBar")));
        }
    }
}

