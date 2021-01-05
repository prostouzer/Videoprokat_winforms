using System;
using System.Collections.Generic;
using System.Text;

namespace videoprokat_winform.Views
{
    interface IMovieFormView : IView
    {
        event Action<string, string, int> OnAddMovie;

        bool ConfirmNewMovie();
    }
}
