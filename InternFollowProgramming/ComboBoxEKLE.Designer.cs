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
            this.button_ekle = new System.Windows.Forms.Button();
            this.textBox_cmbxveri = new System.Windows.Forms.TextBox();
            this.button_cıkıs = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_cmbx
            // 
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
            this.comboBox_cmbx.Location = new System.Drawing.Point(14, 17);
            this.comboBox_cmbx.Name = "comboBox_cmbx";
            this.comboBox_cmbx.Size = new System.Drawing.Size(121, 21);
            this.comboBox_cmbx.TabIndex = 0;
            this.comboBox_cmbx.SelectedIndexChanged += new System.EventHandler(this.comboBox_cmbx_SelectedIndexChanged);
            // 
            // listBox_cmbx
            // 
            this.listBox_cmbx.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox_cmbx.FormattingEnabled = true;
            this.listBox_cmbx.Location = new System.Drawing.Point(12, 51);
            this.listBox_cmbx.Name = "listBox_cmbx";
            this.listBox_cmbx.Size = new System.Drawing.Size(340, 186);
            this.listBox_cmbx.TabIndex = 1;
            this.listBox_cmbx.SelectedIndexChanged += new System.EventHandler(this.listBox_cmbx_SelectedIndexChanged);
            // 
            // button_ekle
            // 
            this.button_ekle.Location = new System.Drawing.Point(358, 16);
            this.button_ekle.Name = "button_ekle";
            this.button_ekle.Size = new System.Drawing.Size(43, 23);
            this.button_ekle.TabIndex = 3;
            this.button_ekle.Text = "EKLE";
            this.button_ekle.UseVisualStyleBackColor = true;
            this.button_ekle.Click += new System.EventHandler(this.button_ekle_Click);
            // 
            // textBox_cmbxveri
            // 
            this.textBox_cmbxveri.Location = new System.Drawing.Point(211, 18);
            this.textBox_cmbxveri.Name = "textBox_cmbxveri";
            this.textBox_cmbxveri.Size = new System.Drawing.Size(141, 20);
            this.textBox_cmbxveri.TabIndex = 5;
            // 
            // button_cıkıs
            // 
            this.button_cıkıs.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cıkıs.Location = new System.Drawing.Point(371, 226);
            this.button_cıkıs.Name = "button_cıkıs";
            this.button_cıkıs.Size = new System.Drawing.Size(30, 23);
            this.button_cıkıs.TabIndex = 6;
            this.button_cıkıs.Text = "x";
            this.button_cıkıs.UseVisualStyleBackColor = true;
            this.button_cıkıs.Visible = false;
            this.button_cıkıs.Click += new System.EventHandler(this.button_cıkıs_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SilToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(90, 26);
            // 
            // SilToolStripMenuItem
            // 
            this.SilToolStripMenuItem.Image = global::InternFollowProgramming.Properties.Resources.Delete_Icon2;
            this.SilToolStripMenuItem.Name = "SilToolStripMenuItem";
            this.SilToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SilToolStripMenuItem.Text = "SİL";
            this.SilToolStripMenuItem.Click += new System.EventHandler(this.SilToolStripMenuItem_Click);
            // 
            // ComboBoxEKLE
            // 
            this.AcceptButton = this.button_ekle;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button_cıkıs;
            this.ClientSize = new System.Drawing.Size(413, 261);
            this.Controls.Add(this.button_cıkıs);
            this.Controls.Add(this.textBox_cmbxveri);
            this.Controls.Add(this.button_ekle);
            this.Controls.Add(this.listBox_cmbx);
            this.Controls.Add(this.comboBox_cmbx);
            this.Name = "ComboBoxEKLE";
            this.Text = "ComboBoxEKLE";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_cmbx;
        private System.Windows.Forms.ListBox listBox_cmbx;
        private System.Windows.Forms.Button button_ekle;
        private System.Windows.Forms.TextBox textBox_cmbxveri;
        private System.Windows.Forms.Button button_cıkıs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SilToolStripMenuItem;
    }
}