using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    public class MoviePresenter 
    {
        private readonly IMovieView _movieView;
        private VideoprokatContext _context;

        public MoviePresenter(IMovieView view)
        {
            _movieView = view;
        }

        public void Run(VideoprokatContext context)
        {
            _context = context;

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
