using System.Configuration;
using Microsoft.EntityFrameworkCore;
using videoprokat_winform.Models;

namespace videoprokat_winform.Contexts
{
    public sealed class VideoprokatContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MovieOriginal> MoviesOriginal { get; set; }
        public DbSet<MovieCopy> MoviesCopies { get; set; }
        public DbSet<Leasing> LeasedCopies { get; set; }

        public VideoprokatContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer("Петрович") {Id = 1}, 
                new Customer("Иванович") {Id = 2}
                );
            modelBuilder.Entity<MovieOriginal>().HasData(
                new MovieOriginal("Терминатор", "Невероятный боевик", 1990) {Id = 1}, 
                new MovieOriginal("Шрек", "Блокбастер девяностых", 2000) {Id = 2}
                );
            modelBuilder.Entity<MovieCopy>().HasData(
                new MovieCopy(1, "Хорошее качество", 80) {Id = 1}, 
                new MovieCopy(1, "Ужасное качество", 50) {Id = 2}, 
                new MovieCopy(2, "На английском языке", 60) {Id = 3}, 
                new MovieCopy(1, "На русском языке", 110) {Id = 4}, 
                new MovieCopy(1, "Качество 360", 30) {Id = 5}, 
                new MovieCopy(2, "В 4D", 200) {Id = 6}
                );
        }
    }
}
