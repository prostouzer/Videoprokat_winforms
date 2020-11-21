using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using videoprokat_winform.Models;

namespace videoprokat_winform
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        VideoprokatContext db;
        private void MainForm_Load(object sender, EventArgs e)
        {
            db = new VideoprokatContext();
            db.MoviesOriginal.Load();

            moviesDataGridView.DataSource = db.MoviesOriginal.Local.ToBindingList();
            moviesDataGridView.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDataGridView.Columns["Copies"].Visible = false;

            moviesDataGridView.Columns["Id"].ReadOnly = true;
        }

        private void moviesDataGridView_SelectionChanged(object sender, EventArgs e) // из оригинального фильма загружаем в таблицу его копии
        {
            if (moviesDataGridView.SelectedCells.Count > 0)
            {
                int CurrentMovieId = Convert.ToInt32(moviesDataGridView.CurrentRow.Cells["Id"].Value);

                var movieCopies = (from r in db.MoviesCopies
                                   where r.MovieId == CurrentMovieId
                                   select r).ToList();
                
                movieCopiesDataGridView.DataSource = movieCopies;

                movieCopiesDataGridView.Columns["Id"].ReadOnly = true;
                movieCopiesDataGridView.Columns["MovieId"].Visible = false;
                movieCopiesDataGridView.Columns["Movie"].Visible = false;
            }
        }

        private void movieCopiesDataGridView_SelectionChanged(object sender, EventArgs e) // из копии фильма загружаем в таблицу инфу по аренде
        {
            if (movieCopiesDataGridView.SelectedCells.Count > 0)
            {
                int CurrentMovieCopyId = Convert.ToInt32(movieCopiesDataGridView.CurrentRow.Cells["Id"].Value);

                //var movieCopyLeasingInfo = (from r in db.LeasedCopies 
                //                            where r.MovieCopy.Id == CurrentMovieCopyId
                //                            select new
                //                            { r.LeasingStartDate, r.LeasingExpectedEndDate, r.Client.Name }).ToList(); // возвращает анонимный тип, который только read only - не поизменять(
                var movieCopyLeasingInfo = (from r in db.LeasedCopies
                                                where r.MovieCopy.Id == CurrentMovieCopyId
                                                select r).ToList();

                movieCopyLeasingDataGridView.DataSource = movieCopyLeasingInfo;
                movieCopyLeasingDataGridView.Columns["Id"].Visible = false;
                //movieCopyLeasingDataGridView.Columns["MovieId"].Visible = false;
                //movieCopyLeasingDataGridView.Columns["Movie"].Visible = false;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void moviesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }

        private void moviesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }

        private void movieCopiesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }

        private void movieCopiesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void movieCopyLeasingDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void movieCopyLeasingDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
