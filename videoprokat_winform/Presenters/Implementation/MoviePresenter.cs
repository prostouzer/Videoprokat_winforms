using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters.Implementation
{
    public class MoviePresenter : IMoviePresenter
    {
        private readonly IMovieView _movieView;
        private readonly IVideoprokatContext _context;

        public MoviePresenter(IMovieView view, IVideoprokatContext context)
        {
            _movieView = view;
            _context = context;

            _movieView.OnAddMovie += AddMovie;
        }

        public void Run()
        {
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
