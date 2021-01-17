using System;
using System.Data.Entity;
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
        void RedrawLeasings(IQueryable<Leasing> leasingsDbSet, DbSet<MovieOriginal> moviesDbSet, DbSet<MovieCopy> movieCopiesDbSet);
        void RedrawCustomers(DbSet<Customer> customersDbSet);
    }
}
