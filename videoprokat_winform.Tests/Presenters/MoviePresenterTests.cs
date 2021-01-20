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
    internal class MoviePresenterTests
    {
        private IMovieView _view;
        private IVideoprokatContext _context;
        private IMoviePresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<IMovieView>();
            _context = Substitute.For<IVideoprokatContext>();
            _presenter = new MoviePresenter(_view, _context);
        }

        [Test]
        public void Run()
        {
            //arrange


            //act
            _presenter.Run();

            //assert
            _view.Received().Show();
        }

        [Test]
        public void MovieAdd_Confirmed()
        {
            //arrange
            _view.ConfirmNewMovie().Returns(true);
            var movies = Substitute.For<DbSet<MovieOriginal>>();
            _context.MoviesOriginal.Returns(movies);
            var testMovie = new MovieOriginal("TEST", "TEST", 9999);

            //act
            _presenter.AddMovie(testMovie);

            //assert
            _context.MoviesOriginal.Received().Add(Arg.Any<MovieOriginal>());
            _context.Received().SaveChanges();
            _view.Received().Close();
        }

        [Test]
        public void MovieAdd_NotConfirmed()
        {
            //arrange
            _view.ConfirmNewMovie().Returns(false);
            var testMovie = new MovieOriginal("TEST", "TEST", 9999);


            //act
            _presenter.AddMovie(testMovie);

            //assert
            _context.MoviesOriginal.DidNotReceive().Add(Arg.Any<MovieOriginal>());
            _context.DidNotReceive().SaveChanges();
            _view.DidNotReceive().Close();
        }
    }
}
