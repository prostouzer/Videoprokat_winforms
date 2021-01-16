using System.Collections.Generic;
using System.Linq;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    public class CustomersPresenter
    {
        private ICustomersView _customersView;
        private VideoprokatContext _context;
        public void Run(VideoprokatContext context)
        {
            _context = context;
            _customersView = new CustomersForm();

            _customersView.OnLoad += LoadCustomers;
            _customersView.OnAddCustomer += AddCustomer;

            _customersView.OnCustomerSelectionChanged += CustomerSelectionChanged;

            _customersView.Show();
        }

        public void LoadCustomers()
        {
            _customersView.RedrawCustomers(_context.Customers.ToList());

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

        public void CustomerSelectionChanged(int customerId)
        {
            List<Leasing> leasings = _context.LeasedCopies.Where(l => l.CustomerId == customerId).ToList();
            List<MovieOriginal> movies = _context.MoviesOriginal.ToList();
            List<MovieCopy> movieCopies = _context.MoviesCopies.ToList();

            _customersView.RedrawLeasings(leasings, movies, movieCopies);
        }
    }
}
