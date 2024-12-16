using System;
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
    public partial class DeleteWindow : Window
    {
        public DeleteWindow()
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
        private void LoginDeleteTextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        private void LoginDeleteTextBox_LostFocus (object sender, RoutedEventArgs e)
        {

        }
        private async void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            string login = LoginDeleteTextBox.Text;
            string passwd = PsswordBoxPass.Password;
            try
            {
                if(login == CurrentUser.Login && await userService.IsPasswordRight(passwd, CurrentUser.UserId))
                {
                    if (await userService.DeleteUser(CurrentUser.UserId))
                    {
                        MessageBox.Show("User Deleted successfully\n Bye.");
                        Application.Current.Shutdown();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error" + ex.Message);
                return;
            }
        }
    }
}
