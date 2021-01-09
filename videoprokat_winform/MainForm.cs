using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class MainForm : Form, IMainView
    {
        //event Action OnOpenCustomersForm;
        //event Action OnOpenImportMoviesForm;
        public event Action OnOpenMovie;
        public event Action OnOpenMovieCopy;

        public event Action<string> OnFilterMovies;
        //event Action OnOpenLeasing;
        //event Action OnOpenReturn;

        public new event Action OnLoad;

        public event Action<int, MovieOriginal> OnUpdateMovie;
        public event Action<int> OnUpdateCopy;

        public new void Show()
        {
            Application.Run(this);
        }
        public MainForm()
        {
            InitializeComponent();

            newMovieButton.Click += (sender, args) => OnOpenMovie?.Invoke(); // Добавить новый фильм
            newMovieCopyButton.Click += (sender, args) => OnOpenMovieCopy?.Invoke(); // Добавить новую копию фильма
            this.Load += (sender, args) => OnLoad?.Invoke();

            mainMenu.Items[0].TextChanged += (sender, args) => OnFilterMovies?.Invoke(mainMenu.Items[0].Text.Trim()); // Поиск фильма
            moviesDgv.CellValueChanged += (sender, args) =>
            {
                var movieId = Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);
                var title = moviesDgv.CurrentRow.Cells["Title"].Value.ToString();
                var description = moviesDgv.CurrentRow.Cells["Description"].Value.ToString();
                var yearReleased = Convert.ToInt32(moviesDgv.CurrentRow.Cells["YearReleased"].Value);
                var updatedMovie = new MovieOriginal(title, description, yearReleased);
                OnUpdateMovie?.Invoke(movieId, updatedMovie);
            };
            moviesDgv.DataError += (sender, args) => ShowWrongFormatError();

            //mainMenu.Items[1].Click += On; // "Клиенты"
            //mainMenu.Items[2].Click += _mainFormPresenter.OpenImportMoviesForm; // "Импорт фильмов"

            //copiesContextMenu.Items[0].Click += _mainFormPresenter.OpenLeasingForm; // "Прокат"

            //leasingContextMenu.Items[0].Click += _mainFormPresenter.OpenReturnForm; // "Вернуть"
        }

        public void ShowWrongFormatError()
        {
            MessageBox.Show("Неправильный формат данных");
        }
        
        public void RedrawMoviesDgv(List<MovieOriginal> moviesList)
        {
            moviesDgv.DataSource = moviesList;
            moviesDgv.Columns["Id"].ReadOnly = true;
            moviesDgv.Columns["Copies"].Visible = false;

            //moviesDgv.Sort(moviesDgv.Columns["Title"], ListSortDirection.Ascending); // несортируемый List

            moviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            moviesDgv.Columns["Id"].HeaderText = "ID";
            moviesDgv.Columns["Title"].HeaderText = "Название";
            moviesDgv.Columns["Description"].HeaderText = "Описание";
            moviesDgv.Columns["YearReleased"].HeaderText = "Год выпуска";

            moviesDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            moviesDgv.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDgv.Columns["YearReleased"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        public void RedrawCopiesDgv(VideoprokatContext db)
        {
            int currentMovieId = Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);

            var movieCopies = (from r in db.MoviesCopies
                               where r.MovieId == currentMovieId
                               select r).ToList();

            copiesDgv.DataSource = movieCopies;

            copiesDgv.Columns["Id"].ReadOnly = true;
            copiesDgv.Columns["Available"].ReadOnly = true;
            copiesDgv.Columns["MovieId"].Visible = false;
            copiesDgv.Columns["Movie"].Visible = false;

            copiesDgv.Columns["Id"].HeaderText = "ID";
            copiesDgv.Columns["Commentary"].HeaderText = "Комментарий";
            copiesDgv.Columns["Available"].HeaderText = "Доступен";
            copiesDgv.Columns["PricePerDay"].HeaderText = "Цена/день";

            copiesDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            copiesDgv.Columns["Commentary"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            copiesDgv.Columns["Available"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            copiesDgv.Columns["PricePerDay"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        public void RedrawLeasingsDgv(VideoprokatContext db)
        {
            int currentMovieCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);

            var movieCopyLeasingInfo = from leasing in db.LeasedCopies
                                       where leasing.MovieCopyId == currentMovieCopyId && leasing.ReturnDate == null
                                       join customer in db.Customers on leasing.CustomerId equals customer.Id
                                       select new
                                       {
                                           id = leasing.Id,
                                           startDate = leasing.LeasingStartDate,
                                           expectedEndDate = leasing.LeasingExpectedEndDate,
                                           totalPrice = leasing.TotalPrice,
                                           customerName = customer.Name
                                       };

            leasingsDgv.DataSource = movieCopyLeasingInfo.ToList();
            leasingsDgv.Columns["Id"].Visible = false;

            leasingsDgv.Columns["startDate"].HeaderText = "Дата начала";
            leasingsDgv.Columns["expectedEndDate"].HeaderText = "Ожидаемый возврат";
            leasingsDgv.Columns["totalPrice"].HeaderText = "Итоговая цена";
            leasingsDgv.Columns["customerName"].HeaderText = "Клиент";

            leasingsDgv.Columns["customerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        //private void moviesDgv_SelectionChanged(object sender, EventArgs e) // из оригинального фильма загружаем в таблицу его копии
        //{
        //    if (MoviesDgv.CurrentRow != null)
        //    {
        //        _mainFormPresenter.RedrawCopiesDgv();
        //        newMovieCopyButton.Enabled = true;
        //    }
        //    else
        //    {
        //        newMovieCopyButton.Enabled = false;
        //    }
        //}
        //private void copiesDgv_SelectionChanged(object sender, EventArgs e) // из копии фильма загружаем в таблицу инфу по аренде
        //{
        //    if (CopiesDgv.SelectedRows.Count > 0)
        //    {
        //        _mainFormPresenter.RedrawLeasingsDgv();

        //        if ((bool)CopiesDgv.CurrentRow.Cells["Available"].Value == false) // нельзя изменять цену за день если копия на данный момент в пользовании
        //        {
        //            CopiesDgv.Columns["PricePerDay"].ReadOnly = true;
        //        }
        //        else
        //        {
        //            CopiesDgv.Columns["PricePerDay"].ReadOnly = false;
        //        }

        //    }
        //}

        //private void moviesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    _mainFormPresenter.db.SaveChanges();
        //}

        //private void moviesDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    MessageBox.Show("Неправильный формат данных");
        //}

        //private void copiesDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    MessageBox.Show("Неправильный формат данных");
        //}

        //private void leasingsDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        //{
        //    MessageBox.Show("Неправильный формат данных");
        //}

        //private void copiesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    _mainFormPresenter.db.SaveChanges();
        //}

        //private void copiesDgv_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        if (CopiesDgv.SelectedCells.Count > 0)
        //        {
        //            if ((bool)CopiesDgv.CurrentRow.Cells["Available"].Value == true)
        //            {
        //                copiesContextMenu.Items[0].Enabled = true; // "Прокат"
        //            }
        //            else
        //            {
        //                copiesContextMenu.Items[0].Enabled = false; // "Прокат"
        //            }
        //        }
        //        else
        //        {
        //            copiesContextMenu.Items[0].Enabled = false; // "Прокат"
        //        }
        //    }
        //}

        //private void leasingsDgv_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        if (LeasingsDgv.SelectedCells.Count > 0)
        //        {
        //            if ((bool)CopiesDgv.CurrentRow.Cells["Available"].Value == false)
        //            {
        //                leasingContextMenu.Items[0].Enabled = true; // "Вернуть"
        //            }
        //            else
        //            {
        //                leasingContextMenu.Items[0].Enabled = false; // "Вернуть"
        //            }
        //        }
        //        else
        //        {
        //            leasingContextMenu.Items[0].Enabled = false; // "Вернуть"
        //        }
        //    }
        //}
        //private void newMovieCopyButton_Click(object sender, EventArgs e)
        //{
        //    _mainFormPresenter.OpenMovieCopyForm();
        //}

        //private void newMovieButton_Click(object sender, EventArgs e)
        //{
        //    _mainFormPresenter.OpenMovie();
        //}

        //private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    _mainFormPresenter.db.Dispose();
        //}
    }
}
