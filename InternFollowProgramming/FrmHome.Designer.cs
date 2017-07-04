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
            this.panel_ust = new System.Windows.Forms.Panel();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.panel_alt = new System.Windows.Forms.Panel();
            this.comboBox_user = new System.Windows.Forms.ComboBox();
            this.checkBox_password = new System.Windows.Forms.CheckBox();
            this.button_login = new System.Windows.Forms.Button();
            this.label_password = new System.Windows.Forms.Label();
            this.label2_user = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.panel_ust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.panel_alt.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_ust
            // 
            this.panel_ust.BackColor = System.Drawing.Color.Black;
            this.panel_ust.Controls.Add(this.pictureBox_logo);
            this.panel_ust.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ust.Location = new System.Drawing.Point(0, 0);
            this.panel_ust.Name = "panel_ust";
            this.panel_ust.Size = new System.Drawing.Size(493, 156);
            this.panel_ust.TabIndex = 1;
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::InternFollowProgramming.Properties.Resources.STAJYERLOGO;
            this.pictureBox_logo.Location = new System.Drawing.Point(133, 27);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(252, 98);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            // 
            // panel_alt
            // 
            this.panel_alt.BackColor = System.Drawing.Color.Gold;
            this.panel_alt.Controls.Add(this.comboBox_user);
            this.panel_alt.Controls.Add(this.checkBox_password);
            this.panel_alt.Controls.Add(this.button_login);
            this.panel_alt.Controls.Add(this.label_password);
            this.panel_alt.Controls.Add(this.label2_user);
            this.panel_alt.Controls.Add(this.textBox_password);
            this.panel_alt.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_alt.Font = new System.Drawing.Font("Adobe Garamond Pro", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_alt.Location = new System.Drawing.Point(0, 156);
            this.panel_alt.Name = "panel_alt";
            this.panel_alt.Size = new System.Drawing.Size(493, 254);
            this.panel_alt.TabIndex = 2;
            // 
            // comboBox_user
            // 
            this.comboBox_user.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox_user.FormattingEnabled = true;
            this.comboBox_user.Location = new System.Drawing.Point(173, 58);
            this.comboBox_user.Name = "comboBox_user";
            this.comboBox_user.Size = new System.Drawing.Size(181, 27);
            this.comboBox_user.TabIndex = 7;
            // 
            // checkBox_password
            // 
            this.checkBox_password.AutoSize = true;
            this.checkBox_password.Font = new System.Drawing.Font("Myriad Pro", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_password.Location = new System.Drawing.Point(360, 100);
            this.checkBox_password.Name = "checkBox_password";
            this.checkBox_password.Size = new System.Drawing.Size(88, 17);
            this.checkBox_password.TabIndex = 5;
            this.checkBox_password.Text = "Şifreyi Göster";
            this.checkBox_password.UseVisualStyleBackColor = true;
            this.checkBox_password.CheckedChanged += new System.EventHandler(this.checkBox_password_CheckedChanged);
            // 
            // button_login
            // 
            this.button_login.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button_login.Font = new System.Drawing.Font("Myriad Pro", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_login.Location = new System.Drawing.Point(301, 156);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(147, 29);
            this.button_login.TabIndex = 4;
            this.button_login.Text = "Giriş Yap";
            this.button_login.UseVisualStyleBackColor = false;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Myriad Pro", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_password.Location = new System.Drawing.Point(61, 104);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(39, 18);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Şifre:";
            // 
            // label2_user
            // 
            this.label2_user.AutoSize = true;
            this.label2_user.Font = new System.Drawing.Font("Myriad Pro", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2_user.Location = new System.Drawing.Point(61, 58);
            this.label2_user.Name = "label2_user";
            this.label2_user.Size = new System.Drawing.Size(89, 18);
            this.label2_user.TabIndex = 2;
            this.label2_user.Text = "Kullanıcı Adı:";
            // 
            // textBox_password
            // 
            this.textBox_password.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_password.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBox_password.Location = new System.Drawing.Point(173, 98);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(181, 25);
            this.textBox_password.TabIndex = 1;
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(493, 370);
            this.Controls.Add(this.panel_alt);
            this.Controls.Add(this.panel_ust);
            this.Name = "FrmHome";
            this.Text = "AKE STAJYER TAKİP SİSTEMİ/ GİRİŞ YAP";
            this.panel_ust.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.panel_alt.ResumeLayout(false);
            this.panel_alt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_ust;
        private System.Windows.Forms.Panel panel_alt;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label2_user;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.CheckBox checkBox_password;
        private System.Windows.Forms.ComboBox comboBox_user;
        private System.Windows.Forms.PictureBox pictureBox_logo;
    }
}

