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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.movieCopiesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.movieCopyLeasingDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // movieCopiesDataGridView
            // 
            this.movieCopiesDataGridView.AllowUserToAddRows = false;
            this.movieCopiesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.movieCopiesDataGridView.Location = new System.Drawing.Point(647, 27);
            this.movieCopiesDataGridView.MultiSelect = false;
            this.movieCopiesDataGridView.Name = "movieCopiesDataGridView";
            this.movieCopiesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.movieCopiesDataGridView.Size = new System.Drawing.Size(418, 248);
            this.movieCopiesDataGridView.TabIndex = 1;
            this.movieCopiesDataGridView.Text = "dataGridView2";
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
            // 
            // moviesDataGridView
            // 
            this.moviesDataGridView.AllowUserToAddRows = false;
            this.moviesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moviesDataGridView.Location = new System.Drawing.Point(12, 27);
            this.moviesDataGridView.Name = "moviesDataGridView";
            this.moviesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.moviesDataGridView.Size = new System.Drawing.Size(629, 410);
            this.moviesDataGridView.TabIndex = 0;
            this.moviesDataGridView.Text = "dataGridView1";
            this.moviesDataGridView.SelectionChanged += new System.EventHandler(this.moviesDataGridView_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1077, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(66, 20);
            this.toolStripMenuItem1.Text = "Фильмы";
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

