using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    public class MovieCopyPresenter
    {
        private IMovieCopyView _movieCopyView;
        private VideoprokatContext _context;
        public void Run(MovieOriginal movie, VideoprokatContext context)
        {
            _context = context;

            _movieCopyView = new MovieCopyForm {CurrentMovie = movie};

            _movieCopyView.OnAddMovieCopy += AddMovieCopy;

            _movieCopyView.Show();
        }

        public void AddMovieCopy(MovieCopy movieCopy)
        {
            if (!_movieCopyView.ConfirmNewMovieCopy()) return;
            _context.MoviesCopies.Add(movieCopy);
            _context.SaveChanges();

            _movieCopyView.Close();
        }
    }
}
