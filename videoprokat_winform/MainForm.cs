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
using System.Linq;

namespace videoprokat_winform
{
    public partial class MainForm : Form
    {
        VideoprokatContext db;
        public MainForm()
        {
            InitializeComponent();

            db = new VideoprokatContext();
            db.MoviesOriginal.Load();

            moviesDataGridView.DataSource = db.MoviesOriginal.Local.ToBindingList();
            moviesDataGridView.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDataGridView.Columns["Copies"].Visible = false;

            moviesDataGridView.Columns["Id"].ReadOnly = true;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void moviesDataGridView_SelectionChanged(object sender, EventArgs e) // из оригинального фильма загружаем в таблицу его копии
        {
            if (moviesDataGridView.SelectedCells.Count > 0)
            {
                int CurrentMovieId = Convert.ToInt32(moviesDataGridView.CurrentRow.Cells["Id"].Value);

                using (db = new VideoprokatContext())
                {
                    var movieCopies = (from r in db.MoviesCopies
                                       where r.MovieId == CurrentMovieId
                                       select r).ToList();
                    movieCopiesDataGridView.DataSource = movieCopies;
                    movieCopiesDataGridView.Columns["Id"].ReadOnly = true;
                }
            }
        }

        private void movieCopiesDataGridView_SelectionChanged(object sender, EventArgs e) // из копии фильма загружаем в таблицу инфу по аренде
        {
            if (movieCopiesDataGridView.SelectedCells.Count > 0)
            {
                int CurrentMovieCopyId = Convert.ToInt32(movieCopiesDataGridView.CurrentRow.Cells["Id"].Value);

                using (db = new VideoprokatContext())
                {
                    var movieCopyLeasingInfo = (from r in db.LeasedCopies
                                                where r.MovieCopy.Id == CurrentMovieCopyId
                                                select new
                                                { r.LeasingStartDate, r.LeasingExpectedEndDate, r.Client.Name }).ToList();
                    movieCopyLeasingDataGridView.DataSource = movieCopyLeasingInfo;
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void moviesDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int CurrentMovieId = Convert.ToInt32(moviesDataGridView.CurrentRow.Cells["Id"].Value);
            var CellValue = moviesDataGridView.CurrentCell.Value;
            using (db = new VideoprokatContext())
            {
                var result = db.MoviesOriginal.FirstOrDefault(a => a.Id == CurrentMovieId);
                if (result != null)
                {
                    result.Description = Convert.ToString(moviesDataGridView.CurrentRow.Cells["Description"].Value);
                    result.Title = Convert.ToString(moviesDataGridView.CurrentRow.Cells["Title"].Value);
                    result.YearReleased = Convert.ToInt32(moviesDataGridView.CurrentRow.Cells["YearReleased"].Value);
                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        private void moviesDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }
    }
}
