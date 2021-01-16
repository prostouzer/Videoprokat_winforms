using System.Data.Entity;
using videoprokat_winform.Models;

namespace videoprokat_winform.Contexts
{
    public class VideoprokatContext : DbContext
    {
        static VideoprokatContext()
        {
            Database.SetInitializer(new Initializer());
        }
        public VideoprokatContext() : base("MyConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<MovieOriginal> MoviesOriginal { get; set; }
        public DbSet<MovieCopy> MoviesCopies { get; set; }
        public DbSet<Leasing> LeasedCopies { get; set; }
    }
}
