using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace InternFollowProgramming
{
    class connect
    {
        SqlConnection con;
        SqlCommand cmd;


        public void Connect() //Kurucu metot
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection("Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e");
            cmd = new SqlCommand();
            cmd.Connection = con;

        }

        public List<Intern> Listele()
        {

            try
            {
                List<Intern> ogrenciListesi = new List<Intern>();
                cmd.CommandText = "Select *From intern";
                cmd.CommandText = "Selecet *From IntershipInformation";
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Intern i = new Intern();
                    //Kişiser Bilgiler
                    i.tcNo = Convert.ToInt32(reader[0].ToString());
                    i.AdSoyad = reader[1].ToString();
                    i.BabaAd = reader[2].ToString();
                    i.AnneAd = reader[3].ToString();
                    i.DogumYeri = reader[4].ToString();
                    i.DogumTarihi = reader[5].ToString();
                    i.Uyrugu = reader[6].ToString();
                    i.Cinsiyet = reader[7].ToString();
                    i.EvTelefonu = reader[8].ToString();
                    i.CepTelefonu = reader[9].ToString();
                    i.Ikametgah = reader[10].ToString();
                    i.EPosta = reader[11].ToString();
                    i.WebAdress = reader[12].ToString();
                    i.Boy = reader[13].ToString();
                    i.Agirlik = reader[14].ToString();
                    i.KanGrubu = reader[15].ToString();
                    // Acil Durumlar İçin irtibat Bilgileri
                    i.acl_AdSoyad = reader[16].ToString();
                    i.acl_Ikametgah = reader[17].ToString();
                    i.acl_Akrabalik = reader[18].ToString();
                    i.acl_Telefon = reader[19].ToString();
                    i.acl_EPosta = reader[20].ToString();
                    i.resim = reader[40].ToString();
                    //Öğrencinin Okul Bilgileri
                    i.OkulTuru = reader[21].ToString();
                    i.OkulAdi = reader[22].ToString();
                    i.Sehir = reader[23].ToString();
                    i.BolumAdi = reader[24].ToString();
                    i.Sinif = reader[25].ToString();
                    i.OkulNo = reader[26].ToString();
                    i.OkulPuani = reader[27].ToString();
                    i.Sigorta = reader[28].ToString();
                    //Staj Detay Bilgileri
                    i.StjKabulDurum = reader[29].ToString();
                    i.StjBasvuruTuru = reader[30].ToString();
                    i.StjYili = reader[31].ToString();
                    i.StjDonemi = reader[32].ToString();
                    i.StjKonusu = reader[33].ToString();
                    i.BslmSuresi = reader[34].ToString();
                    i.BtsSuresi = reader[35].ToString();
                    i.StjSuresi = reader[36].ToString();
                    i.KlnSuresi = reader[37].ToString();
                    i.StjDurumu = reader[38].ToString();
                    i.StjIcerigi = reader[39].ToString();


                    ogrenciListesi.Add(i);
                }

                return ogrenciListesi;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public void Add(Intern i)
        {
            try
            {
                cmd.CommandText = "Insert Into intern(tc_kimlikno,ad_soyad,baba_adı,anne_adı,d_yeri,d_tarih,uyrugu,cinsiyet,ev_tel,cep_tel,adres,e_posta,web_adres,boy,agırlık,kan_grubu,okul_turu,okul_adı,okul_sehir,bolum_adı,sınıf,okul_no,okul_puan,sigorta_evrak_durumu,acil_adsoyad,acil_adres,acil_akrabalık_derecesi,acil_telefon_no,acil_e_posta) values('" + i.tcNo + "','" + i.AdSoyad + "','" + i.BabaAd + "','" + i.AnneAd + "','" + i.DogumYeri + "','" + i.DogumTarihi + "','" + i.Uyrugu + "','" + i.Cinsiyet + "','" + i.EvTelefonu + "','" + i.CepTelefonu + "','" + i.Ikametgah + "','" + i.EPosta + "','" + i.WebAdress + "','" + i.Boy + "','" + i.Agirlik + "','" + i.KanGrubu + "','" + i.OkulTuru + "," + i.OkulAdi + "','" + i.Sehir + "','" + i.BolumAdi + "','" + i.Sinif + "','" + i.OkulNo + "','" + i.OkulPuani + "','" + i.Sigorta + "','" + i.acl_AdSoyad + "','" + i.acl_Ikametgah + "','" + i.acl_Akrabalik + "','" + i.acl_Telefon + "','" + i.acl_EPosta + "')";
                cmd.CommandText = "Insert Into IntershipInformation(staj_kabul_durumu,staj_basvuru_turu,staj_yılı,staj_donem,staj_konusu,staj_baslama_tarihi,staj_bitis_tarihi,staj_süresi,staj_bas_kalan_sure,staj_durumu,staj_konuları) values('" + i.StjKabulDurum + "','" + i.StjBasvuruTuru + "','" + i.StjYili + "','" + i.StjDonemi + "','" + i.StjKonusu + "','" + i.BslmSuresi + "','" + i.BtsSuresi + "','" + i.StjSuresi + "','" + i.KlnSuresi + "','" + i.StjDurumu + "','" + i.StjIcerigi + "')";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception hata)
            {
                System.Windows.Forms.MessageBox.Show(hata.Message);


            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public void Guncelle(Intern eskiKisi, Intern yeniKisi)
        {

            try
            {
                cmd.CommandText = "Update intern SET ad_soyad='" + yeniKisi.AdSoyad + "',baba_adı='" + yeniKisi.BabaAd + "',anne_adı='" + yeniKisi.AnneAd + "',d_yeri='" + yeniKisi.DogumYeri + "',d_tarih='" + yeniKisi.DogumTarihi + "',uyrugu='" + yeniKisi.Uyrugu + "',cinsiyet='" + yeniKisi.Cinsiyet + "',ev_tel='" + yeniKisi.EvTelefonu + "',cep_tel='" + yeniKisi.CepTelefonu + "',adres='" + yeniKisi.Ikametgah + "',e_posta'=" + yeniKisi.EPosta + "',web_adres='" + yeniKisi.WebAdress + "',boy='" + yeniKisi.Boy + "',agırlık='" + yeniKisi.Agirlik + "',kan_grubu='" + yeniKisi.KanGrubu + "',okul_turu='" + yeniKisi.OkulTuru + "',okul_adı='" + yeniKisi.OkulAdi + "',okul_sehir='" + yeniKisi.Sehir + "',bolum_adı='" + yeniKisi.BolumAdi + "',sınıf='" + yeniKisi.Sinif + "',okul_no'" + yeniKisi.OkulNo + "',okul_puan='" + yeniKisi.OkulPuani + "',sigorta_evrak_durumu='" + yeniKisi.Sigorta + "',acil_adsoyad='" + yeniKisi.acl_AdSoyad + "',acil_adres='" + yeniKisi.acl_Ikametgah + "',acil_akabalık_derecesi='" + yeniKisi.acl_Akrabalik + "',acil_telefon_no='" + yeniKisi.acl_Telefon + "',acil_e_posta='" + yeniKisi.acl_EPosta + "' Where tc_kimlikno='" + eskiKisi.tcNo + "'";
                cmd.CommandText = "Update IntershipInformation SET staj_kabul_durumu='" + yeniKisi.StjKabulDurum + "',staj_basvuru_turu='" + yeniKisi.StjBasvuruTuru + "',staj_yılı='" + yeniKisi.StjYili + "',staj_donem='" + yeniKisi + "',staj_konusu='" + yeniKisi.StjKonusu + "',staj_baslama_tarihi='" + yeniKisi.BslmSuresi + "',staj_bitis_tarihi='" + yeniKisi.BtsSuresi + "',staj_süresi='" + yeniKisi.StjSuresi + "',staj_bas_kalan_sure='" + yeniKisi.KlnSuresi + "',staj_durumu='" + yeniKisi.StjDurumu + "',staj_konuları='" + yeniKisi.StjIcerigi + "' Where tc_kimlikno='" + eskiKisi.tcNo + "'";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public void Sil(Intern i)
        {
            try
            {
                cmd.CommandText = "Delete From intern Where tc_kimlikno='" + i.tcNo + "'";
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
    }
}
