using System.Data.Entity;
using videoprokat_winform.Models;

namespace videoprokat_winform.Contexts
{
    public interface IVideoprokatContext
    {
        IDbSet<Customer> Customers { get; set; }
        IDbSet<MovieOriginal> MoviesOriginal { get; set; }
        IDbSet<MovieCopy> MoviesCopies { get; set; }
        IDbSet<Leasing> LeasedCopies { get; set; }
        int SaveChanges();
    }
}