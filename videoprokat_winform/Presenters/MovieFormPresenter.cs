using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    class MovieFormPresenter : IPresenter
    {
        private IMovieFormView _movieFormView;
        public VideoprokatContext _context;
        public void Run(VideoprokatContext context)
        {
            _movieFormView = new MovieForm();
            _movieFormView.Show();
            _context = context;

            _movieFormView.OnAddMovie += AddMovie;
        }

        public void AddMovie(MovieOriginal movie)
        {
            if (_movieFormView.ConfirmNewMovie())
            {
                _context.MoviesOriginal.Add(movie);
                _context.SaveChanges();
                _movieFormView.Close();
            }
        }

        public void Run()
        {
            _movieFormView = new MovieForm();
            _movieFormView.Show();

            _movieFormView.OnAddMovie += AddMovie;
        }
    }
}
