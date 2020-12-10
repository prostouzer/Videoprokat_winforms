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
            this.newCopyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leasingsDgv = new System.Windows.Forms.DataGridView();
            this.leasingContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.returnItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moviesDgv = new System.Windows.Forms.DataGridView();
            this.moviesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.searchBox = new System.Windows.Forms.ToolStripTextBox();
            this.clientsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMoviesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.copiesDgv)).BeginInit();
            this.copiesContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leasingsDgv)).BeginInit();
            this.leasingContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviesDgv)).BeginInit();
            this.moviesContextMenu.SuspendLayout();
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
            this.copiesDgv.Location = new System.Drawing.Point(647, 30);
            this.copiesDgv.MultiSelect = false;
            this.copiesDgv.Name = "copiesDgv";
            this.copiesDgv.ReadOnly = true;
            this.copiesDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.copiesDgv.Size = new System.Drawing.Size(559, 288);
            this.copiesDgv.TabIndex = 1;
            this.copiesDgv.Text = "dataGridView2";
            this.copiesDgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.copiesDgv_CellEndEdit);
            this.copiesDgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.copiesDgv_DataError);
            this.copiesDgv.SelectionChanged += new System.EventHandler(this.copiesDgv_SelectionChanged);
            this.copiesDgv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.copiesDgv_MouseUp);
            // 
            // copiesContextMenu
            // 
            this.copiesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leasingItem,
            this.newCopyItem});
            this.copiesContextMenu.Name = "copiesContextMenu";
            this.copiesContextMenu.Size = new System.Drawing.Size(206, 48);
            // 
            // leasingItem
            // 
            this.leasingItem.Enabled = false;
            this.leasingItem.Name = "leasingItem";
            this.leasingItem.Size = new System.Drawing.Size(205, 22);
            this.leasingItem.Text = "Прокат";
            // 
            // newCopyItem
            // 
            this.newCopyItem.Name = "newCopyItem";
            this.newCopyItem.Size = new System.Drawing.Size(205, 22);
            this.newCopyItem.Text = "Добавить новую копию";
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
            this.leasingsDgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.leasingsDgv_DataError);
            this.leasingsDgv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.leasingsDgv_MouseUp);
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
            this.moviesDgv.ContextMenuStrip = this.moviesContextMenu;
            this.moviesDgv.Location = new System.Drawing.Point(12, 27);
            this.moviesDgv.Name = "moviesDgv";
            this.moviesDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.moviesDgv.Size = new System.Drawing.Size(629, 410);
            this.moviesDgv.TabIndex = 0;
            this.moviesDgv.Text = "dataGridView1";
            this.moviesDgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.moviesDgv_CellEndEdit);
            this.moviesDgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.moviesDgv_DataError);
            this.moviesDgv.SelectionChanged += new System.EventHandler(this.moviesDgv_SelectionChanged);
            // 
            // moviesContextMenu
            // 
            this.moviesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.moviesContextMenu.Name = "moviesContextMenu";
            this.moviesContextMenu.Size = new System.Drawing.Size(168, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.toolStripMenuItem1.Text = "Добавить фильм";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchBox,
            this.clientsMenuItem,
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
            // clientsMenuItem
            // 
            this.clientsMenuItem.Name = "clientsMenuItem";
            this.clientsMenuItem.Size = new System.Drawing.Size(67, 23);
            this.clientsMenuItem.Text = "Клиенты";
            // 
            // importMoviesMenuItem
            // 
            this.importMoviesMenuItem.Name = "importMoviesMenuItem";
            this.importMoviesMenuItem.Size = new System.Drawing.Size(117, 23);
            this.importMoviesMenuItem.Text = "Импорт фильмов";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 449);
            this.Controls.Add(this.leasingsDgv);
            this.Controls.Add(this.copiesDgv);
            this.Controls.Add(this.moviesDgv);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(1234, 488);
            this.Name = "MainForm";
            this.Text = "Видеопрокат";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.copiesDgv)).EndInit();
            this.copiesContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leasingsDgv)).EndInit();
            this.leasingContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.moviesDgv)).EndInit();
            this.moviesContextMenu.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem clientsMenuItem;
        private System.Windows.Forms.ContextMenuStrip leasingContextMenu;
        private System.Windows.Forms.ToolStripMenuItem returnItem;
        private System.Windows.Forms.ToolStripMenuItem newCopyItem;
        private System.Windows.Forms.ContextMenuStrip moviesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

