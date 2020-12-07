
namespace videoprokat_winform
{
    partial class ClientsForm
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
            this.clients = new System.Windows.Forms.DataGridView();
            this.leasedCopies = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.clientNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.clients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leasedCopies)).BeginInit();
            this.SuspendLayout();
            // 
            // clients
            // 
            this.clients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clients.Location = new System.Drawing.Point(12, 12);
            this.clients.Name = "clients";
            this.clients.RowTemplate.Height = 25;
            this.clients.Size = new System.Drawing.Size(382, 204);
            this.clients.TabIndex = 0;
            this.clients.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.clients_CellEndEdit);
            this.clients.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.clients_DataError);
            this.clients.SelectionChanged += new System.EventHandler(this.clients_SelectionChanged);
            // 
            // leasedCopies
            // 
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
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(433, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Добавить нового клиента";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(400, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(333, 90);
            this.button1.TabIndex = 3;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // clientNameTextBox
            // 
            this.clientNameTextBox.Location = new System.Drawing.Point(400, 97);
            this.clientNameTextBox.Name = "clientNameTextBox";
            this.clientNameTextBox.Size = new System.Drawing.Size(333, 23);
            this.clientNameTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(544, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Имя";
            // 
            // ClientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 466);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clientNameTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.leasedCopies);
            this.Controls.Add(this.clients);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ClientsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Клиенты";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientsForm_FormClosed);
            this.Load += new System.EventHandler(this.ClientsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leasedCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView clients;
        private System.Windows.Forms.DataGridView leasedCopies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox clientNameTextBox;
        private System.Windows.Forms.Label label2;
    }
}