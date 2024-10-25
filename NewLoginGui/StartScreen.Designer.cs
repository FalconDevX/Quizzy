namespace NewLoginGui
{
    partial class StartScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            RegisterPanel = new Panel();
            ShowPassCheckBoxRegister = new CheckBox();
            RegisterButton = new Button();
            PassNotMatchLabelRegister = new Label();
            InvalidEmailLabelRegister = new Label();
            RepPassTextBoxRegister = new TextBox();
            PassTextBoxRegister = new TextBox();
            EmailTextBoxRegister = new TextBox();
            RegisterLabel = new Label();
            HaveAccountLabel = new Label();
            LoginPanel = new Panel();
            LoginLabel = new Label();
            LoginButton = new Button();
            ShowPassCheckBoxLogin = new CheckBox();
            DontHaveAccountLabel = new Label();
            InvalidEmailLabelLogin = new Label();
            EmailTextBoxLogin = new TextBox();
            PassTextBoxLogin = new TextBox();
            CloseButton = new Button();
            MinimizeButton = new Button();
            RegisterPanel.SuspendLayout();
            LoginPanel.SuspendLayout();
            SuspendLayout();
            // 
            // RegisterPanel
            // 
            RegisterPanel.BackColor = Color.Transparent;
            RegisterPanel.Controls.Add(ShowPassCheckBoxRegister);
            RegisterPanel.Controls.Add(RegisterButton);
            RegisterPanel.Controls.Add(PassNotMatchLabelRegister);
            RegisterPanel.Controls.Add(InvalidEmailLabelRegister);
            RegisterPanel.Controls.Add(RepPassTextBoxRegister);
            RegisterPanel.Controls.Add(PassTextBoxRegister);
            RegisterPanel.Controls.Add(EmailTextBoxRegister);
            RegisterPanel.Controls.Add(RegisterLabel);
            RegisterPanel.Controls.Add(HaveAccountLabel);
            RegisterPanel.ForeColor = Color.Black;
            RegisterPanel.Location = new Point(870, 56);
            RegisterPanel.Name = "RegisterPanel";
            RegisterPanel.Size = new Size(331, 588);
            RegisterPanel.TabIndex = 0;
            // 
            // ShowPassCheckBoxRegister
            // 
            ShowPassCheckBoxRegister.AutoSize = true;
            ShowPassCheckBoxRegister.Cursor = Cursors.Hand;
            ShowPassCheckBoxRegister.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            ShowPassCheckBoxRegister.ForeColor = Color.FromArgb(252, 252, 252);
            ShowPassCheckBoxRegister.Location = new Point(64, 381);
            ShowPassCheckBoxRegister.Name = "ShowPassCheckBoxRegister";
            ShowPassCheckBoxRegister.Size = new Size(180, 32);
            ShowPassCheckBoxRegister.TabIndex = 20;
            ShowPassCheckBoxRegister.Text = "Show password";
            ShowPassCheckBoxRegister.UseVisualStyleBackColor = true;
            // 
            // RegisterButton
            // 
            RegisterButton.BackColor = Color.FromArgb(82, 242, 138);
            RegisterButton.FlatAppearance.BorderColor = Color.FromArgb(1, 116, 221);
            RegisterButton.FlatAppearance.BorderSize = 0;
            RegisterButton.FlatStyle = FlatStyle.Flat;
            RegisterButton.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RegisterButton.ForeColor = Color.FromArgb(4, 88, 64);
            RegisterButton.Location = new Point(76, 431);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(162, 77);
            RegisterButton.TabIndex = 19;
            RegisterButton.Text = "Register";
            RegisterButton.UseVisualStyleBackColor = false;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // PassNotMatchLabelRegister
            // 
            PassNotMatchLabelRegister.AutoSize = true;
            PassNotMatchLabelRegister.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            PassNotMatchLabelRegister.ForeColor = Color.FromArgb(192, 0, 0);
            PassNotMatchLabelRegister.Location = new Point(46, 346);
            PassNotMatchLabelRegister.Name = "PassNotMatchLabelRegister";
            PassNotMatchLabelRegister.Size = new Size(226, 28);
            PassNotMatchLabelRegister.TabIndex = 18;
            PassNotMatchLabelRegister.Text = "Password do not match";
            // 
            // InvalidEmailLabelRegister
            // 
            InvalidEmailLabelRegister.AutoSize = true;
            InvalidEmailLabelRegister.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            InvalidEmailLabelRegister.ForeColor = Color.FromArgb(192, 0, 0);
            InvalidEmailLabelRegister.Location = new Point(96, 192);
            InvalidEmailLabelRegister.Name = "InvalidEmailLabelRegister";
            InvalidEmailLabelRegister.Size = new Size(127, 28);
            InvalidEmailLabelRegister.TabIndex = 17;
            InvalidEmailLabelRegister.Text = "Invalid email";
            // 
            // RepPassTextBoxRegister
            // 
            RepPassTextBoxRegister.BackColor = Color.FromArgb(24, 24, 24);
            RepPassTextBoxRegister.BorderStyle = BorderStyle.FixedSingle;
            RepPassTextBoxRegister.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RepPassTextBoxRegister.Location = new Point(37, 302);
            RepPassTextBoxRegister.Multiline = true;
            RepPassTextBoxRegister.Name = "RepPassTextBoxRegister";
            RepPassTextBoxRegister.Size = new Size(247, 40);
            RepPassTextBoxRegister.TabIndex = 15;
            // 
            // PassTextBoxRegister
            // 
            PassTextBoxRegister.BackColor = Color.FromArgb(24, 24, 24);
            PassTextBoxRegister.BorderStyle = BorderStyle.FixedSingle;
            PassTextBoxRegister.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            PassTextBoxRegister.Location = new Point(37, 229);
            PassTextBoxRegister.Multiline = true;
            PassTextBoxRegister.Name = "PassTextBoxRegister";
            PassTextBoxRegister.Size = new Size(247, 40);
            PassTextBoxRegister.TabIndex = 10;
            // 
            // EmailTextBoxRegister
            // 
            EmailTextBoxRegister.BackColor = Color.FromArgb(24, 24, 24);
            EmailTextBoxRegister.BorderStyle = BorderStyle.FixedSingle;
            EmailTextBoxRegister.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            EmailTextBoxRegister.Location = new Point(37, 150);
            EmailTextBoxRegister.Multiline = true;
            EmailTextBoxRegister.Name = "EmailTextBoxRegister";
            EmailTextBoxRegister.Size = new Size(247, 40);
            EmailTextBoxRegister.TabIndex = 9;
            // 
            // RegisterLabel
            // 
            RegisterLabel.AutoSize = true;
            RegisterLabel.BackColor = Color.Transparent;
            RegisterLabel.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RegisterLabel.ForeColor = Color.FromArgb(252, 252, 252);
            RegisterLabel.Location = new Point(47, 44);
            RegisterLabel.Name = "RegisterLabel";
            RegisterLabel.Size = new Size(230, 70);
            RegisterLabel.TabIndex = 8;
            RegisterLabel.Text = "Register";
            // 
            // HaveAccountLabel
            // 
            HaveAccountLabel.AutoSize = true;
            HaveAccountLabel.Cursor = Cursors.Hand;
            HaveAccountLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            HaveAccountLabel.ForeColor = SystemColors.ButtonFace;
            HaveAccountLabel.Location = new Point(76, 529);
            HaveAccountLabel.Name = "HaveAccountLabel";
            HaveAccountLabel.Size = new Size(163, 28);
            HaveAccountLabel.TabIndex = 1;
            HaveAccountLabel.Text = "Have an account";
            HaveAccountLabel.Click += HaveAccountLabel_Click;
            // 
            // LoginPanel
            // 
            LoginPanel.BackColor = Color.Transparent;
            LoginPanel.BackgroundImage = (Image)resources.GetObject("LoginPanel.BackgroundImage");
            LoginPanel.Controls.Add(LoginLabel);
            LoginPanel.Controls.Add(LoginButton);
            LoginPanel.Controls.Add(ShowPassCheckBoxLogin);
            LoginPanel.Controls.Add(DontHaveAccountLabel);
            LoginPanel.Controls.Add(InvalidEmailLabelLogin);
            LoginPanel.Controls.Add(EmailTextBoxLogin);
            LoginPanel.Controls.Add(PassTextBoxLogin);
            LoginPanel.ForeColor = Color.Black;
            LoginPanel.Location = new Point(862, 59);
            LoginPanel.Name = "LoginPanel";
            LoginPanel.Size = new Size(346, 558);
            LoginPanel.TabIndex = 20;
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.BackColor = Color.Transparent;
            LoginLabel.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, 238);
            LoginLabel.ForeColor = Color.FromArgb(252, 252, 252);
            LoginLabel.Location = new Point(80, 41);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(181, 70);
            LoginLabel.TabIndex = 19;
            LoginLabel.Text = "Log in";
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.FromArgb(79, 242, 151);
            LoginButton.FlatAppearance.BorderColor = Color.FromArgb(1, 116, 221);
            LoginButton.FlatAppearance.BorderSize = 0;
            LoginButton.FlatStyle = FlatStyle.Flat;
            LoginButton.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 238);
            LoginButton.ForeColor = Color.FromArgb(4, 88, 64);
            LoginButton.Location = new Point(87, 336);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(162, 77);
            LoginButton.TabIndex = 18;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = false;
            LoginButton.Click += LoginButton_Click;
            // 
            // ShowPassCheckBoxLogin
            // 
            ShowPassCheckBoxLogin.AutoSize = true;
            ShowPassCheckBoxLogin.Cursor = Cursors.Hand;
            ShowPassCheckBoxLogin.ForeColor = Color.FromArgb(252, 252, 252);
            ShowPassCheckBoxLogin.Location = new Point(89, 278);
            ShowPassCheckBoxLogin.Name = "ShowPassCheckBoxLogin";
            ShowPassCheckBoxLogin.Size = new Size(164, 29);
            ShowPassCheckBoxLogin.TabIndex = 17;
            ShowPassCheckBoxLogin.Text = "Show password";
            ShowPassCheckBoxLogin.UseVisualStyleBackColor = true;
            // 
            // DontHaveAccountLabel
            // 
            DontHaveAccountLabel.AutoSize = true;
            DontHaveAccountLabel.Cursor = Cursors.Hand;
            DontHaveAccountLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            DontHaveAccountLabel.ForeColor = SystemColors.ButtonFace;
            DontHaveAccountLabel.Location = new Point(61, 441);
            DontHaveAccountLabel.Name = "DontHaveAccountLabel";
            DontHaveAccountLabel.Size = new Size(215, 28);
            DontHaveAccountLabel.TabIndex = 1;
            DontHaveAccountLabel.Text = "Don't have an account";
            DontHaveAccountLabel.Click += DontHaveAccountLabel_Click;
            // 
            // InvalidEmailLabelLogin
            // 
            InvalidEmailLabelLogin.AutoSize = true;
            InvalidEmailLabelLogin.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            InvalidEmailLabelLogin.ForeColor = Color.FromArgb(192, 0, 0);
            InvalidEmailLabelLogin.Location = new Point(108, 188);
            InvalidEmailLabelLogin.Name = "InvalidEmailLabelLogin";
            InvalidEmailLabelLogin.Size = new Size(127, 28);
            InvalidEmailLabelLogin.TabIndex = 16;
            InvalidEmailLabelLogin.Text = "Invalid email";
            // 
            // EmailTextBoxLogin
            // 
            EmailTextBoxLogin.BackColor = Color.FromArgb(24, 24, 24);
            EmailTextBoxLogin.BorderStyle = BorderStyle.FixedSingle;
            EmailTextBoxLogin.ForeColor = SystemColors.Menu;
            EmailTextBoxLogin.Location = new Point(48, 146);
            EmailTextBoxLogin.Multiline = true;
            EmailTextBoxLogin.Name = "EmailTextBoxLogin";
            EmailTextBoxLogin.Size = new Size(247, 40);
            EmailTextBoxLogin.TabIndex = 2;
            // 
            // PassTextBoxLogin
            // 
            PassTextBoxLogin.BackColor = Color.FromArgb(24, 24, 24);
            PassTextBoxLogin.BorderStyle = BorderStyle.FixedSingle;
            PassTextBoxLogin.Location = new Point(48, 222);
            PassTextBoxLogin.Multiline = true;
            PassTextBoxLogin.Name = "PassTextBoxLogin";
            PassTextBoxLogin.Size = new Size(247, 40);
            PassTextBoxLogin.TabIndex = 3;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = Color.FromArgb(36, 36, 36);
            CloseButton.BackgroundImage = (Image)resources.GetObject("CloseButton.BackgroundImage");
            CloseButton.BackgroundImageLayout = ImageLayout.Zoom;
            CloseButton.FlatAppearance.BorderSize = 0;
            CloseButton.FlatStyle = FlatStyle.Flat;
            CloseButton.Location = new Point(1172, 0);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(50, 50);
            CloseButton.TabIndex = 20;
            CloseButton.UseVisualStyleBackColor = false;
            CloseButton.Click += CloseButton_Click;
            // 
            // MinimizeButton
            // 
            MinimizeButton.BackColor = Color.FromArgb(36, 36, 36);
            MinimizeButton.BackgroundImage = (Image)resources.GetObject("MinimizeButton.BackgroundImage");
            MinimizeButton.BackgroundImageLayout = ImageLayout.Zoom;
            MinimizeButton.FlatAppearance.BorderSize = 0;
            MinimizeButton.FlatStyle = FlatStyle.Flat;
            MinimizeButton.Location = new Point(1122, 0);
            MinimizeButton.Name = "MinimizeButton";
            MinimizeButton.Size = new Size(50, 50);
            MinimizeButton.TabIndex = 22;
            MinimizeButton.UseVisualStyleBackColor = false;
            MinimizeButton.Click += MinimizeButton_Click;
            // 
            // StartScreen
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(7, 13, 13);
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1220, 690);
            Controls.Add(CloseButton);
            Controls.Add(MinimizeButton);
            Controls.Add(LoginPanel);
            Controls.Add(RegisterPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "StartScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "StartScreen";
            RegisterPanel.ResumeLayout(false);
            RegisterPanel.PerformLayout();
            LoginPanel.ResumeLayout(false);
            LoginPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel RegisterPanel;
        private Label HaveAccountLabel;
        private Label DontHaveAccountLabel;
        private Label LoginLabel;
        private Button LoginButton;
        private CheckBox ShowPassCheckBoxLogin;
        private Label InvalidEmailLabelLogin;
        private TextBox PassTextBoxLogin;
        private TextBox EmailTextBoxLogin;
        private TextBox EmailTextBoxRegister;
        private Label RegisterLabel;
        private Button RegisterButton;
        private Label PassNotMatchLabelRegister;
        private Label InvalidEmailLabelRegister;
        private TextBox RepPassTextBoxRegister;
        private TextBox PassTextBoxRegister;
        private CheckBox ShowPassCheckBoxRegister;
        private Button CloseButton;
        private Button MinimizeButton;
        private Panel LoginPanel;
    }
}
