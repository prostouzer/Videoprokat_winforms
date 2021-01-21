using System;
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
    internal class LeasingPresenterTests
    {
        private ILeasingView _view;
        private VideoprokatContext _context;
        private ILeasingPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<ILeasingView>();
            var dbContextOptions = new DbContextOptionsBuilder<VideoprokatContext>().UseInMemoryDatabase("TestDb");
            _context = new VideoprokatContext(dbContextOptions.Options);
            _context.Database.EnsureDeleted(); // мне не нужны заполненные данные из OnModelCreating после EnsureCreated
            _presenter = new LeasingPresenter(_view, _context);
        }

        [Test]
        public void Run()
        {
            //arrange
            var movieTest = new MovieOriginal("TEST MOVIE", "TEST MOVIE DESCR", 9999);
            var movieCopyTest = new MovieCopy(1111, "TEST COMMENT", 1111);
            
            //act
            _presenter.Run(movieTest, movieCopyTest);

            //assert
            Assert.AreEqual(movieTest.Title, _view.CurrentMovie.Title);
            Assert.AreEqual(movieTest.Description, _view.CurrentMovie.Description);
            Assert.AreEqual(movieTest.YearReleased, _view.CurrentMovie.YearReleased);

            Assert.AreEqual(movieCopyTest.MovieId, _view.CurrentMovieCopy.MovieId);
            Assert.AreEqual(movieCopyTest.Commentary, _view.CurrentMovieCopy.Commentary);
            Assert.AreEqual(movieCopyTest.PricePerDay, _view.CurrentMovieCopy.PricePerDay);

            _view.Received().RedrawCustomers(Arg.Any<IQueryable<Customer>>());
            _view.Received().Show();
        }

        [Test]
        public void LeasingAdd_Confirmed()
        {
            //arrange
            _view.ConfirmNewLeasing().Returns(true); // юзер соглашается "Подтвердить нового пользователя" (MessageBox)

            var testMovieCopy = new MovieCopy(9999, "TEST", 9999);
            //var movieCopies = new FakeDbSet<MovieCopy> {testMovieCopy}; // используется FakeDbSet т.к. презентер использует статические Linq методы (которые не мокаются). Иначе бы использовал Substitute.For<DbSet<MovieCopy>>
            //_context.MoviesCopies.Returns(movieCopies);

            var testLeasing = new Leasing(DateTime.Now, DateTime.Now, 9999, 0, 9999);
            //var leasings = new FakeDbSet<Leasing>(); 
            //_context.LeasedCopies.Returns(leasings);

            //act
            _presenter.AddLeasing(testLeasing);

            //assert
            Assert.AreEqual(false, testMovieCopy.Available);
            Assert.AreEqual(true, _context.LeasedCopies.Any()); // не использую _context.LeasedCopies.Received()... т.к. LeasedCopies не замоканные
            _context.Received().SaveChanges();
            _view.Received().Close();
        }

        [Test]
        public void LeasingAdd_NotConfirmed()
        {
            //arrange
            _view.ConfirmNewLeasing().Returns(false);

            var testMovieCopy = new MovieCopy(9999, "TEST", 9999);
            //var movieCopies = new FakeDbSet<MovieCopy> { testMovieCopy }; // используется FakeDbSet т.к. презентер использует статические Linq методы (которые не мокаются). Иначе бы использовал Substitute.For<DbSet<MovieCopy>>
            //_context.MoviesCopies.Returns(movieCopies);

            var testLeasing = new Leasing(DateTime.Now, DateTime.Now, 9999, 0, 9999);
            //var leasings = new FakeDbSet<Leasing>();
            //_context.LeasedCopies.Returns(leasings);

            //act
            _presenter.AddLeasing(testLeasing);

            //assert
            Assert.AreEqual(true, testMovieCopy.Available);
            Assert.AreEqual(false, _context.LeasedCopies.Any());
            _context.DidNotReceive().SaveChanges();
            _view.DidNotReceive().Close();
        }
    }
}
