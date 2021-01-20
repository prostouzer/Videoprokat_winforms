using System.Collections.Generic;
using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Presenters;
using videoprokat_winform.Presenters.Implementation;
using videoprokat_winform.Views;

namespace videoprokat_winform.Tests.Presenters
{
    [TestFixture]
    public class ImportMoviesPresenterTests
    {
        private IImportMoviesView _view;
        private IVideoprokatContext _context;
        private IImportMoviesPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IVideoprokatContext>();
            _view = Substitute.For<IImportMoviesView>();
            _presenter = new ImportMoviesPresenter(_view, _context);
        }

        [Test]
        public void ImportMoviesRun()
        {
            //arrange


            //act
            _presenter.Run();

            //assert
            _view.Received().Show();
        }

        [Test]
        public void SelectNewFile()
        {
            //arrange


            //act
            _presenter.SelectNewFile();

            //assert
            _view.Received().ChooseFilePath();
            _view.Received().RedrawMovies(Arg.Any<List<MovieOriginal>>());
            // а можно еще проверить что presenter.ExtractMoviesFromFile(Arg.Any<string>) ?
        }

        [Test]
        public void MoviesUpload_Confirmed()
        {
            //arrange
            _view.ConfirmUploadMovies().Returns(true);
            var movies = Substitute.For<DbSet<MovieOriginal>>();
            _context.MoviesOriginal.Returns(movies);
            var testMovie = new MovieOriginal("TEST TITLE", "TEST DESCR", 9999);
            _presenter.MoviesList.Add(testMovie); // добавляю фильм чтобы был хотя бы один в MoviesList (для цикла foreach)

            //act
            _presenter.UploadMovies();

            //assert
            _context.MoviesOriginal.Received().Add(Arg.Any<MovieOriginal>());
            _context.Received().SaveChanges();
            _view.Received().Close();
        }

        [Test]
        public void MoviesUpload_NotConfirmed()
        {
            //arrange
            _view.ConfirmUploadMovies().Returns(false);
            var testMovie = new MovieOriginal("TEST TITLE", "TEST DESCR", 9999);
            _presenter.MoviesList.Add(testMovie);

            //act
            _presenter.UploadMovies();

            //assert
            _context.MoviesOriginal.DidNotReceive().Add(Arg.Any<MovieOriginal>());
            _context.DidNotReceive().SaveChanges();
            _view.DidNotReceive().Close();
        }

        [Test]
        public void ExtractMoviesFromFile_ValidData()
        {
            //arrange
            const string path = "..\\..\\..\\ImportMoviesExamples\\пример правильных фильмов №1.txt";
            var expected = new List<MovieOriginal>
            {
                new MovieOriginal("имя1", "название1", 2001),
                new MovieOriginal("имя2", "название2", 2002),
                new MovieOriginal("имя3", "название3", 2003),
                new MovieOriginal("имя4", "название4", 2004)

            };

            //act
            _presenter.ExtractMoviesFromFile(path);
            var actual = _presenter.MoviesList;

            //assert
            Assert.AreEqual(expected[0].Title, actual[0].Title);
            Assert.AreEqual(expected[0].Description, actual[0].Description);
            Assert.AreEqual(expected[0].YearReleased, actual[0].YearReleased);

            Assert.AreEqual(expected[1].Title, actual[1].Title);
            Assert.AreEqual(expected[1].Description, actual[1].Description);
            Assert.AreEqual(expected[1].YearReleased, actual[1].YearReleased);

            Assert.AreEqual(expected[2].Title, actual[2].Title);
            Assert.AreEqual(expected[2].Description, actual[2].Description);
            Assert.AreEqual(expected[2].YearReleased, actual[2].YearReleased);

            Assert.AreEqual(expected[3].Title, actual[3].Title);
            Assert.AreEqual(expected[3].Description, actual[3].Description);
            Assert.AreEqual(expected[3].YearReleased, actual[3].YearReleased);
        }

        [Test]
        public void ExtractMoviesFromFile_InvalidData()
        {
            //arrange
            var path = new string("..\\..\\..\\ImportMoviesExamples\\пример неправильных фильмов.txt");

            //act
            _presenter.ExtractMoviesFromFile(path);

            //assert
            _view.Received().SkipWronglyDeclaredMovie(Arg.Any<string[]>());
        }
    }
}
