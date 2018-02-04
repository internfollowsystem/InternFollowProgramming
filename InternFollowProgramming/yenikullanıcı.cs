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
    public partial class yenikullanıcı : Form
    {

        #region baglantımız
       static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=*********";
       
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
       
        SqlCommand cmd = new SqlCommand();
        #endregion

        public yenikullanıcı()
        {
            InitializeComponent();
        }

        private void pictureBox_kaydet_Click(object sender, EventArgs e)
        {
            connection.Open();
            String stajyer = "Insert Into kullanıcı (adı, sifresi, eposta, departman) Values (@adı, @sifresi, @eposta, @departman)";
            SqlCommand command = new SqlCommand(stajyer, connection);
            //kişisel veriler GÜNCELL KOD !!
            command.Parameters.AddWithValue("@adı", textBox_adı.Text);
            command.Parameters.AddWithValue("@sifresi", textBox_sifre.Text);
            command.Parameters.AddWithValue("@eposta", textBox_eposta.Text);
            command.Parameters.AddWithValue("@departman", textBox_departman.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Yeni kullanıcı kaydedildi!");
        }

        private void pictureBox_sil_Click(object sender, EventArgs e)
        {
            connection.Open();
            String stajyer = "Delete from kullanıcı where adı=@adı";
            SqlCommand command = new SqlCommand(stajyer, connection);
            //kişisel veriler GÜNCELL KOD !!
            command.Parameters.AddWithValue("@adı", textBox_adı.Text);
            command.ExecuteNonQuery();
            connection.Close();

			textBox_adı.ResetText();
			textBox_departman.ResetText();
			textBox_eposta.ResetText();
			textBox_sifre.ResetText();
            MessageBox.Show("Kullanıcı silindi!");
        }

        private void pictureBox_güncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string stajyer = "update kullanıcı set adı=@adı, sifresi=@sifresi, eposta@eposta, departman=@departman where adı=@adı";
                command = new SqlCommand(stajyer, connection);
                command.Parameters.AddWithValue("@adı", textBox_adı.Text);
                command.Parameters.AddWithValue("@sifresi", textBox_sifre.Text);
                command.Parameters.AddWithValue("@eposta", textBox_eposta.Text);
                command.Parameters.AddWithValue("@departman", textBox_departman.Text);
                command.ExecuteNonQuery();
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "kullanıcı güncellendi");
            }

            }

        private void pictureBox_bul_Click(object sender, EventArgs e)
        {
            connection.Open();
            cmd.Connection = connection;
            command.Connection = connection;
            string stajyer = "SELECT * from kullanıcı where adı=@adı";
            command = new SqlCommand(stajyer, connection);
            command.Parameters.AddWithValue("@adı", textBox_adı.Text);

            dataadapter = new SqlDataAdapter(command);
            SqlDataReader drstajyer = command.ExecuteReader();
            if (drstajyer.Read())
            {
                textBox_adı.Text = drstajyer["adı"].ToString();
                textBox_sifre.Text= drstajyer["sifresi"].ToString();
                textBox_eposta.Text= drstajyer["eposta"].ToString();
                textBox_departman.Text= drstajyer["departman"].ToString();

            }
            //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
            else
            {
                MessageBox.Show("Kayıtlı Kullanıcı Bulunamadı");
            }
            drstajyer.Close();
            connection.Close();
        }

        private void pictureBox_bul_MouseHover(object sender, EventArgs e)
        {
            pictureBox_bul.Width = 25;
            pictureBox_bul.Height = 25;
        }

        private void pictureBox_bul_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_bul.Width = 20;
            pictureBox_bul.Height = 20;
        }

        private void pictureBox_kaydet_MouseHover(object sender, EventArgs e)
        {
            pictureBox_kaydet.Width = 25;
            pictureBox_kaydet.Height = 25;
        }

        private void pictureBox_kaydet_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_kaydet.Width = 20;
            pictureBox_kaydet.Height = 20;
        }

        private void pictureBox_sil_MouseHover(object sender, EventArgs e)
        {
            pictureBox_sil.Width = 25;
            pictureBox_sil.Height = 25;
        }

        private void pictureBox_sil_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_sil.Width = 20;
            pictureBox_sil.Height = 20;
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
    }
}
