using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface IMovieFormView : IView
    {
        //event Action<string, string, int> OnAddMovie;
        event Action<MovieOriginal> OnAddMovie;

        bool ConfirmNewMovie();
    }
}
