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
    internal class MoviePresenterTests
    {
        private IMovieView _view;
        private VideoprokatContext _context;
        private IMoviePresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<IMovieView>();
            var dbContextOptions = new DbContextOptionsBuilder<VideoprokatContext>().UseInMemoryDatabase("TestDb");
            _context = new VideoprokatContext(dbContextOptions.Options);
            _context.Database.EnsureDeleted(); // мне не нужны заполненные данные из OnModelCreating после EnsureCreated
            _presenter = new MoviePresenter(_view, _context);
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
        public void MovieAdd_Confirmed()
        {
            //arrange
            _view.ConfirmNewMovie().Returns(true); // юзер соглашается "Добавить новый фильм"
            var testMovie = new MovieOriginal("TEST", "TEST", 9999);

            //act
            _presenter.AddMovie(testMovie);

            //assert
            Assert.AreEqual(true, _context.MoviesOriginal.Any());

            _view.Received().Close();
        }

        [Test]
        public void MovieAdd_NotConfirmed()
        {
            //arrange
            _view.ConfirmNewMovie().Returns(false); // юзер отказывается "Добавить новый фильм"
            var testMovie = new MovieOriginal("TEST", "TEST", 9999);

            //act
            _presenter.AddMovie(testMovie);

            //assert
            Assert.AreEqual(false, _context.MoviesOriginal.Any());

            _view.DidNotReceive().Close();
        }
    }
}
