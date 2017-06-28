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
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            string kayit = "SELECT *FROM IntershipInformation";
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

        private void FrmScreen_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            connection.Open();

            command.CommandText = "SELECT *FROM intern";
            command.CommandType = CommandType.Text;

            #region ComboBox'ı yıllara göre ayarlama
            //SqlDataReader dr;
            
            //dr = command.ExecuteReader();
            //while (dr.Read())
            //{
            //    comboBox_yıl.Items.Add(dr["staj_yılı"]);
            //}
            //connection.Close();
            #endregion

            //combobox'a göre sırala
            SqlDataAdapter adaptor = new SqlDataAdapter("Select tc_kimlikno,ad_soyad,staj_kabul_durumu from IntershipInformation where staj_yılı Like '%" + comboBox_yıl.SelectedItem + "%'", connection);
            DataSet ds = new DataSet();
            ds.Clear();
            adaptor.Fill(ds, "IntershipInformation");
            dataGridView_frmscreen.DataSource = ds.Tables["IntershipInformation"];
            adaptor.Dispose();
        }
         
        private void onlisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            string kayit = "SELECT *FROM IntershipInformation where okul_turu='ÖN LİSANS' ";
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
        private void lisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            string kayit = "SELECT *FROM IntershipInformation where okul_turu='LİSANS' ";
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
        private void liseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            string kayit = "SELECT *FROM IntershipInformation where okul_turu='LİSE' ";
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

        private void genelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
            SqlCommand command = new SqlCommand();
            string kayit = "SELECT *FROM IntershipInformation ";
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
    }
}
