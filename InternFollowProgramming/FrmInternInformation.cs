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

using System.Data.OleDb;

namespace InternFollowProgramming
{
    public partial class FrmInternInformation : Form
    {

        #region baglantımız
        //static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=20fbgsbjk07";
        static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
        SqlDataReader datareader;
        
        SqlCommand cmd = new SqlCommand();
        #endregion


        byte[] byteData;

        String yol = "c:\\";
        String klasör = "Stajer Takip";

        public FrmInternInformation()
        {
            InitializeComponent();

            button_update.Enabled = false; //Form açıldığında güncelle butonu pasif olsun.
            button_delete.Enabled = false; //Form açıldığında delete butonu pasif olsun.
            panel_referans.Enabled = false;//Form açıldığında referans paneli pasif olsun.
            textBox_arac.Enabled = false;//Form açıldığında plaka textBoxı pasif olsun.
            textBox_iban.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.

        } 

        private void FrmInternInformation_Load(object sender, EventArgs e)
        {
           // connection.Open();
           // command.CommandText = "Select* From intern"; 
           // cmd.CommandText = "Select* From IntershipInformation";
           // command.Connection = connection;
           // cmd.Connection = connection;
           // command.CommandType = CommandType.Text;
           // cmd.CommandType = CommandType.Text;

           #region comboBox'ın içine verileri çekme kodu
            //SqlDataReader dr;
           //dr = command.ExecuteReader();

           //while (dr.Read())
           //{
           //    comboBoxAdsoyad.Items.Add(dr["ad_soyad"]);
           //}
           #endregion

           // connection.Close();
        }

        #region BUTONLAR

        //KAYDET BUTONU  BİTTİ GÜNCEL !!
       
