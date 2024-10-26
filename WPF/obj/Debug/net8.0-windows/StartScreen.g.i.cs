﻿#pragma checksum "..\..\..\StartScreen.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A25E88595D2F66744F5605DFAF1F69AA0E8D6288"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WPF;


namespace WPF {
    
    
    /// <summary>
    /// StartScreen
    /// </summary>
    public partial class StartScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 70 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel LoginPanel;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBoxLogin;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InvalidEmailLabelLogin;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PassTextBoxLogin;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel RegisterPanel;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBoxRegister;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InvalidEmailLabelRegister;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PassTextBoxRegister;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RepPassTextBoxRegister;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\StartScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PassNotMatchLabel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WPF;V1.0.0.0;component/startscreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StartScreen.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LoginPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.EmailTextBoxLogin = ((System.Windows.Controls.TextBox)(target));
            
            #line 77 "..\..\..\StartScreen.xaml"
            this.EmailTextBoxLogin.LostFocus += new System.Windows.RoutedEventHandler(this.EmailTextBoxLogin_LostFocus);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\StartScreen.xaml"
            this.EmailTextBoxLogin.GotFocus += new System.Windows.RoutedEventHandler(this.EmailTextBoxLogin_GotFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.InvalidEmailLabelLogin = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.PassTextBoxLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 89 "..\..\..\StartScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LoginButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 100 "..\..\..\StartScreen.xaml"
            ((System.Windows.Controls.Label)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.NoAccountLabel_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.RegisterPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.EmailTextBoxRegister = ((System.Windows.Controls.TextBox)(target));
            
            #line 112 "..\..\..\StartScreen.xaml"
            this.EmailTextBoxRegister.LostFocus += new System.Windows.RoutedEventHandler(this.EmailTextBoxRegister_LostFocus);
            
            #line default
            #line hidden
            
            #line 112 "..\..\..\StartScreen.xaml"
            this.EmailTextBoxRegister.GotFocus += new System.Windows.RoutedEventHandler(this.EmailTextBoxRegister_GotFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.InvalidEmailLabelRegister = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.PassTextBoxRegister = ((System.Windows.Controls.TextBox)(target));
            
            #line 125 "..\..\..\StartScreen.xaml"
            this.PassTextBoxRegister.LostFocus += new System.Windows.RoutedEventHandler(this.PassTextBoxRegister_LostFocus);
            
            #line default
            #line hidden
            
            #line 125 "..\..\..\StartScreen.xaml"
            this.PassTextBoxRegister.GotFocus += new System.Windows.RoutedEventHandler(this.PassTextBoxRegister_GotFocus);
            
            #line default
            #line hidden
            return;
            case 11:
            this.RepPassTextBoxRegister = ((System.Windows.Controls.TextBox)(target));
            
            #line 128 "..\..\..\StartScreen.xaml"
            this.RepPassTextBoxRegister.LostFocus += new System.Windows.RoutedEventHandler(this.RepPassTextBoxRegister_LostFocus);
            
            #line default
            #line hidden
            
            #line 128 "..\..\..\StartScreen.xaml"
            this.RepPassTextBoxRegister.GotFocus += new System.Windows.RoutedEventHandler(this.RepPassTextBoxRegister_GotFocus);
            
            #line default
            #line hidden
            return;
            case 12:
            this.PassNotMatchLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            
            #line 137 "..\..\..\StartScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RegisterButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 149 "..\..\..\StartScreen.xaml"
            ((System.Windows.Controls.Label)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.YesAccountLabel_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 156 "..\..\..\StartScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindowButton_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 160 "..\..\..\StartScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MinimizeWindowButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

