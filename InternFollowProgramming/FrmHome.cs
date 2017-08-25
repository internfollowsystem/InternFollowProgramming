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
            if (String.IsNullOrWhiteSpace(comboBox_user.Text) ||
                String.IsNullOrWhiteSpace(textBox_password.Text))
            {
                MessageBox.Show("Giriş Başarısız! Eksiksiz Giriniz!", "..:: HATA ::..",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                connection.Open(); // Bağlantıyı aç.
                string sql = "select* from kullanıcı where adı=@adı AND sifresi=@sifresi";// Sql bağlantı cümlemiz.
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
                    FrmScreen frmscreen = new FrmScreen();
                    frmscreen.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FrmScreen frmscreen = new FrmScreen();
                frmscreen.Show();
            }
        }

        private void pictureBox_password_Click(object sender, EventArgs e)
        {
            if (pictureBox_password.Image==Properties.Resources.göster)
            {
                pictureBox_password.Image = Properties.Resources.gizle;
                textBox_password.PasswordChar = '*';
            }
            else if (pictureBox_password.Image==Properties.Resources.gizle)
            {
                pictureBox_password.Image = Properties.Resources.göster;
                textBox_password.PasswordChar = '\0';
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
    }
}
