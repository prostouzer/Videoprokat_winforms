using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    class MovieCopyPresenter : IPresenter
    {
        private IMovieCopyView _movieCopyView;
        public VideoprokatContext _context;
        public void Run()
        {
            _movieCopyView = new MovieCopyForm();
            _movieCopyView.OnAddMovieCopy += AddMovieCopy;
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
