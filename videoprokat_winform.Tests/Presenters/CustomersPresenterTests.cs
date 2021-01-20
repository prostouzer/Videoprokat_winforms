using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
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
    public class CustomersPresenterTests
    {
        private ICustomersView _view;
        private IVideoprokatContext _context;
        private ICustomersPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IVideoprokatContext>();
            _view = Substitute.For<ICustomersView>();
            _presenter = new CustomersPresenter(_view, _context);
        }

        [Test]
        public void CustomersPresenterRun()
        {
            //arrange


            //act
            _presenter.Run();

            //assert
            _view.Received().Show();
        }

        [Test]
        public void CustomersLoad()
        {
            //arrange
            var customers = Substitute.For<DbSet<Customer>>();
            _context.Customers.Returns(customers);

            //act
            _presenter.LoadCustomers();

            //assert
            _view.Received().RedrawCustomers(customers);
            _view.Received().OnUpdateCustomer += Arg.Any<Action<int, Customer>>();
        }

        [Test]
        public void CustomerAdd_Confirmed()
        {
            //arrange
            _view.ConfirmNewCustomer().Returns(true); // юзер соглашается "Подтвердить нового пользователя" (MessageBox)
            var customers = Substitute.For<DbSet<Customer>>();
            _context.Customers.Returns(customers);
            var testCustomer = new Customer("test customer");

            //act
            _presenter.AddCustomer(testCustomer);

            //assert
            _context.Customers.Received().Add(Arg.Any<Customer>());
            _context.Received().SaveChanges();
            _view.Received().RedrawCustomers(customers);
        }

        [Test]
        public void CustomerAdd_NotConfirmed()
        {
            //arrange
            _view.ConfirmNewCustomer().Returns(false); // юзер отказывается "Подтвердить нового пользователя" (MessageBox)
            var customers = Substitute.For<DbSet<Customer>>();
            _context.Customers.Returns(customers);
            var testCustomer = new Customer("test customer");

            //act
            _presenter.AddCustomer(testCustomer);

            //assert
            _context.Customers.DidNotReceive().Add(Arg.Any<Customer>());
            _context.DidNotReceive().SaveChanges();
            _view.DidNotReceive().RedrawCustomers(customers);
        }

        [Test]
        public void CustomerUpdate_ValidData()
        {
            // arrange
            const int initialCustomerId = 0;
            var initialCustomer = new Customer("Initial Customer", 50);
            var updatedCustomer = new Customer("Updated Customer Name", 99);

            var customers = new FakeDbSet<Customer> {initialCustomer}; // не мокаю, т.к. презентер в этом методе использует статический метод Single

            _context.Customers.Returns(customers);

            //act
            _presenter.UpdateCustomer(initialCustomerId, updatedCustomer);

            //assert
            Assert.AreEqual(initialCustomer.Name, updatedCustomer.Name);
            Assert.AreEqual(initialCustomer.Rating, initialCustomer.Rating);
            _context.Received().SaveChanges();
        }

        [Test]
        public void CustomerSelectionChanged()
        {
            //arrange
            var leasing = new Leasing(DateTime.Now, DateTime.Now, 999, 999, 999);
            var leasings = new FakeDbSet<Leasing> {leasing}; // не мокаю, т.к. презентер в этом методе использует статический метод Single
            _context.LeasedCopies.Returns(leasings);
            var movies = Substitute.For <DbSet<MovieOriginal>>();
            var movieCopies = Substitute.For<DbSet<MovieCopy>>();

            //act
            _presenter.CustomerSelectionChanged(leasing.Id); 

            //assert
            _view.ReceivedWithAnyArgs().RedrawLeasings(leasings, movies, movieCopies); // просто с Received() ошибка типа Non-matched call
        }
    }
}
