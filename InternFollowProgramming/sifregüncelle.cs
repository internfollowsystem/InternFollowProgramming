using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace InternFollowProgramming
{
    public partial class sifregüncelle : Form
    {
        #region baglantımız
        //static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=20fbgsbjk07";
        static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN; MultipleActiveResultSets=True;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        
        SqlCommand cmd = new SqlCommand();
        #endregion
        public sifregüncelle()
        {
            InitializeComponent();
        }

        private void pictureBox_güncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string stajyer = "update kullanıcı set sifresi=@sifresi where adı=@adı";
                command = new SqlCommand(stajyer, connection);
                command.Parameters.AddWithValue("@adı", textBox_adı.Text);
                command.Parameters.AddWithValue("@sifresi", textBox_sifre.Text);
                command.ExecuteNonQuery();
                connection.Close();

				textBox_adı.ResetText();
				textBox_sifre.ResetText();

                MessageBox.Show("Şifre Yenilendi");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "kullanıcı işlemi gerçekleştirilemedi");
            }
        }

        private void pictureBox_güncelle_MouseHover(object sender, EventArgs e)
        {
            pictureBox_güncelle.Width = 25;
            pictureBox_güncelle.Height = 25;
        }

        private void pictureBox_güncelle_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_güncelle.Width = 20;
            pictureBox_güncelle.Height = 20;
        }

        private void sifregüncelle_Load(object sender, EventArgs e)
        {
            
        }
    }
}
