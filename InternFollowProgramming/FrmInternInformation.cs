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
        static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN; MultipleActiveResultSets=True;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataAdapter dataadapter;
        SqlDataReader datareader;
        SqlCommand cmd = new SqlCommand();
        #endregion
        public FrmInternInformation()
        {
            InitializeComponent();


            pictureBox_güncelle.Enabled = false; //Form açıldığında güncelle butonu pasif olsun.
            pictureBox_stajyer_sil.Enabled = false; //Form açıldığında delete butonu pasif olsun.
            panel_referans.Enabled = false;//Form açıldığında referans paneli pasif olsun.
            comboBox_aracplaka.Enabled = false;//Form açıldığında plaka textBoxı pasif olsun.
            panel_bankabilgileri.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            listBox_dosya.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            pictureBox_dosya.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            tabControl_bilgigiriş.TabPages[2].Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            tabControl_bilgigiriş.TabPages[3].Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            tabControl_bilgigiriş.TabPages[4].Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            panel_stajbilgidosya.Enabled = false;
            panel_stajicerigi.Enabled = false;
            textBox_stajyapmadurumu.Visible = false;


        }

        private void FrmInternInformation_Load(object sender, EventArgs e)
        {
           
        }

        #region STAJ İÇERİĞİ EKLEME İŞLEMLERİ

        //COMBOBOX'İÇİNDEKİ STAJ TURUNE GÖRE İCERİKLERİ LİSTBOX'A AKTARMA OLAYI:29 AGUSTOS 2017
        private void comboBox_stajturu_SelectedIndexChanged(object sender, EventArgs e)
        {

            listBox_icerikler.Items.Clear();
            command.Connection = connection;
            connection.Close();
            connection.Open();

            #region COMBOBOX'A VERİ ÇEKME
            string cmbxveri = "SELECT * FROM " + comboBox_stajturu.SelectedItem + "";
            command = new SqlCommand(cmbxveri, connection);

            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                listBox_icerikler.Items.Add(datareader["id"]);
            }
            datareader.Close();
            #endregion
            connection.Close();
            panel_stajicerigi.Enabled = true;
        }

        //STAJ TÜRÜ İÇERİĞİ EKLEME BUTONU:29 AGUSTOS 2017 
        private void button_icerikekle_Click(object sender, EventArgs e)
        {
            listBox_icerikler.Items.Clear();
            command.Connection = connection;
            connection.Open();
            string cmbxicerikekle = "INSERT INTO " + comboBox_stajturu.Text + " (id) VALUES (@id)";
            command = new SqlCommand(cmbxicerikekle, connection);
            command.Parameters.AddWithValue("@id", textBox_icerik.Text);
            command.ExecuteNonQuery();

            MessageBox.Show("İCERİĞİNİZ EKLENDİ");
            textBox_icerik.Clear();

            #region COMBOBOX'A VERİ ÇEKME
            string cmbxveri = "SELECT * FROM " + comboBox_stajturu.SelectedItem + "";
            cmd = new SqlCommand(cmbxveri, connection);

            datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                listBox_icerikler.Items.Add(datareader["id"]);
            }
            datareader.Close();
            #endregion

            connection.Close();

        }

        //İCERİK LİSTBOX'ININ SEÇİLİ OLDUĞU YERİ TEXTBOX'A AKTARMA:29 AGUSTOS 2017 
        private void listBox_icerikler_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_icerik.Text = listBox_icerikler.SelectedItem.ToString();
        }

        //STAJ TÜRÜ İÇERİĞİ ÇIKARTMA BUTONU:29 AGUSTOS 2017 
        private void button_icerikcıkart_Click(object sender, EventArgs e)
        {

            command.Connection = connection;
            connection.Open();
            string cmbxverisil = "DELETE FROM " + comboBox_stajturu.Text + " where id=@id";
            command = new SqlCommand(cmbxverisil, connection);

            command.Parameters.AddWithValue("@id", textBox_icerik.Text);
            command.ExecuteNonQuery();

            MessageBox.Show("İÇERİĞİNİZ SİLİNMİŞTİR");

            listBox_icerikler.Items.Clear();
            #region COMBOBOX'A VERİ ÇEKME
            string cmbxveri = "SELECT * FROM " + comboBox_stajturu.SelectedItem + "";
            command = new SqlCommand(cmbxveri, connection);

            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                listBox_icerikler.Items.Add(datareader["id"]);
            }
            datareader.Close();
            #endregion

            connection.Close();
            textBox_icerik.Clear();
        }

        //İÇERİĞİ STAJBİLGİLERİ İÇİN LİSTBOX'A AKTARMA:29 AGUSTOS 2017 
        private void button_aktar_Click(object sender, EventArgs e)
        {
            textBox_stajicerigi.Clear();
            listBox_stajicerigi.Items.Add(listBox_icerikler.SelectedItem);
            string[] icerikler = new string[listBox_stajicerigi.Items.Count];
            for (int i = 0; i < listBox_stajicerigi.Items.Count; i++)
            {
                icerikler[i] = listBox_stajicerigi.Items[i].ToString();
                textBox_stajicerigi.Text = textBox_stajicerigi.Text + icerikler[i];
            }

        }

        //İÇERİĞİ STAJBİLGİLERİ LİSTBOX'INDAN SİLME :29 AGUSTOS 2017 
        private void button_kaldır_Click(object sender, EventArgs e)
        {
            textBox_stajicerigi.Clear();
            listBox_stajicerigi.Items.Remove(listBox_stajicerigi.SelectedItem);
            string[] icerikler = new string[listBox_stajicerigi.Items.Count];
            for (int i = 0; i < listBox_stajicerigi.Items.Count; i++)
            {
                icerikler[i] = listBox_stajicerigi.Items[i].ToString();
                textBox_stajicerigi.Text = textBox_stajicerigi.Text + "+" + icerikler[i];
            }
        }
        #endregion

        #region BUTONLAR

          #region Kaydet Butonları
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
                            if (connection.State == ConnectionState.Closed)
                            {

                                command.Connection = connection;
                                connection.Open();

                                String stajyer = "Insert Into stajyer (tc_kimlikno,adı_soyadı,baba_adı,anne_adı,dogum_yeri,dogum_tarihi,uyrugu, web_site,kan_grubu,cinsiyet,ev_telefonu,cep_telefonu,ikametgah,e_posta,boy,agırlık,acil_adsoyad,acil_adres,acil_yakınlıgı,acil_eposta, acil_telefon, ortaokul_adı, lise_adı, universite_adı) Values (@tc_kimlikno , @adı_soyadı , @baba_adı , @anne_adı , @dogum_yeri , @dogum_tarihi ,@uyrugu, @web_site, @kan_grubu , @cinsiyet , @ev_telefonu , @cep_telefonu , @ikametgah , @e_posta , @boy , @agırlık , @acil_adsoyad,@acil_adres,@acil_yakınlıgı ,@acil_eposta, @acil_telefon, @ortaokul_adı, @lise_adı, @universite_adı)";
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

                                command.Parameters.AddWithValue("@ortaokul_adı", comboBox_ortaokul.Text);
                                command.Parameters.AddWithValue("@lise_adı", comboBox_lise.Text);
                                command.Parameters.AddWithValue("@universite_adı", comboBox_universite.Text);


                                command.ExecuteNonQuery();
                                connection.Close();
                                MessageBox.Show("Stajyer Kaydedildi");

                                pictureBox_dosya.Enabled = true;
                                pictureBox_dosyayukle.Enabled = true;
                                tabControl_bilgigiriş.TabPages[2].Enabled = true;//Form açıldığında iban textBoxı pasif olsun.
                                tabControl_bilgigiriş.TabPages[3].Enabled = true;
                                tabControl_bilgigiriş.TabPages[4].Enabled = true;
                                tabControl_bilgigiriş.SelectedIndex = 2;
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
                        DialogResult tchata = new DialogResult();

                        tchata= MessageBox.Show("Geçersiz Tc Kimlik Numarasını yanlış girdiniz devam etmek ister misiniz?", "HATA:TC KİMLİK.", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if(tchata==DialogResult.Yes)
                        {
                            command.Connection = connection;
                            connection.Open();

                            String stajyer = "Insert Into stajyer (tc_kimlikno,adı_soyadı,baba_adı,anne_adı,dogum_yeri,dogum_tarihi,uyrugu, web_site,kan_grubu,cinsiyet,ev_telefonu,cep_telefonu,ikametgah,e_posta,boy,agırlık,acil_adsoyad,acil_adres,acil_yakınlıgı,acil_eposta, acil_telefon, ortaokul_adı, lise_adı, universite_adı) Values (@tc_kimlikno , @adı_soyadı , @baba_adı , @anne_adı , @dogum_yeri , @dogum_tarihi ,@uyrugu, @web_site, @kan_grubu , @cinsiyet , @ev_telefonu , @cep_telefonu , @ikametgah , @e_posta , @boy , @agırlık , @acil_adsoyad,@acil_adres,@acil_yakınlıgı ,@acil_eposta, @acil_telefon, @ortaokul_adı, @lise_adı, @universite_adı)";
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

                            command.Parameters.AddWithValue("@ortaokul_adı", comboBox_ortaokul.Text);
                            command.Parameters.AddWithValue("@lise_adı", comboBox_lise.Text);
                            command.Parameters.AddWithValue("@universite_adı", comboBox_universite.Text);


                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Stajyer Kaydedildi");

                            pictureBox_dosya.Enabled = true;
                            pictureBox_dosyayukle.Enabled = true;
                            tabControl_bilgigiriş.TabPages[2].Enabled = true;//Form açıldığında iban textBoxı pasif olsun.
                            tabControl_bilgigiriş.TabPages[3].Enabled = true;
                            tabControl_bilgigiriş.TabPages[4].Enabled = true;
                            tabControl_bilgigiriş.SelectedIndex = 2;
                            listBox_dosya.Enabled = true;
                            panel_stajbilgidosya.Enabled = true;
                            pictureBox_stajbilgisi_kaydet.Enabled = true;
                        }
                        else if(tchata==DialogResult.No)
                        {
                            
                        }
                        else if(tchata==DialogResult.Cancel)
                        {

                        }


                    }
                }
            }

            if(label_resimyolu.Text!=string.Empty)
            {
                Directory.CreateDirectory("O:STAJER_TAKIP\\StajyerGörselleri\\" + textBox_tc.Text);
                string resimAdi = Path.GetFileName(label_resimyolu.Text);
                File.Copy(@"" + label_resimyolu.Text, @"" + @"O:STAJER_TAKIP\\StajyerGörselleri\\" + textBox_tc.Text + "\\"+resimAdi);
                File.Move("O:STAJER_TAKIP\\StajyerGörselleri\\" + textBox_tc.Text + "\\" + resimAdi, "O:STAJER_TAKIP\\StajyerGörselleri\\" + textBox_tc.Text + "\\kişiselgörsel.jpg");
                MessageBox.Show(textBox_adsoyad.Text + "'in Resmi başarılı olarak kaydedildi.");
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
                gunfarki_ilk.ToString();
            }
            else if (gunfarki_ilk < 0 && gunfarki_son > 0)
            {
                durum = "STAJ YAPIYOR";
                gunfarki_ilk = kalan.Days;
                gunfarki_ilk.ToString();

            }
            else if (gunfarki_son < 0)
            {
                durum = "STAJI BITTI";

            }
            #endregion

            cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            cmd.Parameters.AddWithValue("@egitim_durumu", comboBox_egitimdurumu.Text);
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
            cmd.Parameters.AddWithValue("@arac_plaka", comboBox_aracplaka.Text);
            cmd.Parameters.AddWithValue("@mentör", comboBox_mentor.Text);
            cmd.Parameters.AddWithValue("@sigorta_evrak", comboBox_sigorta.Text);
            cmd.Parameters.AddWithValue("@basvuru_turu", comboBox_basvuruturu.Text);
            cmd.Parameters.AddWithValue("@referans_adı", textBox_r_ad.Text);
            cmd.Parameters.AddWithValue("@referans_adres", textBox_r_adres.Text);
            cmd.Parameters.AddWithValue("@referans_telefon", textBox_r_telefon.Text);
            cmd.Parameters.AddWithValue("@referans_eposta", textBox_r_eposta.Text);
            cmd.Parameters.AddWithValue("@staj_acıklama", textBox_staj_aciklama.Text);
            cmd.Parameters.AddWithValue("@staj_turu", comboBox_stajturu.Text);
            cmd.Parameters.AddWithValue("@staj_icerigi", textBox_stajicerigi.Text);
            cmd.Parameters.AddWithValue("@staj_kalan_sure", gunfarki_ilk);


            cmd.ExecuteNonQuery();
            connection.Close();

            #region COMBOBOX'IN İÇİNE STAJ BİLGİLERİNİ ÇEK
            comboBox_staj.Items.Clear();
            string staj = "SELECT DISTINCT staj_turu FROM stajbilgileri where tc_kimlikno=@tc_kimlikno";
            cmd = new SqlCommand(staj, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_staj.Items.Add(datareader["staj_turu"]);
            }
            datareader.Close();
            connection.Close();
            #endregion

            MessageBox.Show("Staj Bilgileri Kaydedildi !");
        }
        #endregion

          #region Sil Butonları
        //STAJYER SİL BUTONU:28 AGUSTOS PAZARTESİ GÜNCEL
        private void pictureBox_sil_Click(object sender, EventArgs e)
        {
            try
            {
                command.Connection = connection;
                connection.Open();
                string stajyer = "DELETE FROM stajyer where tc_kimlikno=@tc_kimlikno";
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
                string stajbilgileri = "DELETE FROM stajbilgileri where staj_id=@staj_id";
                command = new SqlCommand(stajbilgileri, connection);
                command.Parameters.AddWithValue("@staj_id", label_kod.Text);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Staj Bilgileri Silinmiştir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "İşlem gerçekleşememiştir");
            }


            #region COMBOBOX'IN İÇİNE STAJ BİLGİLERİNİ ÇEK
            comboBox_staj.Items.Clear();
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

        #endregion

          #region Güncelle Butonu

        //STAJYER GÜNCELLEME BUTONU :25 AGUSTOS CUMA GÜNCEL 
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

                command.Parameters.AddWithValue("@ortaokul_adı", comboBox_ortaokul.Text);
                command.Parameters.AddWithValue("@lise_adı", comboBox_lise.Text);
                command.Parameters.AddWithValue("@universite", comboBox_universite.Text);
                

                command.ExecuteNonQuery();
                connection.Close();


                if(label_resimyolu.Text!=string.Empty)
                {
                    Directory.CreateDirectory("O:STAJER_TAKIP\\StajyerGörselleri\\" + textBox_tc.Text);
                    string resimAdi = Path.GetFileName(label_resimyolu.Text);
                    File.Copy(@"" + label_resimyolu.Text, @"" + @"O:STAJER_TAKIP\\StajyerGörselleri\\" + textBox_tc.Text + "\\" + resimAdi);
                    MessageBox.Show(textBox_adsoyad.Text + "'in Resmi başarılı olarak kaydedildi.");
                }
                MessageBox.Show("Stajyer Bilgileri Güncellendi");

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        //STAJ GÜNCELLEME BUTONU :28 AGUSTOS CUMA GÜNCEL !!
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
            cmd.Parameters.AddWithValue("@egitim_durumu", comboBox_egitimdurumu.Text);
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
            cmd.Parameters.AddWithValue("@arac_plaka", comboBox_aracplaka.Text);
            cmd.Parameters.AddWithValue("@mentör", comboBox_mentor.Text);


            cmd.Parameters.AddWithValue("@sigorta_evrak", comboBox_sigorta.Text);
            cmd.Parameters.AddWithValue("@basvuru_turu", comboBox_basvuruturu.Text);
            cmd.Parameters.AddWithValue("@referans_adı", textBox_r_ad.Text);
            cmd.Parameters.AddWithValue("@referans_adres", textBox_r_adres.Text);
            cmd.Parameters.AddWithValue("@referans_telefon", textBox_r_telefon.Text);
            cmd.Parameters.AddWithValue("@referans_eposta", textBox_r_eposta.Text);
            cmd.Parameters.AddWithValue("@staj_acıklama", textBox_staj_aciklama.Text);
            cmd.Parameters.AddWithValue("@staj_turu", comboBox_stajturu.Text);

            cmd.Parameters.AddWithValue("@staj_icerigi", textBox_stajicerigi.Text);
            cmd.Parameters.AddWithValue("@staj_kalan_sure", gunfarki_ilk);


            cmd.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Staj Bilgileri Kaydedildi !");

            #region COMBOBOX'IN İÇİNE STAJ BİLGİLERİNİ ÇEK
            comboBox_staj.Items.Clear();
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

