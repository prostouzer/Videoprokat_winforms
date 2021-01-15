using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    public class ImportMoviesPresenter
    {
        private IImportMoviesView _importMoviesView;
        public VideoprokatContext _context;
        public void Run()
        {
            _importMoviesView = new ImportMoviesForm();

            _importMoviesView.OnSelectNewFile += SelectNewFile;
            _importMoviesView.OnUploadMovies += UploadMovies;

            _importMoviesView.Show();
        }

        public void SelectNewFile()
        {
            string path = _importMoviesView.ChooseFilePath();
            ExtractMoviesFromFile(path);

            _importMoviesView.RedrawMovies(moviesList);
        }
        public void UploadMovies()
        {
            if (_importMoviesView.ConfirmUploadMovies())
            {
                foreach (var movie in moviesList)
                {
                    _context.MoviesOriginal.Add(movie);
                }

                _context.SaveChanges();
                _importMoviesView.Close();
            }
        }

        List<MovieOriginal> moviesList = new List<MovieOriginal>();
        private void ExtractMoviesFromFile(string path)
        {
            moviesList.Clear();
            bool abort = false;
            if (path.Trim() != "")
            {
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream && !abort)
                    {
                        var line = reader.ReadLine();
                        var movies = line.Split(';');
                        foreach (string movie in movies)
                        {
                            if (movie != "")
                            {
                                string[] movieValues = movie.Split(",,");

                                try
                                {
                                    string title = movieValues[0];
                                    string description = movieValues[1];
                                    int yearReleased = Convert.ToInt32(movieValues[2]);

                                    MovieOriginal newMovie = new MovieOriginal(title, description, yearReleased);

                                    moviesList.Add(newMovie);
                                }
                                catch
                                {
                                    if (!_importMoviesView.SkipWronglyDeclaredMovie(movieValues)) // неправильно объявлен фильм, отменяем передачу...
                                    {
                                        abort = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (abort == true)
                    {
                        moviesList.Clear();
                    }
                }
            }
        }
    }
}
