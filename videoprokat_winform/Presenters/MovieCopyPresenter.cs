using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    class MovieCopyPresenter
    {
        private IMovieCopyView _movieCopyView;
        public VideoprokatContext _context;
        public void Run(MovieOriginal movie)
        {
            _movieCopyView = new MovieCopyForm();

            _movieCopyView.CurrentMovie = movie;

            _movieCopyView.OnAddMovieCopy += AddMovieCopy;

            _movieCopyView.Show();
        }

        public void AddMovieCopy(MovieCopy movieCopy)
        {
            if (_movieCopyView.ConfirmNewMovieCopy())
            {
                _context.MoviesCopies.Add(movieCopy);
                _context.SaveChanges();

                _movieCopyView.Close();
            }
        }
    }
}
