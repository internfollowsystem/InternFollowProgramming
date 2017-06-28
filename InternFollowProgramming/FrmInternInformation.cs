using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        //database bağlantısı 
        #region database bağlantısı

        static string conString = "Server=10.0.0.51;Database=INTERN;Uid=sa;Password=20fcab9e;";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlCommand cmd = new SqlCommand();
        #endregion
         
        #region Sınıfların Çağrılması
        connect i_add = new connect();// connect sınıfı bağlantısı
        Intern yenikisi = new Intern(); // connect sınıfı bağlantısı
#endregion


        byte[] byteResim = { 0x20 };

        String yol = "c:\\";
        String klasör = "Stajer Takip";
             

        

        public FrmInternInformation()
        {
            InitializeComponent();

            button_update.Enabled = false; //Form açıldığında güncelle butonu pasif olsun.
            button_delete.Enabled = false; //Form açıldığında delete butonu pasif olsun.
            panel_referans.Enabled = false;
        } 

        private void FrmInternInformation_Load(object sender, EventArgs e)
        {
            connection.Open();
            command.CommandText = "Select* From intern"; 
            cmd.CommandText = "Select* From IntershipInformation";
            command.Connection = connection;
            cmd.Connection = connection;
            command.CommandType = CommandType.Text;
            cmd.CommandType = CommandType.Text;

           #region comboBox'ın içine verileri çekme kodu
            //SqlDataReader dr;
            //dr = command.ExecuteReader();

            //while (dr.Read())
            //{
            //    comboBoxAdsoyad.Items.Add(dr["ad_soyad"]);
            //}
            #endregion

            connection.Close();
        }

        #region butonlar
        //KAYDET BUTONU  ( Intern tablosu içi 1 kişi kaydederken, IntershipInformation tablosu çin bunu denetlemiyor. RESİM KAYDETMEDİK)
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
                                String add_intern = "Insert Into intern (tc_kimlikno,ad_soyad,baba_adı,anne_adı,d_yeri,d_tarih,uyrugu,cinsiyet,ev_tel,cep_tel,adres,e_posta,web_adres,boy,agırlık,kan_grubu,iban,acil_adsoyad,acil_adres,acil_akrabalık_derecesi,acil_telefon_no,acil_e_posta) Values (@tc_kimlikno,@ad_soyad,@baba_adı,@anne_adı,@d_yeri,@d_tarih,@uyrugu,@cinsiyet,@ev_tel,@cep_tel,@adres,@e_posta,@web_adres,@boy,@agırlık,@kan_grubu,@iban,@acil_adsoyad,@acil_adres,@acil_akrabalık_derecesi,@acil_telefon_no,@acil_e_posta)";
                                String add_intership= "Insert Into IntershipInformation(tc_kimlikno,ad_soyad,staj_kabul_durumu, staj_basvuru_turu, staj_yılı, staj_donem, staj_konusu, staj_baslangıc_tarihi, staj_bitis_tarihi, staj_süresi, staj_bas_kalan_sure, staj_konuları, staj_servis, staj_plaka, staj_durumu, mentor, acıklama, referans_adı, referans_telefon, referans_adres, referans_e_posta, okul_turu, okul_adı, okul_sehir, bolum_adı, sınıf, okul_no, okul_puan, sigorta_evrak_durumu)Values(@tc_kimlikno,@ad_soyad, @staj_kabul_durumu, @staj_basvuru_turu, @staj_yılı, @staj_donem, @staj_konusu, @staj_baslangıc_tarihi, @staj_bitis_tarihi, @staj_süresi, @staj_bas_kalan_sure, @staj_konuları, @staj_servis, @staj_plaka, @staj_durumu, @mentor, @acıklama, @referans_adı, @referans_telefon, @referans_adres, @referans_e_posta, @okul_turu, @okul_adı, @okul_sehir, @bolum_adı, @sınıf, @okul_no, @okul_puan, @sigorta_evrak_durumu)";
                                connection.Open();
                                command = new SqlCommand(add_intern, connection);
                                



                                //kişisel veriler GÜNCELL KOD !!
                                command.Parameters.AddWithValue("@tc_kimlikno",textBox_tc.Text );
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

                                //staj bilgileri  GÜNCELL KOD !!
                                cmd = new SqlCommand(add_intership, connection);

                               #region Staja kalan süre ve durum Hesapla
                                TimeSpan kalan = dateTimePicker_baslangıc.Value -DateTime.Today;
                                TimeSpan son = dateTimePicker_bitis.Value-DateTime.Today;
                                int gunfarki_ilk = kalan.Days;
                                int gunfarki_son = son.Days;
                                string durum="";

                                if( gunfarki_ilk>0)
                                {
                                    durum = "staja başlamadı";
                                    gunfarki_ilk = kalan.Days;
                                }
                                else if(gunfarki_ilk<0 || gunfarki_son>0)
                                {
                                    durum = "Şuan staj yapıyor";
                                    
                                    
                                }
                                else if (gunfarki_son<0)
                                {
                                    durum = "Stajı Bitti";
                                    
                                }
                                #endregion

                                cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                                cmd.Parameters.AddWithValue("@ad_soyad", textBox_adsoyad.Text);
                                cmd.Parameters.AddWithValue("@staj_kabul_durumu", comboBox_kabuldurumu.Text);
                                cmd.Parameters.AddWithValue("@staj_basvuru_turu", comboBox_basvuruturu.Text);
                                cmd.Parameters.AddWithValue("@staj_yılı", comboBox_stajyili.Text);
                                cmd.Parameters.AddWithValue("@staj_donem", comboBox_stajdonemi.Text);
                                cmd.Parameters.AddWithValue("@staj_konusu", textBox_stajkonusu.Text);
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





                                cmd.ExecuteNonQuery();
                                command.ExecuteNonQuery();

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
                                textBox_stajkonusu.Clear();
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

        //SİL BUTONU ((düzeltilecek))
        private void button_delete_Click(object sender, EventArgs e)
        {
            connection.Open();
            string secmeSorgusu = "SELECT * from intern where tc_kimlikno=@tc_kimlikno";
            string stajSorgusu = "SELECT * from IntershipInformation where ad_soyad=@ad_soyad";

            SqlCommand secmeKomutu = new SqlCommand(secmeSorgusu, connection);
            SqlCommand stajKomutu = new SqlCommand(stajSorgusu, connection);

            secmeKomutu.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            stajKomutu.Parameters.AddWithValue("@ad_soyad", textBox_tcbul.Text);

            SqlDataAdapter da = new SqlDataAdapter(secmeKomutu);
            SqlDataReader dr = secmeKomutu.ExecuteReader();

            if (dr.Read())
            {
                string isim = dr["ad_soyad"].ToString();
                dr.Close();


                DialogResult durum = MessageBox.Show(isim + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

                if (DialogResult.Yes == durum)
                {
                    string silmeSorgusu = "DELETE from intern where tc_kimlikno=@tc_kimlikno";

                    SqlCommand silKomutu = new SqlCommand(silmeSorgusu, connection);
                    silKomutu.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);



                    textBox_tcbul.Clear();
                    dr.Close();
                    connection.Close();

                }
            }
            else
            {
                MessageBox.Show("Stajyer Bulunamadı.");
                dr.Close();
                connection.Close();
            }




            connection.Open();
            SqlDataAdapter daa = new SqlDataAdapter(stajKomutu);
            SqlDataReader drr = stajKomutu.ExecuteReader();

            if (drr.Read())
            {
                string isim = drr["ad_soyad"].ToString();
                drr.Close();


                DialogResult durum = MessageBox.Show(isim + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

                if (DialogResult.Yes == durum)
                {
                    string silmeSorgusu = "DELETE from IntershipInformation where tc_kimlikno=@tc_kimlikno";

                    SqlCommand silKomutu = new SqlCommand(silmeSorgusu, connection);
                    silKomutu.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
                    
                    MessageBox.Show("Kayıt Silindi...");
                    textBox_tc.Clear();
                    drr.Close();
                    connection.Close();



                }
            }
            else
            {
                MessageBox.Show("Stajyer Bulunamadı.");
                drr.Close();
                connection.Close();
            }
       

            
        }

        //GÜNCELLEME BUTONU  GÜNCELL!!
        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
                string update_intern = "update intern set tc_kimlikno=@tc_kimlikno,ad_soyad=@ad_soyad,baba_adı=@baba_adı,anne_adı=@anne_adı,d_yeri=@d_yeri,d_tarih=@d_tarih,uyrugu=@uyrugu,cinsiyet=@cinsiyet,ev_tel=@ev_tel,cep_tel=@cep_tel,adres=@adres,e_posta=@e_posta,web_adres=@web_adres,boy=@boy,agırlık=@agırlık,kan_grubu=@kan_grubu,iban=@iban where tc_kimlikno=@tc_kimlikno";
                string update_intership = "update IntershipInformation set tc_kimlik=@tc_kimlikno,ad_soyad=@ad_soyadstaj_kabul_durumu = @staj_kabul_durumu,staj_basvuru_turu = @staj_basvuru_turu,staj_yılı = @staj_yılı,staj_donem = @staj_donem,staj_konusu = @staj_konusu,staj_baslangıc_tarihi = @staj_baslangıc_tarihi,staj_bitis_tarihi = @staj_bitis_tarihi,staj_süresi = @staj_süresi,staj_bas_kalan_sure = @staj_bas_kalan_sure,staj_konuları=@staj_konuları,staj_servis=@staj_servis,staj_plaka=@staj_plaka,staj_durumu = @staj_durumu,mentor=@mentor,acıklama = @acıklama,sigorta_evrak_durumu=@sigorta_evrak_durumu,referans_adı=@referans_adı,referans_telefon=@referans_telefon,referans_adres=@referans_adres,referans_e_posta=@referans_e_posta,okul_turu = @okul_turu,okul_adı = @okul_adı,okul_sehir = @okul_sehir,bolum_adı = @bolum_adı,sınıf = @sınıf,okul_no = @okul_no,okul_puan = @okul_puan,sigorta_evrak_durumu = @sigorta_evrak_durumu,acil_adsoyad = @acil_adsoyad,acil_adres = @acil_adres,acil_akrabalık_derecesi = @acil_akrabalık_derecesi,acil_telefon_no = @acil_telefon_no,acil_e_posta = @acil_e_posta where tc_kimlikno=@tc_kimlikno";
                SqlCommand command = new SqlCommand(update_intern, connection);

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

                //staj bilgileri  GÜNCELL KOD !!
                cmd = new SqlCommand(update_intership, connection);

                
                #region STAJ DURUM BİLGİSİ VE BASLANGIÇ KALAN SÜRE
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
                //DateTime baslangıc = DateTime.Parse(dateTimePicker_baslangıc.Text);
                //DateTime bitis = DateTime.Parse(dateTimePicker_bitis.Text);
                //string bas_kalan_sure = "";
                //string durum = "";
                //int fark = Convert.ToInt32(baslangıc - DateTime.Today);
                //int son = Convert.ToInt32(DateTime.Today - bitis);
                //if (fark > 0)
                //{
                //    bas_kalan_sure = fark.ToString();
                //    durum = "STAJ YAPMIYOR.";

                //}
                //else if (fark < 0 || son > 0)
                //{
                //    bas_kalan_sure = "0";
                //    durum = "ŞUAN STAJ YAPIYOR.";
                //}
                //else if (son < 0)
                //{

                //    bas_kalan_sure = "0";
                //    durum = "STAJI TAMAMLADI";
                //}
                #endregion

                cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                cmd.Parameters.AddWithValue("@ad_soyad", textBox_adsoyad.Text);
                cmd.Parameters.AddWithValue("@staj_kabul_durumu", comboBox_kabuldurumu.Text);
                cmd.Parameters.AddWithValue("@staj_basvuru_turu", comboBox_basvuruturu.Text);
                cmd.Parameters.AddWithValue("@staj_yılı", comboBox_stajyili.Text);
                cmd.Parameters.AddWithValue("@staj_donem", comboBox_stajdonemi.Text);
                cmd.Parameters.AddWithValue("@staj_konusu", textBox_stajkonusu.Text);
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





                cmd.ExecuteNonQuery();
                command.ExecuteNonQuery();//Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
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
                textBox_stajkonusu.Clear();
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

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        //RESİM YÜKLEME BUTONU
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
                    byteResim = null;

                    FileInfo fInfo = new FileInfo(resimYol);
                    long sayac = fInfo.Length;
                    System.IO.FileStream fStream = new System.IO.FileStream(resimYol, FileMode.Open, FileAccess.Read);
                    BinaryReader bReader = new BinaryReader(fStream);
                    byteResim = bReader.ReadBytes((int)sayac);

                }

            }
            finally
            {

            }




            Directory.CreateDirectory(yol + "\\" + klasör);
        }

        //STAJYER BUL BUTONU GÜNCELL !!SON
        private void button_bul_Click(object sender, EventArgs e)
        {
            button_update.Enabled = true;
            button_delete.Enabled = true;
            button_save.Enabled = false;

            #region Stajyer bul.
            connection.Open();
            command.Connection = connection;
            string interSorgusu = "SELECT * from intern where tc_kimlikno=@tc_kimlikno";
            SqlCommand interKomutu = new SqlCommand(interSorgusu, connection);
            interKomutu.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
            SqlDataAdapter da = new SqlDataAdapter(interKomutu);
            SqlDataReader dr = interKomutu.ExecuteReader();
            if (dr.Read())
            {
                textBox_tc.Text = dr["tc_kimlikno"].ToString();   //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım.       
                textBox_adsoyad.Text = dr["ad_soyad"].ToString();
                textBox_baba.Text = dr["baba_adı"].ToString();
                textBox_anne.Text = dr["anne_adı"].ToString();
                textBox_dyeri.Text = dr["d_yeri"].ToString();
                dateTimePicker_dtarih.Text = dr["d_tarih"].ToString();
                textBox_uyrugu.Text = dr["uyrugu"].ToString();
                comboBox_cinsiyet.Text = dr["cinsiyet"].ToString();
                textBox_evtel.Text = dr["ev_tel"].ToString();
                textBox_ceptel.Text = dr["cep_tel"].ToString();
                textBox_adres.Text = dr["adres"].ToString();
                textBox_eposta.Text = dr["e_posta"].ToString();
                textBox_website.Text = dr["web_adres"].ToString();
                textBox_boy.Text = dr["boy"].ToString();
                textBox_agırlık.Text = dr["agırlık"].ToString();
                comboBox_kangrubu.Text = dr["kan_grubu"].ToString();
                textBox_iban.Text = dr["iban"].ToString();
                textBox_ai_adsoyad.Text = dr["acil_adsoyad"].ToString();
                textBox_ai_adres.Text = dr["acil_adres"].ToString();
                textBox_ai_akrabalık.Text = dr["acil_akrabalık_derecesi"].ToString();
                textBox_ai_telefon.Text = dr["acil_telefon_no"].ToString();
                textBox_ai_eposta.Text = dr["acil_e_posta"].ToString();


                dr.Close();
                connection.Close();//Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
            }
            else
            {
                MessageBox.Show("Kayıt Bulunamadı");
                dr.Close();
                connection.Close();
            }
            #endregion

            #region staj kabul durumu bul
            connection.Open();
            cmd.Connection = connection;
            string intershipSorgusu = "SELECT * from IntershipInformation where ad_soyad=@ad_soyad";
            SqlCommand intershipKomutu = new SqlCommand(intershipSorgusu, connection);
            intershipKomutu.Parameters.AddWithValue("@ad_soyad", textBox_tcbul.Text);
            SqlDataAdapter daa = new SqlDataAdapter(intershipKomutu);
            SqlDataReader drr = intershipKomutu.ExecuteReader();
            if (drr.Read())
            {
                textBox_tc.Text = drr["tc_kimlikno"].ToString();   //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım.       
                textBox_adsoyad.Text = drr["ad_soyad"].ToString();
                comboBox_kabuldurumu.Text = drr["staj_kabul_durumu"].ToString();
                comboBox_basvuruturu.Text = drr["staj_basvuru_turu"].ToString();
                comboBox_stajyili.Text = drr["staj_yılı"].ToString();
                comboBox_stajdonemi.Text = drr["staj_donem"].ToString();
                textBox_stajkonusu.Text = drr["staj_konusu"].ToString();
                dateTimePicker_baslangıc.Value = Convert.ToDateTime(drr["staj_baslangıc_tarihi"].ToString());
                dateTimePicker_bitis.Value = Convert.ToDateTime(drr["staj_bitis_tarihi"].ToString());
                textBox_stajsuresi.Text = drr["staj_süresi"].ToString();
                textBox_stajkonuları.Text = drr["staj_konuları"].ToString();
                comboBox_servis.Text = drr["staj_servis"].ToString();
                textBox_arac.Text = drr["staj_plaka"].ToString();
                comboBox_mentor.Text = drr["mentor"].ToString();
                textBox_aciklama.Text = drr["acıklama"].ToString();
                comboBox_sigorta.Text = drr["sigorta_evrak_durumu"].ToString();
                textBox_r_ad.Text = drr["referans_adı"].ToString();
                textBox_r_telefon.Text = drr["referans_telefon"].ToString();
                textBox_r_adres.Text = drr["referans_adres"].ToString();
                textBox_r_eposta.Text = drr["referans_e_posta"].ToString();
                comboBox_egitim.Text = drr["okul_turu"].ToString();
                textBox_okul.Text = drr["okul_adı"].ToString();
                comboBox_sehir.Text = drr["okul_sehir"].ToString();
                comboBox_bolumadı.Text = drr["bolum_adı"].ToString();
                comboBox_sinif.Text = drr["sınıf"].ToString();
                textBox_okulno.Text = drr["okul_no"].ToString();
                textBox_okulpuan.Text = drr["okul_puan"].ToString();

                drr.Close(); //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
                connection.Close();
            }
            else
            {
                MessageBox.Show("Kayıt Bulunamadı");
                drr.Close();
                connection.Close();
            }
            
           
            #endregion



        }
        #endregion

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
    }
}
