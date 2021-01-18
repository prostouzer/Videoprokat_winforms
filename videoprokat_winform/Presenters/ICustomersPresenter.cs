using videoprokat_winform.Contexts;
using videoprokat_winform.Models;

namespace videoprokat_winform.Presenters
{
    public interface ICustomersPresenter
    {
        void Run();
        void LoadCustomers();
        void AddCustomer(Customer customer);
        void UpdateCustomer(int customerId, Customer updatedCustomer);
        void CustomerSelectionChanged(int customerId);
    }
}