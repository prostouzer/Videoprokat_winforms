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
    internal class MovieCopyPresenterTests
    {
        private IMovieCopyView _view;
        private VideoprokatContext _context;
        private IMovieCopyPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<IMovieCopyView>();
            var dbContextOptions = new DbContextOptionsBuilder<VideoprokatContext>().UseInMemoryDatabase("TestDb");
            _context = new VideoprokatContext(dbContextOptions.Options);
            _presenter = new MovieCopyPresenter(_view, _context);
        }

        [Test]
        public void Run()
        {
            //arrange
            var testMovie = new MovieOriginal("TEST", "TEST", 9999);

            //act
            _presenter.Run(testMovie);

            //asert
            Assert.AreEqual(testMovie.Title, _view.CurrentMovie.Title);
            Assert.AreEqual(testMovie.Description, _view.CurrentMovie.Description);
            Assert.AreEqual(testMovie.YearReleased, _view.CurrentMovie.YearReleased);

            _view.Received().Show();
        }

        [Test]
        public void MovieCopyAdd_Confirmed()
        {

        }

        [Test]
        public void MovieCopyAdd_NotConfirmed()
        {
            //arrange
            _view.ConfirmNewMovieCopy().Returns(false);
            var testMovieCopy = new MovieCopy(9999, "TEST", 9999);

            //act
            _presenter.AddMovieCopy(testMovieCopy);

            //asert
            _context.MoviesCopies.DidNotReceive().Add(Arg.Any<MovieCopy>());
            _context.DidNotReceive().SaveChanges();

            _view.DidNotReceive().Close();
        }
    }
}
