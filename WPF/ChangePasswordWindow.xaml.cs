using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
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

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool OneUppercase(string passwd)
        {
            for (int i = 0; i < passwd.Length; i++)
            {
                if (passwd[i] >= 65 && passwd[i] <= 90)
                {
                    return true;
                }
            }
            return false;
        }

        private bool OneSpecialSign(string passwd)
        {
            for (int i = 0; i < passwd.Length; i++)
            {
                char c = passwd[i];
                if ((c >= 0 && c <= 47) || (c >= 58 && c <= 64) || (c >= 91 && c <= 96) || (c >= 123))
                {
                    return true;
                }
            }
            return false;
        }

        
        private bool IsValidPasswd(string passwd) //passwd length 6-15 // 1 Uppercase Letter // One special sign
        {
            if (passwd.Length < 6)
            {
                //InvalidPassLabel.Content = "Password must be at least 6 letters long";
                return false;
            }
            else if (passwd.Length > 15)
            {
                //InvalidPassLabel.Content = "Password cannot exceed 15 letters";
                return false;
            }
            else if (!OneSpecialSign(passwd))
            {
                //InvalidPassLabel.Content = "Password must contain at least 1 Special Sign";
                return false;
            }
            else if (!OneUppercase(passwd))
            {
                //InvalidPassLabel.Content = "Password must contain at least 1 Uppercase Letter";
                return false;
            }
            return true;
        }
        

        private async void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            string newPasswd = NewPasswordBoxChangePass.Password;
            string repNewPasswd = RetypeNewPasswordBoxChangePass.Password; 
            if(newPasswd != repNewPasswd)
            {
                MessageBox.Show("Password incorrectly retyped");
                return;
            }
            if (!IsValidPasswd(newPasswd))
            {
                MessageBox.Show("New password is incorrect\n Password needs to have at least one Uppercase letter and at least one special Sign");
                return;
            }
            string passwd = PasswordBoxChangePass.Password;
            if (await userService.IsPasswordRight(passwd, CurrentUser.UserId))
            {
                if(await userService.ChangeUserPassword(CurrentUser.UserId, UserService.HashPassword(newPasswd)))
                {
                    MessageBox.Show("Password Changed Successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unexpected DataBase error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Current password incorrect");
                return;
            }

        }
    }
}
