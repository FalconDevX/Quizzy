using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Microsoft.Extensions.Azure;
using Newtonsoft.Json;

namespace WPF
{
    public partial class ChangeLoginWindow : Window
    {
        public ChangeLoginWindow()
        {
            InitializeComponent();
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                var placeholder = passwordBox.Template.FindName("PART_TempText", passwordBox) as TextBlock;
                if (placeholder != null)
                {
                    placeholder.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null && string.IsNullOrEmpty(passwordBox.Password))
            {
                var placeholder = passwordBox.Template.FindName("PART_TempText", passwordBox) as TextBlock;
                if (placeholder != null)
                {
                    placeholder.Visibility = Visibility.Visible;
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                var placeholder = passwordBox.Template.FindName("PART_TempText", passwordBox) as TextBlock;
                if (placeholder != null)
                {
                    placeholder.Visibility = string.IsNullOrEmpty(passwordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private async void ChangeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserService userService = new UserService();
                int userId = CurrentUser.UserId; // Przykładowe ID użytkownika - zamień na dynamiczne.
                string newLogin = NewLoginTextBox.Text;
                if(await userService.IsLoginTakenApi(newLogin))
                {
                    MessageBox.Show("new login invalid");
                    return;
                }
                string currentLogin = PasswordBoxChangeLogin.Password;
                string passwd = PasswordBoxPass.Password;
                if(await userService.IsPasswordRight(passwd, userId) && currentLogin == CurrentUser.Login) 
                {
                    if(await userService.ChangeUserLogin(userId, newLogin))
                    {
                        CurrentUser.Login = newLogin;
                        MessageBox.Show("Login changed succesfully");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Unexpected DataBase Error");
                        return;
                    }
                }
            } 
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
                return;
            }

        }


    }
}