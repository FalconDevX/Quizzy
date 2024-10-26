﻿using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Pokazywanie panelu login i chowanie panelu register
            LoginPanel.Visibility = Visibility.Visible;
            RegisterPanel.Visibility = Visibility.Collapsed;
        }

        //show register panel
        private void NoAccountLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }


        //show login panel
        private void YesAccountLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Visible;
            RegisterPanel.Visibility = Visibility.Collapsed;
        }

        //exit application
        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //minimize application
        private void MinimizeWindowButton_Click(Object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //check if email correct
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        //checking if email in login panel correct after losing focus
        private void EmailTextBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBoxLogin.Text;
            if (!IsValidEmail(email) && email != "")
            {
                InvalidEmailLabelLogin.Visibility = Visibility.Visible;
            }
            else
            {
                InvalidEmailLabelLogin.Visibility = Visibility.Hidden;
            }
        }

        //checking if email in register panel is correct after losing focus
        private void EmailTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBoxRegister.Text;
            if (!IsValidEmail(email) && email!="")
            {
                InvalidEmailLabelRegister.Visibility = Visibility.Visible;
            }
            else
            {
                InvalidEmailLabelRegister.Visibility = Visibility.Hidden;
            }
        }

        //checking if login email textbox has focus
        private void EmailTextBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidEmailLabelLogin.Visibility = Visibility.Hidden;
        }

        //checking if register email textbox has focus
        private void EmailTextBoxRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidEmailLabelRegister.Visibility = Visibility.Hidden;
        }

        //checking lost focus passtextbox i reppasstextbox
        private void PassTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckPasswordsMatch();
        }

        private void RepPassTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckPasswordsMatch();
        }

        //checking got focus passtextbox i reppasstextbox

        private void PassTextBoxRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            PassNotMatchLabel.Visibility = Visibility.Hidden;
        }

        private void RepPassTextBoxRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            PassNotMatchLabel.Visibility = Visibility.Hidden;
        }

        //function which check if two passwords match
        private void CheckPasswordsMatch()
        {
            string password = PassTextBoxRegister.Text;
            string repeatedPassword = RepPassTextBoxRegister.Text;

            if (password != repeatedPassword && !string.IsNullOrEmpty(repeatedPassword) && !string.IsNullOrEmpty(password))
            {
                PassNotMatchLabel.Visibility = Visibility.Visible;
            }
            else
            {
                PassNotMatchLabel.Visibility = Visibility.Hidden;
            }
        }

        //Register i Login Button Click
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBoxRegister.Text;
            string password = PassTextBoxRegister.Text;
            string repeatedPassword = RepPassTextBoxRegister.Text;

            if (password == repeatedPassword && IsValidEmail(EmailTextBoxLogin.Text))
            {
                UserService userService = new UserService();
                userService.RegisterUser(email, password);
                MessageBox.Show("Register successful!");
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBoxLogin.Text;
            string password = PassTextBoxLogin.Text;

            UserService userService = new UserService();
            bool isAuthenticated = userService.LoginUser(email, password);

            if(IsValidEmail(email) && password!="")
            {
                if (isAuthenticated)
                {
                    MessageBox.Show("Login successful.");
                }
                else
                {
                    MessageBox.Show("Wrong credentials. Consider registration");
                }
            }
            else
            {
                MessageBox.Show("Wrong email or password.");
            }
                
        }



    }
}