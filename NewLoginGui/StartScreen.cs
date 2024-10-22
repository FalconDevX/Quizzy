using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using AxWMPLib;

namespace NewLoginGui
{
    public partial class StartScreen : Form
    {
        private bool isPlaceholderEmailTextBoxLogin, isPlaceholderPassTextBoxLogin = true;

        private bool isPlaceholderEmailTextBoxRegister, isPlaceholderPassTextBoxRegister, isPlaceholderRepPassTextBoxRegister = true;

        public StartScreen()
        {
            InitializeComponent();
          
            LoginPanel.BackColor = Color.Transparent;
            RegisterPanel.BackColor = Color.Transparent;

            RegisterPanel.Hide();
            LoginPanel.Show();

            LoginLabel.Select();

            //Napis dla okna
            this.Text = "Quizzy";

            InvalidEmailLabelRegister.Hide();

            //Ustawianie placeholderów na pocz¹tku dzia³ania programu
            SetPlaceholderEmailTextBoxLogin(this, EventArgs.Empty);
            SetPlaceholderPassTextBoxLogin(this, EventArgs.Empty);

            SetPlaceholderEmailTextBoxRegister(this, EventArgs.Empty);
            SetPlaceholderPassTextBoxRegister(this, EventArgs.Empty);
            SetPlaceholderRepPassTextBoxRegister(this, EventArgs.Empty);

            //Zdarzenia dla login Email

            EmailTextBoxLogin.GotFocus += RemovePlaceholderEmailTextBoxLogin;
            EmailTextBoxLogin.GotFocus += HideInvalidEmailLabelLogin;
            EmailTextBoxLogin.LostFocus += SetPlaceholderEmailTextBoxLogin;
            EmailTextBoxLogin.TextChanged += EmailTextBoxLoginTextChanged;

            //Zdarzenia dla text box has³a Login
            PassTextBoxLogin.GotFocus += RemovePlaceholderPassTextBoxLogin;
            PassTextBoxLogin.LostFocus += SetPlaceholderPassTextBoxLogin;
            ShowPassCheckBoxLogin.CheckedChanged += ShowPassCheckBox_CheckedChanged;

            //Zdarzenia dla Register
            PassNotMatchLabelRegister.Visible = false;

            EmailTextBoxRegister.GotFocus += RemovePlaceholderEmailTextBoxRegister;
            EmailTextBoxRegister.LostFocus += SetPlaceholderEmailTextBoxRegister;
            EmailTextBoxRegister.TextChanged += EmailTextBoxTextChangedRegister;
            EmailTextBoxRegister.GotFocus += HideInvalidEmailLabelRegister;

            PassTextBoxRegister.GotFocus += RemovePlaceholderPassTextBoxRegister;
            PassTextBoxRegister.LostFocus += SetPlaceholderPassTextBoxRegister;

            RepPassTextBoxRegister.GotFocus += RemovePlaceholderRepPassTextBoxRegister;
            RepPassTextBoxRegister.LostFocus += SetPlaceholderRepPassTextBoxRegister;

            PassTextBoxRegister.TextChanged += CheckPassMatch;
            RepPassTextBoxRegister.TextChanged += CheckPassMatch;

            RepPassTextBoxRegister.GotFocus += CheckPassMatch;
            PassTextBoxRegister.GotFocus += CheckPassMatch;

            PassTextBoxRegister.LostFocus += CheckPassMatch;
            RepPassTextBoxRegister.LostFocus += CheckPassMatch;


            ShowPassCheckBoxRegister.CheckedChanged += ShowPassCheckBoxCheckedChangedRegister;
        }

        //Funkcje dla panelu Login
        //////////////////////////////////////////////////////////////////////////////////////////////

        //Labele do prze³¹czania siê pomiêdzy panelami
        private void HaveAccountLabel_Click(object sender, EventArgs e)
        {
            RegisterPanel.Hide();
            LoginPanel.Show();
            LoginLabel.Select();
        }

        private void DontHaveAccountLabel_Click(object sender, EventArgs e)
        {
            LoginPanel.Hide();
            RegisterPanel.Show();
            RegisterLabel.Select();
        }

        //Placeholdery na email do loginu

