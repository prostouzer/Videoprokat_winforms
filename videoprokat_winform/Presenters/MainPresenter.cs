using videoprokat_winform.Views;
using videoprokat_winform.Models;
using System.Linq;
using videoprokat_winform.Contexts;

namespace videoprokat_winform.Presenters
{
    public class MainPresenter : IPresenter
    {
        private readonly VideoprokatContext _context;

        private readonly IMainView _mainView;
        private readonly IMoviePresenter _moviePresenter;
        private readonly IMovieCopyPresenter _movieCopyPresenter;
        private readonly ILeasingPresenter _leasingPresenter;
        private readonly ICustomersPresenter _customersPresenter;
        private readonly IImportMoviesPresenter _importMoviesPresenter;
        private readonly IReturnPresenter _returnPresenter;
        public MainPresenter(VideoprokatContext context, IMainView mainView, IMoviePresenter moviePresenter, IMovieCopyPresenter movieCopyPresenter, ILeasingPresenter leasingPresenter, ICustomersPresenter customersPresenter,
            IImportMoviesPresenter importMoviesPresenter, IReturnPresenter returnPresenter)
        {
            _context = context;
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
            _mainView.RedrawMovies(_context.MoviesOriginal);
        }

        public void OpenMovieCopy(int movieId)
        {
            var movie = _context.MoviesOriginal.Single(m => m.Id == movieId);
            _movieCopyPresenter.Run(movie, _context);

            var copiesList = _context.MoviesCopies.Where(c => c.MovieId == _mainView.CurrentMovieId);
            _mainView.RedrawCopies(copiesList);
        }

        public void OpenLeasing(int movieCopyId)
        {
            var movieCopy = _context.MoviesCopies.Single(c => c.Id == movieCopyId);
            var movie = _context.MoviesOriginal.Single(m => m.Id == movieCopy.MovieId);
            _leasingPresenter.Run(movie, movieCopy, _context);

            var movieCopies = _context.MoviesCopies.Where(c => c.MovieId == movie.Id);
            _mainView.RedrawCopies(movieCopies);

            var leasings = _context.LeasedCopies.Where(l => l.MovieCopyId == movieCopy.Id);
            var customers = _context.Customers;
            _mainView.RedrawLeasings(leasings, customers);
        }

        public void OpenImportMovies()
        {
            _importMoviesPresenter.Run(_context);

            _mainView.RedrawMovies(_context.MoviesOriginal);
        }

        public void OpenReturn(int leasingId)
        {
            _returnPresenter.Run(leasingId, _context);

            var copiesList = _context.MoviesCopies.Where(c => c.MovieId == _mainView.CurrentMovieId);
            _mainView.RedrawCopies(copiesList);

        }

        public void LoadMain()
        {
            var moviesList = _context.MoviesOriginal;
            if (moviesList.Any()) _mainView.RedrawMovies(_context.MoviesOriginal);

            _mainView.OnUpdateMovie += UpdateMovie;
            _mainView.OnUpdateMovieCopy += UpdateMovieCopy; // из конструктора выдает ошибку null reference
        }

        public void FilterMovies(string filter)
        {
            var filteredMovies = _context.MoviesOriginal.Where(m => m.Title.Contains(filter));
            if (filteredMovies.Any() && filter != "")
            {
                _mainView.RedrawMovies(filteredMovies);
            }
            else
            {
                _mainView.RedrawMovies(_context.MoviesOriginal);
            }
        }

        public void UpdateMovie(int movieId, MovieOriginal updatedMovie)
        {
            var movie = _context.MoviesOriginal.Single(m => m.Id == movieId);
            movie = updatedMovie;

            _context.SaveChanges();

            _mainView.RedrawMovies(_context.MoviesOriginal);
        }

        public void UpdateMovieCopy(int movieCopyId, MovieCopy updatedMovieCopy)
        {
            var copy = _context.MoviesCopies.Single(c => c.Id == movieCopyId);
            copy = updatedMovieCopy;

            _context.SaveChanges();

            var copiesList = _context.MoviesCopies.Where(c => c.MovieId == copy.MovieId);
            _mainView.RedrawCopies(copiesList);
        }

        public void MovieSelectionChanged(int movieId) // Поменяли фильм, отрисовываем его копии
        {
            var movieCopies = _context.MoviesCopies.Where(c => c.MovieId == movieId);
            _mainView.RedrawCopies(movieCopies); 
        }

        public void MovieCopySelectionChanged(int movieCopyId) // Поменяли копию, отрисовываем ее аренды (прокаты)
        {
            var leasings = _context.LeasedCopies.Where(l => l.MovieCopyId == movieCopyId);
            var customers = _context.Customers;
            _mainView.RedrawLeasings(leasings, customers);
        }
    }
}
