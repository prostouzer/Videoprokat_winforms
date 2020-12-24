using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using videoprokat_winform.Views;
using videoprokat_winform.Models;
using System.Linq;
using System.Data.Entity;
using System.Windows.Forms;

namespace videoprokat_winform.Presenters
{
    class MainFormPresenter : IPresenter
    {
        private readonly IMainFormView _view;
        public VideoprokatContext db = new VideoprokatContext();
        public MainFormPresenter(IMainFormView view)
        {
            _view = view;
            //_view.FilterMovies += () => FilterMovies();
            //_view.OpenCustomersForm += () => OpenCustomersForm();
            //_view.OpenImportMoviesForm += () => OpenImportMoviesForm();

            //_view.OpenLeasingForm += () => OpenLeasingForm();
            //_view.OpenReturnForm += () => OpenReturnForm();
            //_view.OpenMovieForm += () => OpenMovieForm();
            //_view.OpenMovieCopyForm += () => OpenMovieCopyForm();

            //_view.RedrawMoviesDgv += () => RedrawMoviesDgv();
            //_view.RedrawCopiesDgv += () => RedrawCopiesDgv();
            //_view.RedrawLeasingsDgv += () => RedrawLeasingsDgv();

        }
        public void Run()
        {
            _view.Show();
        }

        public void FilterMovies(object sender, EventArgs e)
        {
            var filteredData = db.MoviesOriginal.Where(m => m.Title.Contains(_view.filter)).ToList();
            if (filteredData.Count() > 0 && _view.filter != "")
            {
                _view.MoviesDgv.DataSource = filteredData;
            }
            else
            {
                RedrawMoviesDgv();
            }
        }
        public void OpenCustomersForm(object sender, EventArgs e)
        {
            CustomersForm form = new CustomersForm(db);
            form.ShowDialog();
        }

        public void OpenImportMoviesForm(object sender, EventArgs e)
        {
            ImportMoviesForm form = new ImportMoviesForm(db);
            form.ShowDialog();
            RedrawMoviesDgv();
        }

        public void OpenLeasingForm(object sender, EventArgs e)
        {
            int currentCopyId = Convert.ToInt32(_view.CopiesDgv.CurrentRow.Cells["Id"].Value);
            MovieCopy movieCopy = db.MoviesCopies.First(c => c.Id == currentCopyId);
            LeasingForm form = new LeasingForm(db, movieCopy);
            form.ShowDialog();
            RedrawCopiesDgv();
            RedrawLeasingsDgv();
        }
        public void OpenReturnForm(object sender, EventArgs e)
        {
            int currentLeasingId = Convert.ToInt32(_view.LeasingsDgv.CurrentRow.Cells["Id"].Value);
            Leasing leasing = db.LeasedCopies.First(l => l.Id == currentLeasingId);
            ReturnForm form = new ReturnForm(db, leasing);
            form.ShowDialog();
            RedrawCopiesDgv();
            RedrawLeasingsDgv();
        }

        public void OpenMovieForm()
        {
            MovieForm form = new MovieForm(db);
            form.ShowDialog();
            RedrawMoviesDgv();
        }

        public void OpenMovieCopyForm()
        {
            int currentMovieId = Convert.ToInt32(_view.MoviesDgv.CurrentRow.Cells["Id"].Value);
            MovieOriginal movie = db.MoviesOriginal.First(m => m.Id == currentMovieId);
            MovieCopyForm form = new MovieCopyForm(db, movie);
            form.ShowDialog();
            RedrawCopiesDgv();
        }
        public void RedrawMoviesDgv()
        {
            db.MoviesOriginal.Load();
            _view.MoviesDgv.DataSource = db.MoviesOriginal.Local.ToBindingList();
            _view.MoviesDgv.Columns["Id"].ReadOnly = true;
            _view.MoviesDgv.Columns["Copies"].Visible = false;

            _view.MoviesDgv.Sort(_view.MoviesDgv.Columns["Title"], ListSortDirection.Ascending);

            _view.MoviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            _view.MoviesDgv.Columns["Id"].HeaderText = "ID";
            _view.MoviesDgv.Columns["Title"].HeaderText = "Название";
            _view.MoviesDgv.Columns["Description"].HeaderText = "Описание";
            _view.MoviesDgv.Columns["YearReleased"].HeaderText = "Год выпуска";

            _view.MoviesDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _view.MoviesDgv.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _view.MoviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _view.MoviesDgv.Columns["YearReleased"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        public void RedrawCopiesDgv()
        {
            int currentMovieId = Convert.ToInt32(_view.MoviesDgv.CurrentRow.Cells["Id"].Value);

            var movieCopies = (from r in db.MoviesCopies
                where r.MovieId == currentMovieId
                select r).ToList();

            _view.CopiesDgv.DataSource = movieCopies;

            _view.CopiesDgv.Columns["Id"].ReadOnly = true;
            _view.CopiesDgv.Columns["Available"].ReadOnly = true;
            _view.CopiesDgv.Columns["MovieId"].Visible = false;
            _view.CopiesDgv.Columns["Movie"].Visible = false;

            _view.CopiesDgv.Columns["Id"].HeaderText = "ID";
            _view.CopiesDgv.Columns["Commentary"].HeaderText = "Комментарий";
            _view.CopiesDgv.Columns["Available"].HeaderText = "Доступен";
            _view.CopiesDgv.Columns["PricePerDay"].HeaderText = "Цена/день";

            _view.CopiesDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _view.CopiesDgv.Columns["Commentary"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _view.CopiesDgv.Columns["Available"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            _view.CopiesDgv.Columns["PricePerDay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        public void RedrawLeasingsDgv()
        {
            int CurrentMovieCopyId = Convert.ToInt32(_view.CopiesDgv.CurrentRow.Cells["Id"].Value);

            var movieCopyLeasingInfo = from leasing in db.LeasedCopies
                where leasing.MovieCopyId == CurrentMovieCopyId && leasing.ReturnDate == null
                join customer in db.Customers on leasing.CustomerId equals customer.Id
                select new
                {
                    id = leasing.Id,
                    startDate = leasing.LeasingStartDate,
                    expectedEndDate = leasing.LeasingExpectedEndDate,
                    totalPrice = leasing.TotalPrice,
                    customerName = customer.Name
                };

            _view.LeasingsDgv.DataSource = movieCopyLeasingInfo.ToList();
            _view.LeasingsDgv.Columns["Id"].Visible = false;

            _view.LeasingsDgv.Columns["startDate"].HeaderText = "Дата начала";
            _view.LeasingsDgv.Columns["expectedEndDate"].HeaderText = "Ожидаемый возврат";
            _view.LeasingsDgv.Columns["totalPrice"].HeaderText = "Итоговая цена";
            _view.LeasingsDgv.Columns["customerName"].HeaderText = "Клиент";

            _view.LeasingsDgv.Columns["customerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
