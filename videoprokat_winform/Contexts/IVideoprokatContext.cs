using System.Data.Entity;
using videoprokat_winform.Models;

namespace videoprokat_winform.Contexts
{
    public interface IVideoprokatContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<MovieOriginal> MoviesOriginal { get; set; }
        DbSet<MovieCopy> MoviesCopies { get; set; }
        DbSet<Leasing> LeasedCopies { get; set; }
        int SaveChanges();
    }
}