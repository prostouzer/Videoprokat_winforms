﻿using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface ICustomersView : IView
    {
        event Action OnLoad;
        event Action<Customer> OnAddCustomer;
        event Action<int, Customer> OnUpdateCustomer;
        event Action<int> OnCustomerSelectionChanged;

        bool ConfirmNewCustomer();
        void RedrawLeasings(List<Leasing> leasings, List<MovieOriginal> movies, List<MovieCopy> copies);
        void RedrawCustomers(List<Customer> customers);
    }
}