        private void SetPlaceholderEmailTextBoxLogin(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBoxLogin.Text) || string.IsNullOrEmpty(EmailTextBoxLogin.Text))
            {
                EmailTextBoxLogin.Text = "Enter your email";
                EmailTextBoxLogin.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderEmailTextBoxLogin = true;
                InvalidEmailLabelLogin.Visible = false;
            }
            else if (!IsValidEmail(EmailTextBoxLogin.Text))
            {
                // Jeœli email nie jest poprawny, poka¿ label
                InvalidEmailLabelLogin.Visible = true;
            }
            else
            {
                InvalidEmailLabelLogin.Visible = false;
            }
        }

        private void RemovePlaceholderEmailTextBoxLogin(object? sender, EventArgs s)
        {
            if (isPlaceholderEmailTextBoxLogin)
            {
                EmailTextBoxLogin.Text = "";
                EmailTextBoxLogin.ForeColor = Color.White;
                isPlaceholderEmailTextBoxLogin = false;
            }
        }

        //Œledzenie zmian wpisywanych przez u¿ytkownika i sprawdzanie email
        private void EmailTextBoxLoginTextChanged(object? sender, EventArgs e)
        {
            if (IsValidEmail(EmailTextBoxLogin.Text))
            {
                InvalidEmailLabelLogin.Visible = false; // Ukryj etykietê, jeœli email jest poprawny
            }
        }

        //Sprawdzanie poprawnoœci email
        private bool IsValidEmail(string email)
        {
            // Wyra¿enie regularne do sprawdzenia e-maila
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        //Chowanie InvalidEmailLabel
        private void HideInvalidEmailLabelLogin(object? sender, EventArgs s)
        {
            InvalidEmailLabelLogin.Visible = false;
        }

        //Pokazywanie i ukrywanie has³a
        private void ShowPassCheckBoxLoginCheckedChanged(object? sender, EventArgs e)
        {
            // Jeœli checkbox jest zaznaczony, poka¿ has³o
            if (ShowPassCheckBoxLogin.Checked)
            {
                PassTextBoxLogin.PasswordChar = '\0'; // Ods³oniêcie has³a
            }
            else
            {

                if (isPlaceholderPassTextBoxLogin)
                {
                    PassTextBoxLogin.PasswordChar = '\0';
                }
                else
                {
                    PassTextBoxLogin.PasswordChar = '*'; // Ukrycie has³a gwiazdkami
                }
            }
        }

        //Placholdery dla has³a Login
        private void RemovePlaceholderPassTextBoxLogin(object? sender, EventArgs s)
        {
            if (isPlaceholderPassTextBoxLogin)
            {
                PassTextBoxLogin.Text = "";
                PassTextBoxLogin.ForeColor = Color.White;
                isPlaceholderPassTextBoxLogin = false;
                PassTextBoxLogin.PasswordChar = '*';
            }
        }

        private void SetPlaceholderPassTextBoxLogin(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PassTextBoxLogin.Text))
            {
                PassTextBoxLogin.Text = "Enter your password";
                PassTextBoxLogin.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderPassTextBoxLogin = true;
                PassTextBoxLogin.PasswordChar = '\0';
            }
        }

        //Pokazywanie i ukrywanie has³a
        private void ShowPassCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            // Jeœli checkbox jest zaznaczony, poka¿ has³o
            if (ShowPassCheckBoxLogin.Checked)
            {
                PassTextBoxLogin.PasswordChar = '\0'; // Ods³oniêcie has³a
            }
            else
            {

                if (isPlaceholderPassTextBoxLogin)
                {
                    PassTextBoxLogin.PasswordChar = '\0';
                }
                else
                {
                    PassTextBoxLogin.PasswordChar = '*'; // Ukrycie has³a gwiazdkami
                }
            }
        }

        //Funkcje dla panelu Register
        //////////////////////////////////////////////////////////////////////////////////////////////

        //Placeholdery dla email 
        private void RemovePlaceholderEmailTextBoxRegister(object? sender, EventArgs s)
        {
            if (isPlaceholderEmailTextBoxRegister)
            {
                EmailTextBoxRegister.Text = "";
                EmailTextBoxRegister.ForeColor = Color.White;
                isPlaceholderEmailTextBoxRegister = false;
            }
        }

        private void SetPlaceholderEmailTextBoxRegister(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBoxRegister.Text) || string.IsNullOrEmpty(EmailTextBoxRegister.Text))
            {
                EmailTextBoxRegister.Text = "Enter your email";
                EmailTextBoxRegister.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderEmailTextBoxRegister = true;
                InvalidEmailLabelRegister.Visible = false;
            }
            else if (!IsValidEmail(EmailTextBoxRegister.Text))
            {
                InvalidEmailLabelRegister.Visible = true;
            }
            else
            {
                InvalidEmailLabelRegister.Visible = false;
            }
        }

        //Placeholdery dla has³a
        private void SetPlaceholderPassTextBoxRegister(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PassTextBoxRegister.Text))
            {
                PassTextBoxRegister.Text = "Enter your password";
                PassTextBoxRegister.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderPassTextBoxRegister = true;
                PassTextBoxRegister.PasswordChar = '\0';
            }
        }

        private void RemovePlaceholderPassTextBoxRegister(object? sender, EventArgs s)
        {
            if (isPlaceholderPassTextBoxRegister)
            {
                PassTextBoxRegister.Text = "";
                PassTextBoxRegister.ForeColor = Color.White;
                isPlaceholderPassTextBoxRegister = false;
                SetPasswordChar();
            }
        }

        //Placeholdery dla powtórzonego has³a

        private void SetPlaceholderRepPassTextBoxRegister(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RepPassTextBoxRegister.Text))
            {
                RepPassTextBoxRegister.Text = "Repeat your password";
                RepPassTextBoxRegister.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderRepPassTextBoxRegister = true;
                RepPassTextBoxRegister.PasswordChar = '\0';
            }
        }

        private void RemovePlaceholderRepPassTextBoxRegister(object? sender, EventArgs s)
        {
            if (isPlaceholderRepPassTextBoxRegister)
            {
                RepPassTextBoxRegister.Text = "";
                RepPassTextBoxRegister.ForeColor = Color.White;
                isPlaceholderRepPassTextBoxRegister = false;
                SetPasswordChar();
            }
        }

        //Ods³oniêcie,zas³oniêcie has³a
        private void SetPasswordChar()
        {
            if (ShowPassCheckBoxRegister.Checked)
            {
                PassTextBoxRegister.PasswordChar = '\0';
                RepPassTextBoxRegister.PasswordChar = '\0';
            }
            else
            {
                //Checkbox nie zaznaczony -> ustawanie maski -> jeœli placeholdery nie s¹ aktywne
                PassTextBoxRegister.PasswordChar = isPlaceholderPassTextBoxRegister ? '\0' : '*';
                RepPassTextBoxRegister.PasswordChar = isPlaceholderRepPassTextBoxRegister ? '\0' : '*';
            }
        }

        //Sprawdzanie poprawnoœci emaila

        private void EmailTextBoxTextChangedRegister(object? sender, EventArgs e)
        {
            if (IsValidEmail(EmailTextBoxRegister.Text))
            {
                InvalidEmailLabelRegister.Visible = false; // Ukryj etykietê, jeœli email jest poprawny
            }
        }

        //Ukrywanie label Invalid email
        private void HideInvalidEmailLabelRegister(object? sender, EventArgs s)
        {
            InvalidEmailLabelRegister.Visible = false;
        }

        //Pokazywanie i chowanie labela Password not match
        private void CheckPassMatch(object? sender, EventArgs e)
        {
            if (!isPlaceholderPassTextBoxRegister && !isPlaceholderRepPassTextBoxRegister && !IsPassMatch(PassTextBoxRegister.Text, RepPassTextBoxRegister.Text) && !PassTextBoxRegister.Focused && !RepPassTextBoxRegister.Focused)
            {
                PassNotMatchLabelRegister.Visible = true;
            }
            else
            {
                PassNotMatchLabelRegister.Visible = false;
            }
        }

        //Sprawdzanie czy has³a s¹ jednakowe
        private bool IsPassMatch(string pass1, string pass2)
        {
            if (pass1 == pass2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Odkrywanie pas³a
        private void ShowPassCheckBoxCheckedChangedRegister(object? sender, EventArgs e)
        {
            SetPasswordChar();
        }

        //Wyjœcie z aplikacji
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
