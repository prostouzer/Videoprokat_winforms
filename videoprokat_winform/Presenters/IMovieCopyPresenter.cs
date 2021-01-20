using videoprokat_winform.Models;

namespace videoprokat_winform.Presenters
{
    public interface IMovieCopyPresenter
    {
        void Run(MovieOriginal movie);
        void AddMovieCopy(MovieCopy movieCopy);
    }
}