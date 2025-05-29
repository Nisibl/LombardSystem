namespace LombardSystem.Forms
{
    partial class AddClientForm
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
            txtFullName = new TextBox();
            txtContactInfo = new TextBox();
            txtPassportData = new TextBox();
            btnOk = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(290, 89);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(237, 27);
            txtFullName.TabIndex = 0;
            // 
            // txtContactInfo
            // 
            txtContactInfo.Location = new Point(290, 170);
            txtContactInfo.Name = "txtContactInfo";
            txtContactInfo.Size = new Size(237, 27);
            txtContactInfo.TabIndex = 1;
            // 
            // txtPassportData
            // 
            txtPassportData.Location = new Point(290, 260);
            txtPassportData.Name = "txtPassportData";
            txtPassportData.Size = new Size(237, 27);
            txtPassportData.TabIndex = 2;
            // 
            // btnOk
            // 
            btnOk.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnOk.Location = new Point(266, 321);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(284, 92);
            btnOk.TabIndex = 3;
            btnOk.Text = "Добавить Клиента";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(201, 9);
            label1.Name = "label1";
            label1.Size = new Size(398, 41);
            label1.TabIndex = 4;
            label1.Text = "Введите данные о клиенте";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(378, 58);
            label2.Name = "label2";
            label2.Size = new Size(57, 28);
            label2.TabIndex = 5;
            label2.Text = "ФИО";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(324, 139);
            label3.Name = "label3";
            label3.Size = new Size(166, 28);
            label3.TabIndex = 6;
            label3.Text = "Номер телефона";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(309, 229);
            label4.Name = "label4";
            label4.Size = new Size(199, 28);
            label4.TabIndex = 7;
            label4.Text = "Паспортные данные";
            // 
            // AddClientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnOk);
            Controls.Add(txtPassportData);
            Controls.Add(txtContactInfo);
            Controls.Add(txtFullName);
            Name = "AddClientForm";
            Text = "AddClientForm";
            Load += AddClientForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFullName;
        private TextBox txtContactInfo;
        private TextBox txtPassportData;
        private Button btnOk;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}