using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace videoprokat_winform.Models
{
    class VideoprokatContext : DbContext
    {
        static VideoprokatContext()
        {
            //Database.SetInitializer<VideoprokatContext>(new Initializer());
        }
        public VideoprokatContext() : base("MyConnection")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<MovieOriginal> MoviesOriginal { get; set; }
        public DbSet<MovieCopy> MoviesCopies { get; set; }
        public DbSet<Leasing> LeasedCopies { get; set; }
    }
}