#endregion

        //RESİM SEÇME BUTONU  
        private void pictureBox_resim_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string resimyolu = openFileDialog1.FileName.ToString();
                label_resimyolu.Text = Path.GetFullPath(resimyolu);
                pictureBox_stajyer_resim.ImageLocation = label_resimyolu.Text;
            }

            #region ESKİ KOD
            //try
            //{
            //    OpenFileDialog dialog = new OpenFileDialog();
            //    dialog.Filter = "Resim Dosyaları |*.jpg;*.jpeg;*.png |GIF Dosyaları|*.gif |Tüm Dosyalar |*.*";
            //    dialog.Title = "Select stajyer resim";
            //    if (dialog.ShowDialog() == DialogResult.OK)
            //    {
            //        yol = dialog.FileName;
            //        pictureBox_stajyer_resim.ImageLocation = yol;
            //        label_dosyayolu.Text = yol;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "Resim yüklenemedi");
            //}
#endregion

        }

        //DOSYA SEÇME BUTONU :28 AGUSTOS PAZAR GÜNCEL!!
        private void pictureBox_dosya_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string dosyayolu = openFileDialog1.FileName.ToString();
                label_dosya.Text = Path.GetFullPath(dosyayolu);
                textBox_dosya.Text = Path.GetFullPath(dosyayolu);
            }

        }

        //DOSYA YÜKLEME BUTONU GÜNCEL!!
        private void pictureBox_fileupdate_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory("O:STAJER_TAKIP\\StajyerDosyaları\\" + textBox_tc.Text + "_" + comboBox_stajturu.Text);
            string dosyaAdi = Path.GetFileName(textBox_dosya.Text);
            File.Copy(@"" + label_dosya.Text, @"" + @"O:STAJER_TAKIP\\StajyerDosyaları\\" + textBox_tc.Text + "_" + comboBox_stajturu.Text + "\\" + dosyaAdi);
            MessageBox.Show(textBox_adsoyad.Text + "'in Dökumanı başarılı olarak kaydedildi.");
        }

        //LİSTBOX'TAN KLASÖR AÇ OLAYI: 28 AGUSTOS PAZAR GÜNCEL!!
        private void listBox_dosya_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("O:\\STAJER_TAKIP\\StajyerDosyaları\\" + textBox_tc.Text + "_" + textBox_adsoyad.Text);
        }

        #region VERİTABANINDAN BULMA İŞLEMLERİ
        int i;
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
            SqlDataReader drstajyer = command.ExecuteReader();
            if (drstajyer.Read())
            {
                textBox_tc.Text = drstajyer["tc_kimlikno"].ToString();   //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım.       
                textBox_adsoyad.Text = drstajyer["adı_soyadı"].ToString();
                textBox_baba.Text = drstajyer["baba_adı"].ToString();
                textBox_anne.Text = drstajyer["anne_adı"].ToString();
                textBox_dyeri.Text = drstajyer["dogum_yeri"].ToString();
                dateTimePicker_dtarih.Text = drstajyer["dogum_tarihi"].ToString();
                textBox_uyrugu.Text = drstajyer["uyrugu"].ToString();
                textBox_website.Text = drstajyer["web_site"].ToString();
                comboBox_kangrubu.Text = drstajyer["kan_grubu"].ToString();
                comboBox_cinsiyet.Text = drstajyer["cinsiyet"].ToString();
                textBox_evtel.Text = drstajyer["ev_telefonu"].ToString();
                textBox_ceptel.Text = drstajyer["cep_telefonu"].ToString();
                textBox_adres.Text = drstajyer["ikametgah"].ToString();
                textBox_eposta.Text = drstajyer["e_posta"].ToString();
                textBox_boy.Text = drstajyer["boy"].ToString();
                textBox_agırlık.Text = drstajyer["agırlık"].ToString();
                textBox_ai_adsoyad.Text = drstajyer["acil_adsoyad"].ToString();
                textBox_ai_adres.Text = drstajyer["acil_adres"].ToString();
                textBox_ai_akrabalık.Text = drstajyer["acil_yakınlıgı"].ToString();
                textBox_ai_eposta.Text = drstajyer["acil_eposta"].ToString();
                textBox_ai_telefon.Text = drstajyer["acil_telefon"].ToString();
                comboBox_ortaokul.Text = drstajyer["ortaokul_adı"].ToString();
                comboBox_lise.Text = drstajyer["lise_adı"].ToString();
                comboBox_universite.Text = drstajyer["universite_adı"].ToString();


                pictureBox_stajyer_resim.ImageLocation = label_resimyolu.Text;

            }
            //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
            else
            {
                MessageBox.Show("Kayıtlı Stajyer Bulunamadı");
            }
            drstajyer.Close();
            #endregion

            #region COMBOBOX'IN İÇİNE STAJ BİLGİLERİNİ ÇEK
            comboBox_staj.Items.Clear();
            string staj = "SELECT staj_turu FROM stajbilgileri where tc_kimlikno=@tc_kimlikno";
            cmd = new SqlCommand(staj, connection);
            cmd.Parameters.AddWithValue("@tc_kimlikno", textBox_tcbul.Text);
            datareader = cmd.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_staj.Items.Add(datareader["staj_turu"]);
            }
            datareader.Close();
            connection.Close();
            #endregion

            #region Resmi Göster
            string ResimYolu =@"" + @"O:STAJER_TAKIP\\StajyerGörselleri\\" + textBox_tc.Text + "\\kişiselgörsel.jpg";
            pictureBox_stajyer_resim.ImageLocation = ResimYolu;
            #endregion

            tabControl_bilgigiriş.SelectedTab = tabPage_genel;
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
            dataadapter = new SqlDataAdapter(command);
            SqlDataReader stajkodu = command.ExecuteReader();
            while (stajkodu.Read())
            {

                label_kod.Text = stajkodu["staj_id"].ToString();

            }
            stajkodu.Close();
            connection.Close();
        }

        //STAJ BUL BUTONU GÜNCEL
        private void pictureBox_staj_Click(object sender, EventArgs e)
        {
            textBox_stajyapmadurumu.Visible = true;
            listBox_dosya.Enabled = false;//Form açıldığında iban textBoxı pasif olsun.
            string staj = "SELECT staj_id, tc_kimlikno,egitim_durumu,okul_adı,bolum_adı,sınıf,okul_no,sehir,okul_puanı,okul_acıklama,banka_adı,şube_kodu,hesap_no,iban_no,staj_kabuldurumu,staj_donem,baslangıc_tarihi,bitis_tarihi,staj_yılı,staj_yapmadurumu,staj_suresi,servis_imkanı,arac_plaka,mentör,sigorta_evrak,basvuru_turu,referans_adı,referans_adres,referans_telefon,referans_eposta,staj_acıklama,staj_turu,staj_icerigi, staj_kalan_sure FROM stajbilgileri where tc_kimlikno=@tc_kimlikno and staj_id=@staj_id";
            command = new SqlCommand(staj, connection);
            command.Parameters.AddWithValue("@tc_kimlikno", textBox_tc.Text);
            command.Parameters.AddWithValue("@staj_id", label_kod.Text);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            dataadapter = new SqlDataAdapter(command);
            SqlDataReader drstaj = command.ExecuteReader();
            while (drstaj.Read() == true)
            {
                label_kod.Text = drstaj["staj_id"].ToString();
                textBox_tc.Text = drstaj["tc_kimlikno"].ToString();
                comboBox_kabuldurumu.Text = drstaj["staj_kabuldurumu"].ToString();
                comboBox_stajdonemi.Text = drstaj["staj_donem"].ToString();
                dateTimePicker_baslangıc.Text = drstaj["baslangıc_tarihi"].ToString();
                dateTimePicker_bitis.Text = drstaj["bitis_tarihi"].ToString();
                textBox_stajyapmadurumu.Text = drstaj["staj_yapmadurumu"].ToString();
                textBox_stajsuresi.Text = drstaj["staj_suresi"].ToString();
                comboBox_servis.Text = drstaj["servis_imkanı"].ToString();
                comboBox_aracplaka.Text = drstaj["arac_plaka"].ToString();
                comboBox_mentor.Text = drstaj["mentör"].ToString();
                comboBox_sigorta.Text = drstaj["sigorta_evrak"].ToString();
                comboBox_stajyili.Text = drstaj["staj_yılı"].ToString();
                comboBox_basvuruturu.Text = drstaj["basvuru_turu"].ToString();
                textBox_r_ad.Text = drstaj["referans_adı"].ToString();
                textBox_r_adres.Text = drstaj["referans_adres"].ToString();
                textBox_r_eposta.Text = drstaj["referans_eposta"].ToString();
                textBox_r_telefon.Text = drstaj["referans_telefon"].ToString();
                textBox_staj_aciklama.Text = drstaj["staj_acıklama"].ToString();
                comboBox_egitimdurumu.Text = drstaj["egitim_durumu"].ToString();
                comboBox_okuladı.Text = drstaj["okul_adı"].ToString();
                comboBox_bolumadı.Text = drstaj["bolum_adı"].ToString();
                comboBox_sinif.Text = drstaj["sınıf"].ToString();
                textBox_okulno.Text = drstaj["okul_no"].ToString();
                comboBox_sehir.Text = drstaj["sehir"].ToString();
                textBox_okulpuan.Text = drstaj["okul_puanı"].ToString();
                textBox_okulacıklama.Text = drstaj["okul_acıklama"].ToString();
                textBox_bankaadı.Text = drstaj["banka_adı"].ToString();
                textBox_subekodu.Text = drstaj["şube_kodu"].ToString();
                textBox_hesapno.Text = drstaj["hesap_no"].ToString();
                textBox_iban.Text = drstaj["iban_no"].ToString();
                label_kalansure.Text = drstaj["staj_kalan_sure"].ToString();
                comboBox_stajturu.Text = drstaj["staj_turu"].ToString();
            }
            drstaj.Close();
            connection.Close();

            tabControl_bilgigiriş.SelectedTab = tabPage_okul;
            listBox_dosya.Enabled = true;

            #region DÖKÜMANLARI LİSTBOX'A AKTAR

            listBox_dosya.Items.Clear(); // Listbox'ın içini temizle
            string DosyaYolu = "O:\\STAJER_TAKIP\\StajyerDosyaları\\" + textBox_tc.Text + "_" + comboBox_stajturu.Text;
             if (Directory.Exists(DosyaYolu))
            {
                //GetFiles metodu dosyaları temsil eder. Belirtilen Dizindeki Dosyaları Dizi olarak döndürür
                string[] dosyalar = System.IO.Directory.GetFiles("O:\\STAJER_TAKIP\\StajyerDosyaları\\" + textBox_tc.Text + "_" + comboBox_staj.Text);
                for (int j = 0; j < dosyalar.Length; j++)
                {
                    //klasörler dizisinin i. elemanı listboxa ekle
                    listBox_dosya.Items.Add(dosyalar[j]);
                }
            }
            #endregion
        }
