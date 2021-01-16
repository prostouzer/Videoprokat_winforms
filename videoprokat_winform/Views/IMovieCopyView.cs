using System;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    public interface IMovieCopyView : IView
    {
        MovieOriginal CurrentMovie { get; set; }
        event Action<MovieCopy> OnAddMovieCopy;

        bool ConfirmNewMovieCopy();
    }
}
