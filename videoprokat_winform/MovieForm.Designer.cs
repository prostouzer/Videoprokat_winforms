
namespace videoprokat_winform
{
    partial class MovieForm
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
            this.movieButton = new System.Windows.Forms.Button();
            this.newMovieLabel = new System.Windows.Forms.Label();
            this.movieTitleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.movieDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.yearReleasedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.yearReleasedNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // movieButton
            // 
            this.movieButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.movieButton.Location = new System.Drawing.Point(12, 221);
            this.movieButton.Name = "movieButton";
            this.movieButton.Size = new System.Drawing.Size(505, 68);
            this.movieButton.TabIndex = 8;
            this.movieButton.Text = "Добавить";
            this.movieButton.UseVisualStyleBackColor = true;
            // 
            // newMovieLabel
            // 
            this.newMovieLabel.AutoSize = true;
            this.newMovieLabel.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.newMovieLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.newMovieLabel.Location = new System.Drawing.Point(12, 9);
            this.newMovieLabel.Name = "newMovieLabel";
            this.newMovieLabel.Size = new System.Drawing.Size(142, 25);
            this.newMovieLabel.TabIndex = 9;
            this.newMovieLabel.Text = "Новый фильм";
            // 
            // movieTitleTextBox
            // 
            this.movieTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.movieTitleTextBox.Location = new System.Drawing.Point(12, 100);
            this.movieTitleTextBox.Name = "movieTitleTextBox";
            this.movieTitleTextBox.Size = new System.Drawing.Size(379, 23);
            this.movieTitleTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Название";
            // 
            // movieDescriptionTextBox
            // 
            this.movieDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.movieDescriptionTextBox.Location = new System.Drawing.Point(12, 146);
            this.movieDescriptionTextBox.Multiline = true;
            this.movieDescriptionTextBox.Name = "movieDescriptionTextBox";
            this.movieDescriptionTextBox.Size = new System.Drawing.Size(505, 69);
            this.movieDescriptionTextBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Описание";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Год";
            // 
            // yearReleasedNumericUpDown
            // 
            this.yearReleasedNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yearReleasedNumericUpDown.Location = new System.Drawing.Point(397, 100);
            this.yearReleasedNumericUpDown.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.yearReleasedNumericUpDown.Minimum = new decimal(new int[] {
            1850,
            0,
            0,
            0});
            this.yearReleasedNumericUpDown.Name = "yearReleasedNumericUpDown";
            this.yearReleasedNumericUpDown.Size = new System.Drawing.Size(120, 23);
            this.yearReleasedNumericUpDown.TabIndex = 15;
            this.yearReleasedNumericUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // MovieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 301);
            this.Controls.Add(this.yearReleasedNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.movieDescriptionTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.movieTitleTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newMovieLabel);
            this.Controls.Add(this.movieButton);
            this.MinimumSize = new System.Drawing.Size(550, 340);
            this.Name = "MovieForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новый фильм";
            ((System.ComponentModel.ISupportInitialize)(this.yearReleasedNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button movieButton;
        private System.Windows.Forms.Label newMovieLabel;
        private System.Windows.Forms.TextBox movieTitleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox movieDescriptionTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown yearReleasedNumericUpDown;
    }
}