﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF
{
    public partial class DialogBox : Window
    {
        public DialogBox()
        {
            InitializeComponent();
        }


        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private async void ChangeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string newlogin = DialogBoxTextBlockInfo.Text;

            UserService userService = new UserService();
            if (await userService.IsLoginTakenApi(newlogin))
            {
                DialogBoxTextBlockInfo.Visibility = Visibility.Visible;
            }
            else if(newlogin.Length>7)
            {
                MessageBox.Show("Login too long. Maximum is 7 characters.");   
            }
            else
            {
                userService.ChangeUserLogin(CurrentUser.UserId, newlogin);

                CurrentUser.Login = newlogin;

                this.Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("Account delete");
        }

        private void ChangeLoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DialogBoxTextBlockInfo.Visibility = Visibility.Hidden;
        }
    }
}
