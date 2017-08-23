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
            this.panel_alt = new System.Windows.Forms.Panel();
            this.comboBox_user = new System.Windows.Forms.ComboBox();
            this.label_password = new System.Windows.Forms.Label();
            this.label2_user = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.pictureBox_password = new System.Windows.Forms.PictureBox();
            this.pictureBox_giris = new System.Windows.Forms.PictureBox();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel_ust.SuspendLayout();
            this.panel_alt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_giris)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ust
            // 
            this.panel_ust.BackColor = System.Drawing.Color.Black;
            this.panel_ust.Controls.Add(this.pictureBox_logo);
            this.panel_ust.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ust.Location = new System.Drawing.Point(0, 0);
            this.panel_ust.Name = "panel_ust";
            this.panel_ust.Size = new System.Drawing.Size(428, 156);
            this.panel_ust.TabIndex = 1;
            // 
            // panel_alt
            // 
            this.panel_alt.BackColor = System.Drawing.Color.Gold;
            this.panel_alt.Controls.Add(this.checkBox1);
            this.panel_alt.Controls.Add(this.pictureBox_password);
            this.panel_alt.Controls.Add(this.pictureBox_giris);
            this.panel_alt.Controls.Add(this.comboBox_user);
            this.panel_alt.Controls.Add(this.label_password);
            this.panel_alt.Controls.Add(this.label2_user);
            this.panel_alt.Controls.Add(this.textBox_password);
            this.panel_alt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_alt.Font = new System.Drawing.Font("Adobe Garamond Pro", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_alt.Location = new System.Drawing.Point(0, 155);
            this.panel_alt.Name = "panel_alt";
            this.panel_alt.Size = new System.Drawing.Size(428, 215);
            this.panel_alt.TabIndex = 2;
            // 
            // comboBox_user
            // 
            this.comboBox_user.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox_user.FormattingEnabled = true;
            this.comboBox_user.Location = new System.Drawing.Point(173, 53);
            this.comboBox_user.Name = "comboBox_user";
            this.comboBox_user.Size = new System.Drawing.Size(181, 27);
            this.comboBox_user.TabIndex = 7;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_password.Location = new System.Drawing.Point(61, 104);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(37, 17);
            this.label_password.TabIndex = 3;
            this.label_password.Text = "Şifre:";
            // 
            // label2_user
            // 
            this.label2_user.AutoSize = true;
            this.label2_user.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2_user.Location = new System.Drawing.Point(61, 58);
            this.label2_user.Name = "label2_user";
            this.label2_user.Size = new System.Drawing.Size(78, 17);
            this.label2_user.TabIndex = 2;
            this.label2_user.Text = "Kullanıcı Adı:";
            // 
            // textBox_password
            // 
            this.textBox_password.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_password.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.textBox_password.Location = new System.Drawing.Point(173, 100);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(181, 25);
            this.textBox_password.TabIndex = 1;
            // 
            // pictureBox_password
            // 
            this.pictureBox_password.Image = global::InternFollowProgramming.Properties.Resources.gizle;
            this.pictureBox_password.Location = new System.Drawing.Point(360, 102);
            this.pictureBox_password.Name = "pictureBox_password";
            this.pictureBox_password.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_password.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_password.TabIndex = 9;
            this.pictureBox_password.TabStop = false;
            this.pictureBox_password.Click += new System.EventHandler(this.pictureBox_password_Click);
            // 
            // pictureBox_giris
            // 
            this.pictureBox_giris.Image = global::InternFollowProgramming.Properties.Resources.Giriş;
            this.pictureBox_giris.Location = new System.Drawing.Point(64, 148);
            this.pictureBox_giris.Name = "pictureBox_giris";
            this.pictureBox_giris.Size = new System.Drawing.Size(290, 29);
            this.pictureBox_giris.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_giris.TabIndex = 8;
            this.pictureBox_giris.TabStop = false;
            this.pictureBox_giris.Click += new System.EventHandler(this.pictureBox_giris_Click);
            this.pictureBox_giris.MouseLeave += new System.EventHandler(this.pictureBox_giris_MouseLeave);
            this.pictureBox_giris.MouseHover += new System.EventHandler(this.pictureBox_giris_MouseHover);
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::InternFollowProgramming.Properties.Resources.STAJYERLOGO;
            this.pictureBox_logo.Location = new System.Drawing.Point(88, 27);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(252, 98);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackgroundImage = global::InternFollowProgramming.Properties.Resources.gizle;
            this.checkBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.checkBox1.Location = new System.Drawing.Point(401, 107);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(428, 370);
            this.Controls.Add(this.panel_alt);
            this.Controls.Add(this.panel_ust);
            this.Name = "FrmHome";
            this.Text = "AKE STAJYER TAKİP SİSTEMİ/ GİRİŞ YAP";
            this.panel_ust.ResumeLayout(false);
            this.panel_alt.ResumeLayout(false);
            this.panel_alt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_giris)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_ust;
        private System.Windows.Forms.Panel panel_alt;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label2_user;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.ComboBox comboBox_user;
        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.PictureBox pictureBox_password;
        private System.Windows.Forms.PictureBox pictureBox_giris;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

