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
    class MainPresenter : IPresenter
    {
        private readonly IMainView _mainView;
        private readonly MoviePresenter _moviePresenter;
        private readonly VideoprokatContext _context = new VideoprokatContext();

        public MainPresenter(IMainView mainView, MoviePresenter moviePresenter)
        {
            _mainView = mainView;
            _moviePresenter = moviePresenter;

            _mainView.OnLoad += LoadMain;

            _mainView.OnOpenMovie += OpenMovie;
            _mainView.OnOpenMovieCopy += OpenMovieCopy;

            _mainView.OnFilterMovies += FilterMovies;
            _mainView.OnUpdateMovie += UpdateMovie;
        }

        public void Run()
        {
            _mainView.Show();
        }

        public void OpenMovie()
        {
            _moviePresenter._context = _context;
            _moviePresenter.Run();
            _mainView.RedrawMoviesDgv(_context.MoviesOriginal.ToList());
        }

        public void OpenMovieCopy()
        {

        }

        public void LoadMain()
        {
            _context.MoviesOriginal.Load();
            _mainView.RedrawMoviesDgv(_context.MoviesOriginal.ToList());
        }

        public void FilterMovies(string filter)
        {
            List<MovieOriginal> filteredMovies = _context.MoviesOriginal.Where(m => m.Title.Contains(filter)).ToList();
            if (filteredMovies.Count() > 0 && filter != "")
            {
                _mainView.RedrawMoviesDgv(filteredMovies);
            }
            else
            {
                _mainView.RedrawMoviesDgv(_context.MoviesOriginal.ToList());
            }
        }
        public void UpdateMovie(int movieId, MovieOriginal updatedMovie)
        {
            MovieOriginal movie = _context.MoviesOriginal.First(m => m.Id == movieId);
            movie = updatedMovie;
            _context.SaveChanges(); // надо ли?

            _mainView.RedrawMoviesDgv(_context.MoviesOriginal.ToList());
        }

        //public void OpenImportMoviesForm(object sender, EventArgs e)
        //{
        //    ImportMoviesForm form = new ImportMoviesForm(_context);
        //    form.ShowDialog();
        //    RedrawMoviesDgv();
        //}

        //public void OpenLeasingForm(object sender, EventArgs e)
        //{
        //    int currentCopyId = Convert.ToInt32(_mainView.CopiesDgv.CurrentRow.Cells["Id"].Value);
        //    MovieCopy movieCopy = _context.MoviesCopies.First(c => c.Id == currentCopyId);
        //    LeasingForm form = new LeasingForm(_context, movieCopy);
        //    form.ShowDialog();
        //    RedrawCopiesDgv();
        //    RedrawLeasingsDgv();
        //}
        //public void OpenReturnForm(object sender, EventArgs e)
        //{
        //    int currentLeasingId = Convert.ToInt32(_mainView.LeasingsDgv.CurrentRow.Cells["Id"].Value);
        //    Leasing leasing = _context.LeasedCopies.First(l => l.Id == currentLeasingId);
        //    ReturnForm form = new ReturnForm(_context, leasing);
        //    form.ShowDialog();
        //    RedrawCopiesDgv();
        //    RedrawLeasingsDgv();
        //}

        //public void OpenMovieCopyForm()
        //{
        //    int currentMovieId = Convert.ToInt32(_mainView.MoviesDgv.CurrentRow.Cells["Id"].Value);
        //    MovieOriginal movie = _context.MoviesOriginal.First(m => m.Id == currentMovieId);
        //    MovieCopyForm form = new MovieCopyForm(_context, movie);
        //    form.ShowDialog();
        //    RedrawCopiesDgv();
    }
}
