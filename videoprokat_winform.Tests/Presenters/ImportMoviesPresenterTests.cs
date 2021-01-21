using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        private VideoprokatContext _context;
        private IImportMoviesPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            var dbContextOptions = new DbContextOptionsBuilder<VideoprokatContext>().UseInMemoryDatabase("TestDb");
            _context = new VideoprokatContext(dbContextOptions.Options);
            _context.Database.EnsureDeleted(); // мне не нужны заполненные данные из OnModelCreating после EnsureCreated
            _view = Substitute.For<IImportMoviesView>();
            _presenter = new ImportMoviesPresenter(_view, _context);
        }

        [Test]
        public void Run()
        {
            //act
            _presenter.Run();

            //assert
            _view.Received().Show();
        }

        [Test]
        public void SelectNewFile()
        {
            //act
            _presenter.SelectNewFile();

            //assert
            _view.Received().ChooseFilePath();
            _view.Received().RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
        }

        [Test]
        public void UploadMovies_Confirmed()
        {
            //arrange
            _view.ConfirmUploadMovies().Returns(true); // юзер соглашается добавлять новые фильмы
            var expectedMovie = new MovieOriginal("TEST TITLE", "TEST DESCR", 9999);
            _presenter.MoviesList.Add(expectedMovie); // добавляю фильм чтобы был хотя бы один в MoviesList (для цикла foreach)

            //act
            _presenter.UploadMovies();

            //assert
            Assert.AreSame(expectedMovie, _context.MoviesOriginal.Single());
            _view.Received().Close();
        }

        [Test]
        public void UploadMovies_NotConfirmed()
        {
            //arrange
            _view.ConfirmUploadMovies().Returns(false); // юзер отказывается добавлять новые фильмы
            var testMovie = new MovieOriginal("TEST TITLE", "TEST DESCR", 9999);
            _presenter.MoviesList.Add(testMovie);

            //act
            _presenter.UploadMovies();

            //assert
            Assert.AreEqual(false, _context.MoviesOriginal.Any());
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
