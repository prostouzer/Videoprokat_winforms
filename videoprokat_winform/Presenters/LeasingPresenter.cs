using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    class LeasingPresenter
    {
        private ILeasingView _leasingView;
        public VideoprokatContext _context;
        public void Run(MovieOriginal currentMovie, MovieCopy currentMovieCopy)
        {
            _leasingView = new LeasingForm();

            _leasingView.CurrentMovie = currentMovie;
            _leasingView.CurrentMovieCopy = currentMovieCopy;

            _leasingView.RedrawCustomers(new List<Customer>(_context.Customers.ToList()));

            _leasingView.OnLeaseMovieCopy += AddLeasing;

            _leasingView.Show();
        }

        public void AddLeasing(Leasing leasing)
        {
            if (_leasingView.ConfirmNewLeasing())
            {
                var movieCopy = _context.MoviesCopies.First(c => c.Id == leasing.MovieCopyId);
                movieCopy.Available = false;
                _context.LeasedCopies.Add(leasing);
                _context.SaveChanges();
                _leasingView.Close();
            }
        }
    }
}
