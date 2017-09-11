namespace InternFollowProgramming
{
    partial class FrmMail
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
            this.panel_alt = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_gposta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_micerigi = new System.Windows.Forms.TextBox();
            this.textBox_mkonusu = new System.Windows.Forms.TextBox();
            this.button_mailgonder = new System.Windows.Forms.Button();
            this.textBox_aposta = new System.Windows.Forms.TextBox();
            this.textBox_gsifre = new System.Windows.Forms.TextBox();
            this.panel_ust = new System.Windows.Forms.Panel();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.panel_alt.SuspendLayout();
            this.panel_ust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_alt
            // 
            this.panel_alt.BackColor = System.Drawing.Color.Beige;
            this.panel_alt.Controls.Add(this.label5);
            this.panel_alt.Controls.Add(this.label4);
            this.panel_alt.Controls.Add(this.textBox_gposta);
            this.panel_alt.Controls.Add(this.label3);
            this.panel_alt.Controls.Add(this.label2);
            this.panel_alt.Controls.Add(this.label1);
            this.panel_alt.Controls.Add(this.textBox_micerigi);
            this.panel_alt.Controls.Add(this.textBox_mkonusu);
            this.panel_alt.Controls.Add(this.button_mailgonder);
            this.panel_alt.Controls.Add(this.textBox_aposta);
            this.panel_alt.Controls.Add(this.textBox_gsifre);
            this.panel_alt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_alt.Font = new System.Drawing.Font("Adobe Garamond Pro", 11.25F);
            this.panel_alt.Location = new System.Drawing.Point(0, 184);
            this.panel_alt.Name = "panel_alt";
            this.panel_alt.Size = new System.Drawing.Size(533, 371);
            this.panel_alt.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "Gönderici Şifresi:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 19);
            this.label4.TabIndex = 21;
            this.label4.Text = "Gönderici Maili:";
            // 
            // textBox_gposta
            // 
            this.textBox_gposta.Location = new System.Drawing.Point(174, 36);
            this.textBox_gposta.Name = "textBox_gposta";
            this.textBox_gposta.Size = new System.Drawing.Size(296, 25);
            this.textBox_gposta.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 19);
            this.label3.TabIndex = 19;
            this.label3.Text = "Alıcı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 18;
            this.label2.Text = "Mail İçeriği:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 17;
            this.label1.Text = "Mail Konusu:";
            // 
            // textBox_micerigi
            // 
            this.textBox_micerigi.Location = new System.Drawing.Point(174, 176);
            this.textBox_micerigi.Multiline = true;
            this.textBox_micerigi.Name = "textBox_micerigi";
            this.textBox_micerigi.Size = new System.Drawing.Size(296, 143);
            this.textBox_micerigi.TabIndex = 16;
            // 
            // textBox_mkonusu
            // 
            this.textBox_mkonusu.Location = new System.Drawing.Point(174, 141);
            this.textBox_mkonusu.Name = "textBox_mkonusu";
            this.textBox_mkonusu.Size = new System.Drawing.Size(296, 25);
            this.textBox_mkonusu.TabIndex = 15;
            // 
            // button_mailgonder
            // 
            this.button_mailgonder.Location = new System.Drawing.Point(395, 335);
            this.button_mailgonder.Name = "button_mailgonder";
            this.button_mailgonder.Size = new System.Drawing.Size(75, 28);
            this.button_mailgonder.TabIndex = 14;
            this.button_mailgonder.Text = "Gönder";
            this.button_mailgonder.UseVisualStyleBackColor = true;
            this.button_mailgonder.Click += new System.EventHandler(this.button_mailgonder_Click);
            // 
            // textBox_aposta
            // 
            this.textBox_aposta.Location = new System.Drawing.Point(174, 106);
            this.textBox_aposta.Multiline = true;
            this.textBox_aposta.Name = "textBox_aposta";
            this.textBox_aposta.Size = new System.Drawing.Size(296, 23);
            this.textBox_aposta.TabIndex = 13;
            // 
            // textBox_gsifre
            // 
            this.textBox_gsifre.Location = new System.Drawing.Point(174, 71);
            this.textBox_gsifre.Name = "textBox_gsifre";
            this.textBox_gsifre.Size = new System.Drawing.Size(296, 25);
            this.textBox_gsifre.TabIndex = 12;
            // 
            // panel_ust
            // 
            this.panel_ust.BackColor = System.Drawing.Color.Black;
            this.panel_ust.Controls.Add(this.pictureBox_logo);
            this.panel_ust.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_ust.Location = new System.Drawing.Point(0, 0);
            this.panel_ust.Name = "panel_ust";
            this.panel_ust.Size = new System.Drawing.Size(533, 190);
            this.panel_ust.TabIndex = 12;
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::InternFollowProgramming.Properties.Resources.STAJYERLOGO;
            this.pictureBox_logo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox_logo.Location = new System.Drawing.Point(144, 41);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(252, 98);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            // 
            // FrmMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(533, 555);
            this.Controls.Add(this.panel_alt);
            this.Controls.Add(this.panel_ust);
            this.Name = "FrmMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MAİL GÖNDER";
            this.Load += new System.EventHandler(this.FrmMail_Load);
            this.panel_alt.ResumeLayout(false);
            this.panel_alt.PerformLayout();
            this.panel_ust.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_alt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_gposta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_micerigi;
        private System.Windows.Forms.TextBox textBox_mkonusu;
        private System.Windows.Forms.Button button_mailgonder;
        private System.Windows.Forms.TextBox textBox_aposta;
        private System.Windows.Forms.TextBox textBox_gsifre;
        private System.Windows.Forms.Panel panel_ust;
        private System.Windows.Forms.PictureBox pictureBox_logo;
    }
}