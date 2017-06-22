namespace InternFollowProgramming
{
    partial class FrmHome
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.panel_ust = new System.Windows.Forms.Panel();
            this.label_isim = new System.Windows.Forms.Label();
            this.panel_alt = new System.Windows.Forms.Panel();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.label2_user = new System.Windows.Forms.Label();
            this.label_password = new System.Windows.Forms.Label();
            this.button_login = new System.Windows.Forms.Button();
            this.checkBox_password = new System.Windows.Forms.CheckBox();
            this.button_logout = new System.Windows.Forms.Button();
            this.comboBox_user = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.panel_ust.SuspendLayout();
            this.panel_alt.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::InternFollowProgramming.Properties.Resources.logovektörel1;
            this.pictureBox_logo.Location = new System.Drawing.Point(157, 16);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(126, 69);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            // 
            // panel_ust
            // 
            this.panel_ust.Controls.Add(this.label_isim);
            this.panel_ust.Controls.Add(this.pictureBox_logo);
            this.panel_ust.Location = new System.Drawing.Point(33, 40);
            this.panel_ust.Name = "panel_ust";
            this.panel_ust.Size = new System.Drawing.Size(419, 133);
            this.panel_ust.TabIndex = 1;
            // 
            // label_isim
            // 
            this.label_isim.AutoSize = true;
            this.label_isim.Font = new System.Drawing.Font("Minion Pro", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_isim.Location = new System.Drawing.Point(88, 101);
            this.label_isim.Name = "label_isim";
            this.label_isim.Size = new System.Drawing.Size(255, 18);
            this.label_isim.TabIndex = 1;
            this.label_isim.Text = "INTERN FOLLOW SYSTEM  USER LOGIN ";
            // 
            // panel_alt
            // 
            this.panel_alt.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel_alt.Controls.Add(this.comboBox_user);
            this.panel_alt.Controls.Add(this.button_logout);
            this.panel_alt.Controls.Add(this.checkBox_password);
            this.panel_alt.Controls.Add(this.button_login);
            this.panel_alt.Controls.Add(this.label_password);
            this.panel_alt.Controls.Add(this.label2_user);
            this.panel_alt.Controls.Add(this.textBox_password);
            this.panel_alt.Font = new System.Drawing.Font("Adobe Garamond Pro", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_alt.Location = new System.Drawing.Point(33, 211);
            this.panel_alt.Name = "panel_alt";
            this.panel_alt.Size = new System.Drawing.Size(419, 205);
            this.panel_alt.TabIndex = 2;
            // 
            // textBox_password
            // 
            this.textBox_password.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_password.Location = new System.Drawing.Point(104, 101);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(164, 25);
            this.textBox_password.TabIndex = 1;
            // 
            // label2_user
            // 
            this.label2_user.AutoSize = true;
            this.label2_user.Location = new System.Drawing.Point(18, 58);
            this.label2_user.Name = "label2_user";
            this.label2_user.Size = new System.Drawing.Size(40, 19);
            this.label2_user.TabIndex = 2;
            this.label2_user.Text = "User:";
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(18, 104);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(66, 19);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Password:";
            // 
            // button_login
            // 
            this.button_login.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_login.Location = new System.Drawing.Point(221, 150);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(87, 29);
            this.button_login.TabIndex = 4;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = false;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // checkBox_password
            // 
            this.checkBox_password.AutoSize = true;
            this.checkBox_password.Location = new System.Drawing.Point(286, 104);
            this.checkBox_password.Name = "checkBox_password";
            this.checkBox_password.Size = new System.Drawing.Size(108, 23);
            this.checkBox_password.TabIndex = 5;
            this.checkBox_password.Text = "Şifreyi Göster";
            this.checkBox_password.UseVisualStyleBackColor = true;
            // 
            // button_logout
            // 
            this.button_logout.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_logout.Location = new System.Drawing.Point(319, 150);
            this.button_logout.Name = "button_logout";
            this.button_logout.Size = new System.Drawing.Size(75, 29);
            this.button_logout.TabIndex = 6;
            this.button_logout.Text = "Logout";
            this.button_logout.UseVisualStyleBackColor = false;
            // 
            // comboBox_user
            // 
            this.comboBox_user.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox_user.FormattingEnabled = true;
            this.comboBox_user.Location = new System.Drawing.Point(104, 58);
            this.comboBox_user.Name = "comboBox_user";
            this.comboBox_user.Size = new System.Drawing.Size(164, 27);
            this.comboBox_user.TabIndex = 7;
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(496, 469);
            this.Controls.Add(this.panel_alt);
            this.Controls.Add(this.panel_ust);
            this.Name = "FrmHome";
            this.Text = "INTERN FOLLOW PROGRAMMING";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.panel_ust.ResumeLayout(false);
            this.panel_ust.PerformLayout();
            this.panel_alt.ResumeLayout(false);
            this.panel_alt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.Panel panel_ust;
        private System.Windows.Forms.Label label_isim;
        private System.Windows.Forms.Panel panel_alt;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label2_user;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_logout;
        private System.Windows.Forms.CheckBox checkBox_password;
        private System.Windows.Forms.ComboBox comboBox_user;
    }
}

