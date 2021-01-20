using System;
using System.Linq;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    public interface ICustomersView : IView
    {
        event Action OnLoad;
        event Action<Customer> OnAddCustomer;
        event Action<int, Customer> OnUpdateCustomer;
        event Action<int> OnCustomerSelectionChanged;

        bool ConfirmNewCustomer();
        void RedrawLeasings(IQueryable<Leasing> leasingsDbSet, IQueryable<MovieOriginal> moviesDbSet, IQueryable<MovieCopy> movieCopiesDbSet);
        void RedrawCustomers(IQueryable<Customer> customersDbSet);
    }
}
