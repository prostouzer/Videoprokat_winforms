using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters.Implementation
{
    public class MoviePresenter : IMoviePresenter
    {
        private readonly IMovieView _movieView;
        private VideoprokatContext _context;

        public MoviePresenter(IMovieView view, VideoprokatContext context)
        {
            _movieView = view;
            _context = context;
        }

        public void Run()
        {
            _movieView.OnAddMovie += AddMovie;

            _movieView.Show();
        }

        public void AddMovie(MovieOriginal movie)
        {
            if (!_movieView.ConfirmNewMovie()) return;
            _context.MoviesOriginal.Add(movie);
            _context.SaveChanges();
            _movieView.Close();
        }
    }
}
