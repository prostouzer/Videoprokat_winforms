using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using videoprokat_winform.Contexts;
using videoprokat_winform.Models;
using videoprokat_winform.Presenters;
using videoprokat_winform.Views;

namespace videoprokat_winform.Tests.Presenters
{
    [TestFixture]
    internal class MainPresenterTests
    {
        private IMainView _view;
        private IVideoprokatContext _context;
        private IMoviePresenter _moviePresenter;
        private IMovieCopyPresenter _movieCopyPresenter;
        private ILeasingPresenter _leasingPresenter;
        private ICustomersPresenter _customersPresenter;
        private IImportMoviesPresenter _importMoviesPresenter;
        private IReturnPresenter _returnPresenter;

        private MainPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<IMainView>();
            _context = Substitute.For<IVideoprokatContext>();
            _moviePresenter = Substitute.For<IMoviePresenter>();
            _movieCopyPresenter = Substitute.For<IMovieCopyPresenter>();
            _leasingPresenter = Substitute.For<ILeasingPresenter>();
            _customersPresenter = Substitute.For<ICustomersPresenter>();
            _importMoviesPresenter = Substitute.For<IImportMoviesPresenter>();
            _returnPresenter = Substitute.For<IReturnPresenter>();

            _presenter = new MainPresenter(
                _view, 
                _context, 
                _moviePresenter, 
                _movieCopyPresenter, 
                _leasingPresenter, 
                _customersPresenter, 
                _importMoviesPresenter, 
                _returnPresenter
                );
        }

        [Test]
        public void Run()
        {
            //arrange


            //act
            _presenter.Run();

            //assert
            _view.Received().Show();
        }

        [Test]
        public void OpenCustomers()
        {
            //arrange


            //act
            _presenter.OpenCustomers();

            //assert
            _customersPresenter.Received().Run();
        }

        [Test]
        public void OpenMovie()
        {
            //arrange


            //act
            _presenter.OpenMovie();

            //assert
            _moviePresenter.Received().Run();
            _view.Received().RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
        }

