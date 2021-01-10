using System;
using System.Collections.Generic;
using System.Text;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    interface IImportMoviesView : IView
    {
        event Action OnSelectNewFile;
        event Action OnUploadMovies;

        string ChooseFilePath();
        bool SkipWronglyDeclaredMovie(string[] movieValues);
        void RedrawMovies(List<MovieOriginal> movies);
        bool ConfirmUploadMovies();
    }
}
