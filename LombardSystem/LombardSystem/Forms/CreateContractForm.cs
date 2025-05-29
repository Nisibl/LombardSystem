using LombardSystem.Database;
using LombardSystem.Models;
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
    public partial class CreateContractForm : Form
    {
        public int SelectedClientId { get; private set; }
        public int SelectedItemId { get; private set; }
        public DateTime SignDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public decimal LoanAmount { get; private set; }

        private readonly ComboBox _cmbClients;
        private readonly ComboBox _cmbItems;
        private readonly DateTimePicker _dtpSignDate;
        private readonly DateTimePicker _dtpExpiryDate;
        private readonly NumericUpDown _numLoanAmount;

        public CreateContractForm(List<Client> clients, List<Item> items)
        {
            // Настройка формы
            this.Text = "Создание договора";
            this.Size = new Size(400, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Элементы управления
            var lblClient = new Label { Text = "Клиент:", Top = 20, Left = 20, Width = 100 };
            _cmbClients = new ComboBox { Top = 20, Left = 130, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            _cmbClients.DisplayMember = "FullName";
            _cmbClients.ValueMember = "Id";
            _cmbClients.DataSource = clients;

            var lblItem = new Label { Text = "Предмет:", Top = 60, Left = 20, Width = 100 };
            _cmbItems = new ComboBox { Top = 60, Left = 130, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            _cmbItems.DisplayMember = "Description";
            _cmbItems.ValueMember = "Id";
            _cmbItems.DataSource = items;

            var lblSignDate = new Label { Text = "Дата подписания:", Top = 100, Left = 20, Width = 100 };
            _dtpSignDate = new DateTimePicker { Top = 100, Left = 130, Width = 200, Value = DateTime.Now };

            var lblExpiryDate = new Label { Text = "Дата истечения:", Top = 140, Left = 20, Width = 100 };
            _dtpExpiryDate = new DateTimePicker { Top = 140, Left = 130, Width = 200, Value = DateTime.Now.AddMonths(1) };

            var lblLoanAmount = new Label { Text = "Сумма залога:", Top = 180, Left = 20, Width = 100 };
            _numLoanAmount = new NumericUpDown { Top = 180, Left = 130, Width = 200, DecimalPlaces = 2, Minimum = 0 };
            _numLoanAmount.Maximum = Decimal.MaxValue;

            // Кнопки
            var btnOk = new Button { Text = "Создать", DialogResult = DialogResult.OK, Top = 220, Left = 130, Width = 100 };
            var btnCancel = new Button { Text = "Отмена", DialogResult = DialogResult.Cancel, Top = 220, Left = 240, Width = 100 };

            // Добавление элементов на форму
            this.Controls.AddRange(new Control[] {
            lblClient, _cmbClients,
            lblItem, _cmbItems,
            lblSignDate, _dtpSignDate,
            lblExpiryDate, _dtpExpiryDate,
            lblLoanAmount, _numLoanAmount,
            btnOk, btnCancel

        });

            // Обработчик кнопки OK
            btnOk.Click += (s, e) =>
            {
                // Проверка выбора клиента и предмета
                if (_cmbClients.SelectedItem == null || _cmbItems.SelectedItem == null)
                {
                    MessageBox.Show("Выберите клиента и предмет");
                    this.DialogResult = DialogResult.None;
                    return;
                }

                // Проверка дат
                if (_dtpExpiryDate.Value <= _dtpSignDate.Value)
                {
                    MessageBox.Show("Дата истечения должна быть позже даты подписания");
                    this.DialogResult = DialogResult.None;
                    return;
                }

                // Проверка что предмет не участвует в другом договоре
                var selectedItemId = (int)_cmbItems.SelectedValue;
                using (var dbContext = new DatabaseContext())
                {
                    bool isItemInContract = dbContext.Contracts
                        .Any(c => c.ItemId == selectedItemId);

                    if (isItemInContract)
                    {
                        MessageBox.Show("Этот предмет уже используется в другом договоре",
                                      "Ошибка",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                }

                // Если все проверки пройдены
                SelectedClientId = (int)_cmbClients.SelectedValue;
                SelectedItemId = selectedItemId;
                SignDate = _dtpSignDate.Value;
                ExpiryDate = _dtpExpiryDate.Value;
                LoanAmount = _numLoanAmount.Value;
            };

            // Автоматический расчет суммы залога + проверка доступности предмета
            _cmbItems.SelectedIndexChanged += (s, e) =>
            {
                if (_cmbItems.SelectedItem is Item selectedItem)
                {
                    // Расчет суммы залога
                    _numLoanAmount.Value = selectedItem.EstimatedValue * 0.7m;

                    // Проверка доступности предмета
                    using (var dbContext = new DatabaseContext())
                    {
                        bool isAvailable = !dbContext.Contracts
                            .Any(c => c.ItemId == selectedItem.Id);


                        if (!isAvailable)
                        {
                            MessageBox.Show("Этот предмет уже в другом договоре",
                                          "Предмет недоступен",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                        }
                    }
                }
            };
        }

        private void CreateContractForm_Load(object sender, EventArgs e)
        {

        }
    }
}
