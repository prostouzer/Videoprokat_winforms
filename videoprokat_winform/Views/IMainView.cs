using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface IMainView : IView
    {
        //event Action OnOpenCustomers;
        //event Action OnOpenImportMovies;
        //event Action OnOpenLeasing;
        event Action OnOpenMovie;
        event Action OnOpenMovieCopy;
        //event Action OnOpenReturn;

        event Action<string> OnFilterMovies;

        event Action<int, MovieOriginal> OnUpdateMovie;

        event Action OnLoad;
        void RedrawMoviesDgv(List<MovieOriginal> moviesList);
        void RedrawCopiesDgv(VideoprokatContext db);
        void RedrawLeasingsDgv(VideoprokatContext db);
    }
}
