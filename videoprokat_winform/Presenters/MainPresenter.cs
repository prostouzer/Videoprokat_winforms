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
        private readonly MovieCopyPresenter _movieCopyPresenter;
        private readonly VideoprokatContext _context = new VideoprokatContext();

        public MainPresenter(IMainView mainView, MoviePresenter moviePresenter, MovieCopyPresenter movieCopyPresenter)
        {
            _mainView = mainView;
            _moviePresenter = moviePresenter;
            _movieCopyPresenter = movieCopyPresenter;

            _mainView.OnLoad += LoadMain;

            _mainView.OnOpenMovie += OpenMovie;
            _mainView.OnOpenMovieCopy += OpenMovieCopy;

            _mainView.OnFilterMovies += FilterMovies;

            _mainView.OnMovieSelectionChanged += MovieSelectionChanged;
            _mainView.OnMovieCopySelectionChanged += MovieCopySelectionChanged;
        }

        public void Run()
        {
            _mainView.Show();
        }

        public void OpenMovie()
        {
            _moviePresenter._context = _context;
            _moviePresenter.Run();
            _mainView.RedrawMovies(_context.MoviesOriginal.ToList());
        }

        public void OpenMovieCopy(int movieId)
        {
            var movie = _context.MoviesOriginal.First(m => m.Id == movieId);
            _movieCopyPresenter._context = _context;
            _movieCopyPresenter.Run(movie);

            List<MovieCopy> copiesList = _context.MoviesCopies.Where(c => c.MovieId == _mainView.CurrentMovieId).ToList();
            _mainView.RedrawCopies(copiesList);
        }

        public void LoadMain()
        {
            List<MovieOriginal> moviesList = _context.MoviesOriginal.ToList();
            if (moviesList.Count>0) _mainView.RedrawMovies(_context.MoviesOriginal.ToList());

            _mainView.OnUpdateMovie += UpdateMovie;
            _mainView.OnUpdateMovieCopy += UpdateMovieCopy; // из конструктора выдает ошибку null reference
        }

        public void FilterMovies(string filter)
        {
            List<MovieOriginal> filteredMovies = _context.MoviesOriginal.Where(m => m.Title.Contains(filter)).ToList();
            if (filteredMovies.Count() > 0 && filter != "")
            {
                _mainView.RedrawMovies(filteredMovies);
            }
            else
            {
                _mainView.RedrawMovies(_context.MoviesOriginal.ToList());
            }
        }

        public void UpdateMovie(int movieId, MovieOriginal updatedMovie)
        {
            MovieOriginal movie = _context.MoviesOriginal.First(m => m.Id == movieId);
            movie = updatedMovie;
            _context.SaveChanges();
            _mainView.RedrawMovies(_context.MoviesOriginal.ToList());
        }

        public void UpdateMovieCopy(int movieCopyId, MovieCopy updatedMovieCopy)
        {
            MovieCopy copy = _context.MoviesCopies.First(c => c.Id == movieCopyId);
            copy = updatedMovieCopy;
            _context.SaveChanges();

            List<MovieCopy> copiesList = _context.MoviesCopies.Where(c => c.MovieId == copy.MovieId).ToList();
            _mainView.RedrawCopies(copiesList);
        }

        public void MovieSelectionChanged(int movieId) // Поменяли фильм, отрисовываем его копии
        {
            List<MovieCopy> movieCopies = _context.MoviesCopies.Where(c => c.MovieId == movieId).ToList();
            if (movieCopies.Count>0) _mainView.RedrawCopies(movieCopies);
        }
        public void MovieCopySelectionChanged(int movieCopyId) // Поменяли копию, отрисовываем ее аренды (прокаты)
        {
            List<Leasing> leasings = _context.LeasedCopies.Where(l => l.MovieCopyId == movieCopyId).ToList();
            if (leasings.Count>0) _mainView.RedrawLeasings(leasings);
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
