namespace loginGui
{
    public partial class loginSreen : Form
    {
        private bool isPlaceholderTextBox1, isPlaceholderTextBox2 = true;

        public loginSreen()
        {
            InitializeComponent();

            LoginLabel.Select();

            SetPlaceholderTextBox1(this, EventArgs.Empty);
            SetPlaceholderTextBox2(this, EventArgs.Empty);

            EmailTextBox.GotFocus += RemovePlaceholderTextBox1;
            EmailTextBox.LostFocus += SetPlaceholderTextBox1;

            PassTextBox.GotFocus += RemovePlaceholderTextBox2;
            PassTextBox.LostFocus += SetPlaceholderTextBox2;

        }

        private void SetPlaceholderTextBox1(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailTextBox.Text = "Enter your email";
                EmailTextBox.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderTextBox1 = true;
            }
        }

        private void RemovePlaceholderTextBox1(object? sender, EventArgs s)
        {
            if (isPlaceholderTextBox1)
            {
                EmailTextBox.Text = "";
                EmailTextBox.ForeColor = Color.White;
                isPlaceholderTextBox1 = false;
            }
        }

        private void SetPlaceholderTextBox2(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PassTextBox.Text))
            {
                PassTextBox.Text = "Enter your password";
                PassTextBox.ForeColor = ColorTranslator.FromHtml("#656565");
                isPlaceholderTextBox2 = true;
            }
        }

        private void RemovePlaceholderTextBox2(object? sender, EventArgs s)
        {
            if (isPlaceholderTextBox2)
            {
                PassTextBox.Text = "";
                PassTextBox.ForeColor = Color.White;
                isPlaceholderTextBox2 = false;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}