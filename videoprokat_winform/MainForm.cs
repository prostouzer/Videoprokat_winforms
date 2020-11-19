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
                                                { r.LeasingStartDate, r.LeasingExpectedEndDate, r.Owner.Name }).ToList();
                    movieCopyLeasingDataGridView.DataSource = movieCopyLeasingInfo;
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            using (db = new VideoprokatContext())
            {
                MovieCopy moviecopy = new MovieCopy { Id= 1, Available = true, MovieId = 4, Commentary = "Хорошее качество", PricePerDay = 80 };
                MovieCopy moviecopy2 = new MovieCopy { Id=2, Available = true, MovieId = 4, Commentary = "Плохое качество(((", PricePerDay = 50 };

                db.MoviesCopies.Add(moviecopy);
                db.MoviesCopies.Add(moviecopy2);

                db.SaveChanges();
            }
            //using (db = new VideoprokatContext())
            //{
            //    Client client = new Client { Name = "Петрович" };
            //    MovieCopy copy = db.MoviesCopies.FirstOrDefault(c => c.Id == 5);
            //    Leasing leasing = new Leasing(copy, client);
            //    leasing.LeasingStartDate = Convert.ToDateTime("19.11.2020");
            //    leasing.LeasingEndDate = Convert.ToDateTime("22.11.2020");
            //    db.LeasedCopies.Add(leasing);

            //    db.SaveChanges();
            //}
        }
    }
}
