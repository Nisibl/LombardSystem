using LombardSystem.Facades;
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
    public partial class AuthForm : Form
    {
        private readonly LombardFacade _facade;

        public AuthForm()
        {
            InitializeComponent();
            _facade = new LombardFacade();
            this.FormClosing += (s, e) => Application.Exit();
        }

        private void AuthForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                var user = _facade.Authenticate(txtUsername.Text, txtPassword.Text);

                if (user != null)
                {
                    if (user.Role == "Employee")
                    {
                        var employeeForm = new EmployeeForm(_facade);
                        employeeForm.Show();
                    }
                    else if (user.Role == "Owner")
                    {
                        var ownerForm = new OwnerForm(_facade);
                        ownerForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверные учетные данные", "Ошибка",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
