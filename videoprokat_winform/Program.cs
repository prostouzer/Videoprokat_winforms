using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using videoprokat_winform.Contexts;
using videoprokat_winform.Presenters;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<MainPresenter>()?.Run();

            //var moviePresenter = new MoviePresenter();
            //var movieCopyPresenter = new MovieCopyPresenter();
            //var leasingPresenter = new LeasingPresenter();
            //var customersPresenter = new CustomersPresenter();
            //var importMoviesPresenter = new ImportMoviesPresenter();
            //var returnPresenter = new ReturnPresenter();
            //var mainFormPresenter = new MainPresenter(new MainForm(), moviePresenter, movieCopyPresenter, leasingPresenter, customersPresenter, importMoviesPresenter, returnPresenter);

            //mainFormPresenter.Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<VideoprokatContext>();
            services.AddTransient<MainPresenter>();

            services.AddTransient<IMainView, MainForm>(); 
            services.AddTransient<ICustomersView, CustomersForm>();
            services.AddTransient<IImportMoviesView, ImportMoviesForm>();
            services.AddTransient<ILeasingView, LeasingForm>();
            services.AddTransient<IMovieCopyView, MovieCopyForm>();
            services.AddTransient<IMovieView, MovieForm>();
            services.AddTransient<IReturnView, ReturnForm>();

            services.AddTransient<ICustomersPresenter, CustomersPresenter>();
            services.AddTransient<IImportMoviesPresenter, ImportMoviesPresenter>();
            services.AddTransient<ILeasingPresenter, LeasingPresenter>();
            services.AddTransient<IMovieCopyPresenter, MovieCopyPresenter>();
            services.AddTransient<IMoviePresenter, MoviePresenter>();
            services.AddTransient<IReturnPresenter, ReturnPresenter>();

            return services;
        }
    }
}
