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
    public partial class AddItemForm : Form
    {
        public string Description { get; private set; }
        public decimal EstimatedValue { get; private set; }

        public AddItemForm()
        {
            // Инициализация элементов формы
            var lblDesc = new Label { Text = "Описание:", Top = 20, Left = 20 };
            var txtDesc = new TextBox { Top = 20, Left = 120, Width = 200 };

            var lblValue = new Label { Text = "Оценка:", Top = 60, Left = 20 };
            var numValue = new NumericUpDown { Top = 60, Left = 120, Width = 100, DecimalPlaces = 2 };
            numValue.Maximum = Decimal.MaxValue;

            var btnOk = new Button { Text = "Добавить", DialogResult = DialogResult.OK, Top = 100, Left = 120 };

            // Добавление элементов на форму
            Controls.AddRange(new Control[] { lblDesc, txtDesc, lblValue, numValue, btnOk });

            // Обработчик кнопки
            btnOk.Click += (s, e) =>
            {
                Description = txtDesc.Text;
                EstimatedValue = numValue.Value;
                Close();
            };
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {

        }
    }
}
