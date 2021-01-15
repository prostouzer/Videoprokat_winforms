using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using videoprokat_winform.Models;
using videoprokat_winform.Views;
using videoprokat_winform.Presenters;
using Moq;

namespace videoprokat_winform.Tests.Presenters
{
    [TestClass]
    public class CustomersPresenterTests
    {
        [TestMethod]
        public void CustomersPresenterOpen()
        {
            //// arrange
            //var customersPresenter = new CustomersPresenter();
            //var customersViewMock = new Mock<ICustomersView>();

            ////act
            //customersPresenter.Run();

            ////assert
            //customersViewMock.Verify();
        }

        [TestMethod]
        public void CustomersLoad()
        {
            //arrange
            var customersPresenter = new CustomersPresenter();
            var customersViewMock = new Mock<ICustomersView>();


            //act
            customersPresenter.LoadCustomers();

            //assert

        }

        [TestMethod]
        public void CustomerAdd()
        {

        }

        [TestMethod]
        public void CustomerUpdate()
        {

        }

        [TestMethod]
        public void CustomerSelectionChanged()
        {

        }
    }
}