        [Test]
        public void OpenMovieCopy()
        {
            //arrange
            var movies = new FakeDbSet<MovieOriginal> {new MovieOriginal("TEST", "TEST", 9999)};
            _context.MoviesOriginal.Returns(movies);
            var movieCopies = new FakeDbSet<MovieCopy> {new MovieCopy(9999, "TEST", 9999)};
            _context.MoviesCopies.Returns(movieCopies);

            const int movieId = 0;

            //act
            _presenter.OpenMovieCopy(movieId);

            //assert
            _movieCopyPresenter.Received().Run(Arg.Any<MovieOriginal>());
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void OpenLeasing()
        {
            //arrange
            var movies = new FakeDbSet<MovieOriginal> { new MovieOriginal("TEST", "TEST", 9999) };
            _context.MoviesOriginal.Returns(movies);
            var movieCopies = new FakeDbSet<MovieCopy> { new MovieCopy(0, "TEST", 9999) };
            _context.MoviesCopies.Returns(movieCopies);
            var leasings = new FakeDbSet<Leasing> {new Leasing(DateTime.Now, DateTime.Now, 9999, 9999, 9999)};
            _context.LeasedCopies.Returns(leasings);

            const int movieCopyId = 0;

            //act
            _presenter.OpenLeasing(movieCopyId);

            //assert
            _leasingPresenter.Received().Run(Arg.Any<MovieOriginal>(), Arg.Any<MovieCopy>());
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
            _view.Received().RedrawLeasings(Arg.Any<IQueryable<Leasing>>(), Arg.Any<IQueryable<Customer>>());
        }

        [Test]
        public void OpenImportMovies()
        {
            //arrange


            //act
            _presenter.OpenImportMovies();

            //assert
            _importMoviesPresenter.Received().Run();
            _view.Received().RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
        }

        [Test]
        public void OpenReturn()
        {
            //arrange
            var copies = new FakeDbSet<MovieCopy>();
            _context.MoviesCopies.Returns(copies);

            const int leasingId = 0;

            //act
            _presenter.OpenReturn(leasingId);

            //assert
            _returnPresenter.Received().Run(Arg.Any<int>());
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void LoadMain()
        {
            //arrange
            var movies = new FakeDbSet<MovieOriginal>();
            _context.MoviesOriginal.Returns(movies);

            //act
            _presenter.LoadMain();

            //assert
            _view.RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
            _view.Received().OnUpdateMovie += Arg.Any<Action<int, MovieOriginal>>();
            _view.Received().OnUpdateMovieCopy += Arg.Any<Action<int, MovieCopy>>();
        }

        [Test]
        public void FilterMovies_Filtered()
        {
            //arrange
            var movies = new FakeDbSet<MovieOriginal>
            {
                new MovieOriginal("Терминатор", "Описание терминатора!", 1999),
                new MovieOriginal("Шрек", "Описание шрека!", 2000),
                new MovieOriginal("Терминатор 2", "Описание терминатора 2!", 2001),
                new MovieOriginal("Терминатор 3", "Описание терминатора 3!", 2001),
                new MovieOriginal("Шрек 2", "Описание шрека 2!", 2002),
                new MovieOriginal("Человек-паук", "Описание человека-паука!", 2003)
            };
            _context.MoviesOriginal.Returns(movies);


            //act
            _presenter.FilterMovies("Шрек");

            //assert
            _view.Received().RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
            _view.DidNotReceive().RedrawMovies(_context.MoviesOriginal); // и результат отрисовался, и этот результат не равен _context.MoviesOriginal (т.е. отрисовке всех фильмов когда фильтр не подходит)
        }

        [Test]
        public void FilterMovies_NotFiltered()
        {
            //arrange
            var movies = new FakeDbSet<MovieOriginal>
            {
                new MovieOriginal("Терминатор", "Описание терминатора!", 1999),
                new MovieOriginal("Шрек", "Описание шрека!", 2000),
                new MovieOriginal("Терминатор 2", "Описание терминатора 2!", 2001),
                new MovieOriginal("Терминатор 3", "Описание терминатора 3!", 2001),
                new MovieOriginal("Шрек 2", "Описание шрека 2!", 2002),
                new MovieOriginal("Человек-паук", "Описание человека-паука!", 2003)
            };
            _context.MoviesOriginal.Returns(movies);

            //act
            _presenter.FilterMovies("Мстители");

            //assert
            _view.Received().RedrawMovies(_context.MoviesOriginal); // рисуются все MovieOriginal, т.к. фильтр не подходит ни к одному
        }

        [Test]
        public void UpdateMovie()
        {
            //arrange
            var initialMovie = new MovieOriginal("Старое имя :(", "Старое описание :(", 8888);
            var updatedMovie = new MovieOriginal("НОВОЕ ИМЯ", "НОВОЕ ОПИСАНИЕ!!!", 9999);
            var movies = new FakeDbSet<MovieOriginal> { initialMovie };
            _context.MoviesOriginal.Returns(movies);

            const int initialMovieId = 0;

            //act
            _presenter.UpdateMovie(initialMovieId, updatedMovie);

            //assert
            Assert.AreEqual(updatedMovie.Title, initialMovie.Title);
            Assert.AreEqual(updatedMovie.Description, initialMovie.Description);
            Assert.AreEqual(updatedMovie.YearReleased, initialMovie.YearReleased);

            _context.Received().SaveChanges();
            _view.Received().RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
        }

        [Test]
        public void UpdateMovieCopy()
        {
            //arrange
            var initialMovieCopy = new MovieCopy(9999, "Старый комментарий :(", 7777);
            var updatedMovieCopy = new MovieCopy(9999, "НОВЫЙ КОММЕНТАРИЙ!!!", 8888);
            var movieCopies = new FakeDbSet<MovieCopy> { initialMovieCopy };
            _context.MoviesCopies.Returns(movieCopies);

            const int initialMovieCopyId = 0;

            //act
            _presenter.UpdateMovieCopy(initialMovieCopyId, updatedMovieCopy);

            //assert
            Assert.AreEqual(updatedMovieCopy.Commentary, initialMovieCopy.Commentary);
            Assert.AreEqual(updatedMovieCopy.PricePerDay, initialMovieCopy.PricePerDay);

            _context.Received().SaveChanges();
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void MovieSelectionChanged()
        {
            //arrange
            var movieCopies = new FakeDbSet<MovieCopy>();
            _context.MoviesCopies.Returns(movieCopies);

            const int movieId = 0;

            //act
            _presenter.MovieSelectionChanged(movieId);

            //assert
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void MovieCopySelectionChanged()
        {
            //arrange
            var leasings = new FakeDbSet<Leasing>();
            _context.LeasedCopies.Returns(leasings);
            var customers = new FakeDbSet<Customer>();
            _context.Customers.Returns(customers);

            const int movieCopyId = 0;

            //act
            _presenter.MovieCopySelectionChanged(movieCopyId);

            //assert
            _view.Received().RedrawLeasings(Arg.Any<IQueryable<Leasing>>(), Arg.Any<IQueryable<Customer>>());
        }
    }
}
