using videoprokat_winform.Contexts;
using videoprokat_winform.Models;

namespace videoprokat_winform.Presenters
{
    public interface IMoviePresenter
    {
        void Run();
        void AddMovie(MovieOriginal movie);
    }
}