
namespace videoprokat_winform
{
    partial class ReturnForm
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
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.returnDatePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.returnDateLabel = new System.Windows.Forms.Label();
            this.movieCommentLabel = new System.Windows.Forms.Label();
            this.movieNameLabel = new System.Windows.Forms.Label();
            this.returnButton = new System.Windows.Forms.Button();
            this.ownerNameLabel = new System.Windows.Forms.Label();
            this.expectedEndLabel = new System.Windows.Forms.Label();
            this.totalPriceChangeLabel = new System.Windows.Forms.Label();
            this.fineFormulaLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startDatePicker
            // 
            this.startDatePicker.Enabled = false;
            this.startDatePicker.Location = new System.Drawing.Point(12, 134);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 23);
            this.startDatePicker.TabIndex = 0;
            // 
            // returnDatePicker
            // 
            this.returnDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.returnDatePicker.Location = new System.Drawing.Point(218, 134);
            this.returnDatePicker.Name = "returnDatePicker";
            this.returnDatePicker.Size = new System.Drawing.Size(304, 23);
            this.returnDatePicker.TabIndex = 1;
            this.returnDatePicker.ValueChanged += new System.EventHandler(this.returnDatePicker_ValueChanged);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startDateLabel.Location = new System.Drawing.Point(93, 106);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(46, 25);
            this.startDateLabel.TabIndex = 2;
            this.startDateLabel.Text = "Взят";
            // 
            // returnDateLabel
            // 
            this.returnDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.returnDateLabel.AutoSize = true;
            this.returnDateLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.returnDateLabel.Location = new System.Drawing.Point(314, 106);
            this.returnDateLabel.Name = "returnDateLabel";
            this.returnDateLabel.Size = new System.Drawing.Size(112, 25);
            this.returnDateLabel.TabIndex = 3;
            this.returnDateLabel.Text = "Возвращен";
            // 
            // movieCommentLabel
            // 
            this.movieCommentLabel.AutoSize = true;
            this.movieCommentLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.movieCommentLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.movieCommentLabel.Location = new System.Drawing.Point(12, 69);
            this.movieCommentLabel.Name = "movieCommentLabel";
            this.movieCommentLabel.Size = new System.Drawing.Size(169, 25);
            this.movieCommentLabel.TabIndex = 5;
            this.movieCommentLabel.Text = "Movie Commentary";
            // 
            // movieNameLabel
            // 
            this.movieNameLabel.AutoSize = true;
            this.movieNameLabel.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.movieNameLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.movieNameLabel.Location = new System.Drawing.Point(12, 44);
            this.movieNameLabel.Name = "movieNameLabel";
            this.movieNameLabel.Size = new System.Drawing.Size(127, 25);
            this.movieNameLabel.TabIndex = 4;
            this.movieNameLabel.Text = "Movie Name";
            // 
            // returnButton
            // 
            this.returnButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.returnButton.Location = new System.Drawing.Point(12, 218);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(510, 71);
            this.returnButton.TabIndex = 6;
            this.returnButton.Text = "Вернуть";
            this.returnButton.UseVisualStyleBackColor = true;
            // 
            // ownerNameLabel
            // 
            this.ownerNameLabel.AutoSize = true;
            this.ownerNameLabel.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ownerNameLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ownerNameLabel.Location = new System.Drawing.Point(12, 9);
            this.ownerNameLabel.Name = "ownerNameLabel";
            this.ownerNameLabel.Size = new System.Drawing.Size(168, 31);
            this.ownerNameLabel.TabIndex = 7;
            this.ownerNameLabel.Text = "Owner Name";
            // 
            // expectedEndLabel
            // 
            this.expectedEndLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.expectedEndLabel.AutoSize = true;
            this.expectedEndLabel.Location = new System.Drawing.Point(247, 160);
            this.expectedEndLabel.Name = "expectedEndLabel";
            this.expectedEndLabel.Size = new System.Drawing.Size(73, 15);
            this.expectedEndLabel.TabIndex = 8;
            this.expectedEndLabel.Text = "Ожидается: ";
            // 
            // totalPriceChangeLabel
            // 
            this.totalPriceChangeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.totalPriceChangeLabel.AutoSize = true;
            this.totalPriceChangeLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.totalPriceChangeLabel.Location = new System.Drawing.Point(168, 190);
            this.totalPriceChangeLabel.Name = "totalPriceChangeLabel";
            this.totalPriceChangeLabel.Size = new System.Drawing.Size(152, 25);
            this.totalPriceChangeLabel.TabIndex = 11;
            this.totalPriceChangeLabel.Text = "Штраф/Возврат";
            this.totalPriceChangeLabel.Visible = false;
            // 
            // fineFormulaLabel
            // 
            this.fineFormulaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.fineFormulaLabel.AutoSize = true;
            this.fineFormulaLabel.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fineFormulaLabel.Location = new System.Drawing.Point(141, 178);
            this.fineFormulaLabel.Name = "fineFormulaLabel";
            this.fineFormulaLabel.Size = new System.Drawing.Size(198, 12);
            this.fineFormulaLabel.TabIndex = 12;
            this.fineFormulaLabel.Text = "Штраф = MULTIPLIER*Просроченные дни";
            // 
            // ReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 301);
            this.Controls.Add(this.fineFormulaLabel);
            this.Controls.Add(this.totalPriceChangeLabel);
            this.Controls.Add(this.expectedEndLabel);
            this.Controls.Add(this.ownerNameLabel);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.movieCommentLabel);
            this.Controls.Add(this.movieNameLabel);
            this.Controls.Add(this.returnDateLabel);
            this.Controls.Add(this.startDateLabel);
            this.Controls.Add(this.returnDatePicker);
            this.Controls.Add(this.startDatePicker);
            this.MinimumSize = new System.Drawing.Size(550, 340);
            this.Name = "ReturnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Вернуть";
            this.Load += new System.EventHandler(this.ReturnForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker returnDatePicker;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.Label returnDateLabel;
        private System.Windows.Forms.Label movieCommentLabel;
        private System.Windows.Forms.Label movieNameLabel;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Label ownerNameLabel;
        private System.Windows.Forms.Label expectedEndLabel;
        private System.Windows.Forms.Label totalPriceChangeLabel;
        private System.Windows.Forms.Label fineFormulaLabel;
    }
}