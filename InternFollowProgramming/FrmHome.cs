using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace InternFollowProgramming
{
    public partial class FrmHome : Form
    {
        #region baglantımız
        //static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=20fbgsbjk07";
        static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
        SqlDataReader datareader;
        DataTable datatable;
        
        //SqlCommand cmd = new SqlCommand();
        #endregion

        public FrmHome()
        {
            InitializeComponent();

            #region kullanıcı adlarını veritabanından okuyup (SqlDataReader) combobox'a çeken(ExecuteReader) kod.
            
            command.CommandText = "select * from kullanıcı";
            command.CommandType = CommandType.Text;

            command.Connection = connection;
            connection.Open();
            datareader = command.ExecuteReader();

            while (datareader.Read())
            {
                comboBox_user.Items.Add(datareader["adı"]);
            }

            connection.Close();

            #endregion

            textBox_password.PasswordChar = '*'; // form açıldığında şifreyi "*" karakteriyle göstermek için.
          
        }
        
        private void pictureBox_giris_Click(object sender, EventArgs e)
        {
            // Boş değer girilmesini engelliyoruz.
            if (String.IsNullOrWhiteSpace(comboBox_user.Text) || String.IsNullOrWhiteSpace(textBox_password.Text))
            {
                MessageBox.Show("Giriş Başarısız! Eksiksiz Giriniz!", "..:: HATA ::..",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                connection.Open(); // Bağlantıyı aç.
                string sql = "select adı, sifresi from kullanıcı where adı=@adı AND sifresi=@sifresi";// Sql bağlantı cümlemiz.
                SqlParameter prms1 = new SqlParameter("@adı", comboBox_user.Text);
                SqlParameter prms2 = new SqlParameter("@sifresi", textBox_password.Text);
                command = new SqlCommand(sql, connection);
                command.Parameters.Add(prms1);
                command.Parameters.Add(prms2);
                datatable = new DataTable();
                dataadapter = new SqlDataAdapter(command);
                dataadapter.Fill(datatable);
                connection.Close();

                if (datatable.Rows.Count > 0)
                {
                    //    Giriş gerçekleşti yaptırmak istediğiniz kodu burdan gerçekleştirebilirsiniz.
                    //    Altta yeni form açma işlemi gerçekleştirilmiştir.
                    this.Hide();
                    FrmScreen frmscreen = new FrmScreen();
                    frmscreen.Show();


                }
                else
                {
                    MessageBox.Show("Veritabanında böyle bir kullanıcı bulunamadı");
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }

        private void pictureBox_giris_MouseHover(object sender, EventArgs e)
        {
            pictureBox_giris.Image = Properties.Resources.leaveGiriş1;
        }

        private void pictureBox_giris_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_giris.Image = Properties.Resources.Giriş;
        }

        private void checkBox_visible_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_visible.Checked== true)
            {
                pictureBox_password.Image = Properties.Resources.göster_copy;
                textBox_password.PasswordChar = '\0';
            }
            else if (checkBox_visible.Checked == false)
            {
                pictureBox_password.Image = Properties.Resources.gizle1;
                textBox_password.PasswordChar = '*';
            }
        }

        private void label_sifremiunuttum_MouseHover(object sender, EventArgs e)
        {
            label_sifremiunuttum.Font = new Font(label_sifremiunuttum.Font, FontStyle.Bold);
        }

        private void label_sifremiunuttum_MouseLeave(object sender, EventArgs e)
        {
            label_sifremiunuttum.Font = new Font(label_sifremiunuttum.Font, FontStyle.Regular);
        }

        private void label_yenikayit_MouseHover(object sender, EventArgs e)
        {
            label_yenikayit.Font = new Font(label_yenikayit.Font, FontStyle.Bold);
        }

        private void label_yenikayit_MouseLeave(object sender, EventArgs e)
        {
            label_yenikayit.Font = new Font(label_yenikayit.Font, FontStyle.Regular);
        }

        private void label_sifremiunuttum_Click(object sender, EventArgs e)
        {
            
            if (label_mail.Text != string.Empty)
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "mail.ake.com.tr";
                client.EnableSsl = false;
                client.Credentials = new NetworkCredential("s.ozkaynak@outlook.com", "madame.nuch");
                MailMessage mm = new MailMessage("s.ozkaynak@outlook.com", label_mail.Text);
                mm.Subject = "Mail Hatırlatma";
                mm.Body = label_sifre.Text;
                client.Send(mm);
                MessageBox.Show("Şifre iletilmştir.");
            } 
            else
            {
                MessageBox.Show("Kullanıcının kayıtlı mail adresi yoktur!");
            }
        }

        private void comboBox_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            command.CommandText = "select sifresi,eposta from kullanıcı where adı=@adı";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@adı", comboBox_user.Text);
            command.Connection = connection;
            connection.Open();
            datareader = command.ExecuteReader();

            while (datareader.Read())
            {
                label_sifre.Text = datareader["sifresi"].ToString();
                label_mail.Text = datareader["eposta"].ToString();
            }
            connection.Close();
        }

        private void label_yenikayit_Click(object sender, EventArgs e)
        {
            yenikullanıcı ac = new yenikullanıcı();
            ac.Show();
        }
    }
}
