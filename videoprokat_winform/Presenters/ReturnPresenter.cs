using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    public class ReturnPresenter
    {
        private IReturnView _returnView;
        public VideoprokatContext _context;
        public void Run(int leasingId)
        {
            _returnView = new ReturnForm();

            var leasing = _context.LeasedCopies.First(l => l.Id == leasingId);
            var copy = _context.MoviesCopies.First(c => c.Id == leasing.MovieCopyId);
            var movie = _context.MoviesOriginal.First(m => m.Id == copy.MovieId);
            var customer = _context.Customers.First(c => c.Id == leasing.CustomerId);

            _returnView.CurrentLeasing = leasing;
            _returnView.CurrentMovieCopy = copy;
            _returnView.CurrentMovie = movie;
            _returnView.CurrentCustomer = customer;

            _returnView.OnReturnEarly += ReturnEarly;
            _returnView.OnReturnOnTime += ReturnOnTime;
            _returnView.OnReturnDelayed += ReturnDelayed;

            _returnView.Show();
        }

        public void ReturnEarly(DateTime returnDate)
        {
            if (_returnView.ConfirmReturnEarly())
            {
                var leasing = _returnView.CurrentLeasing;
                leasing.ReturnEarly(returnDate);

                _context.SaveChanges();
                _returnView.Close();
            }
        }

        public void ReturnOnTime()
        {
            if (_returnView.ConfirmReturnOnTime())
            {
                var leasing = _returnView.CurrentLeasing;
                leasing.ReturnOnTime();

                _context.SaveChanges();
                _returnView.Close();
            }
        }

        public void ReturnDelayed(DateTime returnDate, decimal fineMultiplier)
        {
            if (_returnView.ConfirmReturnDelayed())
            {
                var leasing = _returnView.CurrentLeasing;
                leasing.ReturnDelayed(returnDate, fineMultiplier);

                _context.SaveChanges();
                _returnView.Close();
            }
        }
    }
}
