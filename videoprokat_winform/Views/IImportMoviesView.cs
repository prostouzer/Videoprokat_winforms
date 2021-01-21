using System;
using System.Linq;
using videoprokat_winform.Models;

namespace videoprokat_winform.Views
{
    public interface IImportMoviesView : IView
    {
        event Action OnSelectNewFile;
        event Action OnUploadMovies;

        string ChooseFilePath();
        bool SkipWronglyDeclaredMovie(string[] movieValues);
        void RedrawMovies(IQueryable<MovieOriginal> moviesIQueryable);
        bool ConfirmUploadMovies();
    }
}
