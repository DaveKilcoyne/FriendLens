﻿#pragma checksum "C:\Users\Dave\Documents\Visual Studio 2012\Projects\Splashscreen\Splashscreen\Views\Me.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "36365BA7FA5984E76184C663BA6C1C53"
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


namespace Splashscreen.Views {
    
    
    public partial class MeTile : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton RotateButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem ClearLocationsMenu;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock LatitudeTextBlock;
        
        internal System.Windows.Controls.TextBlock LongitudeTextBlock;
        
        internal GART.Controls.ARDisplay ARDisplay;
        
        internal GART.Controls.OverheadMap OverheadMap;
        
        internal GART.Controls.HeadingIndicator HeadingIndicator;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Splashscreen;component/Views/Me.xaml", System.UriKind.Relative));
            this.RotateButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("RotateButton")));
            this.ClearLocationsMenu = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("ClearLocationsMenu")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.LatitudeTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("LatitudeTextBlock")));
            this.LongitudeTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("LongitudeTextBlock")));
            this.ARDisplay = ((GART.Controls.ARDisplay)(this.FindName("ARDisplay")));
            this.OverheadMap = ((GART.Controls.OverheadMap)(this.FindName("OverheadMap")));
            this.HeadingIndicator = ((GART.Controls.HeadingIndicator)(this.FindName("HeadingIndicator")));
        }
    }
}