        private void button_save_Click(object sender, EventArgs e)
        {
            
            string adsoyad;
            string tckimlik;
            tckimlik = textBox_tc.Text;
            adsoyad = textBox_adsoyad.Text;

            if (tckimlik.Length == 0)
            {
                MessageBox.Show("Lütfen Tc Kimlik No alanını doldurunuz !");
            }
            if (adsoyad.Length == 0)
            {
                MessageBox.Show("Lütfen Ad Soyad alanını doldurunuz !");
            }

            else
            {
                if (tckimlik.Length != 11 && textBox_tc.Text != "")
                {
                    MessageBox.Show("Lütfen  Tc kimlik numarasının 11 haneli olmasına dikkat ediniz !");
                }
                if (tckimlik.Length == 11)
                {
                    int index = 0;

                    int toplam = 0;

                    foreach (char n in tckimlik)

                    {

                        if (index < 10)

                        {

                            toplam += Convert.ToInt32(char.ToString(n));

                        }

                        index++;
                    }

                    if (toplam % 10 == Convert.ToInt32(tckimlik[10].ToString()))

                    {
                        try
                        {
                            
                            adsoyad = textBox_adsoyad.Text;
                            
                            

                            //SqlConnection baglanti = new SqlConnection(conString);
                         
                            if (connection.State == ConnectionState.Closed)
                            {
                                
                                command.Connection = connection;
                                connection.Open();

                                String stajyer = "Insert Into intern (tc_kimlikno,ad_soyad,baba_adı,anne_adı,d_yeri,d_tarih,uyrugu,cinsiyet,ev_tel,cep_tel,adres,e_posta,web_adres,boy,agırlık,kan_grubu,iban,acil_adsoyad,acil_adres,acil_akrabalık_derecesi,acil_telefon_no, acil_e_posta) Values (@tc_kimlikno , @ad_soyad , @baba_adı , @anne_adı , @d_yeri , @d_tarih , @uyrugu , @cinsiyet , @ev_tel , @cep_tel , @adres , @e_posta , @web_adres , @boy , @agırlık , @kan_grubu , @iban , @acil_adsoyad,@acil_adres,@acil_akrabalık_derecesi,@acil_telefon_no,@acil_e_posta)";
                                command = new SqlCommand(stajyer, connection);
                                //kişisel veriler GÜNCELL KOD !!
                                command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                                command.Parameters.AddWithValue("@ad_soyad", textBox_adsoyad.Text);
                                command.Parameters.AddWithValue("@baba_adı", textBox_baba.Text);
                                command.Parameters.AddWithValue("@anne_adı", textBox_anne.Text);
                                command.Parameters.AddWithValue("@d_yeri", textBox_dyeri.Text);
                                command.Parameters.AddWithValue("@d_tarih", dateTimePicker_dtarih.Text);
                                command.Parameters.AddWithValue("@uyrugu", textBox_uyrugu.Text);
                                command.Parameters.AddWithValue("@cinsiyet", comboBox_cinsiyet.Text);
                                command.Parameters.AddWithValue("@ev_tel", textBox_evtel.Text);
                                command.Parameters.AddWithValue("@cep_tel", textBox_ceptel.Text);
                                command.Parameters.AddWithValue("@adres", textBox_adres.Text);
                                command.Parameters.AddWithValue("@e_posta", textBox_eposta.Text);
                                command.Parameters.AddWithValue("@web_adres", textBox_website.Text);
                                command.Parameters.AddWithValue("@boy", textBox_boy.Text);
                                command.Parameters.AddWithValue("@agırlık", textBox_agırlık.Text);
                                command.Parameters.AddWithValue("@kan_grubu", comboBox_kangrubu.Text);
                                command.Parameters.AddWithValue("@iban", textBox_iban.Text);

                                

                                //acil durum irtibat GÜNCELL KOD !!
                                command.Parameters.AddWithValue("@acil_adsoyad", textBox_ai_adsoyad.Text);
                                command.Parameters.AddWithValue("@acil_adres", textBox_ai_adres.Text);
                                command.Parameters.AddWithValue("@acil_akrabalık_derecesi", textBox_ai_akrabalık.Text);
                                command.Parameters.AddWithValue("@acil_telefon_no", textBox_ai_telefon.Text);
                                command.Parameters.AddWithValue("@acil_e_posta", textBox_ai_eposta.Text);

                                //MemoryStream ms = new MemoryStream();
                                //pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                                //byte[] byteData = new byte[ms.Length];
                                //ms.Position = 0;
                                //ms.Read(byteData, 0, Convert.ToInt32(ms.Length));
                                //SqlParameter parametre = new SqlParameter("@resim", SqlDbType.Image, byteData.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, byteData);
                                //command.Parameters.Add(parametre);

                                //staj bilgileri  GÜNCELL KOD !!
                                command.ExecuteNonQuery();
                                connection.Close();



                            }
                        }

                        catch (Exception hata)

                        {
                            MessageBox.Show(hata.Message);
                        }
                    }

                    else

                    {
                        MessageBox.Show("Geçersiz Tc Kimlik Numarası");

                    }
                }
            }
        }

