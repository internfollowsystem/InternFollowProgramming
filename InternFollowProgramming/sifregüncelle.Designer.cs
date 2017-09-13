namespace InternFollowProgramming
{
    partial class sifregüncelle
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
			this.components = new System.ComponentModel.Container();
			this.panel_ust = new System.Windows.Forms.Panel();
			this.pictureBox_logo = new System.Windows.Forms.PictureBox();
			this.textBox_sifre = new System.Windows.Forms.TextBox();
			this.textBox_adı = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox_güncelle = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel_ust.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_güncelle)).BeginInit();
			this.SuspendLayout();
			// 
			// panel_ust
			// 
			this.panel_ust.BackColor = System.Drawing.Color.Black;
			this.panel_ust.Controls.Add(this.pictureBox_logo);
			this.panel_ust.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel_ust.Location = new System.Drawing.Point(0, 0);
			this.panel_ust.Name = "panel_ust";
			this.panel_ust.Size = new System.Drawing.Size(452, 190);
			this.panel_ust.TabIndex = 14;
			// 
			// pictureBox_logo
			// 
			this.pictureBox_logo.Image = global::InternFollowProgramming.Properties.Resources.STAJYERLOGO;
			this.pictureBox_logo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.pictureBox_logo.Location = new System.Drawing.Point(109, 42);
			this.pictureBox_logo.Name = "pictureBox_logo";
			this.pictureBox_logo.Size = new System.Drawing.Size(252, 98);
			this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox_logo.TabIndex = 0;
			this.pictureBox_logo.TabStop = false;
			// 
			// textBox_sifre
			// 
			this.textBox_sifre.Location = new System.Drawing.Point(136, 253);
			this.textBox_sifre.Name = "textBox_sifre";
			this.textBox_sifre.Size = new System.Drawing.Size(252, 20);
			this.textBox_sifre.TabIndex = 23;
			// 
			// textBox_adı
			// 
			this.textBox_adı.Location = new System.Drawing.Point(136, 209);
			this.textBox_adı.Name = "textBox_adı";
			this.textBox_adı.Size = new System.Drawing.Size(252, 20);
			this.textBox_adı.TabIndex = 22;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label2.Location = new System.Drawing.Point(53, 254);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 15);
			this.label2.TabIndex = 21;
			this.label2.Text = "Yeni Şifre:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label1.Location = new System.Drawing.Point(53, 210);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 15);
			this.label1.TabIndex = 20;
			this.label1.Text = "Adı:";
			// 
			// pictureBox_güncelle
			// 
			this.pictureBox_güncelle.Image = global::InternFollowProgramming.Properties.Resources.düzenle1;
			this.pictureBox_güncelle.Location = new System.Drawing.Point(365, 279);
			this.pictureBox_güncelle.Name = "pictureBox_güncelle";
			this.pictureBox_güncelle.Size = new System.Drawing.Size(23, 24);
			this.pictureBox_güncelle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox_güncelle.TabIndex = 29;
			this.pictureBox_güncelle.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBox_güncelle, "ŞİFRE GÜNCELLE");
			this.pictureBox_güncelle.Click += new System.EventHandler(this.pictureBox_güncelle_Click);
			this.pictureBox_güncelle.MouseLeave += new System.EventHandler(this.pictureBox_güncelle_MouseLeave);
			this.pictureBox_güncelle.MouseHover += new System.EventHandler(this.pictureBox_güncelle_MouseHover);
			// 
			// sifregüncelle
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Beige;
			this.ClientSize = new System.Drawing.Size(452, 371);
			this.Controls.Add(this.pictureBox_güncelle);
			this.Controls.Add(this.textBox_sifre);
			this.Controls.Add(this.textBox_adı);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel_ust);
			this.Name = "sifregüncelle";
			this.Text = "sifregüncelle";
			this.Load += new System.EventHandler(this.sifregüncelle_Load);
			this.panel_ust.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_güncelle)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_ust;
        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.TextBox textBox_sifre;
        private System.Windows.Forms.TextBox textBox_adı;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox_güncelle;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}