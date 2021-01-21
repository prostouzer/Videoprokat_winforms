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
    public class CustomersPresenterTests
    {
        private ICustomersView _view;
        private VideoprokatContext _context;
        private ICustomersPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            var dbContextOptions = new DbContextOptionsBuilder<VideoprokatContext>().UseInMemoryDatabase("TestDb");
            _context = new VideoprokatContext(dbContextOptions.Options);
            _context.Database.EnsureDeleted(); // мне не нужны заполненные данные из OnModelCreating после EnsureCreated

            _view = Substitute.For<ICustomersView>();
            _presenter = new CustomersPresenter(_view, _context);
            
        }

        [Test]
        public void CustomersPresenterRun()
        {
            //act
            _presenter.Run();

            //assert
            _view.Received().Show();
        }

        [Test]
        public void CustomersLoad()
        {
            //arrange

            //act
            _presenter.LoadCustomers();

            //assert
            _view.Received().RedrawCustomers(_context.Customers);
            _view.Received().OnUpdateCustomer += Arg.Any<Action<int, Customer>>();
        }

        [Test]
        public void CustomerAdd_Confirmed()
        {
            //arrange
            _view.ConfirmNewCustomer().Returns(true); // юзер соглашается "Подтвердить нового пользователя" (MessageBox)
            var testCustomer = new Customer("test customer");

            //act
            _presenter.AddCustomer(testCustomer);

            //assert
            Assert.AreSame(testCustomer, _context.Customers.Single());
            _view.Received().RedrawCustomers(_context.Customers);
        }

        [Test]
        public void CustomerAdd_NotConfirmed()
        {
            //arrange
            _view.ConfirmNewCustomer().Returns(false); // юзер отказывается "Подтвердить нового пользователя" (MessageBox)
            var testCustomer = new Customer("test customer");

            //act
            _presenter.AddCustomer(testCustomer);

            //assert
            Assert.AreEqual(false, _context.Customers.Any());
            _view.DidNotReceive().RedrawCustomers(Arg.Any<IQueryable<Customer>>());
        }

        [Test]
        public void CustomerUpdate()
        {
            // arrange
            var initialCustomer = new Customer("Initial Customer", 50);
            _context.Customers.Add(initialCustomer);
            _context.SaveChanges();
            var updatedCustomer = new Customer("Updated Customer Name", 99);

            //act
            _presenter.UpdateCustomer(initialCustomer.Id, updatedCustomer);

            //assert
            Assert.AreEqual(initialCustomer.Name, updatedCustomer.Name);
            Assert.AreEqual(initialCustomer.Rating, initialCustomer.Rating);
        }

        [Test]
        public void CustomerSelectionChanged()
        {
            //act
            _presenter.CustomerSelectionChanged(9999); 

            //assert
            _view.Received().RedrawLeasings(Arg.Any<IQueryable<Leasing>>(), Arg.Any<IQueryable<MovieOriginal>>(), Arg.Any<IQueryable<MovieCopy>>());
        }
    }
}
