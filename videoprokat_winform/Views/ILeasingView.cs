using System;
using System.Collections.Generic;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    public interface ILeasingView : IView
    {
        MovieOriginal CurrentMovie { get; set; }
        MovieCopy CurrentMovieCopy { get; set; }

        event Action<Leasing> OnLeaseMovieCopy;

        bool ConfirmNewLeasing();
        void RedrawCustomers(List<Customer> customers);
    }
}
