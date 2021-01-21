using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters.Implementation
{
    public class ImportMoviesPresenter : IImportMoviesPresenter
    {
        private readonly IImportMoviesView _importMoviesView;
        private readonly IVideoprokatContext _context;

        public List<MovieOriginal> MoviesList { get; } = new List<MovieOriginal>(); // превратил в открытое свойство для ImportMoviesTests

        public ImportMoviesPresenter(IImportMoviesView view, IVideoprokatContext context)
        {
            _importMoviesView = view;
            _context = context;

            _importMoviesView.OnSelectNewFile += SelectNewFile;
            _importMoviesView.OnUploadMovies += UploadMovies;
        }

        public void Run()
        {
            _importMoviesView.Show();
        }

        public void SelectNewFile()
        {
            var path = _importMoviesView.ChooseFilePath();
            ExtractMoviesFromFile(path);

            _importMoviesView.RedrawMovies(MoviesList.AsQueryable());
        }
        public void UploadMovies()
        {
            if (!_importMoviesView.ConfirmUploadMovies()) return;
            foreach (var movie in MoviesList)
            {
                _context.MoviesOriginal.Add(movie);
            }

            _context.SaveChanges();
            _importMoviesView.Close();
        }

        public void ExtractMoviesFromFile(string path)
        {
            MoviesList.Clear();
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

                        MoviesList.Add(newMovie);
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
                MoviesList.Clear();
            }
        }
    }
}
