using System.Data.Entity;
using System.Linq;
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
        private IVideoprokatContext _context;
        private ICustomersView _view;
        private ICustomersPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IVideoprokatContext>();
            _view = Substitute.For<ICustomersView>();
            _presenter = new CustomersPresenter(_view, _context);
        }

        [Test]
        public void CustomersPresenterOpen()
        {
            //arrange


            //act


            //assert

        }

        [Test]
        public void CustomersLoad()
        {
            //arrange


            //act


            //assert

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
            //var customers = Substitute.For<DbSet<Customer>>();
            //_context.Customers.Returns(customers);
            var customerId = 1;
            var initialCustomer = new Customer("Initial Customer", 50);
            var updatedCustomer = new Customer("Updated Customer Name", 99);
            
            //act
            _presenter.UpdateCustomer(customerId, updatedCustomer);

            //assert
            Assert.AreEqual(initialCustomer.Name, updatedCustomer.Name);
            Assert.AreEqual(initialCustomer.Rating, initialCustomer.Rating);
            _context.Received().SaveChanges();
        }

        [Test]
        public void CustomerUpdate_InvalidData()
        {
            // arrange


            //act


            //assert

        }

        [Test]
        public void CustomerSelectionChanged()
        {
            //arrange


            //act


            //assert

        }
    }
}
