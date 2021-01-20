using System.Data.Entity;
using videoprokat_winform.Models;

namespace videoprokat_winform.Contexts
{
    public class VideoprokatContext : DbContext, IVideoprokatContext
    {
        static VideoprokatContext()
        {
            Database.SetInitializer(new Initializer());
        }
        public VideoprokatContext() : base("MyConnection")
        {

        }

        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<MovieOriginal> MoviesOriginal { get; set; }
        public IDbSet<MovieCopy> MoviesCopies { get; set; }
        public IDbSet<Leasing> LeasedCopies { get; set; }
    }
}
