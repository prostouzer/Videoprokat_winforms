using System;
using System.Collections.Generic;
using System.IO;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    public class ImportMoviesPresenter
    {
        private readonly IImportMoviesView _importMoviesView;
        private VideoprokatContext _context;

        public ImportMoviesPresenter(IImportMoviesView view)
        {
            _importMoviesView = view;
        }

        public void Run(VideoprokatContext context)
        {
            _context = context;

            _importMoviesView.OnSelectNewFile += SelectNewFile;
            _importMoviesView.OnUploadMovies += UploadMovies;

            _importMoviesView.Show();
        }

        public void SelectNewFile()
        {
            var path = _importMoviesView.ChooseFilePath();
            ExtractMoviesFromFile(path);

            _importMoviesView.RedrawMovies(_moviesList);
        }
        public void UploadMovies()
        {
            if (!_importMoviesView.ConfirmUploadMovies()) return;
            foreach (var movie in _moviesList)
            {
                _context.MoviesOriginal.Add(movie);
            }

            _context.SaveChanges();
            _importMoviesView.Close();
        }

        private readonly List<MovieOriginal> _moviesList = new List<MovieOriginal>();
        private void ExtractMoviesFromFile(string path)
        {
            _moviesList.Clear();
            var abort = false;
            if (path.Trim() == "") return;
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream && !abort)
            {
                var line = reader.ReadLine();
                var movies = line.Split(';');
                foreach (var movie in movies)
                {
                    if (movie == "") continue;
                    var movieValues = movie.Split(",,");

                    try
                    {
                        var title = movieValues[0];
                        var description = movieValues[1];
                        var yearReleased = Convert.ToInt32(movieValues[2]);

                        var newMovie = new MovieOriginal(title, description, yearReleased);

                        _moviesList.Add(newMovie);
                    }
                    catch
                    {
                        if (_importMoviesView.SkipWronglyDeclaredMovie(movieValues)) continue;
                        abort = true;
                        break;
                    }
                }
            }
            if (abort)
            {
                _moviesList.Clear();
            }
        }
    }
}
