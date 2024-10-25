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
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //checking if email in register panel is correct after losing focus
        private void EmailTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBoxRegister.Text;
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format. Please enter a valid email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}