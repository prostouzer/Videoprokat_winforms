namespace videoprokat_winform
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.movieCopiesDataGridView = new System.Windows.Forms.DataGridView();
            this.movieCopyLeasingDataGridView = new System.Windows.Forms.DataGridView();
            this.moviesDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.movieCopiesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movieCopyLeasingDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // movieCopiesDataGridView
            // 
            this.movieCopiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.movieCopiesDataGridView.Location = new System.Drawing.Point(647, 27);
            this.movieCopiesDataGridView.MultiSelect = false;
            this.movieCopiesDataGridView.Name = "movieCopiesDataGridView";
            this.movieCopiesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.movieCopiesDataGridView.Size = new System.Drawing.Size(418, 248);
            this.movieCopiesDataGridView.TabIndex = 1;
            this.movieCopiesDataGridView.Text = "dataGridView2";
            this.movieCopiesDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.movieCopiesDataGridView_CellEndEdit);
            this.movieCopiesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.movieCopiesDataGridView_DataError);
            this.movieCopiesDataGridView.SelectionChanged += new System.EventHandler(this.movieCopiesDataGridView_SelectionChanged);
            // 
            // movieCopyLeasingDataGridView
            // 
            this.movieCopyLeasingDataGridView.AllowUserToAddRows = false;
            this.movieCopyLeasingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.movieCopyLeasingDataGridView.Location = new System.Drawing.Point(647, 281);
            this.movieCopyLeasingDataGridView.MultiSelect = false;
            this.movieCopyLeasingDataGridView.Name = "movieCopyLeasingDataGridView";
            this.movieCopyLeasingDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.movieCopyLeasingDataGridView.Size = new System.Drawing.Size(418, 156);
            this.movieCopyLeasingDataGridView.TabIndex = 2;
            this.movieCopyLeasingDataGridView.Text = "dataGridView3";
            this.movieCopyLeasingDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.movieCopyLeasingDataGridView_CellEndEdit);
            this.movieCopyLeasingDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.movieCopyLeasingDataGridView_DataError);
            // 
            // moviesDataGridView
            // 
            this.moviesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moviesDataGridView.Location = new System.Drawing.Point(12, 27);
            this.moviesDataGridView.Name = "moviesDataGridView";
            this.moviesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.moviesDataGridView.Size = new System.Drawing.Size(629, 410);
            this.moviesDataGridView.TabIndex = 0;
            this.moviesDataGridView.Text = "dataGridView1";
            this.moviesDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.moviesDataGridView_CellEndEdit);
            this.moviesDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.moviesDataGridView_DataError);
            this.moviesDataGridView.SelectionChanged += new System.EventHandler(this.moviesDataGridView_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1077, 27);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Text = "Поиск";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(117, 23);
            this.toolStripMenuItem1.Text = "Импорт фильмов";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 449);
            this.Controls.Add(this.movieCopyLeasingDataGridView);
            this.Controls.Add(this.movieCopiesDataGridView);
            this.Controls.Add(this.moviesDataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Videoprokat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.movieCopiesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.movieCopyLeasingDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView moviesDataGridView;
        private System.Windows.Forms.DataGridView movieCopiesDataGridView;
        private System.Windows.Forms.DataGridView movieCopyLeasingDataGridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

