using System.Collections.Generic;
using System.Linq;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters
{
    public class LeasingPresenter
    {
        private ILeasingView _leasingView;
        private VideoprokatContext _context;
        public void Run(MovieOriginal currentMovie, MovieCopy currentMovieCopy, VideoprokatContext context)
        {
            _context = context;

            _leasingView = new LeasingForm
            {
                CurrentMovie = currentMovie,
                CurrentMovieCopy = currentMovieCopy
            };

            _leasingView.RedrawCustomers(new List<Customer>(_context.Customers.ToList()));

            _leasingView.OnLeaseMovieCopy += AddLeasing;

            _leasingView.Show();
        }

        public void AddLeasing(Leasing leasing)
        {
            if (!_leasingView.ConfirmNewLeasing()) return;
            var movieCopy = _context.MoviesCopies.First(c => c.Id == leasing.MovieCopyId);
            movieCopy.Available = false;
            _context.LeasedCopies.Add(leasing);
            _context.SaveChanges();
            _leasingView.Close();
        }
    }
}
