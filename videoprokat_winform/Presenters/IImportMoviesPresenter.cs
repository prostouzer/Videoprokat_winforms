namespace videoprokat_winform.Presenters
{
    public interface IImportMoviesPresenter
    {
        void Run();
        void SelectNewFile();
        void UploadMovies();
        void ExtractMoviesFromFile(string path);
    }
}