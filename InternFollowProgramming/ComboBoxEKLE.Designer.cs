namespace InternFollowProgramming
{
    partial class ComboBoxEKLE
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
            this.comboBox_cmbx = new System.Windows.Forms.ComboBox();
            this.listBox_cmbx = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GüncelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_ekle = new System.Windows.Forms.Button();
            this.textBox_cmbxveri = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_cmbx
            // 
            this.comboBox_cmbx.BackColor = System.Drawing.Color.White;
            this.comboBox_cmbx.FormattingEnabled = true;
            this.comboBox_cmbx.Items.AddRange(new object[] {
            "stajkabuldurumu",
            "stajdonemi",
            "basvuruturu",
            "egitimdurumu",
            "stajturu",
            "aracplaka",
            "mentöradı",
            "bolumadı",
            "universiteadı",
            "ortaokuladı",
            "liseadı",
            "sınıf"});
            this.comboBox_cmbx.Location = new System.Drawing.Point(25, 83);
            this.comboBox_cmbx.Name = "comboBox_cmbx";
            this.comboBox_cmbx.Size = new System.Drawing.Size(121, 21);
            this.comboBox_cmbx.TabIndex = 0;
            this.comboBox_cmbx.SelectedIndexChanged += new System.EventHandler(this.comboBox_cmbx_SelectedIndexChanged);
            // 
            // listBox_cmbx
            // 
            this.listBox_cmbx.BackColor = System.Drawing.Color.White;
            this.listBox_cmbx.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox_cmbx.FormattingEnabled = true;
            this.listBox_cmbx.Location = new System.Drawing.Point(23, 117);
            this.listBox_cmbx.Name = "listBox_cmbx";
            this.listBox_cmbx.Size = new System.Drawing.Size(340, 186);
            this.listBox_cmbx.TabIndex = 1;
            this.listBox_cmbx.SelectedIndexChanged += new System.EventHandler(this.listBox_cmbx_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SilToolStripMenuItem,
            this.GüncelleToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(132, 48);
            // 
            // SilToolStripMenuItem
            // 
            this.SilToolStripMenuItem.Image = global::InternFollowProgramming.Properties.Resources.Delete_Icon2;
            this.SilToolStripMenuItem.Name = "SilToolStripMenuItem";
            this.SilToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.SilToolStripMenuItem.Text = "SİL";
            this.SilToolStripMenuItem.Click += new System.EventHandler(this.SilToolStripMenuItem_Click);
            // 
            // GüncelleToolStripMenuItem
            // 
            this.GüncelleToolStripMenuItem.Image = global::InternFollowProgramming.Properties.Resources.düzenle1;
            this.GüncelleToolStripMenuItem.Name = "GüncelleToolStripMenuItem";
            this.GüncelleToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.GüncelleToolStripMenuItem.Text = "GÜNCELLE";
            this.GüncelleToolStripMenuItem.Click += new System.EventHandler(this.GüncelleToolStripMenuItem_Click);
            // 
            // button_ekle
            // 
            this.button_ekle.Location = new System.Drawing.Point(369, 82);
            this.button_ekle.Name = "button_ekle";
            this.button_ekle.Size = new System.Drawing.Size(43, 23);
            this.button_ekle.TabIndex = 3;
            this.button_ekle.Text = "EKLE";
            this.button_ekle.UseVisualStyleBackColor = true;
            this.button_ekle.Click += new System.EventHandler(this.button_ekle_Click);
            // 
            // textBox_cmbxveri
            // 
            this.textBox_cmbxveri.BackColor = System.Drawing.Color.White;
            this.textBox_cmbxveri.Location = new System.Drawing.Point(222, 84);
            this.textBox_cmbxveri.Name = "textBox_cmbxveri";
            this.textBox_cmbxveri.Size = new System.Drawing.Size(141, 20);
            this.textBox_cmbxveri.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 56);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::InternFollowProgramming.Properties.Resources.AKESTAJYERLOGO1;
            this.pictureBox1.Location = new System.Drawing.Point(25, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lora", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Seçim Kutusu Seç:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lora", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(219, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Seçim Nesnesi Yaz!";
            // 
            // ComboBoxEKLE
            // 
            this.AcceptButton = this.button_ekle;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.ClientSize = new System.Drawing.Size(440, 331);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_cmbxveri);
            this.Controls.Add(this.button_ekle);
            this.Controls.Add(this.listBox_cmbx);
            this.Controls.Add(this.comboBox_cmbx);
            this.Name = "ComboBoxEKLE";
            this.Text = "ComboBoxEKLE";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_cmbx;
        private System.Windows.Forms.ListBox listBox_cmbx;
        private System.Windows.Forms.Button button_ekle;
        private System.Windows.Forms.TextBox textBox_cmbxveri;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SilToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem GüncelleToolStripMenuItem;
    }
}