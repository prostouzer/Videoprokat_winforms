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
        private readonly LeasingPresenter _leasingPresenter;
        private readonly VideoprokatContext _context = new VideoprokatContext();

        public MainPresenter(IMainView mainView, MoviePresenter moviePresenter, MovieCopyPresenter movieCopyPresenter, LeasingPresenter leasingPresenter)
        {
            _mainView = mainView;
            _moviePresenter = moviePresenter;
            _movieCopyPresenter = movieCopyPresenter;
            _leasingPresenter = leasingPresenter;

            _mainView.OnLoad += LoadMain;

            _mainView.OnUpdateMovie += UpdateMovie;

            _mainView.OnUpdateMovieCopy += UpdateMovieCopy; // из конструктора выдает ошибку null reference
            _mainView.OnOpenMovie += OpenMovie;
            _mainView.OnOpenMovieCopy += OpenMovieCopy;
            _mainView.OnOpenLeasing += OpenLeasing;

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

        public void OpenLeasing(int movieCopyId)
        {
            var movieCopy = _context.MoviesCopies.First(c => c.Id == movieCopyId);
            var movie = _context.MoviesOriginal.First(m => m.Id == movieCopy.MovieId);
            _leasingPresenter._context = _context;
            _leasingPresenter.Run(movie, movieCopy);

            List<MovieCopy> movieCopies = _context.MoviesCopies.Where(c => c.MovieId == movie.Id).ToList();
            _mainView.RedrawCopies(movieCopies);

            List<Leasing> leasings = _context.LeasedCopies.Where(l => l.MovieCopyId == movieCopy.Id).ToList();
            List<Customer> customers = _context.Customers.ToList();
            _mainView.RedrawLeasings(leasings, customers);
        }

        public void LoadMain()
        {
            List<MovieOriginal> moviesList = _context.MoviesOriginal.ToList();
            if (moviesList.Count > 0) _mainView.RedrawMovies(_context.MoviesOriginal.ToList());

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
            List<Customer> customers = _context.Customers.ToList();
            //if (leasings.Count>0) 
                _mainView.RedrawLeasings(leasings, customers);
        }

        //public void OpenImportMoviesForm(object sender, EventArgs e)
        //{
        //    ImportMoviesForm form = new ImportMoviesForm(_context);
        //    form.ShowDialog();
        //    RedrawMoviesDgv();
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
    }
}
