using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        private VideoprokatContext _context;
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
            var dbContextOptions = new DbContextOptionsBuilder<VideoprokatContext>().UseInMemoryDatabase("TestDb");
            _context = new VideoprokatContext(dbContextOptions.Options);
            _context.Database.EnsureDeleted(); // мне не нужны заполненные данные из OnModelCreating после EnsureCreated
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
            //act
            _presenter.Run();

            //assert
            _view.Received().Show();
        }

        [Test]
        public void OpenCustomers()
        {
            //act
            _presenter.OpenCustomers();

            //assert
            _customersPresenter.Received().Run();
        }

        [Test]
        public void OpenMovie()
        {
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
            var testMovie = new MovieOriginal("TEST", "TEST", 9999);
            _context.MoviesOriginal.Add(testMovie);
            _context.MoviesCopies.Add(new MovieCopy(9999, "TEST", 9999));
            _context.SaveChanges();

            //act
            _presenter.OpenMovieCopy(testMovie.Id);

            //assert
            _movieCopyPresenter.Received().Run(Arg.Any<MovieOriginal>());
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void OpenLeasing()
        {
            //arrange
            var testMovie = new MovieOriginal("TEST", "TEST", 9999);
            _context.MoviesOriginal.Add(testMovie);
            _context.SaveChanges(); // нужно чтобы testMovieCopy получил актуальный testMovie.Id после добавления в контекст. Иначе testMovie.Id будет 0
            var testMovieCopy = new MovieCopy(testMovie.Id, "TEST", 9999);
            _context.MoviesCopies.Add(testMovieCopy);
            _context.SaveChanges();

            //act
            _presenter.OpenLeasing(testMovieCopy.Id);

            //assert
            _leasingPresenter.Received().Run(Arg.Any<MovieOriginal>(), Arg.Any<MovieCopy>());
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
            _view.Received().RedrawLeasings(Arg.Any<IQueryable<Leasing>>(), Arg.Any<IQueryable<Customer>>());
        }

        [Test]
        public void OpenImportMovies()
        {
            //act
            _presenter.OpenImportMovies();

            //assert
            _importMoviesPresenter.Received().Run();
            _view.Received().RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
        }

        [Test]
        public void OpenReturn()
        {
            //act
            _presenter.OpenReturn(9999);

            //assert
            _returnPresenter.Received().Run(Arg.Any<int>());
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void LoadMain()
        {
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
            //arrang
            _context.MoviesOriginal.Add(new MovieOriginal("Терминатор", "Описание терминатора!", 1999));
            _context.MoviesOriginal.Add(new MovieOriginal("Шрек", "Описание шрека!", 2000));
            _context.MoviesOriginal.Add(new MovieOriginal("Терминатор 2", "Описание терминатора 2!", 2001));
            _context.MoviesOriginal.Add(new MovieOriginal("Терминатор 3", "Описание терминатора 3!", 2001));
            _context.MoviesOriginal.Add(new MovieOriginal("Шрек 2", "Описание шрека 2!", 2002));
            _context.MoviesOriginal.Add(new MovieOriginal("Человек-паук", "Описание человека-паука!", 2003));
            _context.SaveChanges();

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
            _context.MoviesOriginal.Add(new MovieOriginal("Терминатор", "Описание терминатора!", 1999));
            _context.MoviesOriginal.Add(new MovieOriginal("Шрек", "Описание шрека!", 2000));
            _context.MoviesOriginal.Add(new MovieOriginal("Терминатор 2", "Описание терминатора 2!", 2001));
            _context.MoviesOriginal.Add(new MovieOriginal("Терминатор 3", "Описание терминатора 3!", 2001));
            _context.MoviesOriginal.Add(new MovieOriginal("Шрек 2", "Описание шрека 2!", 2002));
            _context.MoviesOriginal.Add(new MovieOriginal("Человек-паук", "Описание человека-паука!", 2003));
            _context.SaveChanges();

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
            _context.MoviesOriginal.Add(initialMovie);
            _context.SaveChanges();
            var updatedMovie = new MovieOriginal("НОВОЕ ИМЯ", "НОВОЕ ОПИСАНИЕ!!!", 9999);

            //act
            _presenter.UpdateMovie(initialMovie.Id, updatedMovie);

            //assert
            Assert.AreEqual(updatedMovie.Title, initialMovie.Title);
            Assert.AreEqual(updatedMovie.Description, initialMovie.Description);
            Assert.AreEqual(updatedMovie.YearReleased, initialMovie.YearReleased);

            _view.Received().RedrawMovies(Arg.Any<IQueryable<MovieOriginal>>());
        }

        [Test]
        public void UpdateMovieCopy()
        {
            //arrange
            var initialMovieCopy = new MovieCopy(9999, "Старый комментарий :(", 7777);
            _context.MoviesCopies.Add(initialMovieCopy);
            _context.SaveChanges();
            var updatedMovieCopy = new MovieCopy(9999, "НОВЫЙ КОММЕНТАРИЙ!!!", 8888);

            //act
            _presenter.UpdateMovieCopy(initialMovieCopy.Id, updatedMovieCopy);
            var actual = _context.MoviesCopies.Single(c=> c.Id == initialMovieCopy.Id);

            //assert
            Assert.AreEqual(updatedMovieCopy.Commentary, actual.Commentary);
            Assert.AreEqual(updatedMovieCopy.PricePerDay, actual.PricePerDay);

            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void MovieSelectionChanged()
        {
            //act
            _presenter.MovieSelectionChanged(9999);

            //assert
            _view.Received().RedrawCopies(Arg.Any<IQueryable<MovieCopy>>());
        }

        [Test]
        public void MovieCopySelectionChanged()
        {
            //act
            _presenter.MovieCopySelectionChanged(9999);

            //assert
            _view.Received().RedrawLeasings(Arg.Any<IQueryable<Leasing>>(), Arg.Any<IQueryable<Customer>>());
        }
    }
}
