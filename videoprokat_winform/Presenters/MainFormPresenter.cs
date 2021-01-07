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
        private readonly IMainFormView _mainFormView;
        private readonly MovieFormPresenter _movieFormPresenter;
        private readonly VideoprokatContext _context = new VideoprokatContext();
        public MainFormPresenter(IMainFormView mainFormView, MovieFormPresenter movieFormPresenter)
        {
            _mainFormView = mainFormView;
            _movieFormPresenter = movieFormPresenter;

            _mainFormView.OnOpenMovieForm += OpenMovieForm;
            _mainFormView.OnFormLoad += LoadMainForm;

            _mainFormView.OnFilterMovies += FilterMovies;
        }

        public void Run()
        {
            _mainFormView.Show();
        }
        public void OpenMovieForm()
        {
            _movieFormPresenter.Run(_context);
            _mainFormView.RedrawMoviesDgv(_context.MoviesOriginal.ToList());
        }
        public void LoadMainForm()
        {
            _context.MoviesOriginal.Load();
            _mainFormView.RedrawMoviesDgv(_context.MoviesOriginal.ToList());
        }
        public void FilterMovies(string filter)
        {
            List<MovieOriginal> filteredMovies = _context.MoviesOriginal.Where(m => m.Title.Contains(filter)).ToList();
            if (filteredMovies.Count() > 0 && filter != "")
            {
                _mainFormView.RedrawMoviesDgv(filteredMovies);
            }
            else
            {
                _mainFormView.RedrawMoviesDgv(_context.MoviesOriginal.ToList());
            }
        }

        //public void FilterMovies(object sender, EventArgs e)
        //{
        //    var filteredData = _context.MoviesOriginal.Where(m => m.Title.Contains(_mainFormView.filter)).ToList();
        //    if (filteredData.Count() > 0 && _mainFormView.filter != "")
        //    {
        //        _mainFormView.MoviesDgv.DataSource = filteredData;
        //    }
        //    else
        //    {
        //        RedrawMoviesDgv();
        //    }
        //}
        //public void OpenCustomersForm(object sender, EventArgs e)
        //{
        //    CustomersForm form = new CustomersForm(_context);
        //    form.ShowDialog();
        //}

        //public void OpenImportMoviesForm(object sender, EventArgs e)
        //{
        //    ImportMoviesForm form = new ImportMoviesForm(_context);
        //    form.ShowDialog();
        //    RedrawMoviesDgv();
        //}

        //public void OpenLeasingForm(object sender, EventArgs e)
        //{
        //    int currentCopyId = Convert.ToInt32(_mainFormView.CopiesDgv.CurrentRow.Cells["Id"].Value);
        //    MovieCopy movieCopy = _context.MoviesCopies.First(c => c.Id == currentCopyId);
        //    LeasingForm form = new LeasingForm(_context, movieCopy);
        //    form.ShowDialog();
        //    RedrawCopiesDgv();
        //    RedrawLeasingsDgv();
        //}
        //public void OpenReturnForm(object sender, EventArgs e)
        //{
        //    int currentLeasingId = Convert.ToInt32(_mainFormView.LeasingsDgv.CurrentRow.Cells["Id"].Value);
        //    Leasing leasing = _context.LeasedCopies.First(l => l.Id == currentLeasingId);
        //    ReturnForm form = new ReturnForm(_context, leasing);
        //    form.ShowDialog();
        //    RedrawCopiesDgv();
        //    RedrawLeasingsDgv();
        //}

        //public void OpenMovieCopyForm()
        //{
        //    int currentMovieId = Convert.ToInt32(_mainFormView.MoviesDgv.CurrentRow.Cells["Id"].Value);
        //    MovieOriginal movie = _context.MoviesOriginal.First(m => m.Id == currentMovieId);
        //    MovieCopyForm form = new MovieCopyForm(_context, movie);
        //    form.ShowDialog();
        //    RedrawCopiesDgv();
    }
}
