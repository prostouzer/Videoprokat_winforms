using System.Linq;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform.Presenters.Implementation
{
    public class LeasingPresenter : ILeasingPresenter
    {
        private readonly ILeasingView _leasingView;
        private VideoprokatContext _context;

        public LeasingPresenter(ILeasingView view, VideoprokatContext context)
        {
            _leasingView = view;
            _context = context;
        }

        public void Run(MovieOriginal currentMovie, MovieCopy currentMovieCopy)
        {
            _leasingView.CurrentMovie = currentMovie;
            _leasingView.CurrentMovieCopy = currentMovieCopy;

            _leasingView.RedrawCustomers(_context.Customers);

            _leasingView.OnLeaseMovieCopy += AddLeasing;

            _leasingView.Show();
        }

        public void AddLeasing(Leasing leasing)
        {
            if (!_leasingView.ConfirmNewLeasing()) return;
            var movieCopy = _context.MoviesCopies.Single(c => c.Id == leasing.MovieCopyId);
            movieCopy.Available = false;
            _context.LeasedCopies.Add(leasing);
            _context.SaveChanges();
            _leasingView.Close();
        }
    }
}
