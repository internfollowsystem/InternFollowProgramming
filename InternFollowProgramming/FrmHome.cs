﻿using System;
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
        
        public FrmHome()
        {
            InitializeComponent();

            #region baglantımız
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            #endregion

            #region kullanıcı adlarını veritabanından okuyup (SqlDataReader) combobox'a çeken(ExecuteReader) kod.

            connection.Open();
            command.CommandText = "SELECT *FROM kullanıcı";
            command.CommandType = CommandType.Text;

            SqlDataReader dr;
            dr = command.ExecuteReader();

            while (dr.Read())
            {
                comboBox_user.Items.Add(dr["kullanıcı_adı"]);
            }

            connection.Close();

            #endregion

            textBox_password.PasswordChar = '*'; // form açıldığında şifreyi "*" karakteriyle göstermek için.
          
        }


        #region Giriş Butonu
        private void button_login_Click(object sender, EventArgs e)
        {

            string connection = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlConnection con = new SqlConnection(connection);

            // Boş değer girilmesini engelliyoruz.
            if (String.IsNullOrWhiteSpace(comboBox_user.Text) ||
                String.IsNullOrWhiteSpace(textBox_password.Text))
            {
                MessageBox.Show("Giriş Başarısız! Eksiksiz Giriniz!", "..:: HATA ::..",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            try
            {
                // Sql bağlantı cümlemiz.

                con.Open(); // Bağlantıyı aç.
                                 // Sorgumuz.
                string sql = "SELECT * FROM KULLANICI WHERE kullanıcı_adı=@kullanıcı_adı AND sifre=@sifre";
                SqlParameter prms1 = new SqlParameter("@kullanıcı_adı", comboBox_user.Text);
                SqlParameter prms2 = new SqlParameter("@sifre", textBox_password.Text);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(prms1);
                cmd.Parameters.Add(prms2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    //    Giriş gerçekleşti yaptırmak istediğiniz kodu burdan gerçekleştirebilirsiniz.
                    //    Altta yeni form açma işlemi gerçekleştirilmiştir.
                    this.Hide();
                    FrmScreen frm = new FrmScreen();
                    frm.Show();
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
        #endregion

        private void checkBox_password_CheckedChanged(object sender, EventArgs e)
        {
            //Şifrenin görünürlüğünü checkBox'a göre ayarladığımız kodlar
            if(checkBox_password.Checked)
            {
                textBox_password.PasswordChar = '\0';
            }
            else
            {
                textBox_password.PasswordChar = '*';
            }
                
        }
    }
}