using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface IMovieCopyView : IView
    {
        MovieOriginal currentMovie { get; set; }
        event Action<MovieCopy> OnAddMovieCopy;

        bool ConfirmNewMovieCopy();
    }
}
