using System;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    public interface IMovieView : IView
    {
        event Action<MovieOriginal> OnAddMovie;

        bool ConfirmNewMovie();
    }
}
