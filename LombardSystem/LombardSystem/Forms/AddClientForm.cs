using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LombardSystem.Forms
{
    public partial class AddClientForm : Form
    {
        public string FullName => txtFullName.Text;
        public string ContactInfo => txtContactInfo.Text;
        public string PassportData => txtPassportData.Text;

        public AddClientForm()
        {
            InitializeComponent();
        }

        private void AddClientForm_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullName) ||
                string.IsNullOrWhiteSpace(ContactInfo) ||
                string.IsNullOrWhiteSpace(PassportData))
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
