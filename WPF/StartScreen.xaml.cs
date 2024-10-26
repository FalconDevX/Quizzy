using System.Text;
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
    public partial class StartScreen : Window
    {
        public StartScreen()
        {
            InitializeComponent();

            //Pokazywanie panelu login i chowanie panelu register
            LoginPanel.Visibility = Visibility.Visible;
            RegisterPanel.Visibility = Visibility.Collapsed;
        }

        //show register panel
        private void NoAccountLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RegisterTextBlock.Focus();
            EmailTextBoxLogin.Text = "";
            PassTextBoxLogin.Text = "";
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }


        //show login panel
        private void YesAccountLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoginTextBlock.Focus();
            EmailTextBoxRegister.Text = "";
            PassTextBoxRegister.Text = "";
            RepPassTextBoxRegister.Text = "";
            InvalidEmailLabelRegister.Visibility = Visibility.Hidden;
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
            string login = NickTextBoxRegister.Text;

            UserService userService = new UserService();

            if (userService.IsLoginTaken(login))
            {
                MessageBox.Show("Login already exists. Please choose a different one.");
                return;
            }

            if (password == repeatedPassword && IsValidEmail(EmailTextBoxRegister.Text) && NickTextBoxRegister.Text!="")
            {
                System.Diagnostics.Debug.WriteLine(NickTextBoxRegister.Text);
                userService.RegisterUser(login, email, password);
                MessageBox.Show("Register successful!");
                MainScreen mainScreen = new MainScreen();
                mainScreen.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string identifier = EmailTextBoxLogin.Text; // Może być login lub email
            string password = PassTextBoxLogin.Text;

            UserService userService = new UserService();
            bool isAuthenticated = userService.LoginUser(identifier, password);

            if (!string.IsNullOrWhiteSpace(identifier) && !string.IsNullOrWhiteSpace(password))
            {
                if (isAuthenticated)
                {
                    MessageBox.Show("Login successful.");
                    MainScreen mainScreen = new MainScreen();
                    mainScreen.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid credentials. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Please fill in both fields.");
            }
        }




    }
}