        private void button_kisiselbilgiler_kaydet_Click(object sender, EventArgs e)
        {


            cmd.Connection = connection;
            connection.Open();
            String stajyer_bilgisi = "Insert Into InternshipInformation(staj_kabul_durumu, staj_basvuru_turu, staj_yılı, staj_donem, staj_konusu, staj_baslangıc_tarihi, staj_bitis_tarihi, staj_süresi, staj_bas_kalan_sure, staj_konuları, staj_servis, staj_plaka, staj_durumu, mentor, acıklama, sigorta_evrak_durumu, referans_adı, referans_telefon, referans_adres, referans_e_posta, okul_turu, okul_adı, okul_sehir, bolum_adı, sınıf, okul_no, okul_puan, tc_kimlikno)Values (@staj_kabul_durumu, @staj_basvuru_turu, @staj_yılı, @staj_donem, @staj_konusu, @staj_baslangıc_tarihi, @staj_bitis_tarihi, @staj_süresi, @staj_bas_kalan_sure, @staj_konuları, @staj_servis, @staj_plaka, @staj_durumu, @mentor, @acıklama, @sigorta_evrak_durumu, @referans_adı, @referans_telefon, @referans_adres, @referans_e_posta, @okul_turu, @okul_adı, @okul_sehir, @bolum_adı, @sınıf, @okul_no, @okul_puan, @tc_kimlikno)";
            cmd = new SqlCommand(stajyer_bilgisi, connection);

            #region Staja kalan süre ve durum Hesapla
            TimeSpan kalan = dateTimePicker_baslangıc.Value - DateTime.Today;
            TimeSpan son = dateTimePicker_bitis.Value - DateTime.Today;
            int gunfarki_ilk = kalan.Days;
            int gunfarki_son = son.Days;
            string durum = "";

            if (gunfarki_ilk > 0)
            {
                durum = "staja başlamadı";
                gunfarki_ilk = kalan.Days;
            }
            else if (gunfarki_ilk < 0 || gunfarki_son > 0)
            {
                durum = "Şuan staj yapıyor";


            }
            else if (gunfarki_son < 0)
            {
                durum = "Stajı Bitti";

            }
            #endregion

            //cmd.Parameters.AddWithValue("@stajyer_id");
            cmd.Parameters.AddWithValue("@staj_kabul_durumu", comboBox_kabuldurumu.Text);
            cmd.Parameters.AddWithValue("@staj_basvuru_turu", comboBox_basvuruturu.Text);
            cmd.Parameters.AddWithValue("@staj_yılı", comboBox_stajyili.Text);
            cmd.Parameters.AddWithValue("@staj_donem", comboBox_stajdonemi.Text);
            cmd.Parameters.AddWithValue("@staj_konusu", comboBox_stajyerkonusu.Text);
            cmd.Parameters.AddWithValue("@staj_baslangıc_tarihi", dateTimePicker_baslangıc.Text);
            cmd.Parameters.AddWithValue("@staj_bitis_tarihi", dateTimePicker_bitis.Text);
            cmd.Parameters.AddWithValue("@staj_süresi", textBox_stajsuresi.Text);
            cmd.Parameters.AddWithValue("@staj_bas_kalan_sure", gunfarki_ilk);
            cmd.Parameters.AddWithValue("@staj_konuları", textBox_stajkonuları.Text);
            cmd.Parameters.AddWithValue("@staj_servis", comboBox_servis.Text);
            cmd.Parameters.AddWithValue("@staj_plaka", textBox_arac.Text);
            cmd.Parameters.AddWithValue("@staj_durumu", durum);
            cmd.Parameters.AddWithValue("@mentor", comboBox_mentor.Text);
            cmd.Parameters.AddWithValue("@acıklama", textBox_aciklama.Text);
            cmd.Parameters.AddWithValue("@sigorta_evrak_durumu", comboBox_sigorta.Text);
            cmd.Parameters.AddWithValue("@referans_adı", textBox_r_ad.Text);
            cmd.Parameters.AddWithValue("@referans_telefon", textBox_r_telefon.Text);
            cmd.Parameters.AddWithValue("@referans_adres", textBox_r_adres.Text);
            cmd.Parameters.AddWithValue("@referans_e_posta", textBox_r_eposta.Text);

            //okul bilgileri  GÜNCELL KOD !!
            cmd.Parameters.AddWithValue("@okul_turu", comboBox_egitim.Text);
            cmd.Parameters.AddWithValue("@okul_adı", textBox_okul.Text);
            cmd.Parameters.AddWithValue("@okul_sehir", comboBox_sehir.Text);
            cmd.Parameters.AddWithValue("@bolum_adı", comboBox_bolumadı.Text);
            cmd.Parameters.AddWithValue("@sınıf", comboBox_sinif.Text);
            cmd.Parameters.AddWithValue("@okul_no", textBox_okulno.Text);
            cmd.Parameters.AddWithValue("@okul_puan", textBox_okulpuan.Text);
            cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);





            cmd.ExecuteNonQuery();


            connection.Close();

            MessageBox.Show("Kayıt Başarılı");

            textBox_okulpuan.Clear();
            textBox_okulno.Clear();
            textBox_okul.Clear();
            textBox_r_eposta.Clear();
            textBox_r_adres.Clear();
            textBox_r_telefon.Clear();
            textBox_r_ad.Clear();
            textBox_aciklama.Clear();
            textBox_arac.Clear();
            textBox_stajkonuları.Clear();
            textBox_stajsuresi.Clear();
            textBox_ai_eposta.Clear();
            textBox_ai_telefon.Clear();
            textBox_ai_akrabalık.Clear();
            textBox_ai_adres.Clear();
            textBox_ai_adsoyad.Clear();
            textBox_ai_adres.Clear();
            textBox_tc.Clear();
            textBox_adsoyad.Clear();
            textBox_baba.Clear();
            textBox_anne.Clear();
            textBox_dyeri.Clear();
            textBox_uyrugu.Clear();
            textBox_evtel.Clear();
            textBox_ceptel.Clear();
            textBox_adres.Clear();
            textBox_eposta.Clear();
            textBox_website.Clear();
            textBox_boy.Clear();
            textBox_agırlık.Clear();
            textBox_iban.Clear();

