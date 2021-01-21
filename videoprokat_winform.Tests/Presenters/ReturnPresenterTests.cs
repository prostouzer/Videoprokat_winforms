using System;
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
    internal class ReturnPresenterTests
    {
        private IReturnView _view;
        private VideoprokatContext _context;
        private IReturnPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<IReturnView>();
            var dbContextOptions = new DbContextOptionsBuilder<VideoprokatContext>().UseInMemoryDatabase("TestDb");
            _context = new VideoprokatContext(dbContextOptions.Options);
            _context.Database.EnsureDeleted(); // мне не нужны заполненные данные из OnModelCreating после EnsureCreated
            _presenter = new ReturnPresenter(_view, _context);
        }

        [Test]
        public void Run()
        {

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
