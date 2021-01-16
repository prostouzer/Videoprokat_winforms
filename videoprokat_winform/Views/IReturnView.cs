using System;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    public interface IReturnView : IView
    {
        MovieOriginal CurrentMovie { get; set; }
        MovieCopy CurrentMovieCopy { get; set; }
        Customer CurrentCustomer { get; set; }
        Leasing CurrentLeasing { get; set; }
        decimal FineMultiplier { get; set; }

        event Action<DateTime> OnReturnEarly;
        event Action OnReturnOnTime; // если вовремя, то returnDate = expectedEndDate
        event Action<DateTime, decimal> OnReturnDelayed;

        bool ConfirmReturnEarly();
        bool ConfirmReturnOnTime();
        bool ConfirmReturnDelayed();
    }
}
