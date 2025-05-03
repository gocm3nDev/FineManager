using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsharpProje.Business;

namespace CsharpProje
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Kaydol kaydol = new Kaydol();
            kaydol.Show();
        }

        private void materialButton2_Click(object sender, EventArgs e) // Giriş kontrolcüsü
        {
            DatabaseOpsUser user = new DatabaseOpsUser();

            string name = txtName.Text;
            string surname = txtSurname.Text;
            string tckn = txtTCKN.Text;

            if (user.QueryToUser(tckn, name, surname) && !user.UserAuth(tckn)) // Vatandaş girişi, .txt dosya kullanım alanı
            {
                MessageBox.Show("Giriş başarılı. Anasayfaya yönlendiriliyorsunuz");

                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string filePath = Path.Combine(projectDirectory, "tempData.txt");

                string data = name + Environment.NewLine + surname + Environment.NewLine + tckn + Environment.NewLine;

                File.WriteAllText(filePath, data);

                Anasayfa anasayfa = new Anasayfa();
                anasayfa.Show();
            }
            else if (user.QueryToUser(tckn, name, surname) && user.UserAuth(tckn)) //Yetkili girişi
            {
                MessageBox.Show("Yetkili girişi başarılı. Panele yönlendiriliyorsunuz.");

                YetkiliPanel yetkiliPanel = new YetkiliPanel();
                yetkiliPanel.Show();
            }
            else
            {
                MessageBox.Show("Giriş başarısız. Giriş bilgilerinizi tekrar gözden geçirin.");
                txtName.Text = "";
                txtSurname.Text = "";
                txtTCKN.Text = "";
            }
        }
    }
}
