using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace videoprokat_winform.Models
{
    //class Initializer : DropCreateDatabaseAlways<VideoprokatContext>
    class Initializer : CreateDatabaseIfNotExists<VideoprokatContext>
    {
        protected override void Seed(VideoprokatContext db)
        {
            MovieOriginal mo1 = new MovieOriginal("Терминатор", "Невероятный боевик", 1990);
            MovieOriginal mo2 = new MovieOriginal("Шрек", "Блокбастер девяностых", 2000);

            db.MoviesOriginal.Add(mo1);
            db.MoviesOriginal.Add(mo2);
            db.SaveChanges(); // сохраняем сначала здесь, чтобы у фильмов появились собственные id, и чтобы копиям было на что ссылаться в movieId

            MovieCopy mc1 = new MovieCopy(1, "Хорошее качество", 80);
            MovieCopy mc2 = new MovieCopy(1, "Ужасное качество", 50);
            MovieCopy mc3 = new MovieCopy(2, "На английском языке", 60);
            MovieCopy mc4 = new MovieCopy(1, "На русском языке", 110);
            MovieCopy mc5 = new MovieCopy(1, "Качество 360", 30);
            MovieCopy mc6 = new MovieCopy(2, "В 4D", 200);
            db.MoviesCopies.Add(mc1);
            db.MoviesCopies.Add(mc2);
            db.MoviesCopies.Add(mc3);
            db.MoviesCopies.Add(mc4);
            db.MoviesCopies.Add(mc5);
            db.MoviesCopies.Add(mc6);

            Customer c1 = new Customer("Петрович");
            Customer c2 = new Customer("Иванович");
            db.Customers.Add(c1);
            db.Customers.Add(c2);

            db.SaveChanges();
        }
    }
}
