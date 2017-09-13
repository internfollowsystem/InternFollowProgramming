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
using System.Drawing.Printing;

namespace InternFollowProgramming
{
    public partial class FrmStajyerBilgiEkrani : Form
    {
        #region baglantımız
        //static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=20fbgsbjk07";
        static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN; MultipleActiveResultSets=True;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
        SqlDataReader datareader;
        SqlCommand cmd = new SqlCommand();
        #endregion

        public FrmStajyerBilgiEkrani()
        {
            InitializeComponent();
        }

        private void FrmStajyerBilgiEkrani_Load(object sender, EventArgs e)
        {
            label_tc.Text = FrmScreen.gonderilecekveri;
            connection.Open();
            cmd.Connection = connection;
            command.Connection = connection;
            string stajyer = "SELECT * from stajyer where tc_kimlikno=@tc_kimlikno";
            command = new SqlCommand(stajyer, connection);
            command.Parameters.AddWithValue("@tc_kimlikno", label_tc.Text);

            dataadapter = new SqlDataAdapter(command);
            SqlDataReader drstajyer = command.ExecuteReader();
            if (drstajyer.Read())
            {
                label_tc.Text = drstajyer["tc_kimlikno"].ToString();   //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım.       
                label_adsoyad.Text = drstajyer["adı_soyadı"].ToString();
                label_babaadı.Text = drstajyer["baba_adı"].ToString();
                label_anneadı.Text = drstajyer["anne_adı"].ToString();
                label_dyeri.Text = drstajyer["dogum_yeri"].ToString();
                label_dtarih.Text = drstajyer["dogum_tarihi"].ToString();
                label_uyrugu.Text = drstajyer["uyrugu"].ToString();
                label_website.Text = drstajyer["web_site"].ToString();
                label_kangrubu.Text = drstajyer["kan_grubu"].ToString();
                label_cinsiyet.Text = drstajyer["cinsiyet"].ToString();
                label_evtel.Text = drstajyer["ev_telefonu"].ToString();
                label_ceptel.Text = drstajyer["cep_telefonu"].ToString();
                label_ikametgah.Text = drstajyer["ikametgah"].ToString();
                label_eposta.Text = drstajyer["e_posta"].ToString();
                label_boy.Text = drstajyer["boy"].ToString();
                label_agırlık.Text = drstajyer["agırlık"].ToString();
                label_ai_adsoyad.Text = drstajyer["acil_adsoyad"].ToString();
                label_ai_adres.Text = drstajyer["acil_adres"].ToString();
                label_ai_yakınlıgı.Text = drstajyer["acil_yakınlıgı"].ToString();
                label_ai_eposta.Text = drstajyer["acil_eposta"].ToString();
                label_ai_telefon.Text = drstajyer["acil_telefon"].ToString();
                label_ortaokul.Text = drstajyer["ortaokul_adı"].ToString();
                label_lise.Text = drstajyer["lise_adı"].ToString();
                label_universite.Text = drstajyer["universite_adı"].ToString();


				#region Resmi Göster
				string ResimYolu = @"" + @"O:STAJER_TAKIP\\StajyerGörselleri\\" + label_tc.Text + "\\kişiselgörsel.jpg";
				pictureBox_stajyer.ImageLocation = ResimYolu;
				pictureBox_stajyer.SizeMode = PictureBoxSizeMode.Zoom;
				#endregion
			}
			//Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
			else
            {
                MessageBox.Show("Kayıtlı Stajyer Bulunamadı");
            }
            drstajyer.Close();

            #region COMBOBOX'IN İÇİNE STAJ BİLGİLERİNİ ÇEK
            string staj = "SELECT staj_turu FROM stajbilgileri where tc_kimlikno=@tc_kimlikno";
            cmd = new SqlCommand(staj, connection);
            cmd.Parameters.AddWithValue("@tc_kimlikno", label_tc.Text);
            datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_stajturu.Items.Add(datareader["staj_turu"]);
            }
            datareader.Close();
            connection.Close();
            #endregion

        }

        private void comboBox_stajturu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "Select staj_id from stajbilgileri where tc_kimlikno=@tc_kimlikno and staj_turu=@staj_turu";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Connection = connection;
            command.Parameters.AddWithValue("@tc_kimlikno", label_tc.Text);
            command.Parameters.AddWithValue("@staj_turu", comboBox_stajturu.Text);
            connection.Open();
            dataadapter = new SqlDataAdapter(command);
            SqlDataReader stajkodu = command.ExecuteReader();
            while (stajkodu.Read())
            {

                textBox_stajkodu.Text = stajkodu["staj_id"].ToString();

            }
            stajkodu.Close();
            connection.Close();

