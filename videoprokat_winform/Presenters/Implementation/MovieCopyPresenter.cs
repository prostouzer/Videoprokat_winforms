using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters.Implementation
{
    public class MovieCopyPresenter : IMovieCopyPresenter
    {
        private readonly IMovieCopyView _movieCopyView;
        private VideoprokatContext _context;

        public MovieCopyPresenter(IMovieCopyView view, VideoprokatContext context)
        {
            _movieCopyView = view;
            _context = context;
        }

        public void Run(MovieOriginal movie)
        {
            _movieCopyView.CurrentMovie = movie;
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
