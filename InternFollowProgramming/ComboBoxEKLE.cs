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

namespace InternFollowProgramming
{
    public partial class ComboBoxEKLE : Form
    {
        #region baglantımız
        //static string conString = "Server=DESKTOP-PBAHQL4;Initial Catalog=INTERN;user id=sa;password=20fbgsbjk07";
        static string conString = "Data Source=10.0.0.51;Initial Catalog=INTERN;user id=sa;password=20fcab9e";
        SqlConnection connection = new SqlConnection(conString);
        SqlCommand command = new SqlCommand();
        SqlDataReader datareader;
        SqlCommand cmd = new SqlCommand();
        #endregion

        public ComboBoxEKLE()
        {
            InitializeComponent();
        }


        private void comboBox_cmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_cmbx.Items.Clear();
            command.Connection = connection;
            connection.Open();

            #region COMBOBOX'A VERİ ÇEKME
            string cmbxveri = "SELECT * FROM " + comboBox_cmbx.SelectedItem + "";
                command = new SqlCommand(cmbxveri, connection);

                datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    listBox_cmbx.Items.Add(datareader["id"].ToString());
                }
                datareader.Close();
            #endregion
            connection.Close();

			if(comboBox_cmbx.Text=="stajturu")
			{
				MessageBox.Show("STAJ TÜRÜ VERİSİ GİRERKEN VERİ KAYBI OLMAMASI İÇİN ŞUNLARA DİKKAT EDİNİZ;\n 1.Türkçe Karakter giriniz! (BİLGİİŞLEM yerine BILGIISLEM) \n 2.Boşluk Bırakmadan yazınız!(BİLGİ İŞLEM YERİNE BİLGİİŞLEM)  \n Dikkatiniz için teşekkür ederiz");
			}
        }

        private void button_ekle_Click(object sender, EventArgs e)
        {
            if(comboBox_cmbx.Text=="stajturu")
            {
                listBox_cmbx.Items.Add(textBox_cmbxveri.Text);
                command.Connection = connection;
                connection.Open();
                string cmbxveriekle = "INSERT INTO " + comboBox_cmbx.Text + " (id) VALUES (@id)";
                command = new SqlCommand(cmbxveriekle, connection);
                command.Parameters.AddWithValue("@id", textBox_cmbxveri.Text);
                command.ExecuteNonQuery();
                cmd = new SqlCommand("CREATE TABLE " + textBox_cmbxveri.Text + "(id nvarchar(max))", connection);
                cmd.ExecuteNonQuery();


                MessageBox.Show("VERİNİZ KAYDEDİLDİ");
            }
            else if(comboBox_cmbx.Text!= "stajturu")
            {
                listBox_cmbx.Items.Add(textBox_cmbxveri.Text);
                command.Connection = connection;
                connection.Open();
                string cmbxveriekle = "INSERT INTO " + comboBox_cmbx.Text + " (id) VALUES (@id)";
                command = new SqlCommand(cmbxveriekle, connection);
                command.Parameters.AddWithValue("@id", textBox_cmbxveri.Text);
                command.ExecuteNonQuery();

                MessageBox.Show("VERİNİZ KAYDEDİLDİ");
            }
            

            listBox_cmbx.Items.Clear();
            #region COMBOBOX'A VERİ ÇEKME
            string cmbxveri = "SELECT * FROM " + comboBox_cmbx.SelectedItem + "";
            command = new SqlCommand(cmbxveri, connection);

            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                listBox_cmbx.Items.Add(datareader["id"]);
            }
            datareader.Close();
            #endregion
            connection.Close();
            textBox_cmbxveri.Clear();
        }

        string secili;
        private void listBox_cmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_cmbxveri.Text = listBox_cmbx.SelectedItem.ToString();
            secili = listBox_cmbx.SelectedItem.ToString();
        }

        
        private void SilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            command.Connection = connection;
            connection.Open();
            string cmbxverisil = "DELETE FROM " + comboBox_cmbx.Text + " where id=@id";
            command = new SqlCommand(cmbxverisil, connection);

            command.Parameters.AddWithValue("@id", textBox_cmbxveri.Text);
            command.ExecuteNonQuery();

            MessageBox.Show("VERİNİZ SİLİNMİŞTİR");

            listBox_cmbx.Items.Clear();
            #region COMBOBOX'A VERİ ÇEKME
            string cmbxveri = "SELECT * FROM " + comboBox_cmbx.SelectedItem + "";
            command = new SqlCommand(cmbxveri, connection);

            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                listBox_cmbx.Items.Add(datareader["id"]);
            }
            datareader.Close();
            #endregion

            connection.Close();
        }
        
        private void button_cıkıs_Click(object sender, EventArgs e)
        {
           
            Application.Exit();
           
        }

        private void GüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            command.Connection = connection;
            connection.Open();
            string cmbxverisil = " UPDATE " + comboBox_cmbx.Text + "  SET id=@id where id=@idd";
            command = new SqlCommand(cmbxverisil, connection);
            secili = listBox_cmbx.SelectedItem.ToString();
            command.Parameters.AddWithValue("@id", textBox_cmbxveri.Text);
            command.Parameters.AddWithValue("@idd", secili);
            command.ExecuteNonQuery();

            MessageBox.Show("VERİNİZ GÜNCELLENMİŞTİR");

            listBox_cmbx.Items.Clear();
            #region COMBOBOX'A VERİ ÇEKME
            string cmbxveri = "SELECT * FROM " + comboBox_cmbx.SelectedItem + "";
            command = new SqlCommand(cmbxveri, connection);

            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                listBox_cmbx.Items.Add(datareader["id"]);
            }
            datareader.Close();
            #endregion

            connection.Close();
        }
    }
}
