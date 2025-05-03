using CsharpProje.Business;
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
    public partial class Kaydol: Form
    {
        public Kaydol()
        {
            InitializeComponent();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            DatabaseOpsUser user = new DatabaseOpsUser();

            string name = txtName.Text;
            string surname = txtSurname.Text;
            string tckn = txtTCKN.Text;
            string tel = txtTel.Text;
            byte auth;

            if (switchAuth.Checked)
                auth = 1;
            else
                auth = 0;

            user.AddUser(name, surname, tckn, tel, auth);
        }

        private void switchAuth_CheckedChanged(object sender, EventArgs e)
        {
            if (switchAuth.Checked)
                switchAuth.Text = "Yetkili";
            else
                switchAuth.Text = "Vatandaş";
        }
    }
}
