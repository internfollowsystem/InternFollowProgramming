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
    public partial class FrmInternInformation : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e");
        SqlCommand cmd = new SqlCommand();
        static string conString = "Server=10.0.0.51;Database=INTERN;Uid=sa;Password=20fcab9e;";



        Connect i_add = new Connect();


        Intern yenikisi = new Intern();
        Intern eskikisi = new Intern();

        byte[] byteResim = { 0x20 };

        String yol = "c:\\";
        String klasör = "Stajer Takip";
        public FrmInternInformation()
        {
            InitializeComponent();
            bttn_guncelle.Enabled = false;
            bttn_kayitsil.Enabled = false;
        }

        private void FrmInternInformation_Load(object sender, EventArgs e)
        {
            cmd.CommandText = "Select* From ogrenci";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbx_adsoyad.Items.Add(dr["ad_soyad"]);
            }
            con.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string tc_no;
            string ad_soyad;
            string tckimlik;
            tckimlik = textBox1.Text;
            ad_soyad = textBox2.Text;

            if (tckimlik.Length == 0)
            {
                MessageBox.Show("Lütfen Tc Kimlik No alanını doldurunuz !");
            }
            if (ad_soyad.Length == 0)
            {
                MessageBox.Show("Lütfen Ad Soyad alanını doldurunuz !");
            }

            else
            {
                if (tckimlik.Length != 11 && textBox1.Text != "")
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
                            tc_no = textBox1.Text;
                            ad_soyad = textBox2.Text;

                            SqlConnection baglanti = new SqlConnection(conString);
                            if (baglanti.State == ConnectionState.Closed)
                            {
                                String query = "Insert Into ogrenci(tc_kimlikno,ad_soyad,baba_adı,anne_adı,d_yeri,d_tarih,uyrugu,cinsiyet,ev_tel,cep_tel,adres,web_adres,boy,agırlık,kan_grubu,okul_turu,okul_adı,okul_sehir,bolum_adı,sınıf,okul_no,okul_puan,sigorta_evrak_durumu,acil_adsoyad,acil_adres,acil_akrabalık_derecesi,acil_telefon_no,acil_e_posta,staj_kabul_durumu,staj_basvuru_turu,staj_yılı,staj_donem,staj_konusu,staj_baslama_tarihi,staj_bitis_tarihi,staj_süresi,staj_bas_kalan_sure,staj_durumu,staj_konuları,e_posta)Values (@tc_kimlikno,@ad_soyad,@baba_adı,@anne_adı,@d_yeri,@d_tarih,@uyrugu,@cinsiyet,@ev_tel,@cep_tel,@adres,@web_adres,@boy,@agırlık,@kan_grubu,@okul_turu,@okul_adı,@okul_sehir,@bolum_adı,@sınıf,@okul_no,@okul_puan,@sigorta_evrak_durumu,@acil_adsoyad,@acil_adres,@acil_akrabalık_derecesi,@acil_telefon_no,@acil_e_posta,@staj_kabul_durumu,@staj_basvuru_turu,@staj_yılı,@staj_donem,@staj_konusu,@staj_baslama_tarihi,@staj_bitis_tarihi,@staj_süresi,@staj_bas_kalan_sure,@staj_durumu,@staj_konuları,@e_posta)";
                                baglanti.Open();
                                cmd = new SqlCommand(query, baglanti);

                                cmd.Parameters.AddWithValue("@tc_kimlikno", textBox1.Text);
                                cmd.Parameters.AddWithValue("@ad_soyad", textBox2.Text);
                                cmd.Parameters.AddWithValue("@baba_adı", textBox3.Text);
                                cmd.Parameters.AddWithValue("@anne_adı", textBox4.Text);
                                cmd.Parameters.AddWithValue("@d_yeri", textBox5.Text);
                                cmd.Parameters.AddWithValue("@d_tarih", dateTimePicker1.Text);
                                cmd.Parameters.AddWithValue("@uyrugu", textBox7.Text);
                                cmd.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
                                cmd.Parameters.AddWithValue("@ev_tel", textBox8.Text);
                                cmd.Parameters.AddWithValue("@cep_tel", textBox6.Text);
                                cmd.Parameters.AddWithValue("@adres", textBox9.Text);
                                cmd.Parameters.AddWithValue("@e_posta", textBox10.Text);

                                cmd.Parameters.AddWithValue("@web_adres", textBox11.Text);
                                cmd.Parameters.AddWithValue("@boy", textBox12.Text);
                                cmd.Parameters.AddWithValue("@agırlık", textBox13.Text);
                                cmd.Parameters.AddWithValue("@kan_grubu", comboBox2.Text);
                                cmd.Parameters.AddWithValue("@okul_turu", comboBox3.Text);
                                cmd.Parameters.AddWithValue("@okul_adı", textBox20.Text);
                                cmd.Parameters.AddWithValue("@okul_sehir", textBox21.Text);
                                cmd.Parameters.AddWithValue("@bolum_adı", textBox22.Text);
                                cmd.Parameters.AddWithValue("@sınıf", comboBox4.Text);
                                cmd.Parameters.AddWithValue("@okul_no", textBox23.Text);
                                cmd.Parameters.AddWithValue("@okul_puan", textBox24.Text);
                                cmd.Parameters.AddWithValue("@sigorta_evrak_durumu", comboBox8.Text);
                                cmd.Parameters.AddWithValue("@acil_adsoyad", textBox14.Text);
                                cmd.Parameters.AddWithValue("@acil_adres", textBox15.Text);
                                cmd.Parameters.AddWithValue("@acil_akrabalık_derecesi", textBox16.Text);
                                cmd.Parameters.AddWithValue("@acil_telefon_no", textBox17.Text);
                                cmd.Parameters.AddWithValue("@acil_e_posta", textBox18.Text);
                                cmd.Parameters.AddWithValue("@staj_kabul_durumu", comboBox5.Text);
                                cmd.Parameters.AddWithValue("@staj_basvuru_turu", textBox19.Text);
                                cmd.Parameters.AddWithValue("@staj_yılı", comboBox6.Text);
                                cmd.Parameters.AddWithValue("@staj_donem", comboBox7.Text);
                                cmd.Parameters.AddWithValue("@staj_konusu", textBox25.Text);
                                cmd.Parameters.AddWithValue("@staj_baslama_tarihi", dateTimePicker2.Text);
                                cmd.Parameters.AddWithValue("@staj_bitis_tarihi", dateTimePicker3.Text);
                                cmd.Parameters.AddWithValue("@staj_süresi", textBox26.Text);
                                cmd.Parameters.AddWithValue("@staj_bas_kalan_sure", textBox27.Text);
                                cmd.Parameters.AddWithValue("@staj_durumu", textBox28.Text);
                                cmd.Parameters.AddWithValue("@staj_konuları", textBox29.Text);

                                cmd.ExecuteNonQuery();
                                baglanti.Close();

                                textBox1.Clear();
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                                textBox5.Clear();
                                textBox6.Clear();
                                textBox7.Clear();
                                textBox8.Clear();
                                textBox9.Clear();
                                textBox10.Clear();
                                textBox11.Clear();
                                textBox12.Clear();
                                textBox13.Clear();
                                textBox14.Clear();
                                textBox15.Clear();
                                textBox16.Clear();
                                textBox17.Clear();
                                textBox18.Clear();
                                textBox19.Clear();
                                textBox20.Clear();
                                textBox21.Clear();
                                textBox22.Clear();
                                textBox23.Clear();
                                textBox24.Clear();
                                textBox25.Clear();
                                textBox26.Clear();
                                textBox27.Clear();
                                textBox28.Clear();
                                textBox29.Clear();

                                MessageBox.Show("Kayıt Başarılı");

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

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            string secmeSorgusu = "SELECT * from ogrenci where tc_kimlikno=@tc_kimlikno";

            SqlCommand secmeKomutu = new SqlCommand(secmeSorgusu, con);
            secmeKomutu.Parameters.AddWithValue("@tc_kimlikno", txtBxASBul.Text);

            SqlDataAdapter da = new SqlDataAdapter(secmeKomutu);
            SqlDataReader dr = secmeKomutu.ExecuteReader();

            if (dr.Read())
            {
                string isim = dr["ad_soyad"].ToString();
                dr.Close();


                DialogResult durum = MessageBox.Show(isim + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);

                if (DialogResult.Yes == durum)
                {
                    string silmeSorgusu = "DELETE from ogrenci where tc_kimlikno=@tc_kimlikno";

                    SqlCommand silKomutu = new SqlCommand(silmeSorgusu, con);
                    silKomutu.Parameters.AddWithValue("@tc_kimlikno", txtBxASBul.Text);
                    silKomutu.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Silindi...");
                    txtBxASBul.Clear();
                    con.Close();

                }
            }
            else
                MessageBox.Show("Stajyer Bulunamadı.");
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string kayit = "update HastaBilgileri set tc_kimlikno=@tc_kimlikno,ad_soyad=@ad_soyad,baba_adı=@baba_adı,anne_adı=@anne_adı,d_yeri=@d_yeri,d_tarih=@d_tarih,uyrugu=@uyrugu,cinsiyet=@cinsiyet,ev_tel=@ev_tel,cep_tel=@cep_tel,adres=@adres,web_adres=@web_adres,boy=@boy,agırlık=@agırlık,kan_grubu=@kan_grubu,okul_turu=@okul_turu,okul_adı=@okul_adı,okul_sehir=@okul_sehir,bolum_adı=@bolum_adı,sınıf=@sınıf,okul_no=@okul_no,okul_puan=@okul_puan,sigorta_evrak_durumu=@sigorta_evrak_durumu,acil_adsoyad=@acil_adsoyad,acil_adres=@acil_adres,acil_akrabalık_derecesi=@acil_akrabalık_derecesi,acil_telefon_no=@acil_telefon_no,acil_e_posta=@acil_e_posta,staj_kabul_durumu=@staj_kabul_durumu,staj_basvuru_turu=@staj_basvuru_turu,staj_yılı=@sıtaj_yılı,staj_donem=@staj_donem,staj_konusu=@staj_konusu,staj_baslama_tarihi=@staj_baslama_tarihi,staj_bitis_tarihi=@staj_bitis_tarihi,staj_süresi=@staj_süresi,staj_bas_kalan_sure=@staj_bas_kalan_sure,staj_durumu=@staj_durumu,staj_konuları=@staj_konuları,e_posta=@e_posta where tc_kimlikno=@tc_kimlikno";
                // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
                SqlCommand komut = new SqlCommand(kayit, con);

                cmd.Parameters.AddWithValue("@tc_kimlikno", textBox1.Text);
                cmd.Parameters.AddWithValue("@ad_soyad", textBox2.Text);
                cmd.Parameters.AddWithValue("@baba_adı", textBox3.Text);
                cmd.Parameters.AddWithValue("@anne_adı", textBox4.Text);
                cmd.Parameters.AddWithValue("@d_yeri", textBox5.Text);
                cmd.Parameters.AddWithValue("@d_tarih", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@uyrugu", textBox7.Text);
                cmd.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
                cmd.Parameters.AddWithValue("@ev_tel", textBox8.Text);
                cmd.Parameters.AddWithValue("@cep_tel", textBox6.Text);
                cmd.Parameters.AddWithValue("@adres", textBox9.Text);
                cmd.Parameters.AddWithValue("@e_posta", textBox10.Text);

                cmd.Parameters.AddWithValue("@web_adres", textBox11.Text);
                cmd.Parameters.AddWithValue("@boy", textBox12.Text);
                cmd.Parameters.AddWithValue("@agırlık", textBox13.Text);
                cmd.Parameters.AddWithValue("@kan_grubu", comboBox2.Text);
                cmd.Parameters.AddWithValue("@okul_turu", comboBox3.Text);
                cmd.Parameters.AddWithValue("@okul_adı", textBox20.Text);
                cmd.Parameters.AddWithValue("@okul_sehir", textBox21.Text);
                cmd.Parameters.AddWithValue("@bolum_adı", textBox22.Text);
                cmd.Parameters.AddWithValue("@sınıf", comboBox4.Text);
                cmd.Parameters.AddWithValue("@okul_no", textBox23.Text);
                cmd.Parameters.AddWithValue("@okul_puan", textBox24.Text);
                cmd.Parameters.AddWithValue("@sigorta_evrak_durumu", comboBox8.Text);
                cmd.Parameters.AddWithValue("@acil_adsoyad", textBox14.Text);
                cmd.Parameters.AddWithValue("@acil_adres", textBox15.Text);
                cmd.Parameters.AddWithValue("@acil_akrabalık_derecesi", textBox16.Text);
                cmd.Parameters.AddWithValue("@acil_telefon_no", textBox17.Text);
                cmd.Parameters.AddWithValue("@acil_e_posta", textBox18.Text);
                cmd.Parameters.AddWithValue("@staj_kabul_durumu", comboBox5.Text);
                cmd.Parameters.AddWithValue("@staj_basvuru_turu", textBox19.Text);
                cmd.Parameters.AddWithValue("@staj_yılı", comboBox6.Text);
                cmd.Parameters.AddWithValue("@staj_donem", comboBox7.Text);
                cmd.Parameters.AddWithValue("@staj_konusu", textBox25.Text);
                cmd.Parameters.AddWithValue("@staj_baslama_tarihi", dateTimePicker2.Text);
                cmd.Parameters.AddWithValue("@staj_bitis_tarihi", dateTimePicker3.Text);
                cmd.Parameters.AddWithValue("@staj_süresi", textBox26.Text);
                cmd.Parameters.AddWithValue("@staj_bas_kalan_sure", textBox27.Text);
                cmd.Parameters.AddWithValue("@staj_durumu", textBox28.Text);
                cmd.Parameters.AddWithValue("@staj_konuları", textBox29.Text);





                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.


                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Stajyer Bilgileri Güncellendi");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                textBox5.Clear();
                textBox6.Clear();
                textBox8.Clear();
                textBox10.Clear();
                textBox11.Clear();
                textBox12.Clear();

            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            bttn_guncelle.Enabled = true;
            bttn_kayitsil.Enabled = true;

            con.Open();
            cmd.Connection = con;
            string secmeSorgusu = "SELECT * from ogrenci where tc_kimlikno=@tc_kimlikno";
            SqlCommand secmeKomutu = new SqlCommand(secmeSorgusu, con);
            secmeKomutu.Parameters.AddWithValue("@tc_kimlikno", txtBxASBul.Text);
            SqlDataAdapter da = new SqlDataAdapter(secmeKomutu);

            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox1.Text = dr["tc_kimlikno"].ToString();   //Datareader ile okunan müşteri tc_kimlino ile isim değişkenine atadım.       
                textBox2.Text = dr["ad_soyad"].ToString();
                textBox3.Text = dr["baba_adı"].ToString();
                textBox4.Text = dr["anne_adı"].ToString();
                textBox5.Text = dr["d_yeri"].ToString();
                textBox6.Text = dr["cep_tel"].ToString();
                textBox7.Text = dr["uyrugu"].ToString();
                textBox8.Text = dr["ev_tel"].ToString();
                textBox9.Text = dr["adres"].ToString();
                textBox10.Text = dr["e_posta"].ToString();
                textBox11.Text = dr["web_adres"].ToString();
                textBox12.Text = dr["boy"].ToString();
                textBox13.Text = dr["agırlık"].ToString();
                textBox14.Text = dr["acil_adsoyad"].ToString();
                textBox15.Text = dr["acil_adres"].ToString();
                textBox16.Text = dr["acil_akrabalık_derecesi"].ToString();
                textBox17.Text = dr["acil_telefon_no"].ToString();
                textBox18.Text = dr["acil_e_posta"].ToString();
                textBox19.Text = dr["staj_basvuru_turu"].ToString();
                textBox20.Text = dr["okul_adı"].ToString();
                textBox21.Text = dr["okul_sehir"].ToString();
                textBox22.Text = dr["bolum_adı"].ToString();
                textBox23.Text = dr["okul_no"].ToString();
                textBox24.Text = dr["okul_puan"].ToString();
                textBox25.Text = dr["staj_konusu"].ToString();
                textBox26.Text = dr["staj_süresi"].ToString();
                textBox27.Text = dr["staj_bas_kalan_sure"].ToString();
                textBox28.Text = dr["staj_durumu"].ToString();
                textBox29.Text = dr["staj_konuları"].ToString();
                comboBox1.Text = dr["cinsiyet"].ToString();
                comboBox2.Text = dr["kan_grubu"].ToString();
                comboBox3.Text = dr["okul_turu"].ToString();
                comboBox4.Text = dr["sınıf"].ToString();
                comboBox5.Text = dr["staj_kabul_durumu"].ToString();
                comboBox6.Text = dr["staj_yılı"].ToString();
                comboBox7.Text = dr["staj_donem"].ToString();
                dateTimePicker1.Text = dr["d_tarih"].ToString();
                dateTimePicker2.Value = Convert.ToDateTime(dr["staj_baslama_tarihi"].ToString());
                dateTimePicker3.Value = Convert.ToDateTime(dr["staj_bitis_tarihi"].ToString());

                dr.Close(); //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
            }
            else
            {
                MessageBox.Show("Kayıt Bulunamadı");
            }
            con.Dispose();
            con.Close();
        }

       
    }
}
