using System;
using System.Windows.Forms;

namespace loginGui
{
    public partial class StartScreen : Form
    {
        private loginSreen loginForm;
        private registerGui registerForm;

        public StartScreen()
        {
            InitializeComponent();

            //instancje formularzy 
            loginForm = new loginSreen();
            registerForm = new registerGui();

            //właściciel formularzy
            loginForm.Owner = this;
            registerForm.Owner = this;

            loginForm.Show();
        }

        // Funkcja do przełączania między oknami logowania i rejestracji
        public void ShowLogin()
        {
            registerForm.Hide();
            loginForm.Show();
        }

        public void ShowRegister()
        {
            loginForm.Hide();     
            registerForm.Show();  
        }
        
    }
}
