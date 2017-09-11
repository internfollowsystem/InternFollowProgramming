using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace InternFollowProgramming
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }


        private void FrmMail_Load(object sender, EventArgs e)
        {
            textBox_aposta.Text = FrmScreen.gonderilecekveri;
        }

        private void button_mailgonder_Click(object sender, EventArgs e)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.EnableSsl = false;
            client.Host = "mail.ake.com.tr";
            client.Credentials = new NetworkCredential(textBox_gposta.Text, textBox_gsifre.Text);
            MailMessage mm = new MailMessage(textBox_gposta.Text, textBox_aposta.Text);
            mm.Subject = textBox_mkonusu.Text;
            mm.Body = textBox_micerigi.Text;
            client.Send(mm);
            MessageBox.Show("Mailiniz iletilmştir.");
        }
    }
}
