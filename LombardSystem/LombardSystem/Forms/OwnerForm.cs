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
    public partial class OwnerForm : Form
    {
        private readonly LombardFacade _facade;

        public OwnerForm(LombardFacade facade)
        {
            InitializeComponent();
            _facade = facade;
            _facade.ProcessExpiredContracts(); 
            LoadProducts();
            this.FormClosing += (s, e) => Application.Exit();

        }

        private void LoadProducts()
        {
            dataGridView1.DataSource = _facade.GetProducts();
        }

        private void OwnerForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdatePrice_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Проверка выбранной строки в DataGridView
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Выберите товар для изменения цены", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверка заполненности поля цены
                if (string.IsNullOrWhiteSpace(txtNewPrice.Text))
                {
                    MessageBox.Show("Введите новую цену", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPrice.Focus(); // Устанавливаем фокус на поле ввода
                    return;
                }

                // Проверка корректности числового значения
                if (!decimal.TryParse(txtNewPrice.Text, out decimal newPrice))
                {
                    MessageBox.Show("Введите корректное числовое значение цены", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNewPrice.SelectAll(); // Выделяем текст для удобного исправления
                    return;
                }

                // Проверка на положительное значение
                if (newPrice <= 0)
                {
                    MessageBox.Show("Цена должна быть больше нуля", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем ID выбранного товара
                var productId = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;

                // Обновляем цену через фасад
                _facade.UpdateProductPrice(productId, newPrice);

                // Обновляем список товаров
                LoadProducts();

                // Сообщение об успехе
                MessageBox.Show("Цена успешно обновлена", "Успех",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Очищаем поле ввода
                txtNewPrice.Clear();
            }
            catch (Exception ex)
            {
                // Обработка непредвиденных ошибок
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
