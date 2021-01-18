using System;
using videoprokat_winform.Contexts;

namespace videoprokat_winform.Presenters
{
    public interface IReturnPresenter
    {
        void Run(int leasingId);
        void ReturnEarly(DateTime returnDate);
        void ReturnOnTime();
        void ReturnDelayed(DateTime returnDate, decimal fineMultiplier);
    }
}