﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "56C711FE8AC7FB5C3240C21C25855705"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PictionaryClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PictionaryClient {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image redMarker;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image orangeMarker;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image yellowMarker;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image greenMarker;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image blueMarker;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image purpleMarker;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image brownMarker;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image greyMarker;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image blackMarker;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock hint;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider thicknessSlider;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas whiteBoard;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image eraser;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PictionaryClient;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.redMarker = ((System.Windows.Controls.Image)(target));
            
            #line 16 "..\..\MainWindow.xaml"
            this.redMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.redMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.orangeMarker = ((System.Windows.Controls.Image)(target));
            
            #line 17 "..\..\MainWindow.xaml"
            this.orangeMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.orangeMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.yellowMarker = ((System.Windows.Controls.Image)(target));
            
            #line 18 "..\..\MainWindow.xaml"
            this.yellowMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.yellowMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.greenMarker = ((System.Windows.Controls.Image)(target));
            
            #line 19 "..\..\MainWindow.xaml"
            this.greenMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.greenMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.blueMarker = ((System.Windows.Controls.Image)(target));
            
            #line 20 "..\..\MainWindow.xaml"
            this.blueMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.blueMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.purpleMarker = ((System.Windows.Controls.Image)(target));
            
            #line 21 "..\..\MainWindow.xaml"
            this.purpleMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.purpleMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.brownMarker = ((System.Windows.Controls.Image)(target));
            
            #line 22 "..\..\MainWindow.xaml"
            this.brownMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.brownMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.greyMarker = ((System.Windows.Controls.Image)(target));
            
            #line 23 "..\..\MainWindow.xaml"
            this.greyMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.greyMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.blackMarker = ((System.Windows.Controls.Image)(target));
            
            #line 24 "..\..\MainWindow.xaml"
            this.blackMarker.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.blackMarker_MouseDown);
            
            #line default
            #line hidden
            return;
            case 10:
            this.hint = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.thicknessSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 12:
            this.whiteBoard = ((System.Windows.Controls.Canvas)(target));
            
            #line 38 "..\..\MainWindow.xaml"
            this.whiteBoard.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.whiteBoard_mouseDown);
            
            #line default
            #line hidden
            
            #line 38 "..\..\MainWindow.xaml"
            this.whiteBoard.MouseMove += new System.Windows.Input.MouseEventHandler(this.Canvas_MouseMove_1);
            
            #line default
            #line hidden
            return;
            case 13:
            this.eraser = ((System.Windows.Controls.Image)(target));
            
            #line 40 "..\..\MainWindow.xaml"
            this.eraser.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.eraser_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
