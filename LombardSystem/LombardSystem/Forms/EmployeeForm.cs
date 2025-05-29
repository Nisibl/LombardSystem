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
    public partial class EmployeeForm : Form
    {
        private readonly LombardFacade _facade;

        public EmployeeForm(LombardFacade facade)
        {
            InitializeComponent();
            this.FormClosing += (s, e) => Application.Exit();
            _facade = facade;
            InitializeTabs();
            LoadData();
            _facade.ProcessExpiredContracts(); // Обработка просроченных договоров при запуске
            LoadData();
        }

        private void InitializeTabs()
        {
            tabControl1.TabPages.Clear();

            // Вкладка клиентов
            var clientsTab = new TabPage("Клиенты");
            InitializeClientsTab(clientsTab);
            tabControl1.TabPages.Add(clientsTab);

            // Вкладка предметов
            var itemsTab = new TabPage("Предметы");
            InitializeItemsTab(itemsTab);
            tabControl1.TabPages.Add(itemsTab);

            // Вкладка договоров
            var contractsTab = new TabPage("Договоры");
            InitializeContractsTab(contractsTab);
            tabControl1.TabPages.Add(contractsTab);

            // Вкладка товаров
            var productsTab = new TabPage("Товары");
            InitializeProductsTab(productsTab);
            tabControl1.TabPages.Add(productsTab);
        }

        private void InitializeClientsTab(TabPage tab)
        {
            // DataGridView для клиентов
            var dgvClients = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = _facade.GetClients()
            };

            // Кнопка добавления
            var btnAddClient = new Button
            {
                Text = "Добавить клиента",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            btnAddClient.Click += (s, e) =>
            {
                var addForm = new AddClientForm();
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    _facade.AddClient(addForm.FullName, addForm.ContactInfo, addForm.PassportData);
                    dgvClients.DataSource = _facade.GetClients();
                }
            };

            tab.Controls.Add(dgvClients);
            tab.Controls.Add(btnAddClient);
        }

        private void InitializeItemsTab(TabPage tab)
        {
            // DataGridView для предметов
            var dgvItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = _facade.GetItems()
            };

            // Кнопка добавления
            var btnAddItem = new Button
            {
                Text = "Добавить предмет",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            btnAddItem.Click += (s, e) =>
            {
                var addForm = new AddItemForm();
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    _facade.AddItem(addForm.Description, addForm.EstimatedValue);
                    dgvItems.DataSource = _facade.GetItems();
                }
            };

            tab.Controls.Add(dgvItems);
            tab.Controls.Add(btnAddItem);
        }

        private void InitializeContractsTab(TabPage tab)
        {
            // DataGridView для договоров
            var dgvContracts = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = _facade.GetContracts()
            };

            // Кнопка создания договора
            var btnCreateContract = new Button
            {
                Text = "Создать договор",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            btnCreateContract.Click += (s, e) =>
            {
                var createForm = new CreateContractForm(_facade.GetClients(), _facade.GetItems());
                if (createForm.ShowDialog() == DialogResult.OK)
                {
                    _facade.CreateContract(
                        createForm.SelectedClientId,
                        createForm.SelectedItemId,
                        createForm.SignDate,
                        createForm.ExpiryDate,
                        createForm.LoanAmount);

                    // Обновляем DataGridView
                    dgvContracts.DataSource = _facade.GetContracts();
                }
            };

            tab.Controls.Add(dgvContracts);
            tab.Controls.Add(btnCreateContract);
        }

        private void InitializeProductsTab(TabPage tab)
        {
            // DataGridView для товаров
            var dgvProducts = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = _facade.GetProducts(),
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Кнопка продажи товара
            var btnSellProduct = new Button
            {
                Text = "Продать выбранный товар",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            btnSellProduct.Click += (s, e) =>
            {
                if (dgvProducts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите товар для продажи");
                    return;
                }

                var productId = (int)dgvProducts.SelectedRows[0].Cells["Id"].Value;
                if (MessageBox.Show("Подтвердите продажу товара", "Продажа",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _facade.SellProduct(productId);
                    dgvProducts.DataSource = _facade.GetProducts();
                }
            };

            tab.Controls.Add(dgvProducts);
            tab.Controls.Add(btnSellProduct);
        }

        private void LoadData()
        {
            // Обновление данных во всех вкладках
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab.Controls[0] is DataGridView dgv)
                {
                    if (tab.Text == "Клиенты") dgv.DataSource = _facade.GetClients();
                    else if (tab.Text == "Предметы") dgv.DataSource = _facade.GetItems();
                    else if (tab.Text == "Договоры") dgv.DataSource = _facade.GetContracts();
                    else if (tab.Text == "Товары") dgv.DataSource = _facade.GetProducts();
                }
            }
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
