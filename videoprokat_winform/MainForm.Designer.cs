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
            this.moviesDgv = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.copiesDgv)).BeginInit();
            this.copiesContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leasingsDgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesDgv)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // copiesDgv
            // 
            this.copiesDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.copiesDgv.ContextMenuStrip = this.copiesContextMenu;
            this.copiesDgv.Location = new System.Drawing.Point(647, 27);
            this.copiesDgv.MultiSelect = false;
            this.copiesDgv.Name = "copiesDgv";
            this.copiesDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.copiesDgv.Size = new System.Drawing.Size(418, 248);
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
            this.leasingsDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.leasingsDgv.Location = new System.Drawing.Point(647, 281);
            this.leasingsDgv.MultiSelect = false;
            this.leasingsDgv.Name = "leasingsDgv";
            this.leasingsDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.leasingsDgv.Size = new System.Drawing.Size(418, 156);
            this.leasingsDgv.TabIndex = 2;
            this.leasingsDgv.Text = "dataGridView3";
            this.leasingsDgv.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.leasingsDgv_CellEndEdit);
            this.leasingsDgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.leasingsDgv_DataError);
            // 
            // moviesDgv
            // 
            this.moviesDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.Controls.Add(this.leasingsDgv);
            this.Controls.Add(this.copiesDgv);
            this.Controls.Add(this.moviesDgv);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Videoprokat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.copiesDgv)).EndInit();
            this.copiesContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leasingsDgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moviesDgv)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView moviesDgv;
        private System.Windows.Forms.DataGridView copiesDgv;
        private System.Windows.Forms.DataGridView leasingsDgv;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip copiesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem leasingItem;
    }
}

