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
            this.components = new System.ComponentModel.Container();
            this.copiesDgv = new System.Windows.Forms.DataGridView();
            this.copiesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.leasingItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leasingsDgv = new System.Windows.Forms.DataGridView();
            this.leasingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.returnItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moviesDgv = new System.Windows.Forms.DataGridView();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.customersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMoviesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMovieCopyButton = new System.Windows.Forms.Button();
            this.newMovieButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.copiesDgv)).BeginInit();
            this.copiesContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leasingsDgv)).BeginInit();
            this.leasingContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviesDgv)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // copiesDgv
            // 
            this.copiesDgv.AllowUserToAddRows = false;
            this.copiesDgv.AllowUserToDeleteRows = false;
            this.copiesDgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copiesDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.copiesDgv.ContextMenuStrip = this.copiesContextMenu;
            this.copiesDgv.Location = new System.Drawing.Point(647, 27);
            this.copiesDgv.MultiSelect = false;
            this.copiesDgv.Name = "copiesDgv";
            this.copiesDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.copiesDgv.Size = new System.Drawing.Size(559, 291);
            this.copiesDgv.TabIndex = 1;
            this.copiesDgv.Text = "dataGridView2";
            // 
            // copiesContextMenu
            // 
            this.copiesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leasingItem});
            this.copiesContextMenu.Name = "copiesContextMenu";
            this.copiesContextMenu.Size = new System.Drawing.Size(115, 26);
            // 
            // leasingItem
            // 
            this.leasingItem.Enabled = false;
            this.leasingItem.Name = "leasingItem";
            this.leasingItem.Size = new System.Drawing.Size(114, 22);
            this.leasingItem.Text = "Прокат";
            // 
            // leasingsDgv
            // 
            this.leasingsDgv.AllowUserToAddRows = false;
            this.leasingsDgv.AllowUserToDeleteRows = false;
            this.leasingsDgv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leasingsDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leasingsDgv.ContextMenuStrip = this.leasingContextMenu;
            this.leasingsDgv.Location = new System.Drawing.Point(647, 321);
            this.leasingsDgv.MultiSelect = false;
            this.leasingsDgv.Name = "leasingsDgv";
            this.leasingsDgv.ReadOnly = true;
            this.leasingsDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.leasingsDgv.Size = new System.Drawing.Size(559, 116);
            this.leasingsDgv.TabIndex = 2;
            this.leasingsDgv.Text = "dataGridView3";
            // 
            // leasingContextMenu
            // 
            this.leasingContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.returnItem});
            this.leasingContextMenu.Name = "leasingContextMenu";
            this.leasingContextMenu.Size = new System.Drawing.Size(119, 26);
            // 
            // returnItem
            // 
            this.returnItem.Name = "returnItem";
            this.returnItem.Size = new System.Drawing.Size(118, 22);
            this.returnItem.Text = "Вернуть";
            // 
            // moviesDgv
            // 
            this.moviesDgv.AllowUserToAddRows = false;
            this.moviesDgv.AllowUserToDeleteRows = false;
            this.moviesDgv.AllowUserToOrderColumns = true;
            this.moviesDgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moviesDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moviesDgv.Location = new System.Drawing.Point(12, 27);
            this.moviesDgv.Name = "moviesDgv";
            this.moviesDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.moviesDgv.Size = new System.Drawing.Size(629, 337);
            this.moviesDgv.TabIndex = 0;
            this.moviesDgv.Text = "dataGridView1";
            this.moviesDgv.SelectionChanged += new System.EventHandler(this.moviesDgv_SelectionChanged);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchBox,
            this.customersMenuItem,
            this.importMoviesMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1218, 27);
            this.mainMenu.TabIndex = 3;
            this.mainMenu.Text = "menuStrip1";
            // 
            // searchBox
            // 
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(100, 23);
            // 
            // customersMenuItem
            // 
            this.customersMenuItem.Name = "customersMenuItem";
            this.customersMenuItem.Size = new System.Drawing.Size(67, 23);
            this.customersMenuItem.Text = "Клиенты";
            // 
            // importMoviesMenuItem
            // 
            this.importMoviesMenuItem.Name = "importMoviesMenuItem";
            this.importMoviesMenuItem.Size = new System.Drawing.Size(117, 23);
            this.importMoviesMenuItem.Text = "Импорт фильмов";
            // 
            // newMovieCopyButton
            // 
            this.newMovieCopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newMovieCopyButton.Enabled = false;
            this.newMovieCopyButton.Location = new System.Drawing.Point(226, 370);
            this.newMovieCopyButton.Name = "newMovieCopyButton";
            this.newMovieCopyButton.Size = new System.Drawing.Size(415, 67);
            this.newMovieCopyButton.TabIndex = 5;
            this.newMovieCopyButton.Text = "Новая копия фильма";
            this.newMovieCopyButton.UseVisualStyleBackColor = true;
            // 
            // newMovieButton
            // 
            this.newMovieButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newMovieButton.Location = new System.Drawing.Point(12, 370);
            this.newMovieButton.Name = "newMovieButton";
            this.newMovieButton.Size = new System.Drawing.Size(208, 67);
            this.newMovieButton.TabIndex = 6;
            this.newMovieButton.Text = "Новый фильм";
            this.newMovieButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 449);
            this.Controls.Add(this.newMovieButton);
            this.Controls.Add(this.newMovieCopyButton);
            this.Controls.Add(this.leasingsDgv);
            this.Controls.Add(this.copiesDgv);
            this.Controls.Add(this.moviesDgv);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(1234, 488);
            this.Name = "MainForm";
            this.Text = "Видеопрокат";
            ((System.ComponentModel.ISupportInitialize)(this.copiesDgv)).EndInit();
            this.copiesContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leasingsDgv)).EndInit();
            this.leasingContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moviesDgv)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView moviesDgv;
        private System.Windows.Forms.DataGridView copiesDgv;
        private System.Windows.Forms.DataGridView leasingsDgv;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripTextBox searchBox;
        private System.Windows.Forms.ToolStripMenuItem importMoviesMenuItem;
        private System.Windows.Forms.ContextMenuStrip copiesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem leasingItem;
        private System.Windows.Forms.ToolStripMenuItem customersMenuItem;
        private System.Windows.Forms.ContextMenuStrip leasingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem returnItem;
        private System.Windows.Forms.Button newMovieCopyButton;
        private System.Windows.Forms.Button newMovieButton;
    }
}

