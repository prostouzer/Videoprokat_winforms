using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace videoprokat_winform.Views
{
    interface IMainFormView : IView
    {
        DataGridView MoviesDgv { get; }
        DataGridView CopiesDgv { get; set; }
        DataGridView LeasingsDgv { get; set; }

        string filter { get; set; }

        //event Action FilterMovies;
        //event Action OpenCustomersForm;
        //event Action OpenImportMoviesForm;

        //event Action OpenLeasingForm;
        //event Action OpenReturnForm;
        //event Action OpenMovieForm;
        //event Action OpenMovieCopyForm;

        //event Action RedrawMoviesDgv;
        //event Action RedrawCopiesDgv;
        //event Action RedrawLeasingsDgv;
    }
}
