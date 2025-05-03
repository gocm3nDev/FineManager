using CsharpProje.Business;
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

namespace CsharpProje
{
    public partial class Anasayfa : Form
    {
        DatabaseOpsPenalty opsPenalty = new DatabaseOpsPenalty();

        public Anasayfa()
        {
            InitializeComponent();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, "tempData.txt");

            if (File.Exists(filePath))
            {
                string[] rows = File.ReadAllLines(filePath);

                if (rows.Length >= 3)
                {
                    lblNameSurname.Text = rows[0] + " " + rows[1];
                    lblTCNO.Text = rows[2];
                }
                else
                {
                    MessageBox.Show("Dosyada yeterli veri yok.");
                }
            }
            else
            {
                MessageBox.Show("Veri dosyası bulunamadı. Şu dosya için bakın: {tempData.txt}");
            }

            if (opsPenalty.PenaltyNumb() < 1)
                btnPay.Enabled = false;
            else
                btnPay.Enabled = true;

            dataGridView1.DataSource = opsPenalty.GetPenaltiesByTCKN(lblTCNO.Text);
        }

        public bool luhnAlgorithm(string cardNo)
        {
            int sum = 0;
            bool doubleIt = false;

            for (int i = cardNo.Length - 1; i >= 0; i--)
            {
                int digit = cardNo[i] - '0';

                if (doubleIt)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                doubleIt = !doubleIt;
            }

            if (sum % 10 == 0)
                return true;
            else
                return false;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string cardNoTemp = kartNoTxt.Text;
            string sktTemp = sktTxt.Text;
            string cardHolder = adTxt.Text;

            string cardNo = cardNoTemp.Replace("-", "");// Bu verilerin girişi masked textbox dan geldiği için '-' karakterlerini swap etmek gerekli
            string skt = sktTemp.Replace("-", "");

            if (chxCorrect.Checked)
            {
                if (cardNo != "" && skt != "" && cardHolder != "")
                {
                    if (luhnAlgorithm(cardNo))
                    {
                        opsPenalty.DeletePenalty(Convert.ToInt32(txtIdNo.Text));
                        dataGridView1.DataSource = opsPenalty.GetPenaltiesByTCKN(lblTCNO.Text);
                        MessageBox.Show("Cezanız başarıyla ödendi.");
                    }
                    else
                        MessageBox.Show("Kart bilgileriniz hatalı. Lütfen tekrar deneyin.");
                }
                else
                    MessageBox.Show("Lütfen kart bilgilerini eksiksiz girin.");

                // Self control
                if (opsPenalty.PenaltyNumb() < 1)
                    btnPay.Enabled = false;
                else
                    btnPay.Enabled = true;
            }
            else
                MessageBox.Show("Lütfen rıza kutucuğunu işaretleyin.");
            

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (txtIdNo.Text != "" || txtIdNo.Text != null)
                depositTxt.Text = opsPenalty.FindPenaltyById(Convert.ToInt32(txtIdNo.Text)).Price.ToString();
        }
    }
}
