using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface IMainFormView : IView
    {
        //event Action OnOpenCustomersForm;
        //event Action OnOpenImportMoviesForm;
        event Action OnOpenMovieForm;

        event Action<string> OnFilterMovies;
        //event Action OnOpenMovieCopyForm;
        //event Action OnOpenLeasingForm;
        //event Action OnOpenReturnForm;

        event Action<int, string, string, int> OnUpdateMovie;

        event Action OnFormLoad;
        void RedrawMoviesDgv(List<MovieOriginal> moviesList);
        void RedrawCopiesDgv(VideoprokatContext db);
        void RedrawLeasingsDgv(VideoprokatContext db);
    }
}
