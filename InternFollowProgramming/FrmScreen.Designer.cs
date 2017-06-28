namespace InternFollowProgramming
{
    partial class FrmScreen
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
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.menuStrip_frmscreen = new System.Windows.Forms.MenuStrip();
            this.stajyerTanımlamaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlisansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lisansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stajyerYönetimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planlamaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yoklamaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView_frmscreen = new System.Windows.Forms.DataGridView();
            this.comboBox_yıl = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.genelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.menuStrip_frmscreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_frmscreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::InternFollowProgramming.Properties.Resources.logovektörel1;
            this.pictureBox_logo.Location = new System.Drawing.Point(12, 42);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(112, 50);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 0;
            this.pictureBox_logo.TabStop = false;
            // 
            // menuStrip_frmscreen
            // 
            this.menuStrip_frmscreen.BackColor = System.Drawing.Color.Chocolate;
            this.menuStrip_frmscreen.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menuStrip_frmscreen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stajyerTanımlamaToolStripMenuItem,
            this.stajyerYönetimToolStripMenuItem,
            this.planlamaToolStripMenuItem,
            this.yoklamaToolStripMenuItem});
            this.menuStrip_frmscreen.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_frmscreen.Name = "menuStrip_frmscreen";
            this.menuStrip_frmscreen.Size = new System.Drawing.Size(1350, 24);
            this.menuStrip_frmscreen.TabIndex = 1;
            this.menuStrip_frmscreen.Text = "menuStrip1";
            // 
            // stajyerTanımlamaToolStripMenuItem
            // 
            this.stajyerTanımlamaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genelToolStripMenuItem,
            this.onlisansToolStripMenuItem,
            this.lisansToolStripMenuItem,
            this.liseToolStripMenuItem1});
            this.stajyerTanımlamaToolStripMenuItem.Name = "stajyerTanımlamaToolStripMenuItem";
            this.stajyerTanımlamaToolStripMenuItem.Size = new System.Drawing.Size(140, 20);
            this.stajyerTanımlamaToolStripMenuItem.Text = "Stajyer Tanımlama";
            // 
            // onlisansToolStripMenuItem
            // 
            this.onlisansToolStripMenuItem.Name = "onlisansToolStripMenuItem";
            this.onlisansToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.onlisansToolStripMenuItem.Text = "ÖN LİSANS";
            this.onlisansToolStripMenuItem.Click += new System.EventHandler(this.onlisansToolStripMenuItem_Click);
            // 
            // lisansToolStripMenuItem
            // 
            this.lisansToolStripMenuItem.Name = "lisansToolStripMenuItem";
            this.lisansToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lisansToolStripMenuItem.Text = "LİSANS";
            this.lisansToolStripMenuItem.Click += new System.EventHandler(this.lisansToolStripMenuItem_Click);
            // 
            // liseToolStripMenuItem1
            // 
            this.liseToolStripMenuItem1.Name = "liseToolStripMenuItem1";
            this.liseToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.liseToolStripMenuItem1.Text = "LİSE";
            this.liseToolStripMenuItem1.Click += new System.EventHandler(this.liseToolStripMenuItem1_Click);
            // 
            // stajyerYönetimToolStripMenuItem
            // 
            this.stajyerYönetimToolStripMenuItem.Name = "stajyerYönetimToolStripMenuItem";
            this.stajyerYönetimToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.stajyerYönetimToolStripMenuItem.Text = "Stajyer Yönetim";
            this.stajyerYönetimToolStripMenuItem.Click += new System.EventHandler(this.stajyerYönetimToolStripMenuItem_Click);
            // 
            // planlamaToolStripMenuItem
            // 
            this.planlamaToolStripMenuItem.Name = "planlamaToolStripMenuItem";
            this.planlamaToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.planlamaToolStripMenuItem.Text = "Planlama";
            // 
            // yoklamaToolStripMenuItem
            // 
            this.yoklamaToolStripMenuItem.Name = "yoklamaToolStripMenuItem";
            this.yoklamaToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.yoklamaToolStripMenuItem.Text = "Yoklama";
            // 
            // dataGridView_frmscreen
            // 
            this.dataGridView_frmscreen.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView_frmscreen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_frmscreen.Location = new System.Drawing.Point(12, 113);
            this.dataGridView_frmscreen.Name = "dataGridView_frmscreen";
            this.dataGridView_frmscreen.Size = new System.Drawing.Size(1322, 604);
            this.dataGridView_frmscreen.TabIndex = 2;
            // 
            // comboBox_yıl
            // 
            this.comboBox_yıl.FormattingEnabled = true;
            this.comboBox_yıl.Location = new System.Drawing.Point(1017, 72);
            this.comboBox_yıl.Name = "comboBox_yıl";
            this.comboBox_yıl.Size = new System.Drawing.Size(121, 21);
            this.comboBox_yıl.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Adobe Garamond Pro Bold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1159, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 21);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Adobe Garamond Pro Bold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1257, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 21);
            this.button2.TabIndex = 5;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // genelToolStripMenuItem
            // 
            this.genelToolStripMenuItem.Name = "genelToolStripMenuItem";
            this.genelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.genelToolStripMenuItem.Text = "GENEL";
            this.genelToolStripMenuItem.Click += new System.EventHandler(this.genelToolStripMenuItem_Click);
            // 
            // FrmScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_yıl);
            this.Controls.Add(this.dataGridView_frmscreen);
            this.Controls.Add(this.pictureBox_logo);
            this.Controls.Add(this.menuStrip_frmscreen);
            this.MainMenuStrip = this.menuStrip_frmscreen;
            this.Name = "FrmScreen";
            this.Text = "FrmScreen";
            this.Load += new System.EventHandler(this.FrmScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.menuStrip_frmscreen.ResumeLayout(false);
            this.menuStrip_frmscreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_frmscreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.MenuStrip menuStrip_frmscreen;
        private System.Windows.Forms.ToolStripMenuItem stajyerTanımlamaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlisansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lisansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stajyerYönetimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planlamaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yoklamaToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView_frmscreen;
        private System.Windows.Forms.ComboBox comboBox_yıl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem liseToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem genelToolStripMenuItem;
    }
}