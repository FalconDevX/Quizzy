namespace loginGui
{
    partial class registerGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(registerGui));
            YesAccountLabel = new Label();
            ShowPassCheckBox = new CheckBox();
            RegisterButton = new Button();
            PassTextBox = new TextBox();
            EmailTextBox = new TextBox();
            RegisterLabel = new Label();
            RepPassTextBox = new TextBox();
            InvalidEmailLabel = new Label();
            PassNotMatchLabel = new Label();
            CloseButton = new Button();
            QuizzyLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)QuizzyLogo).BeginInit();
            SuspendLayout();
            // 
            // YesAccountLabel
            // 
            YesAccountLabel.AutoSize = true;
            YesAccountLabel.Cursor = Cursors.Hand;
            YesAccountLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            YesAccountLabel.ForeColor = Color.FromArgb(131, 131, 131);
            YesAccountLabel.Location = new Point(962, 597);
            YesAccountLabel.Name = "YesAccountLabel";
            YesAccountLabel.Size = new Size(163, 28);
            YesAccountLabel.TabIndex = 13;
            YesAccountLabel.Text = "Have an account";
            YesAccountLabel.Click += YesAccountLabel_Click;
            // 
            // ShowPassCheckBox
            // 
            ShowPassCheckBox.AutoSize = true;
            ShowPassCheckBox.Cursor = Cursors.Hand;
            ShowPassCheckBox.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            ShowPassCheckBox.ForeColor = Color.FromArgb(252, 252, 252);
            ShowPassCheckBox.Location = new Point(958, 461);
            ShowPassCheckBox.Name = "ShowPassCheckBox";
            ShowPassCheckBox.Size = new Size(180, 32);
            ShowPassCheckBox.TabIndex = 12;
            ShowPassCheckBox.Text = "Show password";
            ShowPassCheckBox.UseVisualStyleBackColor = true;
            ShowPassCheckBox.CheckedChanged += ShowPassCheckBox_CheckedChanged;
            // 
            // RegisterButton
            // 
            RegisterButton.BackColor = Color.FromArgb(82, 242, 138);
            RegisterButton.FlatAppearance.BorderColor = Color.FromArgb(1, 116, 221);
            RegisterButton.FlatAppearance.BorderSize = 0;
            RegisterButton.FlatStyle = FlatStyle.Flat;
            RegisterButton.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RegisterButton.ForeColor = Color.FromArgb(4, 88, 64);
            RegisterButton.Location = new Point(962, 507);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(162, 77);
            RegisterButton.TabIndex = 10;
            RegisterButton.Text = "Register";
            RegisterButton.UseVisualStyleBackColor = false;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // PassTextBox
            // 
            PassTextBox.BackColor = Color.FromArgb(24, 24, 24);
            PassTextBox.BorderStyle = BorderStyle.FixedSingle;
            PassTextBox.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            PassTextBox.Location = new Point(924, 305);
            PassTextBox.Multiline = true;
            PassTextBox.Name = "PassTextBox";
            PassTextBox.Size = new Size(247, 40);
            PassTextBox.TabIndex = 9;
            // 
            // EmailTextBox
            // 
            EmailTextBox.BackColor = Color.FromArgb(24, 24, 24);
            EmailTextBox.BorderStyle = BorderStyle.FixedSingle;
            EmailTextBox.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            EmailTextBox.Location = new Point(924, 232);
            EmailTextBox.Multiline = true;
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.Size = new Size(247, 40);
            EmailTextBox.TabIndex = 8;
            // 
            // RegisterLabel
            // 
            RegisterLabel.AutoSize = true;
            RegisterLabel.BackColor = Color.Transparent;
            RegisterLabel.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RegisterLabel.ForeColor = Color.FromArgb(252, 252, 252);
            RegisterLabel.Location = new Point(935, 143);
            RegisterLabel.Name = "RegisterLabel";
            RegisterLabel.Size = new Size(230, 70);
            RegisterLabel.TabIndex = 7;
            RegisterLabel.Text = "Register";
            // 
            // RepPassTextBox
            // 
            RepPassTextBox.BackColor = Color.FromArgb(24, 24, 24);
            RepPassTextBox.BorderStyle = BorderStyle.FixedSingle;
            RepPassTextBox.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            RepPassTextBox.Location = new Point(924, 372);
            RepPassTextBox.Multiline = true;
            RepPassTextBox.Name = "RepPassTextBox";
            RepPassTextBox.Size = new Size(247, 40);
            RepPassTextBox.TabIndex = 14;
            // 
            // InvalidEmailLabel
            // 
            InvalidEmailLabel.AutoSize = true;
            InvalidEmailLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            InvalidEmailLabel.ForeColor = Color.FromArgb(192, 0, 0);
            InvalidEmailLabel.Location = new Point(982, 274);
            InvalidEmailLabel.Name = "InvalidEmailLabel";
            InvalidEmailLabel.Size = new Size(127, 28);
            InvalidEmailLabel.TabIndex = 16;
            InvalidEmailLabel.Text = "Invalid email";
            // 
            // PassNotMatchLabel
            // 
            PassNotMatchLabel.AutoSize = true;
            PassNotMatchLabel.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 238);
            PassNotMatchLabel.ForeColor = Color.FromArgb(192, 0, 0);
            PassNotMatchLabel.Location = new Point(935, 415);
            PassNotMatchLabel.Name = "PassNotMatchLabel";
            PassNotMatchLabel.Size = new Size(226, 28);
            PassNotMatchLabel.TabIndex = 17;
            PassNotMatchLabel.Text = "Password do not match";
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
            CloseButton.TabIndex = 18;
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
            QuizzyLogo.TabIndex = 21;
            QuizzyLogo.TabStop = false;
            // 
            // registerGui
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 15, 15);
            ClientSize = new Size(1328, 778);
            Controls.Add(QuizzyLogo);
            Controls.Add(CloseButton);
            Controls.Add(PassNotMatchLabel);
            Controls.Add(InvalidEmailLabel);
            Controls.Add(RepPassTextBox);
            Controls.Add(YesAccountLabel);
            Controls.Add(ShowPassCheckBox);
            Controls.Add(RegisterButton);
            Controls.Add(PassTextBox);
            Controls.Add(EmailTextBox);
            Controls.Add(RegisterLabel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "registerGui";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "registerGui";
            ((System.ComponentModel.ISupportInitialize)QuizzyLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label YesAccountLabel;
        private CheckBox ShowPassCheckBox;
        private Button RegisterButton;
        private TextBox PassTextBox;
        private TextBox EmailTextBox;
        private Label RegisterLabel;
        private TextBox RepPassTextBox;
        private Label InvalidEmailLabel;
        private Label PassNotMatchLabel;
        private Button CloseButton;
        private PictureBox QuizzyLogo;
    }
}