            string staj = "SELECT staj_id, tc_kimlikno,egitim_durumu,okul_adı,bolum_adı,sınıf,okul_no,sehir,okul_puanı,okul_acıklama,banka_adı,şube_kodu,hesap_no,iban_no,staj_kabuldurumu,staj_donem,baslangıc_tarihi,bitis_tarihi,staj_yılı,staj_yapmadurumu,staj_suresi,servis_imkanı,arac_plaka,mentör,sigorta_evrak,basvuru_turu,referans_adı,referans_adres,referans_telefon,referans_eposta,staj_acıklama,staj_turu,staj_icerigi, staj_kalan_sure FROM stajbilgileri where tc_kimlikno=@tc_kimlikno and staj_id=@staj_id";
            command = new SqlCommand(staj, connection);
            command.Parameters.AddWithValue("@tc_kimlikno", label_tc.Text);
            command.Parameters.AddWithValue("@staj_id", textBox_stajkodu.Text);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            dataadapter = new SqlDataAdapter(command);
            SqlDataReader drstaj = command.ExecuteReader();
            while (drstaj.Read() == true)
            {
                textBox_stajkodu.Text = drstaj["staj_id"].ToString();
                label_tc.Text = drstaj["tc_kimlikno"].ToString();
                label_kabuldurumu.Text = drstaj["staj_kabuldurumu"].ToString();
                label_donemi.Text = drstaj["staj_donem"].ToString();
                label_baslangıctarihi.Text = drstaj["baslangıc_tarihi"].ToString();
                label_bitistarihi.Text = drstaj["bitis_tarihi"].ToString();
                label_staj_yapmadurumu.Text = drstaj["staj_yapmadurumu"].ToString();
                label_stajsure.Text = drstaj["staj_suresi"].ToString();
                label_servis.Text = drstaj["servis_imkanı"].ToString();
                label_plaka.Text = drstaj["arac_plaka"].ToString();
                label_mentöradı.Text = drstaj["mentör"].ToString();
                label_sigorta.Text = drstaj["sigorta_evrak"].ToString();
                label_yıl.Text = drstaj["staj_yılı"].ToString();
                label_basvuru_turu.Text = drstaj["basvuru_turu"].ToString();
                label_radı.Text = drstaj["referans_adı"].ToString();
                label_radres.Text = drstaj["referans_adres"].ToString();
                label_rmail.Text = drstaj["referans_eposta"].ToString();
                label_rtelefon.Text = drstaj["referans_telefon"].ToString();
                label_saciklama.Text = drstaj["staj_acıklama"].ToString();
                label_egitimdurumu.Text = drstaj["egitim_durumu"].ToString();
                label_okul.Text = drstaj["okul_adı"].ToString();
                label_bolum.Text = drstaj["bolum_adı"].ToString();
                label_sınıff.Text = drstaj["sınıf"].ToString();
                label_okulnumarası.Text = drstaj["okul_no"].ToString();
                label_sehir.Text = drstaj["sehir"].ToString();
                label_okulpuanı.Text = drstaj["okul_puanı"].ToString();
                label_oacıklama.Text = drstaj["okul_acıklama"].ToString();
                label_bankaadı.Text = drstaj["banka_adı"].ToString();
                label_subekodu.Text = drstaj["şube_kodu"].ToString();
                label_hesap_no.Text = drstaj["hesap_no"].ToString();
                label_iban_no.Text = drstaj["iban_no"].ToString();
                label_kalan_sure.Text = drstaj["staj_kalan_sure"].ToString();
                label_staj_turu.Text = drstaj["staj_turu"].ToString();
                label_staj_icerigi.Text = drstaj["staj_icerigi"].ToString();
            }
            drstaj.Close();
            connection.Close();

        }

        private Bitmap Snapshot()
        {
            // bitmap nesnesi oluştur
            Bitmap Screenshot = new Bitmap(this.Width, this.Height);

            // bitmapten grafik nesnesi oluştur
            Graphics GFX = Graphics.FromImage(Screenshot);

            // ekrandan programın bulunduğu konumun resmini alalım
            GFX.CopyFromScreen(this.Left, this.Top, 0, 0, this.Size);
            return Screenshot;
        }

        void printBitmap(Object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\\Users\\Win\\Desktop\\AKE\\program_goruntusu.jpg");
            e.Graphics.DrawImage(img, new Point(0, 0));
        }

        private void pictureBox_yazdır_Click(object sender, EventArgs e)
        {
            Snapshot().Save(@"C:\\Users\\Win\\Desktop\\AKE\\program_goruntusu.jpg");
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printBitmap);
            pd.Print();
            File.Delete(@"C:\\Users\\Win\\Desktop\\AKE\\program_goruntusu.jpg");
        }
    }
}
