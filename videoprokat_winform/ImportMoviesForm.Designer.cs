﻿
namespace videoprokat_winform
{
    partial class ImportMoviesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.chooseFilePathButton = new System.Windows.Forms.Button();
            this.uploadMoviesButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.moviesListView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Файл не выбран";
            // 
            // chooseFilePathButton
            // 
            this.chooseFilePathButton.Location = new System.Drawing.Point(12, 86);
            this.chooseFilePathButton.Name = "chooseFilePathButton";
            this.chooseFilePathButton.Size = new System.Drawing.Size(153, 28);
            this.chooseFilePathButton.TabIndex = 1;
            this.chooseFilePathButton.Text = "Выбрать файл";
            this.chooseFilePathButton.UseVisualStyleBackColor = true;
            // 
            // uploadMoviesButton
            // 
            this.uploadMoviesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uploadMoviesButton.Enabled = false;
            this.uploadMoviesButton.Location = new System.Drawing.Point(12, 390);
            this.uploadMoviesButton.Name = "uploadMoviesButton";
            this.uploadMoviesButton.Size = new System.Drawing.Size(440, 129);
            this.uploadMoviesButton.TabIndex = 2;
            this.uploadMoviesButton.Text = "Загрузить фильмы";
            this.uploadMoviesButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(440, 60);
            this.label2.TabIndex = 4;
            this.label2.Text = "Внимание! Фильмы должны быть разделены точкой с запятой, а их названия, \r\nописани" +
    "я и года выпуска должны быть разделены ДВУМЯ запятыми.\r\n\r\nПример: Название1,,Опи" +
    "сание1,,Год1;Название2,,Описание2,,Год2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // moviesListView
            // 
            this.moviesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moviesListView.HideSelection = false;
            this.moviesListView.Location = new System.Drawing.Point(12, 120);
            this.moviesListView.Name = "moviesListView";
            this.moviesListView.Size = new System.Drawing.Size(440, 264);
            this.moviesListView.TabIndex = 5;
            this.moviesListView.UseCompatibleStateImageBehavior = false;
            this.moviesListView.View = System.Windows.Forms.View.List;
            // 
            // ImportMoviesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 531);
            this.Controls.Add(this.moviesListView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uploadMoviesButton);
            this.Controls.Add(this.chooseFilePathButton);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(480, 570);
            this.Name = "ImportMoviesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Импорт фильмов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseFilePathButton;
        private System.Windows.Forms.Button uploadMoviesButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView moviesListView;
    }
}