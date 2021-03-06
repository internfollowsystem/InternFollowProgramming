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
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.IO;




namespace InternFollowProgramming
{
    public partial class FrmScreen : Form
    {
        #region YAPILACAKLAR
        //  BİTTİ1.Görsel butonlara photoshoptan yazı eklenip daha estetik ve kullanıcı odaklı arayüzü hazırlanacak.
        //  BİTTİ 4.StripCoolMenu'den stajbilgisi sil özelliği olsun. (Kullanıcı odaklı)
        //  BİTTİ 5.stajyer göster form ekranı olsun.(Kullanıcı Odaklı)
#endregion

        //22 AĞUSTOS 2017 GÜNCEL !! 
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
      
        #endregion

        //22 AĞUSTOS 2017 GÜNCEL !! 
        #region baglantımız
       static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=*********";
        //
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
        System.Data.DataTable datatable;
        DataSet ds;
       
        #endregion

        int kayitSayisi;
        public FrmScreen()//22 AĞUSTOS 2017 GÜNCEL !!
        {
            InitializeComponent();
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM stajyer";
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
			stajToolStripMenuItem.Enabled = false;

		}

        private void FrmScreen_Load(object sender, EventArgs e)//22 AĞUSTOS 2017 GÜNCEL !!
        {
            command.Connection = connection;
            command.CommandText = "SELECT * FROM stajyer";
            command.CommandType = CommandType.Text;
            

            #region TÜM STAJYERLER LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand stajyer = new SqlCommand();
            stajyer.Connection = connection;
            stajyer.CommandText = "Select Count(tc_kimlikno) FROM stajyer ";
            kayitSayisi = Convert.ToInt32(stajyer.ExecuteScalar());
            label_stajyer.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region LİSANS LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand lisans = new SqlCommand();
            lisans.Connection = connection;
            lisans.CommandText = "SELECT Count(staj_id) FROM stajbilgileri where egitim_durumu='LİSANS'";
            kayitSayisi = Convert.ToInt32(lisans.ExecuteScalar());
            label_lisans.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region ONLİSANS LABELA STAJYER SAYISINI AKTARMA
            connection.Open();
            SqlCommand onlisans = new SqlCommand();
            onlisans.Connection = connection;
            onlisans.CommandText = "SELECT Count(Staj_id) FROM stajbilgileri where egitim_durumu= 'ÖN LİSANS'";
            kayitSayisi = Convert.ToInt32(onlisans.ExecuteScalar());
            label_onlisans.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region LİSE LABELE STAJYER SAYISINI AKTARMA
            connection.Open();
            SqlCommand lise = new SqlCommand();
            lise.Connection = connection;
            lise.CommandText = "SELECT Count(staj_id) FROM stajbilgileri where egitim_durumu= 'LİSE'";
            kayitSayisi = Convert.ToInt32(lise.ExecuteScalar());
            label_lise.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region ŞUAN STAJ YAPANLAR LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand aktif = new SqlCommand();
            aktif.Connection = connection;
            aktif.CommandText = "SELECT count(staj_id) FROM stajbilgileri where staj_yapmadurumu= 'STAJ YAPIYOR'";
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
            cmd1.CommandText = "SELECT DISTINCT staj_yılı FROM stajbilgileri";
            cmd2.CommandText = "SELECT DISTINCT staj_donem FROM stajbilgileri";
            cmd3.CommandText = "SELECT DISTINCT okul_adı FROM stajbilgileri";
            cmd4.CommandText = "SELECT DISTINCT bolum_adı FROM stajbilgileri";
            cmd5.CommandText = "SELECT DISTINCT staj_turu FROM stajbilgileri";
            cmd6.CommandText = "SELECT DISTINCT referans_adı FROM stajbilgileri";
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
                comboBox_s_stajkonuları.Items.Add(dr["staj_turu"]);
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = true;
			stajToolStripMenuItem.Enabled = false;
		}

