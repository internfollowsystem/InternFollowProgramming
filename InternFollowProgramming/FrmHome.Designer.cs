﻿namespace InternFollowProgramming
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            this.panel_ust = new System.Windows.Forms.Panel();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.panel_alt = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_visible = new System.Windows.Forms.CheckBox();
            this.pictureBox_password = new System.Windows.Forms.PictureBox();
            this.pictureBox_giris = new System.Windows.Forms.PictureBox();
            this.comboBox_user = new System.Windows.Forms.ComboBox();
            this.label_password = new System.Windows.Forms.Label();
            this.label2_user = new System.Windows.Forms.Label();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.panel_ust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.panel_alt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_giris)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ust
            // 
            this.panel_ust.BackColor = System.Drawing.Color.Black;
            this.panel_ust.Controls.Add(this.pictureBox_logo);
            resources.ApplyResources(this.panel_ust, "panel_ust");
            this.panel_ust.Name = "panel_ust";
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::InternFollowProgramming.Properties.Resources.STAJYERLOGO;
            resources.ApplyResources(this.pictureBox_logo, "pictureBox_logo");
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.TabStop = false;
            // 
            // panel_alt
            // 
            this.panel_alt.BackColor = System.Drawing.Color.Gold;
            this.panel_alt.Controls.Add(this.label2);
            this.panel_alt.Controls.Add(this.label1);
            this.panel_alt.Controls.Add(this.checkBox_visible);
            this.panel_alt.Controls.Add(this.pictureBox_password);
            this.panel_alt.Controls.Add(this.pictureBox_giris);
            this.panel_alt.Controls.Add(this.comboBox_user);
            this.panel_alt.Controls.Add(this.label_password);
            this.panel_alt.Controls.Add(this.label2_user);
            this.panel_alt.Controls.Add(this.textBox_password);
            resources.ApplyResources(this.panel_alt, "panel_alt");
            this.panel_alt.Name = "panel_alt";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // checkBox_visible
            // 
            resources.ApplyResources(this.checkBox_visible, "checkBox_visible");
            this.checkBox_visible.BackgroundImage = global::InternFollowProgramming.Properties.Resources.gizle;
            this.checkBox_visible.Name = "checkBox_visible";
            this.checkBox_visible.UseVisualStyleBackColor = true;
            this.checkBox_visible.CheckedChanged += new System.EventHandler(this.checkBox_visible_CheckedChanged);
            // 
            // pictureBox_password
            // 
            this.pictureBox_password.Image = global::InternFollowProgramming.Properties.Resources.gizle1;
            resources.ApplyResources(this.pictureBox_password, "pictureBox_password");
            this.pictureBox_password.Name = "pictureBox_password";
            this.pictureBox_password.TabStop = false;
            // 
            // pictureBox_giris
            // 
            this.pictureBox_giris.Image = global::InternFollowProgramming.Properties.Resources.Giriş;
            resources.ApplyResources(this.pictureBox_giris, "pictureBox_giris");
            this.pictureBox_giris.Name = "pictureBox_giris";
            this.pictureBox_giris.TabStop = false;
            this.pictureBox_giris.Click += new System.EventHandler(this.pictureBox_giris_Click);
            this.pictureBox_giris.MouseLeave += new System.EventHandler(this.pictureBox_giris_MouseLeave);
            this.pictureBox_giris.MouseHover += new System.EventHandler(this.pictureBox_giris_MouseHover);
            // 
            // comboBox_user
            // 
            this.comboBox_user.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox_user.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_user, "comboBox_user");
            this.comboBox_user.Name = "comboBox_user";
            // 
            // label_password
            // 
            resources.ApplyResources(this.label_password, "label_password");
            this.label_password.Name = "label_password";
            // 
            // label2_user
            // 
            resources.ApplyResources(this.label2_user, "label2_user");
            this.label2_user.Name = "label2_user";
            // 
            // textBox_password
            // 
            this.textBox_password.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.textBox_password, "textBox_password");
            this.textBox_password.Name = "textBox_password";
            // 
            // FrmHome
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel_alt);
            this.Controls.Add(this.panel_ust);
            this.Name = "FrmHome";
            this.panel_ust.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.panel_alt.ResumeLayout(false);
            this.panel_alt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_giris)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBox_visible;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

