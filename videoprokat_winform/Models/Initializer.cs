using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace videoprokat_winform.Models
{
    class Initializer : DropCreateDatabaseAlways<VideoprokatContext>
    {
        protected override void Seed(VideoprokatContext db)
        {
            MovieOriginal mo1 = new MovieOriginal { Id = 1, Title = "Терминатор", Description = "Невероятный боевик", YearReleased=1990 };
            MovieOriginal mo2 = new MovieOriginal { Id = 2, Title = "Шрек", Description = "Блокбастер девяностых", YearReleased=2000 };
            db.MoviesOriginal.Add(mo1);
            db.MoviesOriginal.Add(mo2);
            MovieCopy mc1 = new MovieCopy { MovieId = 1, Available = true, Commentary = "Хорошее качество", PricePerDay = 80 };
            MovieCopy mc2 = new MovieCopy { MovieId = 1, Available = true, Commentary = "Ужасное качество(((", PricePerDay = 50 };
            MovieCopy mc3 = new MovieCopy { MovieId = 2, Available = true, Commentary = "Пойдет фильмец", PricePerDay = 60 };
            db.MoviesCopies.Add(mc1);
            db.MoviesCopies.Add(mc2);
            db.MoviesCopies.Add(mc3);

            Client c1 = new Client { Name = "Петрович", Rating = 5 };
            Client c2 = new Client { Name = "Ваня", Rating = 4 };
            db.Clients.Add(c1);
            db.Clients.Add(c2);

            //DateTime d1 = new DateTime(2020, 11, 19);
            //DateTime d2 = new DateTime(2020, 11, 22);
            //DateTime d3 = new DateTime(2020, 11, 24);

            //Leasing l1 = new Leasing(mc1, c2, d1, d2);
            //Leasing l2 = new Leasing(mc2, c2, d1, d3);

            Leasing l1 = new Leasing(mc1, c2, Convert.ToDateTime("19/11/2020"), Convert.ToDateTime("22/11/2020"));
            Leasing l2 = new Leasing(mc2, c2, Convert.ToDateTime("19/11/2020"), Convert.ToDateTime("24/11/2020"));
            Leasing l3 = new Leasing(mc3, c1, Convert.ToDateTime("19/11/2020"), Convert.ToDateTime("25/11/2020"));

            //c1.LeaseCopy(l1);
            //c2.LeaseCopy(l2);

            db.LeasedCopies.Add(l1);
            db.LeasedCopies.Add(l2);
            db.LeasedCopies.Add(l3);

            db.SaveChanges();
        }
    }
}
