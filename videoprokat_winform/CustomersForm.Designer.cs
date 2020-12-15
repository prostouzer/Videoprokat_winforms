
namespace videoprokat_winform
{
    partial class CustomersForm
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
            this.customers = new System.Windows.Forms.DataGridView();
            this.leasedCopies = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.addCustomerButton = new System.Windows.Forms.Button();
            this.customerNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.customers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leasedCopies)).BeginInit();
            this.SuspendLayout();
            // 
            // customers
            // 
            this.customers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customers.Location = new System.Drawing.Point(12, 12);
            this.customers.Name = "customers";
            this.customers.RowTemplate.Height = 25;
            this.customers.Size = new System.Drawing.Size(382, 204);
            this.customers.TabIndex = 0;
            this.customers.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.customers_CellEndEdit);
            this.customers.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.customers_DataError);
            this.customers.SelectionChanged += new System.EventHandler(this.customers_SelectionChanged);
            // 
            // leasedCopies
            // 
            this.leasedCopies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.leasedCopies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leasedCopies.Location = new System.Drawing.Point(12, 222);
            this.leasedCopies.Name = "leasedCopies";
            this.leasedCopies.ReadOnly = true;
            this.leasedCopies.RowTemplate.Height = 25;
            this.leasedCopies.Size = new System.Drawing.Size(721, 232);
            this.leasedCopies.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(433, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Добавить нового клиента";
            // 
            // addCustomerButton
            // 
            this.addCustomerButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addCustomerButton.Location = new System.Drawing.Point(400, 126);
            this.addCustomerButton.Name = "addCustomerButton";
            this.addCustomerButton.Size = new System.Drawing.Size(333, 90);
            this.addCustomerButton.TabIndex = 3;
            this.addCustomerButton.Text = "Добавить";
            this.addCustomerButton.UseVisualStyleBackColor = true;
            this.addCustomerButton.Click += new System.EventHandler(this.addCustomerButton_Click);
            // 
            // customerNameTextBox
            // 
            this.customerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customerNameTextBox.Location = new System.Drawing.Point(400, 97);
            this.customerNameTextBox.Name = "customerNameTextBox";
            this.customerNameTextBox.Size = new System.Drawing.Size(333, 23);
            this.customerNameTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Имя";
            // 
            // CustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 466);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customerNameTextBox);
            this.Controls.Add(this.addCustomerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.leasedCopies);
            this.Controls.Add(this.customers);
            this.MinimumSize = new System.Drawing.Size(761, 505);
            this.Name = "CustomersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Клиенты";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CustomersForm_FormClosed);
            this.Load += new System.EventHandler(this.CustomersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leasedCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView customers;
        private System.Windows.Forms.DataGridView leasedCopies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addCustomerButton;
        private System.Windows.Forms.TextBox customerNameTextBox;
        private System.Windows.Forms.Label label2;
    }
}