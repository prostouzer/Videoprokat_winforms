using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
        void RedrawMovies(List<MovieOriginal> moviesList);
        void RedrawCopies(List<MovieCopy> movieCopiesList);
        void RedrawLeasings(List<Leasing> leasingsList, List<Customer> customersList); // второй лист клиентов для того, чтобы отображать еще CustomerName на выводе (исп. join)
    }
}
