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
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;



namespace InternFollowProgramming
{
    public partial class FrmScreen : Form
    {
        #region Print
        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0;
        int iCount = 0;
        int numara;// SAĞ TUŞA TIKLAYARAK VERİ SİLME İŞLEMİNDE KULLANDIK
        #endregion

        #region baglantımız
        static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=20fbgsbjk07";
        //static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
        SqlDataReader datareader;

        System.Data.DataTable datatable;
        DataSet dataset;
        SqlCommandBuilder commandbuilder;

        //SqlCommand cmd = new SqlCommand();
        #endregion

        public FrmScreen()
        {
            InitializeComponent();
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT i.* , s.* FROM intern i Left Join InternInformation s on i.tc_kimlikno=s.tc_kimlikno ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.  
            dataadapter.Dispose();
            connection.Close();

        }

        private void FrmScreen_Load(object sender, EventArgs e)
        {
            command.Connection = connection;


            command.CommandText = "SELECT * FROM InternInformation";
            command.CommandType = CommandType.Text;
            int kayitSayisi;

            #region TÜM STAJYERLER LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand stajyer = new SqlCommand();
            stajyer.Connection = connection;
            stajyer.CommandText = "Select Count(tc_kimlikno) From intern ";
            kayitSayisi = Convert.ToInt32(stajyer.ExecuteScalar());
            label_stajyer.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region LİSANS LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand lisans = new SqlCommand();
            lisans.Connection = connection;
            lisans.CommandText = "Select Count(okul_turu) From InternInformation Where okul_turu= 'Lisans'";
            kayitSayisi = Convert.ToInt32(lisans.ExecuteScalar());
            label_lisans.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region ONLİSANS LABELA STAJYER SAYISINI AKTARMA
            connection.Open();
            SqlCommand onlisans = new SqlCommand();
            onlisans.Connection = connection;
            onlisans.CommandText = "Select Count(okul_turu) From InternInformation Where okul_turu ='On Lisans'";
            kayitSayisi = Convert.ToInt32(onlisans.ExecuteScalar());
            label_onlisans.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region LİSE LABELE STAJYER SAYISINI AKTARMA
            connection.Open();
            SqlCommand lise = new SqlCommand();
            lise.Connection = connection;
            lise.CommandText = "Select Count(okul_turu) From InternInformation Where okul_turu ='Lise'";
            kayitSayisi = Convert.ToInt32(lise.ExecuteScalar());
            label_lise.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region ŞUAN STAJ YAPANLAR LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand aktif = new SqlCommand();
            aktif.Connection = connection;
            aktif.CommandText = "Select Count(staj_durumu) From InternInformation where staj_durumu='STAJ YAPIYOR' ";
            kayitSayisi = Convert.ToInt32(aktif.ExecuteScalar());
            label_suanstajyapanlar.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion



            #region RAPORLAMA COMBOBOXLARININ İÇİNE VERİ TABANINDAN VERİ ÇEKMEK  //TEKRARLANAN VERİLERİ DÜZENLE

            SqlCommand cmd1 = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();
            SqlCommand cmd3 = new SqlCommand();
            SqlCommand cmd4 = new SqlCommand();
            SqlCommand cmd5 = new SqlCommand();
            SqlCommand cmd6 = new SqlCommand();
            cmd1.Connection = connection;
            cmd2.Connection = connection;
            cmd3.Connection = connection;
            cmd4.Connection = connection;
            cmd5.Connection = connection;
            cmd6.Connection = connection;
            connection.Open();
            cmd1.CommandText = "SELECT DISTINCT staj_yılı FROM InternInformation";
            cmd2.CommandText = "SELECT DISTINCT staj_donem FROM InternInformation";
            cmd3.CommandText = "SELECT DISTINCT okul_adı FROM InternInformation";
            cmd4.CommandText = "SELECT DISTINCT bolum_adı FROM InternInformation";
            cmd5.CommandText = "SELECT DISTINCT staj_konusu FROM InternInformation";
            cmd6.CommandText = "SELECT DISTINCT referans_adı FROM InternInformation";
            SqlDataReader dr;

            dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                comboBox_s_yıl.Items.Add(dr["staj_yılı"]);
            }
            dr.Close();
            dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                comboBox_s_donem.Items.Add(dr["staj_donem"]);
            }
            dr.Close();
            dr = cmd3.ExecuteReader();
            while (dr.Read())
            {
                comboBox_s_okul.Items.Add(dr["okul_adı"]);
            }
            dr.Close();
            dr = cmd4.ExecuteReader();
            while (dr.Read())
            {
                comboBox_s_bolum.Items.Add(dr["bolum_adı"]);
            }
            dr.Close();
            dr = cmd5.ExecuteReader();
            while (dr.Read())
            {
                comboBox_s_stajkonuları.Items.Add(dr["staj_konusu"]);
            }
            dr.Close();
            dr = cmd6.ExecuteReader();
            while (dr.Read())
            {
                comboBox_s_referans.Items.Add(dr["referans_adı"]);
            }
            dr.Close();
            connection.Close();
            #endregion

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        #region MENÜ BUTONLARI
        private void stajyerYönetimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInternInformation frm = new FrmInternInformation();
            frm.Show();

        }

        private void genelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM intern i Left Join InternInformation s on i.tc_kimlikno=s.tc_kimlikno ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void onlisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='On Lisans')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }
        private void lisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='Lisans')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            System.Data.DataTable dt = new System.Data.DataTable();
            dataadapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion

        }
        private void liseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='Lise')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }
        #endregion

        #region ÜST SEKME BUTON ÖZELLİĞİ
        private void pictureBox_genel_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM intern i Left Join InternInformation s on i.tc_kimlikno=s.tc_kimlikno ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void label_genel_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM intern i Left Join InternInformation s on i.tc_kimlikno=s.tc_kimlikno ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void pictureBox_lisans_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT i.*, s.* FROM intern as i Join InternInformation as s on i.tc_kimlikno=s.tc_kimlikno where s.okul_turu='Lisans' ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            System.Data.DataTable dt = new System.Data.DataTable();
            dataadapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void label1_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='Lisans')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            System.Data.DataTable dt = new System.Data.DataTable();
            dataadapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void pictureBox_onlisans_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='On Lisans')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void label2_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='On Lisans')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void pictureBox_lise_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='Lise')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void label3_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where okul_turu='Lise')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void pictureBox_suanstajer_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where staj_durumu='STAJ YAPIYOR')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }

        private void label_suanstajyer_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM intern where tc_kimlikno in (SELECT tc_kimlikno FROM InternInformation where staj_durumu='STAJ YAPIYOR')";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion
        }
        #endregion

        #region ÜST SEKME MOUSE OLAYLARI
        private void pictureBox_lisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.Blisans;
            pictureBox_lisans.Height = 52;
            pictureBox_lisans.Width = 52;
        }

        private void pictureBox_onlisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.Bön_lisans_copy;
            pictureBox_onlisans.Height = 52;
            pictureBox_onlisans.Width = 52;

        }

        private void pictureBox_lise_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.Blise_copy;
            pictureBox_lise.Height = 52;
            pictureBox_lise.Width = 52;
        }

        private void pictureBox_lisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.Slisans;
            pictureBox_lisans.Height = 42;
            pictureBox_lisans.Width = 42;
        }

        private void pictureBox_onlisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.Sönlisans;
            pictureBox_onlisans.Height = 42;
            pictureBox_onlisans.Width = 42;
        }

        private void pictureBox_lise_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.Slise;
            pictureBox_lise.Height = 42;
            pictureBox_lise.Width = 42;
        }

        private void pictureBox_genel_MouseHover(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.Bgenel;
            pictureBox_genel.Height = 52;
            pictureBox_genel.Width = 52;
        }

        private void pictureBox_genel_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.genel_stajer;
            pictureBox_genel.Height = 42;
            pictureBox_genel.Width = 42;
        }


        private void pictureBox_suanstajer_MouseHover(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.stajyapıyor;
            pictureBox_suanstajer.Height = 52;
            pictureBox_suanstajer.Width = 52;
        }

        private void pictureBox_suanstajer_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.Sstajyapıyor;
            pictureBox_suanstajer.Height = 42;
            pictureBox_suanstajer.Width = 42;
        }


        private void label_genel_MouseHover(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.Bgenel;
            pictureBox_genel.Height = 52;
            pictureBox_genel.Width = 52;
        }

        private void label_genel_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.genel_stajer;
            pictureBox_genel.Height = 42;
            pictureBox_genel.Width = 42;
        }

        private void labellisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.Blisans;
            pictureBox_lisans.Height = 52;
            pictureBox_lisans.Width = 52;
        }

        private void labellisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.Slisans;
            pictureBox_lisans.Height = 42;
            pictureBox_lisans.Width = 42;
        }

        private void labelonlisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.Bön_lisans_copy;
            pictureBox_onlisans.Height = 52;
            pictureBox_onlisans.Width = 52;
        }

        private void labelonlisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.Sönlisans;
            pictureBox_onlisans.Height = 42;
            pictureBox_onlisans.Width = 42;
        }

        private void labellise_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.Blise_copy;
            pictureBox_lise.Height = 52;
            pictureBox_lise.Width = 52;
        }

        private void labellise_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.Slise;
            pictureBox_lise.Height = 42;
            pictureBox_lise.Width = 42;
        }

        private void label_suanstajyer_MouseHover(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.stajyapıyor;
            pictureBox_suanstajer.Height = 52;
            pictureBox_suanstajer.Width = 52;
        }

        private void label_suanstajyer_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.Sstajyapıyor;
            pictureBox_suanstajer.Height = 42;
            pictureBox_suanstajer.Width = 42;
        }
        #endregion

        #region REFRESH MOUSE OLAYLARI & SAYFA YENİLEME
        private void pictureBox_refresh_MouseHover(object sender, EventArgs e)
        {
            pictureBox_refresh.Height = 42;
            pictureBox_refresh.Width = 42;
        }

        private void pictureBox_refresh_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_refresh.Height = 32;
            pictureBox_refresh.Width = 32;
        }

        private void pictureBox_refresh_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmScreen frmscreen = new FrmScreen();
            frmscreen.Show();

        }
        #endregion

        #region YAZDIRMA MOUSE OLAYLARI & YAZICIDAN ÇIKARTMA
        private void pictureBox_yazdır_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument_frmscreen;
            printDialog.UseEXDialog = true;
            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument_frmscreen.DocumentName = "Test Page Print";
                printDocument_frmscreen.Print();
            }
            /*
            Note: In case you want to show the Print Preview Dialog instead of 
            Print Dialog then comment the above code and uncomment the following code
            */

            //Open the print preview dialog
            //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            //objPPdialog.Document = printDocument1;
            //objPPdialog.ShowDialog();
        }

        private void printDocument_frmscreen_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iCount = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument_frmscreen_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dataGridView.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("AKE STAJYER TAKİP SİSTEMİ", new System.Drawing.Font(dataGridView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("AKE STAJYER TAKİP SİSTEMİ", new System.Drawing.Font(dataGridView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new System.Drawing.Font(dataGridView.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new System.Drawing.Font(dataGridView.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("AKE STAJYER TAKİP SİSTEMİ", new System.Drawing.Font(new System.Drawing.Font(dataGridView.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new System.Drawing.Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new System.Drawing.Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox_yazdır_MouseHover(object sender, EventArgs e)
        {
            pictureBox_yazdır.Height = 42;
            pictureBox_yazdır.Width = 42;
        }

        private void pictureBox_yazdır_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_yazdır.Height = 32;
            pictureBox_yazdır.Width = 32;
        }
        #endregion

        #region EXCEL MOUSE OLAYLARI & EXCELE KAYDETME
        private void pictureBox_excel_MouseHover(object sender, EventArgs e)
        {
            pictureBox_excel.Height = 42;
            pictureBox_excel.Width = 42;
        }

        private void pictureBox_excel_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_excel.Height = 32;
            pictureBox_excel.Width = 32;
        }

        private void pictureBox_excel_Click(object sender, EventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int StartCol = 1;
            int StartRow = 1;
            for (int j = 0; j < dataGridView.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                myRange.Value2 = dataGridView.Columns[j].HeaderText;
            }
            StartRow++;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {

                    Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                    myRange.Value2 = dataGridView[j, i].Value == null ? "" : dataGridView[j, i].Value;
                    myRange.Select();


                }
            }
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)// TEXTBOXA YAZILAN ADSOYAD GİRİŞİNE GÖRE ARAMA YAPIYOR.
        {
            if (textBox_adsoyadara.Text == String.Empty)
            {
                command.Connection = connection;
                connection.Open();
                command.CommandText = "SELECT i.*, s.* FROM intern as i JOIN InternInformation as s on i.tc_kimlikno=s.tc_kimlikno";
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            }
            else
            {
                command.Connection = connection;
                connection.Open();
                command.CommandText = "SELECT i.*, s.* FROM intern as i JOIN InternInformation as s on i.tc_kimlikno = s.tc_kimlikno where i.ad_soyad in (SELECT ad_soyad FROM intern where ad_soyad LIKE  '%" + textBox_adsoyadara.Text + "%')";
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)// SAĞ TIKLA DATAGRİDVİEW ÜZERİNDEN STAJYER SİLİYORUZ.
        {
            command.Connection = connection;
            connection.Open();
            string secili = dataGridView.CurrentRow.Cells[0].Value.ToString();
            command.CommandText = "DELETE FROM intern where tc_kimlikno=@tc_kimlikno";
            command.Parameters.AddWithValue("@tc_kimlikno", secili);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            MessageBox.Show("STAJYER SİLİNDİ");

            datatable= new System.Data.DataTable();
            dataadapter= new SqlDataAdapter("SELECT i.* , s.* FROM intern i Left Join InternInformation s on i.tc_kimlikno=s.tc_kimlikno ",connection);
            dataadapter.Fill(datatable);
            dataGridView.DataSource = datatable;
            dataadapter.Dispose();
            connection.Close();
        }

        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e) // ## STAJYER BİLGİLERİ GÜNCELENİYOR AMA STAJ BİLGİLERİ GÜNCELLENMİYOR.
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
                string stajyer = "update intern set tc_kimlikno=@tc_kimlikno,ad_soyad=@ad_soyad,baba_adı=@baba_adı,anne_adı=@anne_adı,d_yeri=@d_yeri,d_tarih=@d_tarih,uyrugu=@uyrugu,cinsiyet=@cinsiyet,ev_tel=@ev_tel,cep_tel=@cep_tel,adres=@adres,e_posta=@e_posta,web_adres=@web_adres,boy=@boy,agırlık=@agırlık,kan_grubu=@kan_grubu,iban=@iban, acil_adsoyad=@acil_adsoyad,acil_adres=@acil_adres,acil_akrabalık_derecesi=@acil_akrabalık_derecesi, acil_telefon_no=@acil_telefon_no, acil_e_posta=@acil_e_posta  where tc_kimlikno=@tc_kimlikno";
                command = new SqlCommand(stajyer, connection);

                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.

                //kişisel veriler GÜNCELL KOD !!
                command.Parameters.AddWithValue("@tc_kimlikno", dataGridView.CurrentRow.Cells[0].Value.ToString());
                command.Parameters.AddWithValue("@ad_soyad", dataGridView.CurrentRow.Cells[1].Value.ToString());
                command.Parameters.AddWithValue("@baba_adı", dataGridView.CurrentRow.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@anne_adı", dataGridView.CurrentRow.Cells[3].Value.ToString());
                command.Parameters.AddWithValue("@d_yeri", dataGridView.CurrentRow.Cells[4].Value.ToString());
                command.Parameters.AddWithValue("@d_tarih", dataGridView.CurrentRow.Cells[5].Value.ToString());
                command.Parameters.AddWithValue("@uyrugu", dataGridView.CurrentRow.Cells[6].Value.ToString());
                command.Parameters.AddWithValue("@cinsiyet", dataGridView.CurrentRow.Cells[7].Value.ToString());
                command.Parameters.AddWithValue("@ev_tel", dataGridView.CurrentRow.Cells[8].Value.ToString());
                command.Parameters.AddWithValue("@cep_tel", dataGridView.CurrentRow.Cells[9].Value.ToString());
                command.Parameters.AddWithValue("@adres", dataGridView.CurrentRow.Cells[10].Value.ToString());
                command.Parameters.AddWithValue("@e_posta", dataGridView.CurrentRow.Cells[11].Value.ToString());
                command.Parameters.AddWithValue("@web_adres", dataGridView.CurrentRow.Cells[12].Value.ToString());
                command.Parameters.AddWithValue("@boy", dataGridView.CurrentRow.Cells[13].Value.ToString());
                command.Parameters.AddWithValue("@agırlık", dataGridView.CurrentRow.Cells[14].Value.ToString());
                command.Parameters.AddWithValue("@kan_grubu", dataGridView.CurrentRow.Cells[15].Value.ToString());
                command.Parameters.AddWithValue("@iban", dataGridView.CurrentRow.Cells[16].Value.ToString());

                //acil durum irtibat GÜNCELL KOD !!
                command.Parameters.AddWithValue("@acil_adsoyad", dataGridView.CurrentRow.Cells[17].Value.ToString());
                command.Parameters.AddWithValue("@acil_adres", dataGridView.CurrentRow.Cells[18].Value.ToString());
                command.Parameters.AddWithValue("@acil_akrabalık_derecesi", dataGridView.CurrentRow.Cells[19].Value.ToString());
                command.Parameters.AddWithValue("@acil_telefon_no", dataGridView.CurrentRow.Cells[20].Value.ToString());
                command.Parameters.AddWithValue("@acil_e_posta", dataGridView.CurrentRow.Cells[21].Value.ToString());

                command.ExecuteNonQuery();

                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                connection.Close();
                MessageBox.Show("Stajyer Bilgileri Güncellendi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        private void button_s_ara_Click(object sender, EventArgs e)  // ## SORGULARI DÜZELT SADECE SORGU SONRASINDA STAJYER BİLGİSİ GELİYOR, STAJ BİLGİLERİ YOK. EKSİK OLASILIKLARI KONTROL ET!!
        {
            //command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
            //command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
            //command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
            //command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
            //command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
            //command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);

            if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT i.*, s.* FROM intern as i WHERE tc_kimlikno as s in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //0
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //1
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //3
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }//4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);

                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion

            } //5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //1 2
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where  staj_donem=@staj_donem and  bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }//1 3
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where  staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //1 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 2 4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_adı and  referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 2 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konularI=@staj_konusu )";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);

                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 2 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);


                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion

            } //3 4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 3 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu )";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);

                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 3 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);


                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);

                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 4 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);


                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 3
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 2 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 3 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 3 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 3 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);


                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where  staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);


                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 5 6



            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3 4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);


                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 2 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);


                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 2 4 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);


                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 2 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);



                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);
                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //3 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);


                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);

                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //3 4 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_Adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);


                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 3 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);


                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 4 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);



                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 3 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);


                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);

                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 3 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);


                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 3 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);


                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);





                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 2 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);



                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 3 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 3 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_donem=@staj_donem and okul_adı=@okul_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } // 1 4 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);



                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3 4 6
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);


                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);




                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //3 4 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where  referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);




                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 3 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);



                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion

            }// 1 2 3 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where okul_adı=@okul_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);






                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 2 4 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_donem=@staj_donem)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);







                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            }// 1 3 4 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternInformation where staj_yılı=@staj_yılı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();

                #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
                comboBox_s_yıl.Text = String.Empty;
                comboBox_s_donem.Text = String.Empty;
                comboBox_s_okul.Text = String.Empty;
                comboBox_s_bolum.Text = String.Empty;
                comboBox_s_stajkonuları.Text = String.Empty;
                comboBox_s_referans.Text = String.Empty;
                #endregion
            } //2 3 4 5 6

            else
            {
                MessageBox.Show("LÜTFEN RAPORLAMA KRİTERİNİ/KRİTERLERİNİ GİRİNİZ !!");
            }

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJYER BULUNMUŞTUR";
            #endregion

        }



    }
}


