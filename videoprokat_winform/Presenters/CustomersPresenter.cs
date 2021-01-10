using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    class CustomersPresenter
    {
        private ICustomersView _customersView;
        public VideoprokatContext _context;
        public void Run()
        {
            _customersView = new CustomersForm();

            _customersView.OnLoad += LoadCustomers;
            _customersView.OnAddCustomer += AddCustomer;

            _customersView.OnCustomerSelectionChanged += RedrawLeasings;

            _customersView.Show();
        }

        public void LoadCustomers()
        {
            RedrawCustomers();

            _customersView.OnUpdateCustomer += UpdateCustomer;
        }

        public void AddCustomer(Customer customer)
        {
            if (_customersView.ConfirmNewCustomer())
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                _customersView.RedrawCustomers(_context.Customers.ToList());
            }
        }

        public void UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            var customer = _context.Customers.First(c => c.Id == customerId);
            customer = updatedCustomer;

            _context.SaveChanges();
        }
        public void RedrawCustomers()
        {
            _customersView.RedrawCustomers(_context.Customers.ToList());
        }

        public void RedrawLeasings(int customerId)
        {
            List<Leasing> leasings = _context.LeasedCopies.Where(l => l.CustomerId == customerId).ToList();
            List<MovieOriginal> movies = _context.MoviesOriginal.ToList();
            List<MovieCopy> movieCopies = _context.MoviesCopies.ToList();

            _customersView.RedrawLeasings(leasings, movies, movieCopies);
        }
    }
}
