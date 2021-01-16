using System;
using System.Windows.Forms;
using videoprokat_winform.Presenters;

namespace videoprokat_winform
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            var moviePresenter = new MoviePresenter();
            var movieCopyPresenter = new MovieCopyPresenter();
            var leasingPresenter = new LeasingPresenter();
            var customersPresenter = new CustomersPresenter();
            var importMoviesPresenter = new ImportMoviesPresenter();
            var returnPresenter = new ReturnPresenter();
            var mainFormPresenter = new MainPresenter(new MainForm(), moviePresenter, movieCopyPresenter, leasingPresenter, customersPresenter, importMoviesPresenter, returnPresenter);

            mainFormPresenter.Run();
        }
    }
}
