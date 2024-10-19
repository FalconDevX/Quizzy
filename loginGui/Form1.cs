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

            //Placeholdery i textboxy
            SetPlaceholderEmailTextBox(this, EventArgs.Empty);
            SetPlaceholderPassTextBox(this, EventArgs.Empty);

            EmailTextBox.GotFocus += RemovePlaceholderEmailTextBox;
            EmailTextBox.LostFocus += SetPlaceholderEmailTextBox;

            PassTextBox.GotFocus += RemovePlaceholderPassTextBox;
            PassTextBox.LostFocus += SetPlaceholderPassTextBox;

            //Ustawianie domyœlnie gwiadek do has³a i reakcja na checkbox
            
            //PassTextBox.PasswordChar = '*';

            ShowPassCheckBox.CheckedChanged += ShowPassCheckBox_CheckedChanged;


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
                PassTextBox.PasswordChar = '*';
            }
        }

        //wyjsæie z aplikacji
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Pokazywanie i ukrywanie has³a
        private void ShowPassCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Jeœli checkbox jest zaznaczony, poka¿ has³o
            if (ShowPassCheckBox.Checked)
            {
                PassTextBox.PasswordChar = '\0'; // Ods³oniêcie has³a
            }
            else
            {
                
                if(isPlaceholderPassTextBox)
                {
                    PassTextBox.PasswordChar = '\0';
                }
                else
                {
                    PassTextBox.PasswordChar = '*'; // Ukrycie has³a gwiazdkami
                }
            }
        }
    }
}