#endregion

        #region ARTI BUTONLARI
        private void button_ortaokul_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cbx = new ComboBoxEKLE();
            cbx.Show();
        }
        private void button_lise_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_universite_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_stajkabuldurumu_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_stajdonemi_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_mentör_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_plaka_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_basvuruturu_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_egitim_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_okul_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_bolum_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_sınıf_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }

        private void button_stajturu_Click(object sender, EventArgs e)
        {
            ComboBoxEKLE cmbx = new ComboBoxEKLE();
            cmbx.Show();
        }
        #endregion

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
                comboBox_aracplaka.Enabled = true;
            }
            else if (comboBox_servis.Text == "YOK")
            {
                comboBox_aracplaka.Enabled = false;
            }

        }

        private void comboBox_egitim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_egitimdurumu.Text == "Lise")
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

        #region ieri tuşları
        private void label44_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 1; //İndex olarak geçişleri sağlayabiliriz
        }

        private void label45_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 2;
            //tabControl_bilgigiriş.SelectedTab = tabPage_staj; //Pages üzerindende aynı şekildegeçişleri sağlayabiliriz. Alternatif gösterim
        }

        private void label54_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 4;
        }

        private void label46_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 3;
        }
#endregion

        #region geri tuşları

        private void label2_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 2;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            tabControl_bilgigiriş.SelectedIndex = 3;
        }
        #endregion

        #endregion

        #region MOUSE HAREKETLERİ
        private void textBox_tcbul_MouseHover(object sender, EventArgs e)
        {
            if (textBox_tcbul.Text == "TC Kimlik No İle Ara")
            {
                textBox_tcbul.Text = String.Empty;
            }


        }

        private void textBox_tcbul_MouseLeave(object sender, EventArgs e)
        {
            if (textBox_tcbul.Text == string.Empty)
            {
                textBox_tcbul.Text = "TC Kimlik No İle Ara";
            }

        }

        private void pictureBox_resim_MouseHover(object sender, EventArgs e)
        {
            pictureBox_resim.Width = 30;
            pictureBox_resim.Height = 30;
        }

        private void pictureBox_resim_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_resim.Width = 25;
            pictureBox_resim.Height = 25;
        }

        private void pictureBox_dosya_MouseHover(object sender, EventArgs e)
        {
            pictureBox_dosya.Width = 30;
            pictureBox_dosya.Height = 30;
        }

        private void pictureBox_dosya_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_dosya.Width = 25;
            pictureBox_dosya.Height = 25;
        }

        private void pictureBox_fileupdate_MouseHover(object sender, EventArgs e)
        {
            pictureBox_dosyayukle.Width = 30;
            pictureBox_dosyayukle.Height = 30;
        }

        private void pictureBox_fileupdate_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_dosyayukle.Width = 25;
            pictureBox_dosyayukle.Height = 25;
        }

        private void pictureBox_bul_MouseHover(object sender, EventArgs e)
        {
            pictureBox_bul.Width = 30;
            pictureBox_bul.Height = 30;
        }

        private void pictureBox_bul_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_bul.Width = 25;
            pictureBox_bul.Height = 25;
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
            pictureBox_stajyer_kaydet.Width = 70;
            pictureBox_stajyer_kaydet.Height = 35;
        }

        private void pictureBox_kaydet_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajyer_kaydet.Width = 60;
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
            pictureBox_güncelle.Width = 30;
            pictureBox_güncelle.Height = 30;
        }

        private void pictureBox_güncelle_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_güncelle.Width = 25;
            pictureBox_güncelle.Height = 25;
        }

        private void pictureBox_sil_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajyer_sil.Width = 30;
            pictureBox_stajyer_sil.Height = 30;
        }

        private void pictureBox_sil_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajyer_sil.Width = 25;
            pictureBox_stajyer_sil.Height = 25;
        }

        private void pictureBox_yenile_Click(object sender, EventArgs e)
        {
            comboBox_aracplaka.Items.Clear();
            comboBox_basvuruturu.Items.Clear();
            comboBox_bolumadı.Items.Clear();
            comboBox_egitimdurumu.Items.Clear();
            comboBox_kabuldurumu.Items.Clear();
            comboBox_sinif.Items.Clear();
            comboBox_lise.Items.Clear();
            comboBox_mentor.Items.Clear();
            comboBox_okuladı.Items.Clear();
            comboBox_ortaokul.Items.Clear();
            comboBox_stajturu.Items.Clear();
            comboBox_stajdonemi.Items.Clear();
            comboBox_universite.Items.Clear();

            #region COMBOBAXIN İÇİNE VERİ ÇEKME
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM ortaokuladı", connection);
            connection.Open();
            datareader = cmd1.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_ortaokul.Items.Add(datareader["id"]);
            }
            datareader.Close();


            SqlCommand cmd2 = new SqlCommand("SELECT * FROM liseadı", connection);

            datareader = cmd2.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_lise.Items.Add(datareader["id"]);
            }
            datareader.Close();

            SqlCommand cmd3 = new SqlCommand("SELECT * FROM universiteadı", connection);

            datareader = cmd3.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_universite.Items.Add(datareader["id"]);
            }
            datareader.Close();

            SqlCommand cmd4 = new SqlCommand("SELECT * FROM stajkabuldurumu", connection);

            datareader = cmd4.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_kabuldurumu.Items.Add(datareader["id"]);
            }
            datareader.Close();

            SqlCommand cmd5 = new SqlCommand("SELECT * FROM stajdonemi", connection);

            datareader = cmd5.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_stajdonemi.Items.Add(datareader["id"]);
            }
            datareader.Close();


            SqlCommand cmd6 = new SqlCommand("SELECT * FROM mentöradı", connection);

            datareader = cmd6.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_mentor.Items.Add(datareader["id"]);
            }
            datareader.Close();


            SqlCommand cmd7 = new SqlCommand("SELECT * FROM aracplaka", connection);

            datareader = cmd7.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_aracplaka.Items.Add(datareader["id"]);
            }
            datareader.Close();


            SqlCommand cmd8 = new SqlCommand("SELECT * FROM basvuruturu", connection);

            datareader = cmd8.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_basvuruturu.Items.Add(datareader["id"]);
            }
            datareader.Close();


            SqlCommand cmd9 = new SqlCommand("SELECT * FROM egitimdurumu", connection);

            datareader = cmd9.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_egitimdurumu.Items.Add(datareader["id"]);
            }
            datareader.Close();
            connection.Close();

            SqlCommand cmd12 = new SqlCommand("SELECT * FROM bolumadı", connection);
            connection.Open();
            datareader = cmd12.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_bolumadı.Items.Add(datareader["id"]);
            }
            datareader.Close();


            SqlCommand cmd13 = new SqlCommand("SELECT * FROM sınıf", connection);

            datareader = cmd13.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_sinif.Items.Add(datareader["id"]);
            }
            datareader.Close();

            SqlCommand cmd14 = new SqlCommand("SELECT * FROM stajturu", connection);
            datareader = cmd14.ExecuteReader();
            while (datareader.Read())
            {
                comboBox_stajturu.Items.Add(datareader["id"]);
            }
            datareader.Close();
            connection.Close();
            #endregion
        }


        private void pictureBox_yenile_MouseHover(object sender, EventArgs e)
        {
            pictureBox_yenile.Width = 30;
            pictureBox_yenile.Height = 30;
        }

        private void pictureBox_yenile_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_yenile.Width = 25;
            pictureBox_yenile.Height = 25;
        }

        private void pictureBox_stajsil_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajsil.Width = 30;
            pictureBox_stajsil.Height = 30;
        }

        private void pictureBox_stajsil_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajsil.Width = 25;
            pictureBox_stajsil.Height = 25;
        }

        private void pictureBox_stajgüncelle_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajgüncelle.Width = 30;
            pictureBox_stajgüncelle.Height = 30;
        }

        private void pictureBox_stajgüncelle_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajgüncelle.Width = 25;
            pictureBox_stajgüncelle.Height = 25;
        }

        private void pictureBox_stajbilgisi_kaydet_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajbilgisi_kaydet.Width = 65;
            pictureBox_stajbilgisi_kaydet.Height = 30;
        }

        private void pictureBox_stajbilgisi_kaydet_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajbilgisi_kaydet.Width = 60;
            pictureBox_stajbilgisi_kaydet.Height = 25;
        }



        private void pictureBox_sıfırla_MouseHover(object sender, EventArgs e)
        {
            pictureBox_sıfırla.Width = 65;
            pictureBox_sıfırla.Height = 30;
        }

        private void pictureBox_sıfırla_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_sıfırla.Width = 60;
            pictureBox_sıfırla.Height = 25;
        }

        private void pictureBox_stajsıfırla_MouseHover(object sender, EventArgs e)
        {
            pictureBox_stajsıfırla.Width = 65;
            pictureBox_stajsıfırla.Height = 30;
        }

        private void pictureBox_stajsıfırla_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_stajsıfırla.Width = 60;
            pictureBox_stajsıfırla.Height = 25;
        }
        #endregion

        private void comboBox_egitimdurumu_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox_egitimdurumu.Text == "LİSE")
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                comboBox_okuladı.Items.Clear();
                SqlCommand cmd11 = new SqlCommand("SELECT id FROM liseadı", connection);
                dataadapter = new SqlDataAdapter(cmd11);
                SqlDataReader dr11 = cmd11.ExecuteReader();
                while (dr11.Read())
                {
                    comboBox_okuladı.Items.Add(dr11["id"].ToString());
                }
                dr11.Close();
                connection.Close();

                panel_bankabilgileri.Enabled = true;
            }
            else if (comboBox_egitimdurumu.Text != "LİSE")
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                comboBox_okuladı.Items.Clear();
                SqlCommand cmd10 = new SqlCommand("SELECT id FROM universiteadı", connection);
                dataadapter = new SqlDataAdapter(cmd10);
                SqlDataReader dr10 = cmd10.ExecuteReader();
                while (dr10.Read())
                {
                    comboBox_okuladı.Items.Add(dr10["id"].ToString());
                }
                dr10.Close();
                connection.Close();
                panel_bankabilgileri.Enabled = false;
            }

        }

        private void comboBox_egitimdurumu_Click(object sender, EventArgs e)
        {
            comboBox_egitimdurumu.Items.Clear();
            connection.Open();
            SqlCommand cmd9 = new SqlCommand("SELECT * FROM egitimdurumu", connection);
            SqlDataReader dr9;
            dr9 = cmd9.ExecuteReader();
            while (dr9.Read())
            {
                comboBox_egitimdurumu.Items.Add(dr9["id"].ToString());
            }
            dr9.Close();
            connection.Close();
        }

        private void comboBox_stajturu_Click(object sender, EventArgs e)
        {
            comboBox_stajturu.Items.Clear();
            connection.Open();
            SqlCommand cmd14 = new SqlCommand("SELECT * FROM stajturu", connection);
            SqlDataReader dr14;
            dr14 = cmd14.ExecuteReader();
            while (dr14.Read())
            {
                comboBox_stajturu.Items.Add(dr14["id"].ToString());
            }
            dr14.Close();
            connection.Close();
        }

        private void comboBox_sehir_Click(object sender, EventArgs e)
        {
            comboBox_sehir.Items.Clear();
            connection.Open();
            SqlCommand cmd15 = new SqlCommand("SELECT isim FROM iller", connection);
            SqlDataReader dr15;
            dr15 = cmd15.ExecuteReader();
            while (dr15.Read())
            {
                comboBox_sehir.Items.Add(dr15["isim"].ToString());
            }
            dr15.Close();
            connection.Close();
        }

        private void comboBox_ortaokul_Click(object sender, EventArgs e)
        {
            comboBox_ortaokul.Items.Clear();
            connection.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM ortaokuladı", connection);
            SqlDataReader dr1;
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox_ortaokul.Items.Add(dr1["id"].ToString());
            }
            dr1.Close();
            connection.Close();
        }

        private void comboBox_lise_Click(object sender, EventArgs e)
        {
            comboBox_lise.Items.Clear();
            connection.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM liseadı", connection);
            SqlDataReader dr2;
            dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox_lise.Items.Add(dr2["id"].ToString());
            }
            dr2.Close();
            connection.Close();
        }

        private void comboBox_universite_Click(object sender, EventArgs e)
        {
            comboBox_universite.Items.Clear();
            connection.Open();
            SqlCommand cmd3 = new SqlCommand("SELECT * FROM universiteadı", connection);
            SqlDataReader dr3;
            dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                comboBox_universite.Items.Add(dr3["id"].ToString());
            }
            dr3.Close();
            connection.Close();
        }

        private void comboBox_kabuldurumu_Click(object sender, EventArgs e)
        {
            comboBox_kabuldurumu.Items.Clear();
            connection.Open();
            SqlCommand cmd4 = new SqlCommand("SELECT * FROM stajkabuldurumu", connection);
            SqlDataReader dr4;
            dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                comboBox_kabuldurumu.Items.Add(dr4["id"].ToString());
            }
            dr4.Close();
            connection.Close();
        }

        private void comboBox_stajdonemi_Click(object sender, EventArgs e)
        {
            comboBox_stajdonemi.Items.Clear();
            connection.Open();
            SqlCommand cmd5 = new SqlCommand("SELECT * FROM stajdonemi", connection);
            SqlDataReader dr5;
            dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                comboBox_stajdonemi.Items.Add(dr5["id"].ToString());
            }
            dr5.Close();
            connection.Close();
        }

        private void comboBox_mentor_Click(object sender, EventArgs e)
        {
            comboBox_mentor.Items.Clear();
            connection.Open();
            SqlCommand cmd6 = new SqlCommand("SELECT * FROM mentöradı", connection);
            SqlDataReader dr6;
            dr6 = cmd6.ExecuteReader();
            while (dr6.Read())
            {
                comboBox_mentor.Items.Add(dr6["id"].ToString());
            }
            dr6.Close();
            connection.Close();
        }

        private void comboBox_aracplaka_Click(object sender, EventArgs e)
        {
            comboBox_aracplaka.Items.Clear();
            connection.Open();
            SqlCommand cmd7 = new SqlCommand("SELECT * FROM aracplaka", connection);
            SqlDataReader dr7;
            dr7 = cmd7.ExecuteReader();
            while (dr7.Read())
            {
                comboBox_aracplaka.Items.Add(dr7["id"].ToString());
            }
            dr7.Close();
            connection.Close();
        }

        private void comboBox_basvuruturu_Click(object sender, EventArgs e)
        {
            comboBox_basvuruturu.Items.Clear();
            connection.Open();
            SqlCommand cmd8 = new SqlCommand("SELECT * FROM basvuruturu", connection);
            SqlDataReader dr8;
            dr8 = cmd8.ExecuteReader();
            while (dr8.Read())
            {
                comboBox_basvuruturu.Items.Add(dr8["id"].ToString());
            }
            dr8.Close();
            connection.Close();
        }

        private void comboBox_okuladı_Click(object sender, EventArgs e)
        {
            if (comboBox_egitimdurumu.Text == "LİSE")
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                comboBox_okuladı.Items.Clear();
                SqlCommand cmd11 = new SqlCommand("SELECT id FROM liseadı", connection);
                dataadapter = new SqlDataAdapter(cmd11);
                SqlDataReader dr11 = cmd11.ExecuteReader();
                while (dr11.Read())
                {
                    comboBox_okuladı.Items.Add(dr11["id"].ToString());
                }
                dr11.Close();
                connection.Close();
            }
            else if (comboBox_egitimdurumu.Text != "LİSE")
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                comboBox_okuladı.Items.Clear();
                SqlCommand cmd10 = new SqlCommand("SELECT id FROM universiteadı", connection);
                dataadapter = new SqlDataAdapter(cmd10);
                SqlDataReader dr10 = cmd10.ExecuteReader();
                while (dr10.Read())
                {
                    comboBox_okuladı.Items.Add(dr10["id"].ToString());
                }
                dr10.Close();
                connection.Close();
            }
        }

        private void comboBox_bolumadı_Click(object sender, EventArgs e)
        {
            comboBox_bolumadı.Items.Clear();
            connection.Open();
            SqlCommand cmd12 = new SqlCommand("SELECT * FROM bolumadı", connection);
            SqlDataReader dr12;
            dr12 = cmd12.ExecuteReader();
            while (dr12.Read())
            {
                comboBox_bolumadı.Items.Add(dr12["id"].ToString());
            }
            dr12.Close();
            connection.Close();
        }

        private void comboBox_sinif_Click(object sender, EventArgs e)
        {
            comboBox_sinif.Items.Clear();
            connection.Open();
            SqlCommand cmd13 = new SqlCommand("SELECT * FROM sınıf", connection);
            SqlDataReader dr13;
            dr13 = cmd13.ExecuteReader();
            while (dr13.Read())
            {
                comboBox_sinif.Items.Add(dr13["id"].ToString());
            }
            dr13.Close();
            connection.Close();
        }

        private void pictureBox_sıfırla_Click(object sender, EventArgs e)
        {
            textBox_tcbul.Clear();
            textBox_tc.Clear();
            textBox_adsoyad.Clear();
            textBox_baba.Clear();
            textBox_anne.Clear();
            textBox_dyeri.Clear();
            dateTimePicker_dtarih.DataBindings.Clear();
            textBox_uyrugu.Clear();
            textBox_website.Clear();
            comboBox_cinsiyet.Text = "";
            comboBox_kangrubu.Text = "";
            comboBox_ortaokul.Text = "";
            comboBox_lise.Text = "";
            comboBox_universite.Text = "";
            textBox_evtel.Clear();
            textBox_ceptel.Clear();
            textBox_adres.Clear();
            textBox_eposta.Clear();
            textBox_boy.Clear();
            textBox_agırlık.Clear();
            textBox_ai_adsoyad.Clear();
            textBox_ai_akrabalık.Clear();
            textBox_ai_adres.Clear();
            textBox_ai_eposta.Clear();
            textBox_ai_telefon.Clear();
            textBox_adres.Clear();
            textBox_agırlık.Clear();
        }

        private void pictureBox_stajsıfırla_Click(object sender, EventArgs e)
        {
            #region  verileri sil
            label_kod.Text = "00";
            textBox_okulpuan.Clear();
            textBox_okulno.Clear();
            comboBox_egitimdurumu.ResetText();
            label_kalansure.Text = "";
            textBox_stajyapmadurumu.Text = "";
            textBox_r_eposta.Clear();
            textBox_r_adres.Clear();
            textBox_r_telefon.Clear();
            textBox_r_ad.Clear();
            textBox_staj_aciklama.Clear();
            comboBox_aracplaka.Text = "";
            comboBox_stajturu.Text = "";
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

            comboBox_lise.Text = "";
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
#endregion
        }

    }
}


