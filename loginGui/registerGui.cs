using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            SetPlaceholderEmailTextBox(this, EventArgs.Empty);
            SetPlaceholderPassTextBox(this, EventArgs.Empty);
            SetPlaceholderRepPassTextBox(this, EventArgs.Empty);

            EmailTextBox.GotFocus += RemovePlaceholderEmailTextBox;
            EmailTextBox.LostFocus += SetPlaceholderEmailTextBox;

            PassTextBox.GotFocus += RemovePlaceholderPassTextBox;
            PassTextBox.LostFocus += SetPlaceholderPassTextBox;

            RepPassTextBox.GotFocus += RemovePlaceholderRepPassTextBox;
            RepPassTextBox.LostFocus += SetPlaceholderRepPassTextBox;

            ShowPassCheckBox.CheckedChanged += ShowPassCheckBox_CheckedChanged;
        }

        private void YesAccountLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginSreen loginForm = new loginSreen();
            loginForm.ShowDialog();
            this.Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetPlaceholderEmailTextBox(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailTextBox.Text = "Enter your email";
                EmailTextBox.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderEmailTextBox = true;
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

        private void ShowPassCheckBox_CheckedChanged(object sender, EventArgs e)
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

    }
}
