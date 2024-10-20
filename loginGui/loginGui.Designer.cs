namespace loginGui
{
    partial class loginSreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginSreen));
            LoginLabel = new Label();
            EmailTextBox = new TextBox();
            PassTextBox = new TextBox();
            button1 = new Button();
            ShowPassCheckBox = new CheckBox();
            LackAccountLabel = new Label();
            InvalidEmailLabel = new Label();
            CloseButton = new Button();
            QuizzyLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)QuizzyLogo).BeginInit();
            SuspendLayout();
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.BackColor = Color.Transparent;
            LoginLabel.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, 238);
            LoginLabel.ForeColor = Color.FromArgb(252, 252, 252);
            LoginLabel.Location = new Point(960, 143);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(181, 70);
            LoginLabel.TabIndex = 0;
            LoginLabel.Text = "Log in";
            // 
            // EmailTextBox
            // 
            EmailTextBox.BackColor = Color.FromArgb(24, 24, 24);
            EmailTextBox.BorderStyle = BorderStyle.FixedSingle;
            EmailTextBox.Location = new Point(926, 252);
            EmailTextBox.Multiline = true;
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.Size = new Size(247, 40);
            EmailTextBox.TabIndex = 1;
            // 
            // PassTextBox
            // 
            PassTextBox.BackColor = Color.FromArgb(24, 24, 24);
            PassTextBox.BorderStyle = BorderStyle.FixedSingle;
            PassTextBox.Location = new Point(926, 325);
            PassTextBox.Multiline = true;
            PassTextBox.Name = "PassTextBox";
            PassTextBox.Size = new Size(247, 40);
            PassTextBox.TabIndex = 2;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(79, 242, 151);
            button1.FlatAppearance.BorderColor = Color.FromArgb(1, 116, 221);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1.ForeColor = Color.FromArgb(4, 88, 64);
            button1.Location = new Point(970, 419);
            button1.Name = "button1";
            button1.Size = new Size(162, 77);
            button1.TabIndex = 3;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // ShowPassCheckBox
            // 
            ShowPassCheckBox.AutoSize = true;
            ShowPassCheckBox.Cursor = Cursors.Hand;
            ShowPassCheckBox.ForeColor = Color.FromArgb(252, 252, 252);
            ShowPassCheckBox.Location = new Point(966, 373);
            ShowPassCheckBox.Name = "ShowPassCheckBox";
            ShowPassCheckBox.Size = new Size(180, 32);
            ShowPassCheckBox.TabIndex = 5;
            ShowPassCheckBox.Text = "Show password";
            ShowPassCheckBox.UseVisualStyleBackColor = true;
            ShowPassCheckBox.CheckedChanged += ShowPassCheckBox_CheckedChanged;
            // 
            // LackAccountLabel
            // 
            LackAccountLabel.AutoSize = true;
            LackAccountLabel.Cursor = Cursors.Hand;
            LackAccountLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            LackAccountLabel.ForeColor = Color.FromArgb(131, 131, 131);
            LackAccountLabel.Location = new Point(941, 510);
            LackAccountLabel.Name = "LackAccountLabel";
            LackAccountLabel.Size = new Size(215, 28);
            LackAccountLabel.TabIndex = 14;
            LackAccountLabel.Text = "Don't have an account";
            LackAccountLabel.Click += LackAccountLabel_Click;
            // 
            // InvalidEmailLabel
            // 
            InvalidEmailLabel.AutoSize = true;
            InvalidEmailLabel.ForeColor = Color.FromArgb(192, 0, 0);
            InvalidEmailLabel.Location = new Point(987, 294);
            InvalidEmailLabel.Name = "InvalidEmailLabel";
            InvalidEmailLabel.Size = new Size(127, 28);
            InvalidEmailLabel.TabIndex = 15;
            InvalidEmailLabel.Text = "Invalid email";
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.FromArgb(36, 36, 36);
            CloseButton.BackgroundImage = (Image)resources.GetObject("CloseButton.BackgroundImage");
            CloseButton.BackgroundImageLayout = ImageLayout.Zoom;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Location = new Point(1278, -1);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(50, 50);
            CloseButton.TabIndex = 19;
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // QuizzyLogo
            // 
            QuizzyLogo.Image = (Image)resources.GetObject("QuizzyLogo.Image");
            QuizzyLogo.Location = new Point(62, 75);
            QuizzyLogo.Name = "QuizzyLogo";
            QuizzyLogo.Size = new Size(719, 583);
            QuizzyLogo.SizeMode = PictureBoxSizeMode.Zoom;
            QuizzyLogo.TabIndex = 20;
            QuizzyLogo.TabStop = false;
            // 
            // loginSreen
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(1328, 778);
            Controls.Add(QuizzyLogo);
            Controls.Add(CloseButton);
            Controls.Add(InvalidEmailLabel);
            Controls.Add(LackAccountLabel);
            Controls.Add(ShowPassCheckBox);
            Controls.Add(button1);
            Controls.Add(PassTextBox);
            Controls.Add(EmailTextBox);
            Controls.Add(LoginLabel);
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "loginSreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)QuizzyLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LoginLabel;
        private TextBox EmailTextBox;
        private TextBox PassTextBox;
        private Button button1;
        private CheckBox ShowPassCheckBox;
        private Label LackAccountLabel;
        private Label InvalidEmailLabel;
        private Button CloseButton;
        private PictureBox QuizzyLogo;
    }
}
