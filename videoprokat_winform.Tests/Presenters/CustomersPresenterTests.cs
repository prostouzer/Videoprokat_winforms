using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.DataCollection;
using videoprokat_winform.Models;
using videoprokat_winform.Views;
using videoprokat_winform.Presenters;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using videoprokat_winform.Contexts;

namespace videoprokat_winform.Tests.Presenters
{
    [TestFixture]
    public class CustomersPresenterTests
    {
        private ICustomersView _view;
        private VideoprokatContext _context;
        private CustomersPresenter _presenter;
        [SetUp]
        public void SetUp()
        {
            //_view = Substitute.For<ICustomersView>();
            ////_context = Substitute.For<VideoprokatContext>();
            ////var customers = Substitute.For<DbSet<Customer>>();
            ////_context.Set<Customer>().Returns(customers);
            ////_context.Customers.Returns(customers); // исключение
            
            ////_context.Customers.ToList().Returns(new List<Customer>());

            //_presenter = new CustomersPresenter();
            //_presenter._context = _context;
            //_presenter.Run(_view);
        }

        [Test]
        public void CustomersPresenterOpen()
        {

        }

        [Test]
        public void CustomersLoad()
        {
        //    //arrange

        //    //act
        //    _presenter.LoadCustomers();
        //    _view.Received().RedrawCustomers(new List<Customer>());

        //    //assert


        }

        [Test]
        public void CustomerAdd()
        {

        }

        [Test]
        public void CustomerUpdate()
        {

        }

        [Test]
        public void CustomerSelectionChanged()
        {

        }
    }
}