        #region MENÜ BUTONLARI
        private void stajyerYönetimToolStripMenuItem_Click(object sender, EventArgs e)
        {
			gonderilecekveri = "TC Kimlik No İle Ara";
			FrmInternInformation frm = new FrmInternInformation();
            frm.Show();

        }

        private void genelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT * FROM stajyer i Left Join stajbilgileri s on i.tc_kimlikno=s.tc_kimlikno ";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
        }

        private void onlisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT i.* FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri where egitim_durumu='On Lisans')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
        }
        private void lisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT i.* FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri where egitim_durumu='Lisans')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion

        }
        private void liseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT i.* FROM stajyer as i  JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri where egitim_durumu='Lise')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
        }
        #endregion

        //22 AĞUSTOS 2017 GÜNCEL !! 
        #region ÜST SEKME BUTON ÖZELLİĞİ  
        private void pictureBox_genel_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT * FROM stajyer";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter = new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            ds = new DataSet();
            dataadapter.Fill(ds,"stajyer");
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = ds.Tables[0];
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();

            #region TABLODAKİ VERİ SAYISINI BULAN SORGU
            int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion

            mailGönderToolStripMenuItem.Enabled = true;
			stajToolStripMenuItem.Enabled = false;

		}

        private void label_genel_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT * FROM stajyer ";
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

            mailGönderToolStripMenuItem.Enabled = true;
			stajToolStripMenuItem.Enabled = false;
			#region TABLODAKİ VERİ SAYISINI BULAN SORGU
			int kayitsayisi;
            kayitsayisi = dataGridView.RowCount;
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
        }

        private void pictureBox_lisans_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri  where egitim_durumu='Lisans')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}

        private void label1_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri  where egitim_durumu='Lisans')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}

        private void pictureBox_onlisans_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri where egitim_durumu='ÖN LİSANS')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}

        private void label2_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri where egitim_durumu='ÖN LİSANS')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}

        private void pictureBox_lise_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri  where egitim_durumu='Lise')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}

        private void label3_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where s.egitim_durumu in (SELECT egitim_durumu FROM stajbilgileri where egitim_durumu='Lise')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}

        private void pictureBox_suanstajer_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where s.staj_yapmadurumu in (SELECT staj_yapmadurumu FROM stajbilgileri  where staj_yapmadurumu='STAJ YAPIYOR')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}

        private void label_suanstajyer_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string kayit = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where s.staj_yapmadurumu in (SELECT staj_yapmadurumu FROM stajbilgileri where staj_yapmadurumu='STAJ YAPIYOR')";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion
            mailGönderToolStripMenuItem.Enabled = false;
			stajToolStripMenuItem.Enabled = true;
		}
        #endregion 

        //22 AĞUSTOS 2017 GÜNCEL !! 
        #region ÜST SEKME MOUSE OLAYLARI
        private void pictureBox_lisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.Slisans;
            pictureBox_lisans.Height = 30;
            pictureBox_lisans.Width = 30;
        }

        private void pictureBox_onlisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.Sönlisans;
            pictureBox_onlisans.Height = 30;
            pictureBox_onlisans.Width = 30;

        }

        private void pictureBox_lise_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.Slise;
            pictureBox_lise.Height = 30;
            pictureBox_lise.Width = 30;
        }

        private void pictureBox_lisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.yeni_lisans_stajyer1;
            pictureBox_lisans.Height = 25;
            pictureBox_lisans.Width = 25;
        }

        private void pictureBox_onlisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.yeni_önlisans_stajyer1;
            pictureBox_onlisans.Height = 25;
            pictureBox_onlisans.Width = 25;
        }

        private void pictureBox_lise_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.yeni_lise_stajyer1;
            pictureBox_lise.Height = 25;
            pictureBox_lise.Width = 25;
        }

        private void pictureBox_genel_MouseHover(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.genel_stajer;
            pictureBox_genel.Height = 30;
            pictureBox_genel.Width = 30;
        }

        private void pictureBox_genel_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.Yeni_genel_stajyer1;
            pictureBox_genel.Height = 25;
            pictureBox_genel.Width = 25;
        }


        private void pictureBox_suanstajer_MouseHover(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.Sstajyapıyor;
            pictureBox_suanstajer.Height = 30;
            pictureBox_suanstajer.Width = 30;
        }

        private void pictureBox_suanstajer_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.yeni_staj_yapıyor2;
            pictureBox_suanstajer.Height = 25;
            pictureBox_suanstajer.Width = 25;
        }


        private void label_genel_MouseHover(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.genel_stajer;
            pictureBox_genel.Height = 30;
            pictureBox_genel.Width = 30;
        }

        private void label_genel_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_genel.Image = Properties.Resources.Yeni_genel_stajyer1;
            pictureBox_genel.Height = 25;
            pictureBox_genel.Width = 25;
        }

        private void labellisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.Slisans;
            pictureBox_lisans.Height = 30;
            pictureBox_lisans.Width = 30;
        }

        private void labellisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lisans.Image = Properties.Resources.yeni_lisans_stajyer1;
            pictureBox_lisans.Height = 25;
            pictureBox_lisans.Width = 25;
        }

        private void labelonlisans_MouseHover(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.Sönlisans;
            pictureBox_onlisans.Height = 30;
            pictureBox_onlisans.Width = 30;
        }

        private void labelonlisans_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_onlisans.Image = Properties.Resources.yeni_önlisans_stajyer1;
            pictureBox_onlisans.Height = 25;
            pictureBox_onlisans.Width = 25;
        }

        private void labellise_MouseHover(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.Slise;
            pictureBox_lise.Height = 30;
            pictureBox_lise.Width = 30;
        }

        private void labellise_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_lise.Image = Properties.Resources.yeni_lise_stajyer1;
            pictureBox_lise.Height = 25;
            pictureBox_lise.Width = 25;
        }

        private void label_suanstajyer_MouseHover(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.Sstajyapıyor;
            pictureBox_suanstajer.Height = 30;
            pictureBox_suanstajer.Width = 30;
        }

        private void label_suanstajyer_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.yeni_staj_yapıyor2;
            pictureBox_suanstajer.Height = 25;
            pictureBox_suanstajer.Width = 25;
        }
        #endregion//22 AĞUSTOS 2017 GÜNCEL !!

        //22 AĞUSTOS 2017 GÜNCEL !! 
        #region REFRESH MOUSE OLAYLARI & SAYFA YENİLEME
        private void pictureBox_refresh_MouseHover(object sender, EventArgs e)
        {
            pictureBox_refresh.Height = 30;
            pictureBox_refresh.Width = 30;
        }

        private void pictureBox_refresh_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_refresh.Height = 25;
            pictureBox_refresh.Width = 25;
        }

        private void pictureBox_refresh_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmScreen frmscreen = new FrmScreen();
            frmscreen.Show();
			mailGönderToolStripMenuItem.Enabled = true;

		}
        #endregion

        //22 AĞUSTOS 2017 GÜNCEL !! 
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
            pictureBox_yazdır.Height = 30;
            pictureBox_yazdır.Width = 30;
        }

        private void pictureBox_yazdır_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_yazdır.Height = 25;
            pictureBox_yazdır.Width = 25;
        }
        #endregion

        //22 AĞUSTOS 2017 GÜNCEL !! 
        #region EXCEL MOUSE OLAYLARI & EXCELE KAYDETME
        private void pictureBox_excel_MouseHover(object sender, EventArgs e)
        {
            pictureBox_excel.Height = 30;
            pictureBox_excel.Width = 30;
        }

        private void pictureBox_excel_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_excel.Height = 25;
            pictureBox_excel.Width = 25;
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

        //22 AĞUSTOS 2017 GÜNCEL !! 
        private void textBox1_TextChanged(object sender, EventArgs e)// TEXTBOXA YAZILAN ADSOYAD GİRİŞİNE GÖRE ARAMA YAPIYOR.
        {
            if (textBox_adsoyadara.Text == String.Empty)
            {
                command.Connection = connection;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                command.CommandText = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Left Join stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno";
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
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
				command.CommandText = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Left Join stajbilgileri as s on i.tc_kimlikno = s.tc_kimlikno where i.adı_soyadı in (SELECT adı_soyadı FROM stajyer where adı_soyadı LIKE  '%" + textBox_adsoyadara.Text + "%')";
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            }
			stajToolStripMenuItem.Enabled = true;
		}

        //22 AĞUSTOS 2017 GÜNCEL !! 
        private void stajyerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
           //label_tcsil.Text= dataGridView.CurrentRow.Cells[0].Value.ToString();
           
            command.CommandText = "DELETE FROM stajyer where tc_kimlikno=@tc_kimlikno";
            command.Parameters.AddWithValue("@tc_kimlikno", dataGridView.CurrentRow.Cells[0].Value.ToString());
            command.ExecuteNonQuery();

			//string secili = dataGridView.CurrentRow.Cells[0].Value.ToString();
			//string stajturu = dataGridView.CurrentRow.Cells[4].Value.ToString();
			if (Directory.Exists("O:STAJER_TAKIP\\StajyerGörselleri\\" + dataGridView.CurrentRow.Cells[0].Value.ToString())&& Directory.Exists("O:STAJER_TAKIP\\StajyerDosyaları\\" + dataGridView.CurrentRow.Cells[0].Value.ToString() + "_" + dataGridView.CurrentRow.Cells[4].Value.ToString()))
			{
				Directory.Delete("O:STAJER_TAKIP\\StajyerDosyaları\\" + dataGridView.CurrentRow.Cells[0].Value.ToString() + "_" + dataGridView.CurrentRow.Cells[4].Value.ToString(), true);
				Directory.Delete("O:STAJER_TAKIP\\StajyerGörselleri\\" + dataGridView.CurrentRow.Cells[0].Value.ToString(), true);
			}
			
			MessageBox.Show("STAJYER SİLİNDİ");

            datatable = new System.Data.DataTable();
            dataadapter = new SqlDataAdapter("SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer i Left Join stajbilgileri s on i.tc_kimlikno=s.tc_kimlikno ", connection);
            dataadapter.Fill(datatable);
            dataGridView.DataSource = datatable;
            dataadapter.Dispose();
            connection.Close();

            #region TÜM STAJYERLER LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand stajyer = new SqlCommand();
            stajyer.Connection = connection;
            stajyer.CommandText = "Select Count(tc_kimlikno) FROM stajyer ";
            kayitSayisi = Convert.ToInt32(stajyer.ExecuteScalar());
            label_stajyer.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region LİSANS LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand lisans = new SqlCommand();
            lisans.Connection = connection;
            lisans.CommandText = "SELECT Count(tc_kimlikno) FROM stajbilgileri where egitim_durumu='Lisans'";
            kayitSayisi = Convert.ToInt32(lisans.ExecuteScalar());
            label_lisans.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region ONLİSANS LABELA STAJYER SAYISINI AKTARMA
            connection.Open();
            SqlCommand onlisans = new SqlCommand();
            onlisans.Connection = connection;
            onlisans.CommandText = "SELECT Count(tc_kimlikno) FROM stajbilgileri where egitim_durumu= 'On Lisans'";
            kayitSayisi = Convert.ToInt32(onlisans.ExecuteScalar());
            label_onlisans.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region LİSE LABELE STAJYER SAYISINI AKTARMA
            connection.Open();
            SqlCommand lise = new SqlCommand();
            lise.Connection = connection;
            lise.CommandText = "SELECT Count(tc_kimlikno) FROM stajbilgileri where egitim_durumu= 'Lise'";
            kayitSayisi = Convert.ToInt32(lise.ExecuteScalar());
            label_lise.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
            #endregion

            #region ŞUAN STAJ YAPANLAR LABELA STAJYER SAYISINI AKTARMA     
            connection.Open();
            SqlCommand aktif = new SqlCommand();
            aktif.Connection = connection;
            aktif.CommandText = "SELECT count(tc_kimlikno) FROM stajbilgileri where staj_yapmadurumu= 'STAJ YAPIYOR'";
            kayitSayisi = Convert.ToInt32(aktif.ExecuteScalar());
            label_suanstajyapanlar.Text = Convert.ToString(kayitSayisi.ToString());
            connection.Close();
			#endregion

			stajToolStripMenuItem.Enabled = true;
		}

		private void stajToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//label_idsil.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
			connection.Close();
			command.Connection = connection;
			connection.Open();
			command.CommandText = "DELETE FROM stajbilgileri where staj_id=@staj_id";
			command.Parameters.AddWithValue("@staj_id", dataGridView.CurrentRow.Cells[2].Value.ToString());
			command.ExecuteNonQuery();
			command.Parameters.Clear();
			if (Directory.Exists("O:STAJER_TAKIP\\StajyerDosyaları\\" + dataGridView.CurrentRow.Cells[0].Value.ToString() + "_" + dataGridView.CurrentRow.Cells[4].Value.ToString()))
			{
				Directory.Delete("O:STAJER_TAKIP\\StajyerDosyaları\\" + dataGridView.CurrentRow.Cells[0].Value.ToString() + "_" + dataGridView.CurrentRow.Cells[4].Value.ToString(), true);
			}
			MessageBox.Show("STAJ SİLİNDİ");

			datatable = new System.Data.DataTable();
			dataadapter = new SqlDataAdapter("SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno", connection);
			dataadapter.Fill(datatable);
			dataGridView.DataSource = datatable;
			dataadapter.Dispose();
			connection.Close();

			#region TÜM STAJYERLER LABELA STAJYER SAYISINI AKTARMA     
			connection.Open();
			SqlCommand stajyer = new SqlCommand();
			stajyer.Connection = connection;
			stajyer.CommandText = "Select Count(tc_kimlikno) FROM stajyer ";
			kayitSayisi = Convert.ToInt32(stajyer.ExecuteScalar());
			label_stajyer.Text = Convert.ToString(kayitSayisi.ToString());
			connection.Close();
			#endregion

			#region LİSANS LABELA STAJYER SAYISINI AKTARMA     
			connection.Open();
			SqlCommand lisans = new SqlCommand();
			lisans.Connection = connection;
			lisans.CommandText = "SELECT Count(tc_kimlikno) FROM stajbilgileri where egitim_durumu='Lisans'";
			kayitSayisi = Convert.ToInt32(lisans.ExecuteScalar());
			label_lisans.Text = Convert.ToString(kayitSayisi.ToString());
			connection.Close();
			#endregion

			#region ONLİSANS LABELA STAJYER SAYISINI AKTARMA
			connection.Open();
			SqlCommand onlisans = new SqlCommand();
			onlisans.Connection = connection;
			onlisans.CommandText = "SELECT Count(tc_kimlikno) FROM stajbilgileri where egitim_durumu= 'On Lisans'";
			kayitSayisi = Convert.ToInt32(onlisans.ExecuteScalar());
			label_onlisans.Text = Convert.ToString(kayitSayisi.ToString());
			connection.Close();
			#endregion

			#region LİSE LABELE STAJYER SAYISINI AKTARMA
			connection.Open();
			SqlCommand lise = new SqlCommand();
			lise.Connection = connection;
			lise.CommandText = "SELECT Count(tc_kimlikno) FROM stajbilgileri where egitim_durumu= 'Lise'";
			kayitSayisi = Convert.ToInt32(lise.ExecuteScalar());
			label_lise.Text = Convert.ToString(kayitSayisi.ToString());
			connection.Close();
			#endregion

			#region ŞUAN STAJ YAPANLAR LABELA STAJYER SAYISINI AKTARMA     
			connection.Open();
			SqlCommand aktif = new SqlCommand();
			aktif.Connection = connection;
			aktif.CommandText = "SELECT count(tc_kimlikno) FROM stajbilgileri where staj_yapmadurumu= 'STAJ YAPIYOR'";
			kayitSayisi = Convert.ToInt32(aktif.ExecuteScalar());
			label_suanstajyapanlar.Text = Convert.ToString(kayitSayisi.ToString());
			connection.Close();
			#endregion
			stajToolStripMenuItem.Enabled = true;
		}
           
       
        //22AGUSTOS RAPORLAMA GÜNCEL DEĞİL SADECE COPY-PASTE OLACAK  ---- 23ağustos güncel gibi ama kontrol ett
        private void button_s_ara_Click(object sender, EventArgs e)  //RAPORLAMA
        {
            //command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
            //command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
            //command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
            //command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
            //command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
            //command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);

            if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)  and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);



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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı)  and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);

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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);


                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);

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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);

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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);


                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);
                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);


                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı)  and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı)  and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);


                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);



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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);


                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);

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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)  and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);



                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu) ";
                command = new SqlCommand(ara, connection);


                command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);

                command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);

                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem)  and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);
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
            }// 1 3 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);


                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
			else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
			{
				command.Connection = connection;
				connection.Open();
				string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)";
				command = new SqlCommand(ara, connection);
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
			}// 1 3 5 6

			else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where  s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.referans_adı in (SELECT referans_adı FROM stajbilgileri where referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);




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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where bolum_adı=@bolum_adı)";
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

			else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
			{
				command.Connection = connection;
				connection.Open();
				string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı)";
				command = new SqlCommand(ara, connection);
				command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);

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
			} //2 4 5 6
			else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in (SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı) and s.staj_donem in (SELECT staj_donem FROM stajbilgileri where  staj_donem=@staj_donem)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.referans_adı in (SELECT referans_adı FROM stajbilgileri where  referans_adı=@referans_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_turu in (SELECT staj_turu FROM stajbilgileri where staj_turu=@staj_turu)";
                command = new SqlCommand(ara, connection);



                command.Parameters.AddWithValue("@staj_turu", comboBox_s_stajkonuları.SelectedItem);



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

			else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
			{
				command.Connection = connection;
				connection.Open();
				string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.bolum_adı in (SELECT bolum_adı FROM stajbilgileri where  bolum_adı=@bolum_adı)";
				command = new SqlCommand(ara, connection);



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

			}// 1 2 3 5 6
			else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.okul_adı in (SELECT okul_adı FROM stajbilgileri where okul_adı=@okul_adı)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i JOIN stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_donem in (SELECT staj_donem FROM stajbilgileri where staj_donem=@staj_donem)";
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
                string ara = "SELECT  i.tc_kimlikno, i.adı_soyadı, s.staj_id, i.cinsiyet, s.staj_turu, s.staj_icerigi, s.egitim_durumu, s.okul_adı, s.bolum_adı, s.okul_acıklama, s.staj_kabuldurumu, s.staj_donem, s.baslangıc_tarihi, s.bitis_tarihi, s.staj_yılı, s.staj_yapmadurumu, s.staj_suresi, s.servis_imkanı, s.arac_plaka,s.mentör, s.basvuru_turu,s.referans_adı,s.staj_acıklama FROM stajyer as i Join stajbilgileri as s on i.tc_kimlikno=s.tc_kimlikno where s.staj_yılı in(SELECT staj_yılı FROM stajbilgileri where staj_yılı=@staj_yılı)";
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
            label_aranan_stajyer_sayısı.Text = kayitsayisi + " STAJ BULUNMUŞTUR";
            #endregion

            #region COMBOBOX'LARIN İÇİNİ SIFIRLIYOR.
            comboBox_s_yıl.Items.Clear();
            comboBox_s_donem.Items.Clear();
            comboBox_s_okul.Items.Clear();
            comboBox_s_bolum.Items.Clear();
            comboBox_s_stajkonuları.Items.Clear();
            comboBox_s_referans.Items.Clear();
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
            cmd1.CommandText = "SELECT DISTINCT staj_yılı FROM stajbilgileri";
            cmd2.CommandText = "SELECT DISTINCT staj_donem FROM stajbilgileri";
            cmd3.CommandText = "SELECT DISTINCT okul_adı FROM stajbilgileri";
            cmd4.CommandText = "SELECT DISTINCT bolum_adı FROM stajbilgileri";
            cmd5.CommandText = "SELECT DISTINCT staj_turu FROM stajbilgileri";
            cmd6.CommandText = "SELECT DISTINCT referans_adı FROM stajbilgileri";
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
                comboBox_s_stajkonuları.Items.Add(dr["staj_turu"]);
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
			stajToolStripMenuItem.Enabled = true;
			mailGönderToolStripMenuItem.Enabled = false;

		}

        public static string gonderilecekveri;
        private void mailGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string secili = dataGridView.CurrentRow.Cells[0].Value.ToString();
            command = new SqlCommand("SELECT e_posta FROM stajyer where tc_kimlikno=@tc_kimlikno", connection);
            connection.Open();
            command.Parameters.AddWithValue("tc_kimlikno", secili);
            dataadapter = new SqlDataAdapter(command);
            SqlDataReader datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                gonderilecekveri = datareader["e_posta"].ToString();
            }
            datareader.Close();

            if (gonderilecekveri == String.Empty)
            {
                MessageBox.Show("Stajyerin kayıtlı mail adresi bulunmamakta");
            }
            else
            {
                FrmMail frm = new FrmMail();
                frm.Show();
                
            }
            connection.Close();
            mailGönderToolStripMenuItem.Enabled = true;
        }

        private void sTAJYERGÖSTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
			   
            label_tcsil.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            command = new SqlCommand("SELECT tc_kimlikno FROM stajyer where tc_kimlikno=@tc_kimlikno", connection);
            connection.Open();
            command.Parameters.AddWithValue("tc_kimlikno", label_tcsil.Text);
            dataadapter = new SqlDataAdapter(command);
            SqlDataReader datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                gonderilecekveri = datareader["tc_kimlikno"].ToString();
            }
            datareader.Close();

            if (gonderilecekveri == "tc:")
            {
                MessageBox.Show("Stajyer tc'si alınamadı");
            }
            else
            {
                FrmStajyerBilgiEkrani sbe = new FrmStajyerBilgiEkrani();
                sbe.Show();

            }
            connection.Close();
        }

        private void label_hata_MouseHover(object sender, EventArgs e)
        {
            label_hata.Font = new System.Drawing.Font(label_hata.Font, label_hata.Font.Style ^ FontStyle.Underline);
        }

        private void label_hata_MouseLeave(object sender, EventArgs e)
        {
            label_hata.Font = new System.Drawing.Font(label_hata.Font, label_hata.Font.Style ); 
        }

        private void label_hata_Click(object sender, EventArgs e)
        {
            gonderilecekveri = "s.ozkaynak@outlook.com";
            FrmMail frm = new FrmMail();
            frm.Show();
        }

		private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			gonderilecekveri = dataGridView.CurrentRow.Cells[0].Value.ToString();
			FrmInternInformation düzenle = new FrmInternInformation();
			düzenle.Show();

		}
	}
}


