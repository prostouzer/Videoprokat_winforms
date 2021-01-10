using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface IReturnView : IView
    {
        MovieOriginal CurrentMovie { get; set; }
        MovieCopy CurrentCopy { get; set; }
        Customer CurrentCustomer { get; set; }
        Leasing CurrentLeasing { get; set; }
        decimal FineMultiplier { get; set; }

        event Action<DateTime> OnReturnEarly;
        event Action OnReturnOnTime; // если вовремя - returnDate = expectedEndDate
        event Action<DateTime, decimal> OnReturnDelayed;

        bool ConfirmReturnEarly();
        bool ConfirmReturnOnTime();
        bool ConfirmReturnDelayed();
    }
}
