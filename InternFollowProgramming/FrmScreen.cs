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
    public partial class FrmScreen : Form
    {
        public FrmScreen()
        {
            InitializeComponent();
        }

        private void FrmScreen_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();
            command.CommandText = "SELECT *FROM ogrenci";


            command.CommandType = CommandType.Text;
            
            //ComboBox'ı yılları göre ayarlama
            SqlDataReader dr;
            connection.Open();
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                comboBox_yıl.Items.Add(dr["staj_yılı"]);
            }
            connection.Close();

            //combobox'a göre sırala
            SqlDataAdapter adaptor = new SqlDataAdapter("Select ID,Tarih,Süre,Egitimci,Konu,Tip,Yer from Veriler where EgitimAlan Like '%" + comboBox_yıl.SelectedItem + "%'", connection);
            DataSet ds = new DataSet();
            ds.Clear();
            adaptor.Fill(ds, "Veriler");
            dataGridView_frmscreen.DataSource = ds.Tables["Veriler"];
            adaptor.Dispose();
        }
          
      

            private void üniversiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            string kayit = "SELECT *FROM ogrenci where okul_turu='üniversite' ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView_frmscreen.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            connection.Close();
        }

        private void liseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            string kayit = "SELECT *FROM ogrenci where okul_turu='lise' ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView_frmscreen.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            connection.Close();
        }

        private void stajyerYönetimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInternInformation frm = new FrmInternInformation();
            frm.Show();
        }
    }
}
