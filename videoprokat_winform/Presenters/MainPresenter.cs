using System.Collections.Generic;
using videoprokat_winform.Views;
using videoprokat_winform.Models;
using System.Linq;
using videoprokat_winform.Contexts;

namespace videoprokat_winform.Presenters
{
    public class MainPresenter : IPresenter
    {
        private readonly VideoprokatContext _context = new VideoprokatContext();

        private readonly IMainView _mainView;
        private readonly MoviePresenter _moviePresenter;
        private readonly MovieCopyPresenter _movieCopyPresenter;
        private readonly LeasingPresenter _leasingPresenter;
        private readonly CustomersPresenter _customersPresenter;
        private readonly ImportMoviesPresenter _importMoviesPresenter;
        private readonly ReturnPresenter _returnPresenter;
        public MainPresenter(IMainView mainView, MoviePresenter moviePresenter, MovieCopyPresenter movieCopyPresenter, LeasingPresenter leasingPresenter, CustomersPresenter customersPresenter,
            ImportMoviesPresenter importMoviesPresenter, ReturnPresenter returnPresenter)
        {
            _mainView = mainView;
            _moviePresenter = moviePresenter;
            _movieCopyPresenter = movieCopyPresenter;
            _leasingPresenter = leasingPresenter;
            _customersPresenter = customersPresenter;
            _importMoviesPresenter = importMoviesPresenter;
            _returnPresenter = returnPresenter;

            _mainView.OnLoad += LoadMain;

            _mainView.OnOpenCustomers += OpenCustomers;
            _mainView.OnOpenMovie += OpenMovie;
            _mainView.OnOpenMovieCopy += OpenMovieCopy;
            _mainView.OnOpenLeasing += OpenLeasing;
            _mainView.OnOpenImportMovies += OpenImportMovies;
            _mainView.OnOpenReturn += OpenReturn;

            _mainView.OnUpdateMovie += UpdateMovie;
            _mainView.OnUpdateMovieCopy += UpdateMovieCopy;

            _mainView.OnFilterMovies += FilterMovies;

            _mainView.OnMovieSelectionChanged += MovieSelectionChanged;
            _mainView.OnMovieCopySelectionChanged += MovieCopySelectionChanged;
        }

        public void Run()
        {
            _mainView.Show();
        }

        public void OpenCustomers()
        {
            _customersPresenter.Run(_context);
        }

        public void OpenMovie()
        {
            _moviePresenter.Run(_context);
            _mainView.RedrawMovies(_context.MoviesOriginal.ToList());
        }

        public void OpenMovieCopy(int movieId)
        {
            var movie = _context.MoviesOriginal.First(m => m.Id == movieId);
            _movieCopyPresenter.Run(movie, _context);

            List<MovieCopy> copiesList = _context.MoviesCopies.Where(c => c.MovieId == _mainView.CurrentMovieId).ToList();
            _mainView.RedrawCopies(copiesList);
        }

        public void OpenLeasing(int movieCopyId)
        {
            var movieCopy = _context.MoviesCopies.First(c => c.Id == movieCopyId);
            var movie = _context.MoviesOriginal.First(m => m.Id == movieCopy.MovieId);
            _leasingPresenter.Run(movie, movieCopy, _context);

            List<MovieCopy> movieCopies = _context.MoviesCopies.Where(c => c.MovieId == movie.Id).ToList();
            _mainView.RedrawCopies(movieCopies);

            List<Leasing> leasings = _context.LeasedCopies.Where(l => l.MovieCopyId == movieCopy.Id).ToList();
            List<Customer> customers = _context.Customers.ToList();
            _mainView.RedrawLeasings(leasings, customers);
        }

        public void OpenImportMovies()
        {
            _importMoviesPresenter.Run(_context);

            _mainView.RedrawMovies(_context.MoviesOriginal.ToList());
        }

        public void OpenReturn(int leasingId)
        {
            _returnPresenter.Run(leasingId, _context);

            List<MovieCopy> copiesList = _context.MoviesCopies.Where(c => c.MovieId == _mainView.CurrentMovieId).ToList();
            _mainView.RedrawCopies(copiesList);

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
            if (filteredMovies.Any() && filter != "")
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
            _mainView.RedrawCopies(movieCopies); 
        }

        public void MovieCopySelectionChanged(int movieCopyId) // Поменяли копию, отрисовываем ее аренды (прокаты)
        {
            List<Leasing> leasings = _context.LeasedCopies.Where(l => l.MovieCopyId == movieCopyId).ToList();
            List<Customer> customers = _context.Customers.ToList();
            _mainView.RedrawLeasings(leasings, customers);
        }
    }
}
