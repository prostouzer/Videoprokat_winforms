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
            //arrange
            // добавляем и сразу же сохраняем по цепочке чтобы последующие элементы получили актуальное значение необходимого Id
            var testMovie = new MovieOriginal("TEST TITLE", "TEST DESCR", 9999);
            _context.MoviesOriginal.Add(testMovie);
            _context.SaveChanges();
            var testMovieCopy = new MovieCopy(testMovie.Id, "TEST COMMENT", 9999);
            _context.MoviesCopies.Add(testMovieCopy);
            _context.SaveChanges();
            var testCustomer = new Customer("TEST NAME");
            _context.Customers.Add(testCustomer);
            _context.SaveChanges();
            var testLeasing = new Leasing(DateTime.Now, DateTime.Now, testCustomer.Id, testMovieCopy.Id, 9999);
            _context.LeasedCopies.Add(testLeasing);
            _context.SaveChanges();

            //act
            _presenter.Run(testLeasing.Id);

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
            _view.ConfirmReturnEarly().Returns(true); // юзер соглашается "Вернуть фильм"

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            // взяли 20 января, договорились вернуть 25, но вернули 23
            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var returnDate = new DateTime(2020, 01, 23);
            var testLeasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = testLeasing;

            testLeasing.ReturnEarly(returnDate);

            //act
            _presenter.ReturnEarly(returnDate);

            //assert
            Assert.AreEqual(testLeasing.TotalPrice, _view.CurrentLeasing.TotalPrice);
            Assert.AreEqual(testLeasing.MovieCopy.Available, _view.CurrentLeasing.MovieCopy.Available);
            Assert.AreEqual(testLeasing.ReturnDate, _view.CurrentLeasing.ReturnDate);

            _view.Received().Close();
        }

        [Test]
        public void ReturnEarly_NotConfirmed()
        {
            //arrange
            _view.ConfirmReturnEarly().Returns(false); // юзер отказывается "Вернуть фильм"

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var returnDate = new DateTime(2020, 01, 23);
            var testLeasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = testLeasing;

            //act
            _presenter.ReturnEarly(returnDate);

            //assert
            Assert.AreEqual(false, _view.CurrentLeasing.MovieCopy.Available);

            _view.DidNotReceive().Close();
        }

        [Test]
        public void ReturnOnTime_Confirmed()
        {
            //arrange
            _view.ConfirmReturnOnTime().Returns(true); // юзер соглашается "Вернуть фильм"

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде
            
            // вернули в срок, следовательно ReturnDate = ExpectedEndDate;
            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var testLeasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = testLeasing;

            testLeasing.ReturnOnTime();

            //act
            _presenter.ReturnOnTime();

            //assert
            Assert.AreEqual(testLeasing.TotalPrice, _view.CurrentLeasing.TotalPrice);
            Assert.AreEqual(testLeasing.MovieCopy.Available, _view.CurrentLeasing.MovieCopy.Available);
            Assert.AreEqual(testLeasing.ReturnDate, _view.CurrentLeasing.ReturnDate);

            _view.Received().Close();
        }

        [Test]
        public void ReturnOnTime_NotConfirmed()
        {
            //arrange
            _view.ConfirmReturnOnTime().Returns(false); // юзер отказывается "Вернуть фильм"

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var testLeasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = testLeasing;

            //act
            _presenter.ReturnOnTime();

            //assert
            Assert.AreEqual(false, _view.CurrentLeasing.MovieCopy.Available);

            _view.DidNotReceive().Close();
        }

        [Test]
        public void ReturnDelayed_Confirmed()
        {
            //arrange
            _view.ConfirmReturnDelayed().Returns(true); // юзер соглашается "Вернуть фильм"

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

            // взяли 20 января, договорились вернуть 25, но вернули 28
            var startDate = new DateTime(2020, 01, 20);
            var expectedEndDate = new DateTime(2020, 01, 25);
            var returnDate = new DateTime(2020, 01, 28);
            var testLeasing = new Leasing(startDate, expectedEndDate, 0, 0, 50) {MovieCopy = testMovieCopy};
            _view.CurrentLeasing = testLeasing;

            const decimal fineMultiplier = 2;
            testLeasing.ReturnDelayed(returnDate);

            //act
            _presenter.ReturnDelayed(returnDate, fineMultiplier);

            //assert
            Assert.AreEqual(testLeasing.TotalPrice, _view.CurrentLeasing.TotalPrice);
            Assert.AreEqual(testLeasing.MovieCopy.Available, _view.CurrentLeasing.MovieCopy.Available);
            Assert.AreEqual(testLeasing.ReturnDate, _view.CurrentLeasing.ReturnDate);

            _view.Received().Close();
        }

        [Test]
        public void ReturnDelayed_NotConfirmed()
        {
            //arrange
            _view.ConfirmReturnDelayed().Returns(false); // юзер отказывается "Вернуть фильм"

            var testMovieCopy = new MovieCopy(9999, "TEST COMMENT", 50) {Available = false}; // копия у кого-то в аренде

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

            _view.DidNotReceive().Close();
        }
    }
}
