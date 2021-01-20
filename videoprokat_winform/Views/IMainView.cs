using System;
using System.Linq;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    public interface IMainView : IView
    {
        int CurrentMovieId { get; }

        event Action OnOpenCustomers;
        event Action OnOpenImportMovies;
        event Action<int> OnOpenLeasing;
        event Action OnOpenMovie;
        event Action<int> OnOpenMovieCopy;
        event Action<int> OnOpenReturn;

        event Action<int> OnMovieSelectionChanged;
        event Action<int> OnMovieCopySelectionChanged;

        event Action<string> OnFilterMovies;

        event Action<int, MovieOriginal> OnUpdateMovie;
        event Action<int, MovieCopy> OnUpdateMovieCopy;

        event Action OnLoad;
        void RedrawMovies(IQueryable<MovieOriginal> moviesListIQueryable);
        void RedrawCopies(IQueryable<MovieCopy> movieCopiesIQueryable);
        void RedrawLeasings(IQueryable<Leasing> leasingsList, IQueryable<Customer> customersList); // второй лист клиентов для того, чтобы отображать еще CustomerName на выводе (исп. join)
    }
}
