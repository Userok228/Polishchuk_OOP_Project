﻿#pragma checksum "..\..\..\..\Pages\ProjectSummaryPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4543EDF545EE3FB4B8765F3FE0E93D3A5FA4425C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Jora;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Jora {
    
    
    /// <summary>
    /// ProjectSummaryPage
    /// </summary>
    public partial class ProjectSummaryPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_IssuesTotal;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_SaveAdver;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Summary;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbx_Adver;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Board;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Chat;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Team;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl scrllvwr_Issues;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_HelpTextAdver;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Jora;V1.0.0.0;component/pages/projectsummarypage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lbl_IssuesTotal = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.btn_SaveAdver = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
            this.btn_SaveAdver.Click += new System.Windows.RoutedEventHandler(this.btn_SaveAdver_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_Summary = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.txtbx_Adver = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
            this.txtbx_Adver.GotFocus += new System.Windows.RoutedEventHandler(this.txtbx_Adver_GotFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_Board = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
            this.btn_Board.Click += new System.Windows.RoutedEventHandler(this.btn_Board_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_Chat = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
            this.btn_Chat.Click += new System.Windows.RoutedEventHandler(this.btn_Chat_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_Team = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\Pages\ProjectSummaryPage.xaml"
            this.btn_Team.Click += new System.Windows.RoutedEventHandler(this.btn_Team_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.scrllvwr_Issues = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 9:
            this.lbl_HelpTextAdver = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

