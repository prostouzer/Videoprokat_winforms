using videoprokat_winform.Models;

namespace videoprokat_winform.Presenters
{
    public interface ILeasingPresenter
    {
        void Run(MovieOriginal currentMovie, MovieCopy currentMovieCopy);
        void AddLeasing(Leasing leasing);
    }
}