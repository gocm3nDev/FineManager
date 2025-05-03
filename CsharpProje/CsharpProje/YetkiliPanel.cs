using CsharpProje.Business;
using CsharpProje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpProje
{
    public partial class YetkiliPanel: Form
    {
        DatabaseOpsUser opsUser = new DatabaseOpsUser();
        DatabaseOpsLP opsLP = new DatabaseOpsLP();
        DatabaseOpsPenalty opsPenalty = new DatabaseOpsPenalty();

        public YetkiliPanel()
        {
            InitializeComponent();
        }

        private void YetkiliPanel_Load(object sender, EventArgs e) // Panel yüklendiğinde kullanıcılar datagrid'e gelsin
        {
            lblVatandasSayisi.Text = opsUser.CitizenNumb().ToString();
            lblPlakaSayisi.Text = opsLP.LicensePlateNumbers().ToString();
            lblToplamCeza.Text = opsPenalty.PenaltyNumb().ToString();
            lblYetkiliSayisi.Text = opsUser.AuthPersonNumb().ToString();

            dataGridView1.DataSource = opsUser.ListUsers();
        }

        private void materialButton1_Click(object sender, EventArgs e) // Plaka kaydetme
        {
            string licensePlate = txtPlate.Text;
            string tckn = txtTCKN.Text;

            if (opsLP.AddLicensePlate(licensePlate, tckn))
                MessageBox.Show("Plaka kaydı başarılı.");
            else
                MessageBox.Show("Geçerli TC Kimlik Numarasına sahip olan bir vatandaş bulunamadı.\nTekrar deneyin.");
            
            txtPlate.Text = "";
            txtTCKN.Text = "";
        }

        private void switchQueryType_CheckedChanged(object sender, EventArgs e) // Switch metin kontrolü
        {
            if (switchQueryType.Checked)
            {
                switchQueryType.Text = "TCKN";
                lblQueryType.Text = "TCKN";
            }
            else
            {
                switchQueryType.Text = "Plaka";
                lblQueryType.Text = "Plaka";
            }
        }

        private void btnQuery_Click(object sender, EventArgs e) // Sorgu paneli
        {
            if (switchQueryType.Checked) // TCKN ile sorgula
            {
                string tckn = txtQueryInput.Text;

                if (tckn == null || txtQueryInput.Text == null)
                    MessageBox.Show("Lütfen TCKN alanını boş geçmeyin.");
                else
                    dataGridView1.DataSource = opsUser.GetUserByTCKN(tckn);
            }
            else // Plaka ile sorgula
            {
                string licensePlate = txtQueryInput.Text;
                var user = opsLP.GetUserByLP(licensePlate);

                if (user != null)
                    dataGridView1.DataSource = new List<TBL_USERS> { user };
                else
                    MessageBox.Show("Plakaya bağlı kullanıcı bulunamadı");
            }
        }

        private void lblReflesh_Click(object sender, EventArgs e) // Datagrid'i refleshleme butonu
        {
            dataGridView1.DataSource = opsUser.ListUsers();
        }

        private void materialButton2_Click(object sender, EventArgs e) // Ceza yazma işlemleri
        {
            string tckn = txtTCKN_P.Text;
            string licensePlate = txtPlate_P.Text;
            int price = Convert.ToInt32(txtPrice_P.Text);

            if (opsPenalty.AddPenalty(tckn, licensePlate, price))
            {
                MessageBox.Show("Ceza talebi başarıyla oluşturuldu.");
                txtTCKN_P.Text = "";
                txtPrice_P.Text = "";
                txtPlate_P.Text = "";
            }
            else
            {
                MessageBox.Show("Ceza talebi oluşturulurken bir hata meydana geldi.");
                txtTCKN_P.Text = "";
                txtPrice_P.Text = "";
                txtPlate_P.Text = "";
            }
        }
    }
}
