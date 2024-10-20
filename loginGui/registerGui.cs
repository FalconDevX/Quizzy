using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace loginGui
{
    public partial class registerGui : Form
    {
        private bool isPlaceholderEmailTextBox, isPlaceholderPassTextBox, isPlaceholderRepPassTextBox = true;

        public registerGui()
        {
            InitializeComponent();

            RegisterLabel.Select();

            PassNotMatchLabel.Visible = false;

            SetPlaceholderEmailTextBox(this, EventArgs.Empty);
            SetPlaceholderPassTextBox(this, EventArgs.Empty);
            SetPlaceholderRepPassTextBox(this, EventArgs.Empty);

            EmailTextBox.GotFocus += RemovePlaceholderEmailTextBox;
            EmailTextBox.LostFocus += SetPlaceholderEmailTextBox;
            EmailTextBox.TextChanged += EmailTextBox_TextChanged;
            EmailTextBox.GotFocus += HideInvalidEmailLabel;

            PassTextBox.GotFocus += RemovePlaceholderPassTextBox;
            PassTextBox.LostFocus += SetPlaceholderPassTextBox;

            RepPassTextBox.GotFocus += RemovePlaceholderRepPassTextBox;
            RepPassTextBox.LostFocus += SetPlaceholderRepPassTextBox;

            PassTextBox.TextChanged += CheckPassMatch;
            RepPassTextBox.TextChanged += CheckPassMatch;

            RepPassTextBox.GotFocus += CheckPassMatch;
            PassTextBox.GotFocus += CheckPassMatch;

            PassTextBox.LostFocus += CheckPassMatch;
            RepPassTextBox.LostFocus += CheckPassMatch;


            ShowPassCheckBox.CheckedChanged += ShowPassCheckBox_CheckedChanged;
        }

        private void YesAccountLabel_Click(object sender, EventArgs e)
        {
            StartScreen startscreen = (StartScreen)this.Owner;
            startscreen.ShowLogin();
        }

        private void HideInvalidEmailLabel(object? sender, EventArgs s)
        {
            InvalidEmailLabel.Visible = false;
        }

        private void SetPlaceholderEmailTextBox(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text))
            {
                EmailTextBox.Text = "Enter your email";
                EmailTextBox.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderEmailTextBox = true;
                InvalidEmailLabel.Visible = false;
            }
            else if (!IsValidEmail(EmailTextBox.Text))
            {
                // Jeśli email nie jest poprawny, pokaż label
                InvalidEmailLabel.Visible = true;
            }
            else
            {
                InvalidEmailLabel.Visible = false;
            }
        }

        private void RemovePlaceholderEmailTextBox(object? sender, EventArgs s)
        {
            if (isPlaceholderEmailTextBox)
            {
                EmailTextBox.Text = "";
                EmailTextBox.ForeColor = Color.White;
                isPlaceholderEmailTextBox = false;
            }
        }

        private void SetPlaceholderPassTextBox(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PassTextBox.Text))
            {
                PassTextBox.Text = "Enter your password";
                PassTextBox.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderPassTextBox = true;
                PassTextBox.PasswordChar = '\0';
            }
        }

        private void RemovePlaceholderPassTextBox(object? sender, EventArgs s)
        {
            if (isPlaceholderPassTextBox)
            {
                PassTextBox.Text = "";
                PassTextBox.ForeColor = Color.White;
                isPlaceholderPassTextBox = false;
                SetPasswordChar();
            }
        }

        private void SetPlaceholderRepPassTextBox(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RepPassTextBox.Text))
            {
                RepPassTextBox.Text = "Repeat your password";
                RepPassTextBox.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderRepPassTextBox = true;
                RepPassTextBox.PasswordChar = '\0';
            }
        }

        private void RemovePlaceholderRepPassTextBox(object? sender, EventArgs s)
        {
            if (isPlaceholderRepPassTextBox)
            {
                RepPassTextBox.Text = "";
                RepPassTextBox.ForeColor = Color.White;
                isPlaceholderRepPassTextBox = false;
                SetPasswordChar();
            }
        }

        private void ShowPassCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            SetPasswordChar();
        }

        private void SetPasswordChar()
        {
            if (ShowPassCheckBox.Checked)
            {
                // Jeśli checkbox jest zaznaczony, odsłaniamy oba hasła
                PassTextBox.PasswordChar = '\0';
                RepPassTextBox.PasswordChar = '\0';
            }
            else
            {
                // Jeśli checkbox nie jest zaznaczony, ustawiamy maskę, tylko jeśli placeholdery nie są aktywne
                PassTextBox.PasswordChar = isPlaceholderPassTextBox ? '\0' : '*';
                RepPassTextBox.PasswordChar = isPlaceholderRepPassTextBox ? '\0' : '*';
            }
        }

        private void EmailTextBox_TextChanged(object? sender, EventArgs e)
        {
            if (IsValidEmail(EmailTextBox.Text))
            {
                InvalidEmailLabel.Visible = false; // Ukryj etykietę, jeśli email jest poprawny
            }
        }
        private bool IsValidEmail(string email)
        {
            // Wyrażenie regularne do sprawdzenia e-maila
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

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

        private void CheckPassMatch(object? sender, EventArgs e)
        {
            if (!isPlaceholderPassTextBox && !isPlaceholderRepPassTextBox && !IsPassMatch(PassTextBox.Text, RepPassTextBox.Text) && !PassTextBox.Focused && !RepPassTextBox.Focused)
            {
                PassNotMatchLabel.Visible = true;
            }
            else
            {
                PassNotMatchLabel.Visible = false;
            }
        }
        private void RegisterButton_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
