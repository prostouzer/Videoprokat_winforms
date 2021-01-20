using System;
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
    internal class ReturnPresenterTests
    {
        private IReturnView _view;
        private IVideoprokatContext _context;
        private IReturnPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<IReturnView>();
            _context = Substitute.For<IVideoprokatContext>();
            _presenter = new ReturnPresenter(_view, _context);
        }

        [Test]
        public void Run()
        {
            //arrange
            var testLeasing = new Leasing(DateTime.Now, DateTime.Now, 0, 0, 9999);
            var testMovieCopy = new MovieCopy(0, "TEST COMMENT", 9999);
            var testMovie = new MovieOriginal("TEST TITLE", "TEST DESCR", 9999);
            var testCustomer = new Customer("TEST NAME");

            var leasings = new FakeDbSet<Leasing> {testLeasing};
            _context.LeasedCopies.Returns(leasings);
            var copies = new FakeDbSet<MovieCopy> {testMovieCopy};
            _context.MoviesCopies.Returns(copies);
            var movies  = new FakeDbSet<MovieOriginal> {testMovie};
            _context.MoviesOriginal.Returns(movies);
            var customers = new FakeDbSet<Customer> {testCustomer};
            _context.Customers.Returns(customers);

            const int leasingId = 0;

            //act
            _presenter.Run(leasingId);

            //assert
            Assert.AreEqual(testLeasing.StartDate, _view.CurrentLeasing.StartDate);
            Assert.AreEqual(testLeasing.ExpectedEndDate, _view.CurrentLeasing.ExpectedEndDate);
            Assert.AreEqual(testLeasing.CustomerId, _view.CurrentLeasing.CustomerId);
            Assert.AreEqual(testLeasing.MovieCopyId, _view.CurrentLeasing.MovieCopyId);
            Assert.AreEqual(testLeasing.TotalPrice, _view.CurrentLeasing.TotalPrice);

            Assert.AreEqual(testMovieCopy.MovieId, _view.CurrentMovieCopy.MovieId);
            Assert.AreEqual(testMovieCopy.Commentary, _view.CurrentMovieCopy.Commentary);
            Assert.AreEqual(testMovieCopy.PricePerDay, _view.CurrentMovieCopy.PricePerDay);

            Assert.AreEqual(testMovie.Title, _view.CurrentMovie.Title);
            Assert.AreEqual(testMovie.Description, _view.CurrentMovie.Description);
            Assert.AreEqual(testMovie.YearReleased, _view.CurrentMovie.YearReleased);

            Assert.AreEqual(testCustomer.Name, _view.CurrentCustomer.Name);

            _view.Received().Show();
        }

        [Test]
        public void ReturnEarly_Confirmed()
        {
            //arrange
            _view.ConfirmReturnEarly().Returns(true);

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            // взяли 20 января, договорились вернуть 25, но вернули 23
            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var returnDate = new DateTime(2020, 01, 23);
            var leasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = leasing;

            leasing.ReturnEarly(returnDate);

            //act
            _presenter.ReturnEarly(returnDate);

            //assert
            Assert.AreEqual(leasing.TotalPrice, _view.CurrentLeasing.TotalPrice);
            Assert.AreEqual(leasing.MovieCopy.Available, _view.CurrentLeasing.MovieCopy.Available);
            Assert.AreEqual(leasing.ReturnDate, _view.CurrentLeasing.ReturnDate);

            _context.Received().SaveChanges();
            _view.Received().Close();
        }

        [Test]
        public void ReturnEarly_NotConfirmed()
        {
            //arrange
            _view.ConfirmReturnEarly().Returns(false);

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var returnDate = new DateTime(2020, 01, 23);
            var leasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = leasing;

            //act
            _presenter.ReturnEarly(returnDate);

            //assert
            Assert.AreEqual(false, _view.CurrentLeasing.MovieCopy.Available);
            _context.DidNotReceive().SaveChanges();
            _view.DidNotReceive().Close();
        }

        [Test]
        public void ReturnOnTime_Confirmed()
        {
            //arrange
            _view.ConfirmReturnOnTime().Returns(true);

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var leasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = leasing;

            leasing.ReturnOnTime();

            //act
            _presenter.ReturnOnTime();

            //assert
            Assert.AreEqual(leasing.TotalPrice, _view.CurrentLeasing.TotalPrice);
            Assert.AreEqual(leasing.MovieCopy.Available, _view.CurrentLeasing.MovieCopy.Available);
            Assert.AreEqual(leasing.ReturnDate, _view.CurrentLeasing.ReturnDate);

            _context.Received().SaveChanges();
            _view.Received().Close();
        }

        [Test]
        public void ReturnOnTime_NotConfirmed()
        {
            //arrange
            _view.ConfirmReturnOnTime().Returns(false);

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var leasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = leasing;

            //act
            _presenter.ReturnOnTime();

            //assert
            Assert.AreEqual(false, _view.CurrentLeasing.MovieCopy.Available);
            _context.DidNotReceive().SaveChanges();
            _view.DidNotReceive().Close();

        }

        [Test]
        public void ReturnDelayed_Confirmed()
        {
            //arrange
            _view.ConfirmReturnDelayed().Returns(true);

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            // взяли 20 января, договорились вернуть 25, но вернули 28
            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var returnDate = new DateTime(2020, 01, 28);
            var leasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = leasing;

            const decimal fineMultiplier = 2;
            leasing.ReturnDelayed(returnDate);

            //act
            _presenter.ReturnDelayed(returnDate, fineMultiplier);

            //assert
            Assert.AreEqual(leasing.TotalPrice, _view.CurrentLeasing.TotalPrice);
            Assert.AreEqual(leasing.MovieCopy.Available, _view.CurrentLeasing.MovieCopy.Available);
            Assert.AreEqual(leasing.ReturnDate, _view.CurrentLeasing.ReturnDate);

            _context.Received().SaveChanges();
            _view.Received().Close();
        }

        [Test]
        public void ReturnDelayed_NotConfirmed()
        {
            //arrange
            _view.ConfirmReturnDelayed().Returns(false);

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            // взяли 20 января, договорились вернуть 25, но вернули 28
            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var returnDate = new DateTime(2020, 01, 28);
            var leasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = leasing;

            const decimal fineMultiplier = 2;

            //act
            _presenter.ReturnDelayed(returnDate, fineMultiplier);

            //assert
            Assert.AreEqual(false, _view.CurrentLeasing.MovieCopy.Available);
            _context.DidNotReceive().SaveChanges();
            _view.DidNotReceive().Close();
        }
    }
}
