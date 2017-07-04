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
        //static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=20fbgsbjk07";
        static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
        
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
            string kayit = "SELECT *FROM intern where tc_kimlikno IN (SELECT tc_kimlikno FROM InternshipInformation where okul_turu='ÜNİVERSİTE')"; //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command= new SqlCommand(kayit, connection);//Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter= new SqlDataAdapter(command); //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable); //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = dataadapter;//Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            connection.Close();

        }

        private void FrmScreen_Load(object sender, EventArgs e)
        {     
            command.Connection = connection;
            connection.Open();

            command.CommandText = "SELECT *FROM InternshipInformation";
            command.CommandType = CommandType.Text;

            #region LİSANS LABELA STAJYER SAYISINI AKTARMA
            //            connection.Open();
            //            SqlCommand lisans = new SqlCommand();
            //            lisans.CommandText= "Select Count(*) From IntershipInformation Where okul_turu=”LİSANS”";
            //            SqlDataReader lsns;
            //            lsns = lisans.ExecuteReader(); 
            //            while (lsns.Read())
            //            {
            //                label_lisans.Text = lsns["okul_turu"].ToString();
            //            }
            //            lsns.Close();
            //            connection.Close();
            #endregion

            #region ONLİSANS LABELA STAJYER SAYISINI AKTARMA
            //            connection.Open();
            //            SqlCommand onlisans = new SqlCommand();
            //            onlisans.CommandText = "Select Count(*) From IntershipInformation Where okul_turu =”ÖN LİSANS”";
            //            SqlDataReader nlsns;
            //            nlsns = onlisans.ExecuteReader();
            //            while (nlsns.Read())
            //            {
            //                label_onlisans.Text = nlsns["okul_turu"].ToString();
            //            }
            //            nlsns.Close();
            //            connection.Close();
            #endregion

            #region LİSE LABELE STAJYER SAYISINI AKTARMA
            //            connection.Open();
            //            SqlCommand lise = new SqlCommand();
            //            lise.CommandText = "Select Count(*) From IntershipInformation Where okul_turu ='LİSE'";
            //            SqlDataReader ls;
            //            ls = lise.ExecuteReader();
            //            while (ls.Read())
            //            {
            //                label_lise.Text = ls["okul_turu"].ToString();
            //            }
            //            ls.Close();
            //            connection.Close();
            #endregion

          


            //dataadapter= new SqlDataAdapter("Select * from IntershipInformation where staj_yılı Like '%" + comboBox_yıl.Text + "%'", connection);

            //dataset.Clear();
            //dataadapter.Fill(dataset, "IntershipInformation");
            //dataGridView.DataSource = dataset.Tables["IntershipInformation"];
            //dataadapter.Dispose();
            connection.Close();

            #region RAPORLAMA COMBOBOXLARININ İÇİNE VERİ TABANINDAN VERİ ÇEKMEK
            connection.Open();  
            SqlDataReader dr;

            dr = command.ExecuteReader();
            while (dr.Read())
            {
                comboBox_s_yıl.Items.Add(dr["staj_yılı"]);
                comboBox_s_donem.Items.Add(dr["staj_donem"]);
                comboBox_s_okul.Items.Add(dr["okul_adı"]);
                comboBox_s_bolum.Items.Add(dr["bolum_adı"]);
                comboBox_s_stajkonuları.Items.Add(dr["staj_konusu"]);
                comboBox_s_referans.Items.Add(dr["referans_adı"]);
            }
            connection.Close();
            #endregion

            


        }
         
        private void onlisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='ÖN LİSANS' ";
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
        }
        private void lisansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='LİSANS' ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter= new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            System.Data.DataTable dt = new System.Data.DataTable();
            dataadapter.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();
           
        }
        private void liseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='LİSE' ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter= new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable = new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();
        }

        private void stajyerYönetimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInternInformation frm = new FrmInternInformation();
            frm.Show();
            
        }

        private void genelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM intern i Left Join InternshipInformation s on i.tc_kimlikno=s.tc_kimlikno ";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            command = new SqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            dataadapter= new SqlDataAdapter(command);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            datatable= new System.Data.DataTable();
            dataadapter.Fill(datatable);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView.DataSource = datatable;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            dataadapter.Dispose();
            connection.Close();
        }


        #region ÜST SEKME BUTON ÖZELLİĞİ
        private void pictureBox_genel_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation ";
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

        private void label_genel_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation ";
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

        private void pictureBox_lisans_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='LİSANS' ";
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
        }

        private void label1_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='LİSANS' ";
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
        }

        private void pictureBox_onlisans_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='ÖN LİSANS' ";
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
        }

        private void label2_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='ÖN LİSANS' ";
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
        }

        private void pictureBox_lise_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='LİSE' ";
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

        private void label3_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where okul_turu='LİSE' ";
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

        private void pictureBox_suanstajer_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where staj_durumu='ŞUAN STAJ YAPIYOR' ";
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

        private void label_suanstajyer_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string kayit = "SELECT *FROM InternshipInformation where staj_durumu='ŞUAN STAJ YAPIYOR' ";
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
            pictureBox_genel.Height =42;
            pictureBox_genel.Width =42;
        }


        private void pictureBox_suanstajer_MouseHover(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.Bşuanstajer;
            pictureBox_suanstajer.Height = 52;
            pictureBox_suanstajer.Width = 52;
        }

        private void pictureBox_suanstajer_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.şuanstajer;
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
            pictureBox_suanstajer.Image = Properties.Resources.Bşuanstajer;
            pictureBox_suanstajer.Height = 52;
            pictureBox_suanstajer.Width = 52;
        }

        private void label_suanstajyer_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_suanstajer.Image = Properties.Resources.şuanstajer;
            pictureBox_suanstajer.Height = 42;
            pictureBox_suanstajer.Width = 42;
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

        private void dataGridView_frmscreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//farenin sağ tuşuna basılmışsa
            {

                int satir = dataGridView.HitTest(e.X, e.Y).RowIndex;
                if (satir > -1) //www.ahmetcansever.com
                {
                    dataGridView.Rows[satir].Selected = true;//bu tıkladığımız alanı seçtiriyoruz
                    numara = Convert.ToInt32(dataGridView.Rows[satir].Cells["tc_kimlikno"].Value);
                }
            }
        }

        void Sil(int numara)
        {
            try
            {
                command.Connection = connection;
                connection.Open();
                string stajyer = "DELETE FROM intern where tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation WHERE tc_kimlikno = @tc_kimlikno)";
                command = new SqlCommand(stajyer, connection);
                command.Parameters.AddWithValue("@tc_kimlikno", numara);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Kayıt Silinmiştir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Kayıt Silinemedi");
            }

        }
        void Doldur()
        {
            connection.Close();
            connection.Open();
            dataadapter = new SqlDataAdapter("SELECT *FROM intern i Left Join InternshipInformation s on i.tc_kimlikno=s.tc_kimlikno ", connection);
            datatable = new System.Data.DataTable();
            
            dataadapter.Fill(datatable);
            connection.Close();
            dataGridView.DataSource = datatable;

        }



        public void digeriniYenile()
        {
            command.Connection = connection;
            connection.Open();
            dataadapter = new SqlDataAdapter("Select * from InternshipInformation", connection);
            dataset = new DataSet();
            dataadapter.Fill(dataset, "InternshipInformation");
            dataGridView.DataSource = dataset.Tables[0];
            connection.Close();
        }
        public void DataGridGuncelle()
        {
            commandbuilder = new SqlCommandBuilder(dataadapter);
            dataadapter.Update(dataset, "InternshipInformation");
            MessageBox.Show("Kayıt Güncellend");
            digeriniYenile();
        }

        
        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sil(numara);
            Doldur();
            
        }

        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridGuncelle();
        }

        private void button_s_ara_Click(object sender, EventArgs e)
        {
            //command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);
            //command.Parameters.AddWithValue("@staj_donem", comboBox_s_donem.SelectedItem);
            //command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul);
            //command.Parameters.AddWithValue("@bolum_adı", comboBox_s_bolum.SelectedItem);
            //command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);
            //command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);

            if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            } //1
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans)";
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
            } //2
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans)";
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
            } //3
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans)";
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
            }//4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and referans_adı=@referans_adı)";
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
            } //5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu)";
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
            } //6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            } //1 2
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where  staj_donem=@staj_donem and  bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            }//1 3
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            } // 1 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where  staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and referans_adı=@referans_adı)";
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
            } // 1 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu)";
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
            } //1 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            } //2 3
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            } // 2 4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_adı and  referans_adı=@referans)";
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
            } // 2 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_adı and staj_konularI=@staj_konusu )";
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
            } // 2 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and staj_konusu=@staj_konusu and referans_adı=@referans)";
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
            } //3 4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
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
            } // 3 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu )";
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
            } // 3 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and referans_adı=@referans_adı)";
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
            } // 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu)";
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
            } // 4 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
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
            } // 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            }// 1 2 3
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            }// 1 2 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
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
            } // 1 2 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
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
            }// 1 2 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            }// 1 3 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
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
            }// 1 3 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
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
            }// 1 3 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and referans_adı=@referans_adı)";
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
            } // 1 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_donem=@staj_donem and okul_adı=@okul_adı and staj_konusu=@staj_konusu)";
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
            } // 1 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where  staj_donem=@staj_donem and okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
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
            } // 1 5 6



            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            } //2 3 4
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
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
            } //2 3 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
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
            } //2 3 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and referans_adı=@referans_adı)";
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
            } // 2 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            } // 2 4 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
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
            } // 2 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and referans_adı=@referans_adı)";
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
            } //3 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and staj_konusu=@staj_konusu)";
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
            } //3 4 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and bolum_adı=@bolum_Adı)";
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
            } // 3 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem and okul_adı=@okul_adı)";
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
            } // 4 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_konusu=@staj_konusu and referans_adı=@referans_adı)";
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
            }// 1 2 3 4
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where bolum_adı=@bolum_Adı and referans_adı=@referans_adı)";
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
            }// 1 2 3 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where bolum_adı=@bolum_Adı and staj_konusu=@staj_konusu)";
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
            }// 1 2 3 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı and referans_adı=@referans_adı)";
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
            }// 1 2 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı and staj_konusu=@staj_konusu)";
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
            }// 1 2 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı and bolum_adı=@bolum_Adı)";
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
            } // 1 2 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and referans_adı=@referans_adı)";
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
            }// 1 3 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu)";
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
            }// 1 3 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_donem=@staj_donem and okul_adı=@okul_adı)";
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
            } // 1 4 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and referans_adı=@referans_adı)";
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
            } //2 3 4 5
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_konusu=@staj_konusu)";
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
            } //2 3 4 6
            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem != null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and bolum_adı=@bolum_Adı)";
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
            } //2 3 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı and staj_donem=@staj_donem)";
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
            } //3 4 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem != null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where  referans_adı=@referans_adı)";
                command = new SqlCommand(ara, connection);




                command.Parameters.AddWithValue("@referans_adı", comboBox_s_referans.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            }// 1 2 3 4 5
            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem != null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_konusu=@staj_konusu)";
                command = new SqlCommand(ara, connection);



                command.Parameters.AddWithValue("@staj_konusu", comboBox_s_stajkonuları.SelectedItem);



                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            }// 1 2 3 4 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem != null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where okul_adı=@okul_adı)";
                command = new SqlCommand(ara, connection);

               command.Parameters.AddWithValue("@okul_adı", comboBox_s_okul.SelectedItem);






                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            }// 1 2 4 5 6

            else if (comboBox_s_yıl.SelectedItem == null && comboBox_s_donem.SelectedItem != null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_donem=@staj_donem)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_donem", comboBox_s_yıl);







                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            }// 1 3 4 5 6

            else if (comboBox_s_yıl.SelectedItem != null && comboBox_s_donem.SelectedItem == null && comboBox_s_okul.SelectedItem == null && comboBox_s_bolum.SelectedItem == null && comboBox_s_stajkonuları.SelectedItem == null && comboBox_s_referans.SelectedItem == null)
            {
                command.Connection = connection;
                connection.Open();
                string ara = "SELECT * FROM intern WHERE tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation where staj_yılı=@staj_yılı)";
                command = new SqlCommand(ara, connection);
                command.Parameters.AddWithValue("@staj_yılı", comboBox_s_yıl.SelectedItem);


                command.ExecuteNonQuery();
                dataadapter = new SqlDataAdapter(command);
                datatable = new System.Data.DataTable();
                dataadapter.Fill(datatable);
                dataGridView.DataSource = datatable;
                dataadapter.Dispose();
                connection.Close();
            } //2 3 4 5 6

            else
            {
                MessageBox.Show("LÜTFEN RAPORLAMA KRİTERİNİ/KRİTERLERİNİ GİRİNİZ !!");
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if(e.KeyChar == (char)13)
            //{
            //    DataGridView dv = dt.DefaultView;
            //    dv.RowFilter = string.Format("ad_soyad like '%{0}%' OR tc_kimlik_no like '%{0}%'", textBox1.Text);
            //    dataGridView.DataSource = dv.ToTable();
            //}
        }
    }
}

