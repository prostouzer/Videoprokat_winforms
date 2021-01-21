using System.Collections.Generic;
using System.Linq;
using videoprokat_winform.Models;

namespace videoprokat_winform.Presenters
{
    public interface IImportMoviesPresenter
    {
        List<MovieOriginal> MoviesList { get; }
        void Run();
        void SelectNewFile();
        void UploadMovies();
        void ExtractMoviesFromFile(string path);
    }
}