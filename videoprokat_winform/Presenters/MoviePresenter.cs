using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    class MoviePresenter : IPresenter
    {
        private IMovieView _movieView;
        public VideoprokatContext _context;

        //public void Run(VideoprokatContext context)
        //{
        //    _movieView = new MovieForm();
        //    _movieView.Show();
        //    _context = context;

        //    _movieView.OnAddMovie += AddMovie;
        //}

        public void AddMovie(MovieOriginal movie)
        {
            if (_movieView.ConfirmNewMovie())
            {
                _context.MoviesOriginal.Add(movie);
                _context.SaveChanges();
                _movieView.Close();
            }
        }

        public void Run()
        {
            _movieView = new MovieForm();
            _movieView.OnAddMovie += AddMovie;

            _movieView.Show();
        }
    }
}
