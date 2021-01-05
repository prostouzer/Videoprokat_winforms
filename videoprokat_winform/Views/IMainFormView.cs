using System;
using System.Collections.Generic;
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
        //event Action OnOpenMovieCopyForm;
        //event Action OnOpenLeasingForm;
        //event Action OnOpenReturnForm;

        event Action OnFormLoad;

        void RedrawMoviesDgv(VideoprokatContext db);
        void RedrawCopiesDgv(VideoprokatContext db);
        void RedrawLeasingsDgv(VideoprokatContext db);
    }
}
