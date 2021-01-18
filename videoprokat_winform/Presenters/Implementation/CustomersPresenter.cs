using System.Linq;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters.Implementation
{
    public class CustomersPresenter : ICustomersPresenter
    {
        private readonly ICustomersView _customersView;
        private readonly VideoprokatContext _context;

        public CustomersPresenter(ICustomersView view, VideoprokatContext context)
        {
            _customersView = view;
            _context = context;

            _customersView.OnLoad += LoadCustomers;
            _customersView.OnAddCustomer += AddCustomer;
            _customersView.OnCustomerSelectionChanged += CustomerSelectionChanged;
        }

        public void Run()
        {
            _customersView.Show();
        }

        public void LoadCustomers()
        {
            _customersView.RedrawCustomers(_context.Customers);

            _customersView.OnUpdateCustomer += UpdateCustomer;
        }

        public void AddCustomer(Customer customer)
        {
            if (!_customersView.ConfirmNewCustomer()) return;
            _context.Customers.Add(customer);
            _context.SaveChanges();
            _customersView.RedrawCustomers(_context.Customers);
        }

        public void UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            var customer = _context.Customers.Single(c => c.Id == customerId);
            customer.Name = updatedCustomer.Name;
            customer.Rating = updatedCustomer.Rating;

            _context.SaveChanges();
        }

        public void CustomerSelectionChanged(int customerId)
        {
            var leasings = _context.LeasedCopies.Where(l => l.CustomerId == customerId);
            var movies = _context.MoviesOriginal;
            var movieCopies = _context.MoviesCopies;

            _customersView.RedrawLeasings(leasings, movies, movieCopies);
        }
    }
}
