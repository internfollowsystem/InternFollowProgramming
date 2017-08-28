﻿using System;
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


        string yol = "";
        public FrmInternInformation()
        {
            InitializeComponent();


            pictureBox_güncelle.Enabled = false; //Form açıldığında güncelle butonu pasif olsun.
            pictureBox_stajyer_sil.Enabled = false; //Form açıldığında delete butonu pasif olsun.
            panel_referans.Enabled = false;//Form açıldığında referans paneli pasif olsun.
            textBox_aracplaka.Enabled = false;//Form açıldığında plaka textBoxı pasif olsun.
            panel_bankabilgileri.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            listBox_dosya.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            pictureBox_dosya.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            tabControl_bilgigiriş.TabPages[2].Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            tabControl_bilgigiriş.TabPages[3].Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            tabControl_bilgigiriş.TabPages[4].Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            panel_stajbilgidosya.Enabled = false;


        }

        private void FrmInternInformation_Load(object sender, EventArgs e)
        {
         
        }

        #region BUTONLAR

        //STAJYER KAYDET BUTONU 25 AGUSTOS GÜNCEL !!
        private void pictureBox_kaydet_Click(object sender, EventArgs e)
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
                            byte[] resim = null;
                            FileStream fileStream = new FileStream(yol, FileMode.Open, FileAccess.Read);
                            BinaryReader binaryReader = new BinaryReader(fileStream);
                            resim = binaryReader.ReadBytes((int)fileStream.Length);
                            if (connection.State == ConnectionState.Closed)
                            {

                                command.Connection = connection;
                                connection.Open();

                                String stajyer = "Insert Into stajyer (tc_kimlikno,adı_soyadı,baba_adı,anne_adı,dogum_yeri,dogum_tarihi,uyrugu, web_site,kan_grubu,cinsiyet,ev_telefonu,cep_telefonu,ikametgah,e_posta,boy,agırlık,acil_adsoyad,acil_adres,acil_yakınlıgı,acil_eposta, acil_telefon, ortaokul_adı, lise_adı, universite_adı, resim) Values (@tc_kimlikno , @adı_soyadı , @baba_adı , @anne_adı , @dogum_yeri , @dogum_tarihi ,@uyrugu, @web_site, @kan_grubu , @cinsiyet , @ev_telefonu , @cep_telefonu , @ikametgah , @e_posta , @boy , @agırlık , @acil_adsoyad,@acil_adres,@acil_yakınlıgı ,@acil_eposta, @acil_telefon, @ortaokul_adı, @lise_adı, @universite_adı, @resim)";
                                command = new SqlCommand(stajyer, connection);
                                //kişisel veriler GÜNCELL KOD !!
                                command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                                command.Parameters.AddWithValue("@adı_soyadı", textBox_adsoyad.Text);
                                command.Parameters.AddWithValue("@baba_adı", textBox_baba.Text);
                                command.Parameters.AddWithValue("@anne_adı", textBox_anne.Text);
                                command.Parameters.AddWithValue("@dogum_yeri", textBox_dyeri.Text);
                                command.Parameters.AddWithValue("@dogum_tarihi", dateTimePicker_dtarih.Text);
                                command.Parameters.AddWithValue("@uyrugu", textBox_uyrugu.Text);
                                command.Parameters.AddWithValue("@web_site", textBox_website.Text);
                                command.Parameters.AddWithValue("@kan_grubu", comboBox_kangrubu.Text);
                                command.Parameters.AddWithValue("@cinsiyet", comboBox_cinsiyet.Text);
                                command.Parameters.AddWithValue("@ev_telefonu", textBox_evtel.Text);
                                command.Parameters.AddWithValue("@cep_telefonu", textBox_ceptel.Text);
                                command.Parameters.AddWithValue("@ikametgah", textBox_adres.Text);
                                command.Parameters.AddWithValue("@e_posta", textBox_eposta.Text);
                                command.Parameters.AddWithValue("@boy", textBox_boy.Text);
                                command.Parameters.AddWithValue("@agırlık", textBox_agırlık.Text);

                                //acil durum irtibat GÜNCELL KOD !!
                                command.Parameters.AddWithValue("@acil_adsoyad", textBox_ai_adsoyad.Text);
                                command.Parameters.AddWithValue("@acil_adres", textBox_ai_adres.Text);
                                command.Parameters.AddWithValue("@acil_yakınlıgı", textBox_ai_akrabalık.Text);
                                command.Parameters.AddWithValue("@acil_eposta", textBox_ai_eposta.Text);
                                command.Parameters.AddWithValue("@acil_telefon", textBox_ai_telefon.Text);

                                command.Parameters.AddWithValue("@ortaokul_adı", textBox_ortaokul.Text);
                                command.Parameters.AddWithValue("@lise_adı", textBox_lise.Text);
                                command.Parameters.AddWithValue("@universite_adı", textBox_universite.Text);
                                command.Parameters.AddWithValue("@resim", resim);

                                command.ExecuteNonQuery();
                                connection.Close();
                                MessageBox.Show("Stajyer Kaydedildi");

                                pictureBox_dosya.Enabled = true;
                                pictureBox_dosyayukle.Enabled = true;
                                tabControl_bilgigiriş.TabPages[2].Enabled = true;//Form açıldığında iban textBoxı pasif olsun.
                                tabControl_bilgigiriş.TabPages[3].Enabled = true;
                                tabControl_bilgigiriş.TabPages[4].Enabled = true;
                                listBox_dosya.Enabled = true;
                                panel_stajbilgidosya.Enabled = true;
                                pictureBox_stajbilgisi_kaydet.Enabled = true;
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

        //STAJ KAYDET 25 AGUSTOS GÜNCEL !!
        private void pictureBox_stajbilgisi_kaydet_Click(object sender, EventArgs e)
        {
            #region Doldurulması Zorunlu Alanlar
            string stajyili = comboBox_stajyili.Text;
            string stajdonemi = comboBox_stajdonemi.Text;
            string okuladı = comboBox_okuladı.Text;
            string bolumadı = comboBox_bolumadı.Text;
            string stajturu = comboBox_stajturu.Text;
            string referansadı = textBox_r_ad.Text;
            if (panel_referans.Enabled == true)
            {
                if (referansadı.Length == 0)
                {
                    MessageBox.Show("Lütfen Referans Adı alanını doldurunuz !");
                }
            }

            if (stajyili.Length == 0)
            {
                MessageBox.Show("Lütfen Staj Yıl alanını doldurunuz !");
            }
            if (stajdonemi.Length == 0)
            {
                MessageBox.Show("Lütfen Staj Dönemi alanını doldurunuz !");
            }
            if (okuladı.Length == 0)
            {
                MessageBox.Show("Lütfen Okul Adı alanını doldurunuz !");
            }
            if (bolumadı.Length == 0)
            {
                MessageBox.Show("Lütfen Bolum Adı alanını doldurunuz !");
            }
            if (stajturu.Length == 0)
            {
                MessageBox.Show("Lütfen Staj Türü alanını doldurunuz !");
            }
            #endregion

            command.Connection = connection;
            cmd.Connection = connection;
            connection.Open();
            String stajyer_bilgisi = "Insert Into stajbilgileri(tc_kimlikno,egitim_durumu,okul_adı,bolum_adı,sınıf,okul_no,sehir,okul_puanı,okul_acıklama,banka_adı,şube_kodu,hesap_no,iban_no,staj_kabuldurumu,staj_donem,baslangıc_tarihi,bitis_tarihi,staj_yılı,staj_yapmadurumu,staj_suresi,servis_imkanı,arac_plaka,mentör,sigorta_evrak,basvuru_turu,referans_adı,referans_adres,referans_telefon,referans_eposta,staj_acıklama,staj_turu,staj_icerigi,staj_kalan_sure) VALUES (@tc_kimlikno,@egitim_durumu,@okul_adı,@bolum_adı,@sınıf,@okul_no,@sehir,@okul_puanı,@okul_acıklama,@banka_adı,@şube_kodu,@hesap_no,@iban_no,@staj_kabuldurumu,@staj_donem,@baslangıc_tarihi,@bitis_tarihi,@staj_yılı,@staj_yapmadurumu,@staj_suresi,@servis_imkanı,@arac_plaka,@mentör,@sigorta_evrak,@basvuru_turu,@referans_adı,@referans_adres,@referans_telefon,@referans_eposta,@staj_acıklama,@staj_turu,@staj_icerigi,@staj_kalan_sure)";
            cmd = new SqlCommand(stajyer_bilgisi, connection);

            #region Staja kalan süre ve durum Hesapla
            TimeSpan kalan = dateTimePicker_baslangıc.Value - DateTime.Today;
            TimeSpan son = dateTimePicker_bitis.Value - DateTime.Today;
            int gunfarki_ilk = kalan.Days;
            int gunfarki_son = son.Days;
            string durum = "";

            if (gunfarki_ilk > 0)
            {
                durum = "STAJA BASLAMADI";
                gunfarki_ilk = kalan.Days;
            }
            else if (gunfarki_ilk < 0 || gunfarki_son > 0)
            {
                durum = "STAJ YAPIYOR";


            }
            else if (gunfarki_son < 0)
            {
                durum = "STAJI BITTI";

            }
            #endregion

            cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            cmd.Parameters.AddWithValue("@egitim_durumu", comboBox_egitim.Text);
            cmd.Parameters.AddWithValue("@okul_adı", comboBox_okuladı.Text);
            cmd.Parameters.AddWithValue("@bolum_adı", comboBox_bolumadı.Text);
            cmd.Parameters.AddWithValue("@sınıf", comboBox_sinif.Text);
            cmd.Parameters.AddWithValue("@okul_no", textBox_okulno.Text);
            cmd.Parameters.AddWithValue("@sehir", comboBox_sehir.Text);
            cmd.Parameters.AddWithValue("@okul_puanı", textBox_okulpuan.Text);
            cmd.Parameters.AddWithValue("@okul_acıklama", textBox_okulacıklama.Text);
            cmd.Parameters.AddWithValue("@banka_adı", textBox_bankaadı.Text);
            cmd.Parameters.AddWithValue("@şube_kodu", textBox_subekodu.Text);
            cmd.Parameters.AddWithValue("@hesap_no", textBox_hesapno.Text);
            cmd.Parameters.AddWithValue("@iban_no", textBox_iban.Text);
            cmd.Parameters.AddWithValue("@staj_kabuldurumu", comboBox_kabuldurumu.Text);
            cmd.Parameters.AddWithValue("@staj_donem", comboBox_stajdonemi.Text);
            cmd.Parameters.AddWithValue("@baslangıc_tarihi", dateTimePicker_baslangıc.Text);
            cmd.Parameters.AddWithValue("@bitis_tarihi", dateTimePicker_bitis.Text);
            cmd.Parameters.AddWithValue("@staj_yılı", comboBox_stajyili.Text);
            cmd.Parameters.AddWithValue("@staj_yapmadurumu", durum);
            cmd.Parameters.AddWithValue("@staj_suresi", textBox_stajsuresi.Text);
            cmd.Parameters.AddWithValue("@servis_imkanı", comboBox_servis.Text);
            cmd.Parameters.AddWithValue("@arac_plaka", textBox_aracplaka.Text);
            cmd.Parameters.AddWithValue("@mentör", comboBox_mentor.Text);

            //okul bilgileri  GÜNCELL KOD !!
            cmd.Parameters.AddWithValue("@sigorta_evrak", comboBox_sigorta.Text);
            cmd.Parameters.AddWithValue("@basvuru_turu", comboBox_basvuruturu.Text);
            cmd.Parameters.AddWithValue("@referans_adı", textBox_r_ad.Text);
            cmd.Parameters.AddWithValue("@referans_adres", textBox_r_adres.Text);
            cmd.Parameters.AddWithValue("@referans_telefon", textBox_r_telefon.Text);
            cmd.Parameters.AddWithValue("@referans_eposta", textBox_r_eposta.Text);
            cmd.Parameters.AddWithValue("@staj_acıklama", textBox_staj_aciklama.Text);
            cmd.Parameters.AddWithValue("@staj_turu", comboBox_stajturu.Text);
            string icerik = "";
            cmd.Parameters.AddWithValue("@staj_icerigi", icerik);
            cmd.Parameters.AddWithValue("@staj_kalan_sure", gunfarki_ilk);


            cmd.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Staj Bilgileri Kaydedildi !");

            textBox_okulpuan.Clear();
            textBox_okulno.Clear();
            textBox_lise.Clear();
            textBox_r_eposta.Clear();
            textBox_r_adres.Clear();
            textBox_r_telefon.Clear();
            textBox_r_ad.Clear();
            textBox_staj_aciklama.Clear();
            textBox_aracplaka.Clear();

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
            comboBox_okuladı.Text = "";
            comboBox_kabuldurumu.Text = "";
            comboBox_kangrubu.Text = "";
            comboBox_mentor.Text = "";
            comboBox_sehir.Text = "";
            comboBox_servis.Text = "";
            comboBox_sigorta.Text = "";
            comboBox_sinif.Text = "";
            comboBox_stajdonemi.Text = "";
            comboBox_stajturu.Text = "";
            comboBox_stajyili.Text = "";
        }


        //STAJYER SİL BUTONU:28 AGUSTOS PAZARTESİ GÜNCEL
        private void pictureBox_sil_Click(object sender, EventArgs e)
        {
            try
            {
                command.Connection = connection;
                connection.Open();
                string stajyer = "DELETE FROM stajyer where tc_kimlikno@tc_kimlikno";
                command = new SqlCommand(stajyer, connection);
                command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Kayıt Silinmiştir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Kayıt Silinemedi");
            }
          
        }

        //STAJ SİL BUTONU :28 AGUSTOS PAZARTESİ GÜNCEL
        private void pictureBox_stajsil_Click(object sender, EventArgs e)
        {
            try
            {
                command.Connection = connection;
                connection.Open();
                string staj = "DELETE FROM stajbilgileri where staj_id=@staj_id";
                command = new SqlCommand(staj, connection);
                command.Parameters.AddWithValue("@staj_id", label_kod.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Staj Bilgileri Silinmiştir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "İşlem gerçekleşememiştir");
            }

        }

        //STAJYER GÜNCELLEME BUTONU :25 AGUSTOS CUMA GÜNCEL ama her defasında yen resim seçmek gerekiyor.   ****
        private void pictureBox_güncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string stajyer = "update stajyer set tc_kimlikno=@tc_kimlikno,adı_soyadı=@adı_soyadı,baba_adı=@baba_adı,anne_adı=@anne_adı,dogum_yeri=@dogum_yeri,dogum_tarihi=@dogum_tarihi,uyrugu=@uyrugu,cinsiyet=@cinsiyet,ev_telefonu=@ev_telefonu,cep_telefonu=@cep_telefonu,ikametgah=@ikametgah,e_posta=@e_posta,web_site=@web_site,boy=@boy,agırlık=@agırlık,kan_grubu=@kan_grubu, acil_adsoyad=@acil_adsoyad,acil_adres=@acil_adres,acil_yakınlıgı=@acil_yakınlıgı, acil_telefon=@acil_telefon, acil_eposta=@acil_eposta  where tc_kimlikno=@tc_kimlikno";
                command = new SqlCommand(stajyer, connection);
                //kişisel veriler GÜNCELL KOD !!
                command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                command.Parameters.AddWithValue("@adı_soyadı", textBox_adsoyad.Text);
                command.Parameters.AddWithValue("@baba_adı", textBox_baba.Text);
                command.Parameters.AddWithValue("@anne_adı", textBox_anne.Text);
                command.Parameters.AddWithValue("@dogum_yeri", textBox_dyeri.Text);
                command.Parameters.AddWithValue("@dogum_tarihi", dateTimePicker_dtarih.Text);
                command.Parameters.AddWithValue("@uyrugu", textBox_uyrugu.Text);
                command.Parameters.AddWithValue("@web_site", textBox_website.Text);
                command.Parameters.AddWithValue("@kan_grubu", comboBox_kangrubu.Text);
                command.Parameters.AddWithValue("@cinsiyet", comboBox_cinsiyet.Text);
                command.Parameters.AddWithValue("@ev_telefonu", textBox_evtel.Text);
                command.Parameters.AddWithValue("@cep_telefonu", textBox_ceptel.Text);
                command.Parameters.AddWithValue("@ikametgah", textBox_adres.Text);
                command.Parameters.AddWithValue("@e_posta", textBox_eposta.Text);
                command.Parameters.AddWithValue("@boy", textBox_boy.Text);
                command.Parameters.AddWithValue("@agırlık", textBox_agırlık.Text);

                command.Parameters.AddWithValue("@acil_adsoyad", textBox_ai_adsoyad.Text);
                command.Parameters.AddWithValue("@acil_adres", textBox_ai_adres.Text);
                command.Parameters.AddWithValue("@acil_yakınlıgı", textBox_ai_akrabalık.Text);
                command.Parameters.AddWithValue("@acil_telefon", textBox_ai_telefon.Text);
                command.Parameters.AddWithValue("@acil_eposta", textBox_ai_eposta.Text);

                command.Parameters.AddWithValue("@ortaokul_adı", textBox_ortaokul.Text);
                command.Parameters.AddWithValue("@lise_adı", textBox_lise.Text);
                command.Parameters.AddWithValue("@universite", textBox_universite.Text);


                byte[] resim = null;
                FileStream fileStream = new FileStream(yol, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                resim = binaryReader.ReadBytes((int)fileStream.Length);

                command.Parameters.AddWithValue("@resim", resim);

                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Stajyer Bilgileri Güncellendi");
                #region verileri sil
                //textBox_okulpuan.Clear();
                //textBox_okulno.Clear();
                //textBox_lise.Clear();
                //textBox_r_eposta.Clear();
                //textBox_r_adres.Clear();
                //textBox_r_telefon.Clear();
                //textBox_r_ad.Clear();
                //textBox_staj_aciklama.Clear();
                //textBox_arac.Clear();
                //textBox_stajkonuları.Clear();
                //textBox_stajsuresi.Clear();
                //textBox_ai_eposta.Clear();
                //textBox_ai_telefon.Clear();
                //textBox_ai_akrabalık.Clear();
                //textBox_ai_adres.Clear();
                //textBox_ai_adsoyad.Clear();
                //textBox_ai_adres.Clear();
                //textBox_tc.Clear();
                //textBox_adsoyad.Clear();
                //textBox_baba.Clear();
                //textBox_anne.Clear();
                //textBox_dyeri.Clear();
                //textBox_uyrugu.Clear();
                //textBox_evtel.Clear();
                //textBox_ceptel.Clear();
                //textBox_adres.Clear();
                //textBox_eposta.Clear();
                //textBox_website.Clear();
                //textBox_boy.Clear();
                //textBox_agırlık.Clear();
                //textBox_iban.Clear();

                //comboBox_basvuruturu.Text = "";
                //comboBox_bolumadı.Text = "";
                //comboBox_cinsiyet.Text = "";
                //comboBox_okuladı.Text = "";
                //comboBox_kabuldurumu.Text = "";
                //comboBox_kangrubu.Text = "";
                //comboBox_mentor.Text = "";
                //comboBox_sehir.Text = "";
                //comboBox_servis.Text = "";
                //comboBox_sigorta.Text = "";
                //comboBox_sinif.Text = "";
                //comboBox_stajdonemi.Text = "";
                //comboBox_stajturu.Text = "";
                //comboBox_stajyili.Text = "";
                #endregion
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        private void pictureBox_stajgüncelle_Click(object sender, EventArgs e)
        {

            #region Doldurulması Zorunlu Alanlar
            string stajyili = comboBox_stajyili.Text;
            string stajdonemi = comboBox_stajdonemi.Text;
            string okuladı = comboBox_okuladı.Text;
            string bolumadı = comboBox_bolumadı.Text;
            string stajturu = comboBox_stajturu.Text;
            string referansadı = textBox_r_ad.Text;
            if (panel_referans.Enabled == true)
            {
                if (referansadı.Length == 0)
                {
                    MessageBox.Show("Lütfen Referans Adı alanını doldurunuz !");
                }
            }

            if (stajyili.Length == 0)
            {
                MessageBox.Show("Lütfen Staj Yıl alanını doldurunuz !");
            }
            if (stajdonemi.Length == 0)
            {
                MessageBox.Show("Lütfen Staj Dönemi alanını doldurunuz !");
            }
            if (okuladı.Length == 0)
            {
                MessageBox.Show("Lütfen Okul Adı alanını doldurunuz !");
            }
            if (bolumadı.Length == 0)
            {
                MessageBox.Show("Lütfen Bolum Adı alanını doldurunuz !");
            }
            if (stajturu.Length == 0)
            {
                MessageBox.Show("Lütfen Staj Türü alanını doldurunuz !");
            }
            #endregion

            cmd.Connection = connection;
            connection.Open();
            String stajbilgisi = "Update stajbilgileri set tc_kimlikno=@tc_kimlikno,egitim_durumu=@egitim_durumu ,okul_adı=@okul_adı ,bolum_adı=@bolum_adı,sınıf=@sınıf,okul_no=@okul_no, sehir=@sehir ,okul_puanı=@okul_puanı ,okul_acıklama=@okul_acıklama ,banka_adı=@banka_adı ,şube_kodu=şube_kodu ,hesap_no=@hesap_no ,iban_no=@iban_no ,staj_kabuldurumu=@staj_kabuldurumu,staj_donem=@staj_donem ,baslangıc_tarihi=@baslangıc_tarihi ,bitis_tarihi=@bitis_tarihi , staj_yılı=@staj_yılı ,staj_yapmadurumu=@staj_yapmadurumu,staj_suresi=@staj_suresi,servis_imkanı=@servis_imkanı ,arac_plaka=@arac_plaka ,mentör=@mentör,sigorta_evrak=@sigorta_evrak ,basvuru_turu=@basvuru_turu ,referans_adı=@referans_adı ,referans_adres=@referans_adres,referans_telefon=@referans_telefon ,referans_eposta=@referans_eposta ,staj_acıklama=@staj_acıklama ,staj_turu=@staj_turu ,staj_icerigi=@staj_icerigi ,staj_kalan_sure=@staj_kalan_sure where staj_id=@staj_id";
            cmd = new SqlCommand(stajbilgisi, connection);

            #region Staja kalan süre ve durum Hesapla
            TimeSpan kalan = dateTimePicker_baslangıc.Value - DateTime.Today;
            TimeSpan son = dateTimePicker_bitis.Value - DateTime.Today;
            int gunfarki_ilk = kalan.Days;
            int gunfarki_son = son.Days;
            string durum = "";

            if (gunfarki_ilk > 0)
            {
                durum = "STAJA BASLAMADI";
                gunfarki_ilk = kalan.Days;
            }
            else if (gunfarki_ilk < 0 || gunfarki_son > 0)
            {
                durum = "STAJ YAPIYOR";


            }
            else if (gunfarki_son < 0)
            {
                durum = "STAJI BITTI";

            }
            #endregion

            cmd.Parameters.AddWithValue("@staj_id", label_kod.Text);
            cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            cmd.Parameters.AddWithValue("@egitim_durumu", comboBox_egitim.Text);
            cmd.Parameters.AddWithValue("@okul_adı", comboBox_okuladı.Text);
            cmd.Parameters.AddWithValue("@bolum_adı", comboBox_bolumadı.Text);
            cmd.Parameters.AddWithValue("@sınıf", comboBox_sinif.Text);
            cmd.Parameters.AddWithValue("@okul_no", textBox_okulno.Text);
            cmd.Parameters.AddWithValue("@sehir", comboBox_sehir.Text);
            cmd.Parameters.AddWithValue("@okul_puanı", textBox_okulpuan.Text);
            cmd.Parameters.AddWithValue("@okul_acıklama", textBox_okulacıklama.Text);
            cmd.Parameters.AddWithValue("@banka_adı", textBox_bankaadı.Text);
            cmd.Parameters.AddWithValue("@şube_kodu", textBox_subekodu.Text);
            cmd.Parameters.AddWithValue("@hesap_no", textBox_hesapno.Text);
            cmd.Parameters.AddWithValue("@iban_no", textBox_iban.Text);
            cmd.Parameters.AddWithValue("@staj_kabuldurumu", comboBox_kabuldurumu.Text);
            cmd.Parameters.AddWithValue("@staj_donem", comboBox_stajdonemi.Text);
            cmd.Parameters.AddWithValue("@baslangıc_tarihi", dateTimePicker_baslangıc.Text);
            cmd.Parameters.AddWithValue("@bitis_tarihi", dateTimePicker_bitis.Text);
            cmd.Parameters.AddWithValue("@staj_yılı", comboBox_stajyili.Text);
            cmd.Parameters.AddWithValue("@staj_yapmadurumu", durum);
            cmd.Parameters.AddWithValue("@staj_suresi", textBox_stajsuresi.Text);
            cmd.Parameters.AddWithValue("@servis_imkanı", comboBox_servis.Text);
            cmd.Parameters.AddWithValue("@arac_plaka", textBox_aracplaka.Text);
            cmd.Parameters.AddWithValue("@mentör", comboBox_mentor.Text);


            cmd.Parameters.AddWithValue("@sigorta_evrak", comboBox_sigorta.Text);
            cmd.Parameters.AddWithValue("@basvuru_turu", comboBox_basvuruturu.Text);
            cmd.Parameters.AddWithValue("@referans_adı", textBox_r_ad.Text);
            cmd.Parameters.AddWithValue("@referans_adres", textBox_r_adres.Text);
            cmd.Parameters.AddWithValue("@referans_telefon", textBox_r_telefon.Text);
            cmd.Parameters.AddWithValue("@referans_eposta", textBox_r_eposta.Text);
            cmd.Parameters.AddWithValue("@staj_acıklama", textBox_staj_aciklama.Text);
            cmd.Parameters.AddWithValue("@staj_turu", comboBox_stajturu.Text);
            string icerik = "";
            cmd.Parameters.AddWithValue("@staj_icerigi", icerik);
            cmd.Parameters.AddWithValue("@staj_kalan_sure", gunfarki_ilk);


            cmd.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Staj Bilgileri Kaydedildi !");

        }

        //RESİM SEÇME BUTONU  
        private void pictureBox_resim_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Resim Dosyaları |*.jpg;*.jpeg;*.png |GIF Dosyaları|*.gif |Tüm Dosyalar |*.*";
                dialog.Title = "Select stajyer resim";
                if(dialog.ShowDialog()==DialogResult.OK)
                {
                    yol = dialog.FileName.ToString();
                    pictureBox_stajyer_resim.ImageLocation = yol;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"Resim yüklenemedi");
            }
        }
        
        //DOSYA SEÇME BUTONU 
        private void pictureBox_dosya_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "pdf dosyalar |*.pdf";
            dialog.Title = "SELECT file dosya_adı ";
            if (dialog.ShowDialog() == DialogResult.OK)

            {
                yol= Path.GetFileName(dialog.FileName);
                byte[] Dosya = null;
                Dosya = File.ReadAllBytes(dialog.FileName);
                textBox_dosya.Text = yol;
            }
        }

        //DOSYA YÜKLEME BUTONU
        private void pictureBox_fileupdate_Click(object sender, EventArgs e)
        {
             try
                {
                    string dosya_uzantisi = openFileDialog_dosya.Filter;
                    DateTime yukleme_tarihi = DateTime.Today;
                    command.Connection = connection;
                    cmd.Connection = connection;
                    connection.Open();
                    string files = "insert into [file] (tc_kimlikno, dosya_adı, dosya_uzantisi, yukleme_tarihi) Values (@tc_kimlikno, @dosya_adı, @dosya_uzantisi, @yukleme_tarihi )";
                    command = new SqlCommand(files, connection);
                    command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                    command.Parameters.AddWithValue("@dosya_adı", textBox_dosya.Text);
                    command.Parameters.AddWithValue("@dosya_uzantisi", dosya_uzantisi);
                    command.Parameters.AddWithValue("@yukleme_tarihi", yukleme_tarihi);
                    command.ExecuteNonQuery();

                    MessageBox.Show("DÖKÜMAN EKLENDİ");
                listBox_dosya.Items.Clear();
                string list = "SELECT * From [file] where tc_kimlikno=@tc_kimlikno";
                cmd = new SqlCommand(list, connection);
                cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listBox_dosya.Items.Add(dr["dosya_adı"]);
                }
                dr.Close();
                connection.Close();
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Dosya yüklenemedi");
                    connection.Close();
                }
            }
        
        //STAJYER BUL BUTONU GÜNCEL !!
        private void pictureBox_bul_Click(object sender, EventArgs e)
        {

            pictureBox_güncelle.Enabled = true;
            pictureBox_stajyer_sil.Enabled = true;
            pictureBox_stajbilgisi_kaydet.Enabled = true;
            pictureBox_dosya.Enabled = true;
            pictureBox_dosyayukle.Enabled = true;
            tabControl_bilgigiriş.TabPages[2].Enabled = true;
            tabControl_bilgigiriş.TabPages[3].Enabled = true;
            tabControl_bilgigiriş.TabPages[4].Enabled = true;
            listBox_dosya.Enabled = true;
            panel_stajbilgidosya.Enabled = true;

            #region Stajyer bul.
            connection.Open();
            cmd.Connection = connection;
            command.Connection = connection;
            string stajyer = "SELECT * from stajyer where tc_kimlikno=@tc_kimlikno";
            command = new SqlCommand(stajyer, connection);
            command.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
            
            dataadapter = new SqlDataAdapter(command);
            datareader = command.ExecuteReader();
            if (datareader.Read())
            {
                textBox_tc.Text = datareader["tc_kimlikno"].ToString();   //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım.       
                textBox_adsoyad.Text =datareader["adı_soyadı"].ToString();
                textBox_baba.Text = datareader["baba_adı"].ToString();
                textBox_anne.Text = datareader["anne_adı"].ToString();
                textBox_dyeri.Text = datareader["dogum_yeri"].ToString();
                dateTimePicker_dtarih.Text = datareader["dogum_tarihi"].ToString();
                textBox_uyrugu.Text = datareader["uyrugu"].ToString();
                textBox_website.Text = datareader["web_site"].ToString();
                comboBox_kangrubu.Text = datareader["kan_grubu"].ToString();
                comboBox_cinsiyet.Text = datareader["cinsiyet"].ToString();
                textBox_evtel.Text = datareader["ev_telefonu"].ToString();
                textBox_ceptel.Text = datareader["cep_telefonu"].ToString();
                textBox_adres.Text = datareader["ikametgah"].ToString();
                textBox_eposta.Text = datareader["e_posta"].ToString();
                textBox_boy.Text = datareader["boy"].ToString();
                textBox_agırlık.Text = datareader["agırlık"].ToString();
                textBox_ai_adsoyad.Text = datareader["acil_adsoyad"].ToString();
                textBox_ai_adres.Text = datareader["acil_adres"].ToString();
                textBox_ai_akrabalık.Text = datareader["acil_yakınlıgı"].ToString();
                textBox_ai_eposta.Text = datareader["acil_eposta"].ToString();
                textBox_ai_telefon.Text = datareader["acil_telefon"].ToString();
                textBox_ortaokul.Text = datareader["ortaokul_adı"].ToString();
                textBox_lise.Text = datareader["lise_adı"].ToString();
                textBox_universite.Text = datareader["universite_adı"].ToString();
                byte[] resim = (byte[])(datareader["resim"]);

                if (resim == null)
                    pictureBox_stajyer_resim.Image = null;
                else
                {
                    MemoryStream memoryStream = new MemoryStream(resim);
                    pictureBox_stajyer_resim.Image = Image.FromStream(memoryStream);
                }
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

            #region COMBOBOX'IN İÇİNE STAJ BİLGİLERİNİ ÇEK
            string staj = "SELECT DISTINCT staj_turu FROM stajbilgileri where tc_kimlikno=@tc_kimlikno";
            cmd = new SqlCommand(staj, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
            datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_staj.Items.Add(datareader["staj_turu"]);
            }
            datareader.Close();
            connection.Close();
            #endregion
            
        }

        //STAJ KODU GETİR KODU
        private void comboBox_staj_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "Select staj_id from stajbilgileri where tc_kimlikno=@tc_kimlikno and staj_turu=@staj_turu";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Connection = connection;
            command.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
            command.Parameters.AddWithValue("@staj_turu", comboBox_staj.Text);
            connection.Open();
            SqlDataReader datareader;
            dataadapter = new SqlDataAdapter(command);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {

                label_kod.Text = datareader["staj_id"].ToString();

            }
            datareader.Close();
            connection.Close();
        }

        //STAJ BUL BUTONU GÜNCEL
        private void pictureBox_staj_Click(object sender, EventArgs e)
        {
           
            listBox_dosya.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            command.Connection = connection;
            connection.Open();
            string staj = "SELECT staj_id, tc_kimlikno,egitim_durumu,okul_adı,bolum_adı,sınıf,okul_no,sehir,okul_puanı,okul_acıklama,banka_adı,şube_kodu,hesap_no,iban_no,staj_kabuldurumu,staj_donem,baslangıc_tarihi,bitis_tarihi,staj_yılı,staj_yapmadurumu,staj_suresi,servis_imkanı,arac_plaka,mentör,sigorta_evrak,basvuru_turu,referans_adı,referans_adres,referans_telefon,referans_eposta,staj_acıklama,staj_turu,staj_icerigi,staj_kalan_sure FROM stajbilgileri where tc_kimlikno=@tc_kimlikno and staj_turu=@staj_turu";
            command = new SqlCommand(staj, connection);
            command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            command.Parameters.AddWithValue("@staj_turu", comboBox_staj.Text);
            SqlDataReader dr;
            dataadapter = new SqlDataAdapter(command);
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                label_kod.Text = dr["staj_id"].ToString();
                textBox_tc.Text = dr["tc_kimlikno"].ToString();
                comboBox_kabuldurumu.Text = dr["staj_kabuldurumu"].ToString();
                comboBox_stajdonemi.Text = dr["staj_donem"].ToString();
                dateTimePicker_baslangıc.Text = dr["baslangıc_tarihi"].ToString();
                dateTimePicker_bitis.Text = dr["bitis_tarihi"].ToString();
                textBox_stajyapmadurumu.Text = dr["staj_yapmadurumu"].ToString();
                textBox_stajsuresi.Text = dr["staj_suresi"].ToString();
                comboBox_servis.Text = dr["servis_imkanı"].ToString();
                textBox_aracplaka.Text = dr["arac_plaka"].ToString();
                comboBox_mentor.Text = dr["mentör"].ToString();
                comboBox_sigorta.Text = dr["sigorta_evrak"].ToString();
                comboBox_stajyili.Text = dr["staj_yılı"].ToString();
                comboBox_basvuruturu.Text = dr["basvuru_turu"].ToString();
                textBox_r_ad.Text = dr["referans_adı"].ToString();
                textBox_r_adres.Text = dr["referans_adres"].ToString();
                textBox_r_eposta.Text = dr["referans_eposta"].ToString();
                textBox_r_telefon.Text = dr["referans_telefon"].ToString();
                textBox_staj_aciklama.Text = dr["staj_acıklama"].ToString();
                comboBox_egitim.Text = dr["egitim_durumu"].ToString();
                comboBox_okuladı.Text = dr["okul_adı"].ToString();
                comboBox_bolumadı.Text = dr["bolum_adı"].ToString();
                comboBox_sinif.Text = dr["sınıf"].ToString();
                textBox_okulno.Text = dr["okul_no"].ToString();
                comboBox_sehir.Text = dr["sehir"].ToString();
                textBox_okulpuan.Text = dr["okul_puanı"].ToString();
                textBox_okulacıklama.Text = dr["okul_acıklama"].ToString();
                textBox_bankaadı.Text = dr["banka_adı"].ToString();
                textBox_subekodu.Text = dr["şube_kodu"].ToString();
                textBox_hesapno.Text = dr["hesap_no"].ToString();
                textBox_iban.Text = dr["iban_no"].ToString();
                comboBox_stajturu.Text = dr["staj_turu"].ToString();
                checkBox1.Text = dr["staj_icerigi"].ToString();
                label_kalansure.Text = dr["staj_kalan_sure"].ToString();

            }
            dr.Close();
            connection.Close();

           

            #region DÖKÜMANLARI LİSTBOX'A AKTAR
            //string list = "SELECT * From [file] where tc_kimlikno=@tc_kimlikno";
            //cmd = new SqlCommand(list, connection);
            //cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            //SqlDataReader datareader;
            //datareader = cmd.ExecuteReader();
            //while (datareader.Read())
            //{
            //    listBox_dosya.Items.Add(datareader["dosya_adı"]);
            //}
            //dr.Close();
            #endregion
        }


        #endregion

        #region SEÇİMLERE BAĞLI OLARAK AKTİF/PASİFLİK DURUMLARI

        private void comboBox_basvuruturu_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBox_basvuruturu.Text == "REFERANS")
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
                textBox_aracplaka.Enabled = true;
            }
            else if(comboBox_servis.Text == "YOK")
            {
                textBox_aracplaka.Enabled = false;
            }

        }

        private void comboBox_egitim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_egitim.Text == "Lise")
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
            tabControl_bilgigiriş.SelectedIndex = 1; //İndex olarak geçişleri sağlayabiliriz
        }

        private void label45_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedTab =tabPage_okul; //Pages üzerindende aynı şekildegeçişleri sağlayabiliriz. Alternatif gösterim
        }

        private void label46_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 3;
        }








        #endregion

        #region MOUSE HAREKETLERİ
        private void textBox_tcbul_MouseHover(object sender, EventArgs e)
        {
            if (textBox_tcbul.Text == "tc kimlik no giriniz !")
            {
                textBox_tcbul.Text = String.Empty;
            }
           

        }

        private void textBox_tcbul_MouseLeave(object sender, EventArgs e)
        {
            if(textBox_tcbul.Text == string.Empty)
            {
                textBox_tcbul.Text = "tc kimlik no giriniz !";
            }
            
        }

        private void pictureBox_resim_MouseHover(object sender, EventArgs e)
        {
            pictureBox_resim.Width = 40;
            pictureBox_resim.Height = 40;
        }

        private void pictureBox_resim_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_resim.Width = 30;
            pictureBox_resim.Height = 30;
        }

        private void pictureBox_dosya_MouseHover(object sender, EventArgs e)
        {
            pictureBox_dosya.Width = 25;
            pictureBox_dosya.Height = 25;
        }

        private void pictureBox_dosya_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_dosya.Width = 20;
            pictureBox_dosya.Height = 20;
        }

        private void pictureBox_fileupdate_MouseHover(object sender, EventArgs e)
        {
            pictureBox_dosyayukle.Width = 25;
            pictureBox_dosyayukle.Height = 25;
        }

        private void pictureBox_fileupdate_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_dosyayukle.Width = 20;
            pictureBox_dosyayukle.Height = 20;
        }

        private void pictureBox_bul_MouseHover(object sender, EventArgs e)
        {
            pictureBox_bul.Width = 40;
            pictureBox_bul.Height = 40;
        }

        private void pictureBox_bul_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_bul.Width = 32;
            pictureBox_bul.Height = 32;
        }

        private void pictureBox_staj_MouseHover(object sender, EventArgs e)
        {
            pictureBox_staj.Width = 30;
            pictureBox_staj.Height = 30;
        }

        private void pictureBox_staj_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_staj.Width = 25;
            pictureBox_staj.Height = 25;
        }

        private void pictureBox_kaydet_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajyer_kaydet.Width = 30;
            pictureBox_stajyer_kaydet.Height = 30;
        }

        private void pictureBox_kaydet_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajyer_kaydet.Width = 25;
            pictureBox_stajyer_kaydet.Height = 25;
        }

        private void pictureBox_kaydet_stajbilgisi_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajbilgisi_kaydet.Width = 30;
            pictureBox_stajbilgisi_kaydet.Height = 30;
        }

        private void pictureBox_kaydet_stajbilgisi_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajbilgisi_kaydet.Width = 25;
            pictureBox_stajbilgisi_kaydet.Height = 25;
        }

        private void pictureBox_güncelle_MouseHover(object sender, EventArgs e)
        {
            pictureBox_güncelle.Width = 40;
            pictureBox_güncelle.Height = 40;
        }

        private void pictureBox_güncelle_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_güncelle.Width = 32;
            pictureBox_güncelle.Height = 32;
        }

        private void pictureBox_sil_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajyer_sil.Width = 40;
            pictureBox_stajyer_sil.Height = 40;
        }

        private void pictureBox_sil_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajyer_sil.Width = 32;
            pictureBox_stajyer_sil.Height = 32;
        }






		#endregion

		
       

        
    }
}
