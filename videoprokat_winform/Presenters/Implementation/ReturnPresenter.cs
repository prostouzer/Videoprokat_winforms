using System;
using System.Linq;
using videoprokat_winform.Contexts;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters.Implementation
{
    public class ReturnPresenter : IReturnPresenter
    {
        private readonly IReturnView _returnView;
        private readonly IVideoprokatContext _context;

        public ReturnPresenter(IReturnView view, IVideoprokatContext context)
        {
            _returnView = view;
            _context = context;

            _returnView.OnReturnEarly += ReturnEarly;
            _returnView.OnReturnOnTime += ReturnOnTime;
            _returnView.OnReturnDelayed += ReturnDelayed;
        }

        public void Run(int leasingId)
        {
            var leasing = _context.LeasedCopies.Single(l => l.Id == leasingId);
            var copy = _context.MoviesCopies.Single(c => c.Id == leasing.MovieCopyId);
            var movie = _context.MoviesOriginal.Single(m => m.Id == copy.MovieId);
            var customer = _context.Customers.Single(c => c.Id == leasing.CustomerId);

            _returnView.CurrentLeasing = leasing;
            _returnView.CurrentMovieCopy = copy;
            _returnView.CurrentMovie = movie;
            _returnView.CurrentCustomer = customer;

            _returnView.Show();
        }

        public void ReturnEarly(DateTime returnDate)
        {
            if (!_returnView.ConfirmReturnEarly()) return;
            var leasing = _returnView.CurrentLeasing;
            leasing.ReturnEarly(returnDate);

            _context.SaveChanges();
            _returnView.Close();
        }

        public void ReturnOnTime()
        {
            if (!_returnView.ConfirmReturnOnTime()) return;
            var leasing = _returnView.CurrentLeasing;
            leasing.ReturnOnTime();

            _context.SaveChanges();
            _returnView.Close();
        }

        public void ReturnDelayed(DateTime returnDate, decimal fineMultiplier)
        {
            if (!_returnView.ConfirmReturnDelayed()) return;
            var leasing = _returnView.CurrentLeasing;
            leasing.ReturnDelayed(returnDate, fineMultiplier);

            _context.SaveChanges();
            _returnView.Close();
        }
    }
}
