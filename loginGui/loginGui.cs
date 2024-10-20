using System.Text.RegularExpressions;

namespace loginGui
{
    public partial class loginSreen : Form
    {
        private bool isPlaceholderEmailTextBox, isPlaceholderPassTextBox = true;

        public loginSreen()
        {
            InitializeComponent();

            //Ustawianie kursora/focusu na label
            LoginLabel.Select();

            InvalidEmailLabel.Visible = false;

            //Placeholdery i textboxy
            SetPlaceholderEmailTextBox(this, EventArgs.Empty);
            SetPlaceholderPassTextBox(this, EventArgs.Empty);

            EmailTextBox.GotFocus += RemovePlaceholderEmailTextBox;
            EmailTextBox.LostFocus += SetPlaceholderEmailTextBox;
            EmailTextBox.TextChanged += EmailTextBox_TextChanged;

            PassTextBox.GotFocus += RemovePlaceholderPassTextBox;
            PassTextBox.LostFocus += SetPlaceholderPassTextBox;

            ShowPassCheckBox.CheckedChanged += ShowPassCheckBox_CheckedChanged;        

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
                // Jeœli email nie jest poprawny, poka¿ label
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
                PassTextBox.PasswordChar = '*';
            }
        }

        //wyjsæie z aplikacji
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Pokazywanie i ukrywanie has³a
        private void ShowPassCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            // Jeœli checkbox jest zaznaczony, poka¿ has³o
            if (ShowPassCheckBox.Checked)
            {
                PassTextBox.PasswordChar = '\0'; // Ods³oniêcie has³a
            }
            else
            {

                if (isPlaceholderPassTextBox)
                {
                    PassTextBox.PasswordChar = '\0';
                }
                else
                {
                    PassTextBox.PasswordChar = '*'; // Ukrycie has³a gwiazdkami
                }
            }
        }
        private void EmailTextBox_TextChanged(object? sender, EventArgs e)
        {
            if (IsValidEmail(EmailTextBox.Text))
            {
                InvalidEmailLabel.Visible = false; // Ukryj etykietê, jeœli email jest poprawny
            }
        }

        private void LackAccountLabel_Click(object sender, EventArgs e)
        { 
            this.Hide();
            registerGui registerForm = new registerGui();
            registerForm.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu MainMenuForm = new MainMenu();
            MainMenuForm.ShowDialog();
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            // Wyra¿enie regularne do sprawdzenia e-maila
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}