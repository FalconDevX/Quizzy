﻿using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    public partial class StartScreen : Window
    {
        public StartScreen()
        {
            InitializeComponent();

            LoginPanel.Visibility = Visibility.Visible;
            RegisterPanel.Visibility = Visibility.Collapsed;
        }

        //showing panel with animation
        private void ShowPanel(StackPanel panelToShow, StackPanel panelToHide)
        {
            var fadeOut = (Storyboard)FindResource("FadeOut");
            fadeOut.Completed += (s, e) =>
            {
                panelToHide.Visibility = Visibility.Collapsed;
                panelToShow.Visibility = Visibility.Visible;

                var fadeIn = (Storyboard)FindResource("FadeIn");
                fadeIn.Begin(panelToShow);
            };

            fadeOut.Begin(panelToHide);
        }

        // show register panel
        private void NoAccountLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EmailTextBoxLogin.Text = "";
            EmailTextBoxRegister.Text = "";
            ShowPasswordLogin.IsChecked = false;

            PassTextBoxLogin.Text = "";
            PassBoxLogin.Password = "";
            PassTextBorLogin.Visibility = Visibility.Collapsed;
            PassBorLogin.Visibility = Visibility.Visible;

            Keyboard.ClearFocus();

            PassBoxLogin.Style = Resources["PasswordStyle"] as Style;
            NickTextBoxRegister.LostFocus += NickTextBoxRegister_LostFocus;
            EmailTextBoxRegister_LostFocus(EmailTextBoxRegister, new RoutedEventArgs());
            NickTextBoxRegister_LostFocus(NickTextBoxRegister, new RoutedEventArgs());
            PassTextBoxRegister_LostFocus(PassTextBoxRegister, new RoutedEventArgs());
            RepPassTextBoxRegister_LostFocus(PassTextBoxRegister, new RoutedEventArgs());

            ShowPanel(RegisterPanel, LoginPanel);
        }

        // show login panel
        private void YesAccountLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //LoginTextBlock.Focus();
            EmailTextBoxRegister.Text = "";
            NickTextBoxRegister.Text = "";
            ShowPasswordRegister.IsChecked = false;
            RepPassBoxRegister.Password = "";
            PassBoxRegister.Password = "";

            // Resetowanie haseł i widoczności dla panelu rejestracji
            PassTextBorRegister.Visibility = Visibility.Collapsed;
            PassBorRegister.Visibility = Visibility.Visible;
            RepPassTextBorRegister.Visibility = Visibility.Collapsed;
            RepPassBorRegister.Visibility = Visibility.Visible;

            InvalidEmailLabelRegister.Visibility = Visibility.Hidden;

            Keyboard.ClearFocus();

            PassBoxRegister.Style = Resources["PasswordStyle"] as Style;
            RepPassBoxRegister.Style = Resources["PasswordStyle"] as Style;

            EmailTextBoxLogin_LostFocus(EmailTextBoxLogin, new RoutedEventArgs());
            PassTextBoxLogin_LostFocus(PassTextBoxRegister, new RoutedEventArgs());

            ShowPanel(LoginPanel, RegisterPanel);
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

        //check if passwd is correct
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
                InvalidPassLabel.Content = "Password must be at least 6 letters long";
                return false;
            }
            else if (passwd.Length > 15)
            {
                InvalidPassLabel.Content = "Password cannot exceed 15 letters";
                return false;
            }
            else if (!OneSpecialSign(passwd))
            {
                InvalidPassLabel.Content = "Password must contain at least 1 Special Sign";
                return false;
            }
            else if (!OneUppercase(passwd))
            {
                InvalidPassLabel.Content = "Password must contain at least 1 Uppercase Letter";
                return false;
            }
            return true;
        }

        //function which check if two passwords match
        private void CheckPasswordsMatch()
        {
            string password = ShowPasswordRegister.IsChecked == true ? PassTextBoxRegister.Text : PassBoxRegister.Password;
            string repeatedPassword = ShowPasswordRegister.IsChecked == true ? RepPassTextBoxRegister.Text : RepPassBoxRegister.Password;

            if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(repeatedPassword))
            {
                if (password != repeatedPassword)
                {
                    PassNotMatchLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    PassNotMatchLabel.Visibility = Visibility.Hidden;
                }
            }
        }


        private void CheckPasswordsValid()
        {
            string password = PassBoxRegister.Password;
            if (password == "")
            {
                InvalidPassLabel.Visibility = Visibility.Hidden;
            }
            else if (IsValidPasswd(password))
            {
                InvalidPassLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                InvalidPassLabel.Visibility = Visibility.Visible;
            }
        }

        //shows / hides password in login
        private void ShowPassLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordLogin.IsChecked == true)
            {
                PassBorLogin.Visibility = Visibility.Collapsed;
                PassTextBorLogin.Visibility = Visibility.Visible;
                PassTextBoxLogin.Text = PassBoxLogin.Password;
            }
            else
            {
                PassTextBorLogin.Visibility = Visibility.Collapsed;
                PassBorLogin.Visibility = Visibility.Visible;
                PassBoxLogin.Password = PassTextBoxLogin.Text;
            }
        }

        //shows/hides password in register
        private void ShowPassRegister_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordRegister.IsChecked == true)
            {
                PassBorRegister.Visibility = Visibility.Collapsed;
                PassTextBorRegister.Visibility = Visibility.Visible;
                PassTextBoxRegister.Text = PassBoxRegister.Password;
                RepPassBorRegister.Visibility = Visibility.Collapsed;
                RepPassTextBorRegister.Visibility = Visibility.Visible;
                RepPassTextBoxRegister.Text = RepPassBoxRegister.Password;
            }
            else
            {
                PassTextBorRegister.Visibility = Visibility.Collapsed;
                PassBorRegister.Visibility = Visibility.Visible;
                PassBoxRegister.Password = PassTextBoxRegister.Text;
                RepPassTextBorRegister.Visibility = Visibility.Collapsed;
                RepPassBorRegister.Visibility = Visibility.Visible;
                RepPassBoxRegister.Password = RepPassTextBoxRegister.Text;
            }
        }

        //email login lost focus
        private void EmailTextBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            //pokazywanie placeholdera poprzez zmiane tag
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "Type e-mail or login";
            }
        }

        //checking if login email textbox has focus
        private void EmailTextBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            //ukrywanie placeholdera poprzez zmiane tag
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "";
            }
        }

        //checking lost focus for login password

        private void PassTextBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PassBoxLogin.Password.Length == 0)
                PassBoxLogin.Style = Resources["PasswordStyle"] as Style;
            CheckPasswordsMatch();
        }

        //checking got focus for login password

        private void PassTextBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            PassBoxLogin.Style = null;
        }

        //checking if register email textbox has focus
        private void EmailTextBoxRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            //ukrywanie placeholdera poprzez zmiane tag
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "";
            }

            InvalidEmailLabelRegister.Visibility = Visibility.Hidden;
        }

        //checking if email in register panel is correct after losing focus
        private void EmailTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            //pokazywanie placeholdera poprzez zmiane tag
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "Type e-mail";
            }

            string email = EmailTextBoxRegister.Text;
            if (!IsValidEmail(email) && email != "")
            {
                InvalidEmailLabelRegister.Visibility = Visibility.Visible;
            }
            else
            {
                InvalidEmailLabelRegister.Visibility = Visibility.Hidden;
            }
        }

        private void NickTextBoxRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            //ukrywanie placeholdera poprzez zmiane tag
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "";
            }
        }

        private void NickTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            //pokazywanie placeholdera poprzez zmiane tag
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "")
            {
                textBox.Tag = "Type login";
            }
        }

        //checking got focus passtextbox i reppasstextbox
        private void PassTextBoxRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckPasswordsValid();
            PassBoxRegister.Style = null;
            PassNotMatchLabel.Visibility = Visibility.Hidden;
            InvalidPassLabel.Visibility = Visibility.Hidden;
        }

        private void RepPassTextBoxRegister_GotFocus(object sender, RoutedEventArgs e)
        {
            RepPassBoxRegister.Style = null;
            PassNotMatchLabel.Visibility = Visibility.Hidden;
        }

        //checking lost focus passtextbox i reppasstextbox
        private void PassTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PassBoxRegister.Password.Length == 0)
                PassBoxRegister.Style = Resources["PasswordStyle"] as Style;
            CheckPasswordsValid();
            CheckPasswordsMatch();
        }

        private void RepPassTextBoxRegister_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RepPassBoxRegister.Password.Length == 0)
                RepPassBoxRegister.Style = Resources["PasswordStyle"] as Style;
            CheckPasswordsMatch();
        }

        //login button click
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;

            string identifier = EmailTextBoxLogin.Text.Trim();
            string password;

            if (ShowPasswordLogin.IsChecked == true)
            {
                password = PassTextBoxLogin.Text.Trim();
            }
            else
            {
                password = PassBoxLogin.Password.Trim();
            }

            if (!string.IsNullOrWhiteSpace(identifier) && password != "")
            {
                UserService userService = new UserService();

                LoadingSpinner.Visibility = Visibility.Visible;
                
                bool isAuthenticated = await userService.LoginUser(identifier, password);
                
                if (isAuthenticated)
                {
                    LoginButton.IsEnabled = true;
                    LoadingSpinner.Visibility = Visibility.Hidden;
                    MessageBox.Show("Login successful.");

                    CurrentUser.UserId = await userService.GetUserIdByLoginApi(CurrentUser.Login);
                    AzureBlobAPI azureBlobAPI = new AzureBlobAPI(); 
                    if (await azureBlobAPI.CheckContainerExistsByUserID($"ident{CurrentUser.UserId}"))
                    {
                        await azureBlobAPI.DownloadAndExtractBlobsAsync($"ident{CurrentUser.UserId}");
                    }
                    else
                    {
                        await azureBlobAPI.CreateContainerAsync($"ident{CurrentUser.UserId}");
                        await azureBlobAPI.CreateContainerAsync($"files{CurrentUser.UserId}");
                        await azureBlobAPI.DownloadAndExtractBlobsAsync($"ident{CurrentUser.UserId}");
                    }

                    MainScreen mainScreen = new MainScreen();
                    mainScreen.Show();
                    this.Close();
                }
                else
                {
                    await Task.Delay(5000);
                    LoginButton.IsEnabled = true;

                    LoadingSpinner.Visibility = Visibility.Hidden;
                 
                    
                    MessageBox.Show("Invalid credentials. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Please fill in both fields.");
            }
        }


        //Register button click
        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBoxRegister.Text.Trim();
            string password;
            string repeatedPassword;
            string login = NickTextBoxRegister.Text.Trim();

            if (ShowPasswordLogin.IsChecked == false)
            {
                password = PassBoxRegister.Password.Trim();
                repeatedPassword = RepPassBoxRegister.Password.Trim();
            }
            else
            {
                password = PassTextBoxRegister.Text.Trim();
                repeatedPassword = RepPassTextBoxRegister.Text.Trim();
            }

            UserService userService = new UserService();

            if (password != repeatedPassword)
            {
                CheckPasswordsMatch();
                return;
            }

            if (login != "")
            {
                if (await userService.IsLoginTakenApi(login))
                {
                    MessageBox.Show("Login already exists. Please choose a different one.");
                    return;
                }
            }
            
            if(email != "")
            {
                if (await userService.IsEmailTakenApi(email))
                {
                    MessageBox.Show("Email already exists. Please choose a different one.");
                    return;
                }
            }
            
            if (IsValidEmail(email) && !string.IsNullOrWhiteSpace(login) && !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(password) && IsValidPasswd(password))
            {
                MainScreen mainScreen = new MainScreen();

                string displayName = login.Contains("@") ? login.Split('@')[0] : login;

                System.Diagnostics.Debug.WriteLine(login);

                bool IsRegistrationSuccess = await userService.RegisterUserApi(login, email, password);

                if (IsRegistrationSuccess)
                {

                    MessageBox.Show("Register successful!");
                    CurrentUser.Login = login;
                    CurrentUser.UserId = await userService.GetUserIdByLoginApi(login);
                    AzureBlobAPI azureBlobAPI = new AzureBlobAPI();
                    await azureBlobAPI.CreateContainerAsync($"ident{CurrentUser.UserId}");
                    var answer = await azureBlobAPI.CreateContainerAsync($"files{CurrentUser.UserId}");
                    MessageBox.Show(answer);
                    mainScreen.UserNameTextBlock.Text = $"Hi, {displayName}";
                    mainScreen.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Registration not successfull");
                }

            }
            else
            {
                MessageBox.Show("Please fill all textboxes with valid data.");
            }
        }

        //Password in passwordbox changed for loginand register panel
        private void PasswordBoxLogin_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassBoxLogin.Password.Length == 0)
                PassBoxLogin.Style = Resources["PasswordStyle"] as Style;
            else
                PassBoxLogin.Style = null;
        }

        private void PasswordBoxRegister_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassBoxRegister.Password.Length == 0)
                PassBoxRegister.Style = Resources["PasswordStyle"] as Style;
            else
                PassBoxRegister.Style = null;
        }

        private void RepPasswordBoxRegister_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RepPassBoxRegister.Password.Length == 0)
                RepPassBoxRegister.Style = Resources["PasswordStyle"] as Style;
            else
                RepPassBoxRegister.Style = null;
        }

        //pressing Enter depends on panel
        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (LoginPanel.Visibility == Visibility.Visible)
                {
                    LoginButton_Click(LoginButton, new RoutedEventArgs());
                }
                else if (RegisterPanel.Visibility == Visibility.Visible)
                {
                    RegisterButton_Click(RegisterButton, new RoutedEventArgs());
                }
            }
        }

        // Dragging window when the left mouse button is pressed
        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                // Zmieniamy kursor na "Hand"
                StartScreenMainGrid.Cursor = Cursors.Hand;

                // Upewniamy się, że okno nie jest zmaksymalizowane
                if (this.WindowState != WindowState.Maximized)
                {
                    this.DragMove();
                }
            }
        }

        // Reset cursor when the left mouse button is released
        private void MainGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Przywracamy domyślny kursor
            StartScreenMainGrid.Cursor = Cursors.Arrow;
        }


    }
}