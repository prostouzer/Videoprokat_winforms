using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface ILeasingView : IView
    {
        MovieOriginal currentMovie { get; set; }
        MovieCopy currentMovieCopy { get; set; }

        event Action<Leasing> OnLeaseMovieCopy;

        bool ConfirmNewLeasing();
        void PopulateWithCustomers(List<Customer> customers);
    }
}
