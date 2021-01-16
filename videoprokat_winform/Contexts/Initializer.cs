using System.Data.Entity;
using videoprokat_winform.Models;

namespace videoprokat_winform.Contexts
{
    //internal class Initializer : DropCreateDatabaseAlways<VideoprokatContext>
    internal class Initializer : CreateDatabaseIfNotExists<VideoprokatContext>
    {
        protected override void Seed(VideoprokatContext db)
        {
            var mo1 = new MovieOriginal("Терминатор", "Невероятный боевик", 1990);
            var mo2 = new MovieOriginal("Шрек", "Блокбастер девяностых", 2000);

            db.MoviesOriginal.Add(mo1);
            db.MoviesOriginal.Add(mo2);
            db.SaveChanges(); // сохраняем сначала здесь, чтобы у фильмов появились собственные id, и чтобы копиям было на что ссылаться в movieId

            var mc1 = new MovieCopy(1, "Хорошее качество", 80);
            var mc2 = new MovieCopy(1, "Ужасное качество", 50);
            var mc3 = new MovieCopy(2, "На английском языке", 60);
            var mc4 = new MovieCopy(1, "На русском языке", 110);
            var mc5 = new MovieCopy(1, "Качество 360", 30);
            var mc6 = new MovieCopy(2, "В 4D", 200);
            db.MoviesCopies.Add(mc1);
            db.MoviesCopies.Add(mc2);
            db.MoviesCopies.Add(mc3);
            db.MoviesCopies.Add(mc4);
            db.MoviesCopies.Add(mc5);
            db.MoviesCopies.Add(mc6);

            var c1 = new Customer("Петрович");
            var c2 = new Customer("Иванович");
            db.Customers.Add(c1);
            db.Customers.Add(c2);

            db.SaveChanges();
        }
    }
}