            comboBox_basvuruturu.Text = "";
            comboBox_bolumadı.Text = "";
            comboBox_cinsiyet.Text = "";
            comboBox_egitim.Text = "";
            comboBox_kabuldurumu.Text = "";
            comboBox_kangrubu.Text = "";
            comboBox_mentor.Text = "";
            comboBox_sehir.Text = "";
            comboBox_servis.Text = "";
            comboBox_sigorta.Text = "";
            comboBox_sinif.Text = "";
            comboBox_stajdonemi.Text = "";
            comboBox_stajyerkonusu.Text = "";
            comboBox_stajyili.Text = "";

            MessageBox.Show("Kulanıcı Kaydedildi");

        }

        //SİL BUTONU 
        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                command.Connection = connection;
                connection.Open();
                string stajyer = "DELETE FROM intern where tc_kimlikno in(SELECT tc_kimlikno FROM InternshipInformation WHERE tc_kimlikno = @tc_kimlikno)";
                command = new SqlCommand(stajyer, connection);
                command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Kayıt Silinmiştir");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "Kayıt Silinemedi");
            }

            #region eski sorgu
            //connection.Open();
            ////string stajyer_silme = "DELETE FROM intern WHERE tc_kimlikno IN(SELECT tc_kimlikno FROM InternshipInformation where tc_kimlino='" + textBox_tc + "')";
            ////SqlCommand stajyer_komutu = new SqlCommand(stajyer_silme, connection);

            //string secmeSorgusu = "SELECT * from intern where tc_kimlikno=@tc_kimlikno";

            //command = new SqlCommand(secmeSorgusu, connection);
            //command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);


            //dataadapter = new SqlDataAdapter(command);
            //datareader = command.ExecuteReader();

            //if (datareader.Read())
            //{
            //    string isim = datareader["ad_soyad"].ToString();
            //    datareader.Close();


            //    DialogResult durum = MessageBox.Show(isim + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

            //    if (DialogResult.Yes == durum)
            //    {
            //        string stajyer_silme = "DELETE FROM intern WHERE tc_kimlikno IN(SELECT tc_kimlikno FROM InternshipInformation where tc_kimlikno=@tc_kimlikno)";
            //        SqlCommand stajyer_komutu = new SqlCommand(stajyer_silme, connection);
            //        stajyer_komutu.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            //        dataadapter = new SqlDataAdapter(stajyer_komutu);
            //        datareader = stajyer_komutu.ExecuteReader();

            //        //string stajyer_bilgi_silme = "DELETE from InternshipInfprmation where tc_kimlikno='" + textBox_tc.Text + "'";
            //        //SqlCommand stajyer_bilgi_komutu = new SqlCommand(stajyer_silme, connection);
            //        datareader.Close();
            //        stajyer_komutu.ExecuteNonQuery();
            //        MessageBox.Show("Kayıt Silindi...");

            //        textBox_tcbul.Clear();
            //        connection.Close();

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Stajyer Bulunamadı.");
            //    connection.Close();
            //}
            #endregion

        }

        //GÜNCELLEME BUTONU  BİTTİ GÜNCEL!!
        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
                string stajyer= "update intern set tc_kimlikno=@tc_kimlikno,ad_soyad=@ad_soyad,baba_adı=@baba_adı,anne_adı=@anne_adı,d_yeri=@d_yeri,d_tarih=@d_tarih,uyrugu=@uyrugu,cinsiyet=@cinsiyet,ev_tel=@ev_tel,cep_tel=@cep_tel,adres=@adres,e_posta=@e_posta,web_adres=@web_adres,boy=@boy,agırlık=@agırlık,kan_grubu=@kan_grubu,iban=@iban, acil_adsoyad=@acil_adsoyad,acil_adres=@acil_adres,acil_akrabalık_derecesi=@acil_akrabalık_derecesi, acil_telefon_no=@acil_telefon_no, acil_e_posta=@acil_e_posta  where tc_kimlikno=@tc_kimlikno";
                command= new SqlCommand(stajyer, connection);

                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.

                //kişisel veriler GÜNCELL KOD !!
                command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                command.Parameters.AddWithValue("@ad_soyad", textBox_adsoyad.Text);
                command.Parameters.AddWithValue("@baba_adı", textBox_baba.Text);
                command.Parameters.AddWithValue("@anne_adı", textBox_anne.Text);
                command.Parameters.AddWithValue("@d_yeri", textBox_dyeri.Text);
                command.Parameters.AddWithValue("@d_tarih", dateTimePicker_dtarih.Text);
                command.Parameters.AddWithValue("@uyrugu", textBox_uyrugu.Text);
                command.Parameters.AddWithValue("@cinsiyet", comboBox_cinsiyet.Text);
                command.Parameters.AddWithValue("@ev_tel", textBox_evtel.Text);
                command.Parameters.AddWithValue("@cep_tel", textBox_ceptel.Text);
                command.Parameters.AddWithValue("@adres", textBox_adres.Text);
                command.Parameters.AddWithValue("@e_posta", textBox_eposta.Text);
                command.Parameters.AddWithValue("@web_adres", textBox_website.Text);
                command.Parameters.AddWithValue("@boy", textBox_boy.Text);
                command.Parameters.AddWithValue("@agırlık", textBox_agırlık.Text);
                command.Parameters.AddWithValue("@kan_grubu", comboBox_kangrubu.Text);
                command.Parameters.AddWithValue("@iban", textBox_iban.Text);

                

                //acil durum irtibat GÜNCELL KOD !!
                command.Parameters.AddWithValue("@acil_adsoyad", textBox_ai_adsoyad.Text);
                command.Parameters.AddWithValue("@acil_adres", textBox_ai_adres.Text);
                command.Parameters.AddWithValue("@acil_akrabalık_derecesi", textBox_ai_akrabalık.Text);
                command.Parameters.AddWithValue("@acil_telefon_no", textBox_ai_telefon.Text);
                command.Parameters.AddWithValue("@acil_e_posta", textBox_ai_eposta.Text);

                //MemoryStream ms = new MemoryStream();
                //pictureBox1.Image.Save(ms, ImageFormat.Jpeg);
                //byte[] byteData = new byte[ms.Length];
                //ms.Position = 0;
                //ms.Read(byteData, 0, Convert.ToInt32(ms.Length));
                //SqlParameter parametre = new SqlParameter("@resim", SqlDbType.Image, byteData.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, byteData);
                //command.Parameters.Add(parametre);

                //staj bilgileri  GÜNCELL KOD !!
                string stajyer_bilgi = "update InternshipInformation set staj_kabul_durumu = @staj_kabul_durumu,staj_basvuru_turu = @staj_basvuru_turu,staj_yılı = @staj_yılı,staj_donem = @staj_donem,staj_konusu = @staj_konusu,staj_baslangıc_tarihi = @staj_baslangıc_tarihi,staj_bitis_tarihi = @staj_bitis_tarihi,staj_süresi = @staj_süresi,staj_bas_kalan_sure = @staj_bas_kalan_sure,staj_konuları=@staj_konuları,staj_servis=@staj_servis,staj_plaka=@staj_plaka,staj_durumu = @staj_durumu,mentor=@mentor,acıklama = @acıklama,sigorta_evrak_durumu=@sigorta_evrak_durumu,referans_adı=@referans_adı,referans_telefon=@referans_telefon,referans_adres=@referans_adres,referans_e_posta=@referans_e_posta,okul_turu = @okul_turu,okul_adı = @okul_adı,okul_sehir = @okul_sehir,bolum_adı = @bolum_adı,sınıf = @sınıf,okul_no = @okul_no,okul_puan = @okul_puan, tc_kimlikno=@tc_kimlikno where tc_kimlikno=@tc_kimlikno";
                cmd = new SqlCommand(stajyer_bilgi, connection);


                #region STAJ DURUM BİLGİSİ VE BASLANGIÇ KALAN SÜRE

                TimeSpan kalan = dateTimePicker_baslangıc.Value - DateTime.Today;
                TimeSpan son = dateTimePicker_bitis.Value - DateTime.Today;

                int gunfarki_ilk = kalan.Days;
                int gunfarki_son = son.Days;

                string durum = "";

                if (gunfarki_ilk > 0)
                {
                    durum = "STAJA BAŞLAMADI";
                    gunfarki_ilk = kalan.Days;
                }
                else if (gunfarki_ilk < 0 || gunfarki_son > 0)
                {
                    durum = "ŞUAN STAJ YAPIYOR";

                }
                else if (gunfarki_son < 0)
                {
                    durum = "STAJI BİTTİ";
                }   
                #endregion

                cmd.Parameters.AddWithValue("@staj_kabul_durumu", comboBox_kabuldurumu.Text);
                cmd.Parameters.AddWithValue("@staj_basvuru_turu", comboBox_basvuruturu.Text);
                cmd.Parameters.AddWithValue("@staj_yılı", comboBox_stajyili.Text);
                cmd.Parameters.AddWithValue("@staj_donem", comboBox_stajdonemi.Text);
                cmd.Parameters.AddWithValue("@staj_konusu", comboBox_stajyerkonusu.Text);
                cmd.Parameters.AddWithValue("@staj_baslangıc_tarihi", dateTimePicker_baslangıc.Text);
                cmd.Parameters.AddWithValue("@staj_bitis_tarihi", dateTimePicker_bitis.Text);
                cmd.Parameters.AddWithValue("@staj_süresi", textBox_stajsuresi.Text);
                cmd.Parameters.AddWithValue("@staj_bas_kalan_sure", gunfarki_ilk);
                cmd.Parameters.AddWithValue("@staj_durumu", durum);
                cmd.Parameters.AddWithValue("@staj_konuları", textBox_stajkonuları.Text);
                cmd.Parameters.AddWithValue("@staj_servis", comboBox_servis.Text);
                cmd.Parameters.AddWithValue("@staj_plaka", textBox_arac.Text);
                cmd.Parameters.AddWithValue("@mentor", comboBox_mentor.Text);
                cmd.Parameters.AddWithValue("@acıklama", textBox_aciklama.Text);
                cmd.Parameters.AddWithValue("@sigorta_evrak_durumu", comboBox_sigorta.Text);
                cmd.Parameters.AddWithValue("@referans_adı", textBox_r_ad.Text);
                cmd.Parameters.AddWithValue("@referans_telefon", textBox_r_telefon.Text);
                cmd.Parameters.AddWithValue("@referans_adres", textBox_r_adres.Text);
                cmd.Parameters.AddWithValue("@referans_e_posta", textBox_r_eposta.Text);

                //okul bilgileri  GÜNCELL KOD !!
                cmd.Parameters.AddWithValue("@okul_turu", comboBox_egitim.Text);
                cmd.Parameters.AddWithValue("@okul_adı", textBox_okul.Text);
                cmd.Parameters.AddWithValue("@okul_sehir", comboBox_sehir.Text);
                cmd.Parameters.AddWithValue("@bolum_adı", comboBox_bolumadı.Text);
                cmd.Parameters.AddWithValue("@sınıf", comboBox_sinif.Text);
                cmd.Parameters.AddWithValue("@okul_no", textBox_okulno.Text);
                cmd.Parameters.AddWithValue("@okul_puan", textBox_okulpuan.Text);
                cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);


                command.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                connection.Close();
                MessageBox.Show("Stajyer Bilgileri Güncellendi");

                textBox_okulpuan.Clear();
                textBox_okulno.Clear();
                textBox_okul.Clear();
                textBox_r_eposta.Clear();
                textBox_r_adres.Clear();
                textBox_r_telefon.Clear();
                textBox_r_ad.Clear();
                textBox_aciklama.Clear();
                textBox_arac.Clear();
                textBox_stajkonuları.Clear();
                textBox_stajsuresi.Clear(); 
                textBox_ai_eposta.Clear();
                textBox_ai_telefon.Clear();
                textBox_ai_akrabalık.Clear();
                textBox_ai_adres.Clear();
                textBox_ai_adsoyad.Clear();
                textBox_ai_adres.Clear();
                textBox_tc.Clear();
                textBox_adsoyad.Clear();
                textBox_baba.Clear();
                textBox_anne.Clear();
                textBox_dyeri.Clear();
                textBox_uyrugu.Clear();
                textBox_evtel.Clear();
                textBox_ceptel.Clear();
                textBox_adres.Clear();
                textBox_eposta.Clear();
                textBox_website.Clear();
                textBox_boy.Clear();
                textBox_agırlık.Clear();
                textBox_iban.Clear();

                comboBox_basvuruturu.Text = "";
                comboBox_bolumadı.Text = "";
                comboBox_cinsiyet.Text = "";
                comboBox_egitim.Text = "";
                comboBox_kabuldurumu.Text = "";
                comboBox_kangrubu.Text = "";
                comboBox_mentor.Text = "";
                comboBox_sehir.Text = "";
                comboBox_servis.Text = "";
                comboBox_sigorta.Text = "";
                comboBox_sinif.Text = "";
                comboBox_stajdonemi.Text = "";
                comboBox_stajyerkonusu.Text = "";
                comboBox_stajyili.Text = "";

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        //RESİM YÜKLEME BUTONU  BİTTİ GÜNCEL!!
        private void button_resim_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdialog = new OpenFileDialog();
                fdialog.Filter = "Pictures|*.jpg";
                fdialog.InitialDirectory = "C://";
                if (DialogResult.OK == fdialog.ShowDialog())
                {

                    string resimYol = fdialog.FileName;
                    pictureBox1.Image = Image.FromFile(resimYol);
                    byteData = null;

                    FileInfo fInfo = new FileInfo(resimYol);
                    long sayac = fInfo.Length;
                    System.IO.FileStream fStream = new System.IO.FileStream(resimYol, FileMode.Open, FileAccess.Read);
                    BinaryReader bReader = new BinaryReader(fStream);
                    byteData = bReader.ReadBytes((int)sayac);

                }

            }
            finally
            {

            }




            Directory.CreateDirectory(yol + "\\" + klasör);
        }

        //STAJYER BUL BUTONU BİTTİ GÜNCEL !!
        private void button_bul_Click(object sender, EventArgs e)
        {
            button_update.Enabled = true;
            button_delete.Enabled = true;
            button_save.Enabled = false;

            #region Stajyer bul.
            connection.Open();
            command.Connection = connection;
            string interSorgusu = "SELECT * from intern where tc_kimlikno=@tc_kimlikno";
            command = new SqlCommand(interSorgusu, connection);
            command.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
            dataadapter= new SqlDataAdapter(command);
            datareader= command.ExecuteReader();
            if (datareader.Read())
            {
                textBox_tc.Text = datareader["tc_kimlikno"].ToString();   //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım.       
                textBox_adsoyad.Text = datareader["ad_soyad"].ToString();
                textBox_baba.Text = datareader["baba_adı"].ToString();
                textBox_anne.Text = datareader["anne_adı"].ToString();
                textBox_dyeri.Text = datareader["d_yeri"].ToString();
                dateTimePicker_dtarih.Text = datareader["d_tarih"].ToString();
                textBox_uyrugu.Text = datareader["uyrugu"].ToString();
                comboBox_cinsiyet.Text = datareader["cinsiyet"].ToString();
                textBox_evtel.Text = datareader["ev_tel"].ToString();
                textBox_ceptel.Text = datareader["cep_tel"].ToString();
                textBox_adres.Text = datareader["adres"].ToString();
                textBox_eposta.Text = datareader["e_posta"].ToString();
                textBox_website.Text = datareader["web_adres"].ToString();
                textBox_boy.Text = datareader["boy"].ToString();
                textBox_agırlık.Text = datareader["agırlık"].ToString();
                comboBox_kangrubu.Text = datareader["kan_grubu"].ToString();
                textBox_iban.Text = datareader["iban"].ToString();
                textBox_ai_adsoyad.Text = datareader["acil_adsoyad"].ToString();
                textBox_ai_adres.Text = datareader["acil_adres"].ToString();
                textBox_ai_akrabalık.Text = datareader["acil_akrabalık_derecesi"].ToString();
                textBox_ai_telefon.Text = datareader["acil_telefon_no"].ToString();
                textBox_ai_eposta.Text = datareader["acil_e_posta"].ToString();


                datareader.Close();
                connection.Close();//Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
            }
            else
            {
                MessageBox.Show("Kayıt Bulunamadı");
                datareader.Close();
                connection.Close();
            }
            #endregion

            #region staj kabul durumu bul
            connection.Open();
            cmd.Connection = connection;
            string intershipSorgusu = "SELECT * from InternshipInformation where tc_kimlikno=@tc_kimlikno";
            command= new SqlCommand(intershipSorgusu, connection);
            command.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
            dataadapter= new SqlDataAdapter(command);
            datareader = command.ExecuteReader();
            if (datareader.Read())
            {                                                            
                 //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım. 
                comboBox_kabuldurumu.Text = datareader["staj_kabul_durumu"].ToString();
                comboBox_basvuruturu.Text = datareader["staj_basvuru_turu"].ToString();
                comboBox_stajyili.Text = datareader["staj_yılı"].ToString();
                comboBox_stajdonemi.Text = datareader["staj_donem"].ToString();
                comboBox_stajyerkonusu.Text = datareader["staj_konusu"].ToString();
                dateTimePicker_baslangıc.Value = Convert.ToDateTime(datareader["staj_baslangıc_tarihi"].ToString());
                dateTimePicker_bitis.Value = Convert.ToDateTime(datareader["staj_bitis_tarihi"].ToString());
                textBox_stajsuresi.Text = datareader["staj_süresi"].ToString();
                textBox_stajkonuları.Text = datareader["staj_konuları"].ToString();
                comboBox_servis.Text = datareader["staj_servis"].ToString();
                textBox_arac.Text = datareader["staj_plaka"].ToString();
                comboBox_mentor.Text = datareader["mentor"].ToString();
                textBox_aciklama.Text = datareader["acıklama"].ToString();
                comboBox_sigorta.Text = datareader["sigorta_evrak_durumu"].ToString();
                textBox_r_ad.Text = datareader["referans_adı"].ToString();
                textBox_r_telefon.Text = datareader["referans_telefon"].ToString();
                textBox_r_adres.Text = datareader["referans_adres"].ToString();
                textBox_r_eposta.Text = datareader["referans_e_posta"].ToString();
                comboBox_egitim.Text = datareader["okul_turu"].ToString();
                textBox_okul.Text = datareader["okul_adı"].ToString();
                comboBox_sehir.Text = datareader["okul_sehir"].ToString();
                comboBox_bolumadı.Text = datareader["bolum_adı"].ToString();
                comboBox_sinif.Text = datareader["sınıf"].ToString();
                textBox_okulno.Text = datareader["okul_no"].ToString();
                textBox_okulpuan.Text = datareader["okul_puan"].ToString();
                //textBox_tc.Text = datareader["tc_kimlikno"].ToString();

                datareader.Close(); //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
                connection.Close();
            }
            else
            {
                MessageBox.Show("Kayıt Bulunamadı");
                datareader.Close();
                connection.Close();
            }
            
           
            #endregion



        }
        #endregion

        #region SEÇİMLERE BAĞLI OLARAK AKTİF/PASİFLİK DURUMLARI
        private void checkBox_referans_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_referans.Checked)
            {
                panel_referans.Enabled = true;
            }
            else
            {
                panel_referans.Enabled = false;
            }
        }

        private void comboBox_servis_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox_servis.Text == "VAR")
            {
                textBox_arac.Enabled = true;
            }
            else if(comboBox_servis.Text == "YOK")
            {
                textBox_arac.Enabled = false;
            }

        }

        private void comboBox_egitim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_egitim.Text == "LİSE")
            {
                textBox_iban.Enabled = true;
            }
            else
            {
                textBox_iban.Enabled = false;
            }
            
        }
        #endregion

        #region TABCONTROLLER ARASI LABELLE GEÇİŞ
        private void label44_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1; //İndex olarak geçişleri sağlayabiliriz
        }

        private void label45_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab =tabPage3; //Pages üzerindende aynı şekildegeçişleri sağlayabiliriz. Alternatif gösterim
        }

        private void label46_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }
        #endregion

      
    }
}
