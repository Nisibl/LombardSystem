namespace LombardSystem.Forms
{
    partial class OwnerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            btnUpdatePrice = new Button();
            txtNewPrice = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(202, 36);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(586, 381);
            dataGridView1.TabIndex = 0;
            // 
            // btnUpdatePrice
            // 
            btnUpdatePrice.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnUpdatePrice.Location = new Point(3, 236);
            btnUpdatePrice.Name = "btnUpdatePrice";
            btnUpdatePrice.Size = new Size(193, 87);
            btnUpdatePrice.TabIndex = 1;
            btnUpdatePrice.Text = "Изменить цену";
            btnUpdatePrice.UseVisualStyleBackColor = true;
            btnUpdatePrice.Click += btnUpdatePrice_Click_1;
            // 
            // txtNewPrice
            // 
            txtNewPrice.Location = new Point(30, 184);
            txtNewPrice.Name = "txtNewPrice";
            txtNewPrice.Size = new Size(142, 27);
            txtNewPrice.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
            label1.Location = new Point(29, 162);
            label1.Name = "label1";
            label1.Size = new Size(143, 19);
            label1.TabIndex = 3;
            label1.Text = "Введите новую цену";
            // 
            // OwnerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(txtNewPrice);
            Controls.Add(btnUpdatePrice);
            Controls.Add(dataGridView1);
            Name = "OwnerForm";
            Text = "OwnerForm";
            Load += OwnerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnUpdatePrice;
        private TextBox txtNewPrice;
        private Label label1;
    }
}