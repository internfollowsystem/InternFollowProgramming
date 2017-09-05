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
            this.textBox_gsifre = new System.Windows.Forms.TextBox();
            this.textBox_aposta = new System.Windows.Forms.TextBox();
            this.button_mailgonder = new System.Windows.Forms.Button();
            this.textBox_mkonusu = new System.Windows.Forms.TextBox();
            this.textBox_micerigi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_gposta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_gsifre
            // 
            this.textBox_gsifre.Location = new System.Drawing.Point(186, 60);
            this.textBox_gsifre.Name = "textBox_gsifre";
            this.textBox_gsifre.Size = new System.Drawing.Size(296, 20);
            this.textBox_gsifre.TabIndex = 0;
            // 
            // textBox_aposta
            // 
            this.textBox_aposta.Location = new System.Drawing.Point(186, 95);
            this.textBox_aposta.Multiline = true;
            this.textBox_aposta.Name = "textBox_aposta";
            this.textBox_aposta.Size = new System.Drawing.Size(296, 20);
            this.textBox_aposta.TabIndex = 2;
            // 
            // button_mailgonder
            // 
            this.button_mailgonder.Location = new System.Drawing.Point(407, 375);
            this.button_mailgonder.Name = "button_mailgonder";
            this.button_mailgonder.Size = new System.Drawing.Size(75, 23);
            this.button_mailgonder.TabIndex = 3;
            this.button_mailgonder.Text = "button1";
            this.button_mailgonder.UseVisualStyleBackColor = true;
            this.button_mailgonder.Click += new System.EventHandler(this.button_mailgonder_Click);
            // 
            // textBox_mkonusu
            // 
            this.textBox_mkonusu.Location = new System.Drawing.Point(186, 130);
            this.textBox_mkonusu.Name = "textBox_mkonusu";
            this.textBox_mkonusu.Size = new System.Drawing.Size(296, 20);
            this.textBox_mkonusu.TabIndex = 4;
            // 
            // textBox_micerigi
            // 
            this.textBox_micerigi.Location = new System.Drawing.Point(186, 165);
            this.textBox_micerigi.Multiline = true;
            this.textBox_micerigi.Name = "textBox_micerigi";
            this.textBox_micerigi.Size = new System.Drawing.Size(296, 195);
            this.textBox_micerigi.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Mail Konusu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mail İçeriği:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Alıcı:";
            // 
            // textBox_gposta
            // 
            this.textBox_gposta.Location = new System.Drawing.Point(186, 25);
            this.textBox_gposta.Name = "textBox_gposta";
            this.textBox_gposta.Size = new System.Drawing.Size(296, 20);
            this.textBox_gposta.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Gönderici Maili:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(61, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Gönderici Şifresi:";
            // 
            // FrmMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(574, 411);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_gposta);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_micerigi);
            this.Controls.Add(this.textBox_mkonusu);
            this.Controls.Add(this.button_mailgonder);
            this.Controls.Add(this.textBox_aposta);
            this.Controls.Add(this.textBox_gsifre);
            this.Name = "FrmMail";
            this.Text = "MAİL GÖNDER";
            this.Load += new System.EventHandler(this.FrmMail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_gsifre;
        private System.Windows.Forms.TextBox textBox_aposta;
        private System.Windows.Forms.Button button_mailgonder;
        private System.Windows.Forms.TextBox textBox_mkonusu;
        private System.Windows.Forms.TextBox textBox_micerigi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_gposta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}