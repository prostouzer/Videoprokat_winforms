using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class MainForm : Form, IMainView
    {
        public int CurrentMovieId => Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);

        public event Action OnOpenCustomers;
        public event Action OnOpenImportMovies;
        public event Action OnOpenMovie;
        public event Action<int> OnOpenMovieCopy;

        public event Action<string> OnFilterMovies;
        public event Action<int> OnOpenLeasing;
        public event Action<int> OnOpenReturn;

        public event Action<int> OnMovieSelectionChanged;
        public event Action<int> OnMovieCopySelectionChanged;

        public new event Action OnLoad;

        public event Action<int, MovieOriginal> OnUpdateMovie;
        public event Action<int, MovieCopy> OnUpdateMovieCopy;

        public new void Show()
        {
            Application.Run(this);
        }

        public MainForm()
        {
            InitializeComponent();

            this.Load += (sender, args) => OnLoad?.Invoke();

            newMovieButton.Click += (sender, args) => OnOpenMovie?.Invoke(); // Добавить новый фильм
            newMovieCopyButton.Click += (sender, args) => OnOpenMovieCopy?.Invoke(CurrentMovieId); // Добавить новую копию фильма
            mainMenu.Items[1].Click += (sender, args) => OnOpenCustomers?.Invoke(); // "Клиенты"
            mainMenu.Items[2].Click += (sender, args) => OnOpenImportMovies?.Invoke(); // "Импорт фильмов"
            copiesContextMenu.Items[0].Click += (sender, args) => OnOpenLeasing?.Invoke(Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value)); // "Прокат"
            leasingContextMenu.Items[0].Click += (sender, args) => OnOpenReturn?.Invoke(Convert.ToInt32(leasingsDgv.CurrentRow.Cells["Id"].Value)); // "Вернуть"

            mainMenu.Items[0].TextChanged += (sender, args) => OnFilterMovies?.Invoke(mainMenu.Items[0].Text.Trim()); // Поиск фильма

            moviesDgv.CellEndEdit += (sender, args) =>
            {
                var movieId = CurrentMovieId;
                var title = moviesDgv.CurrentRow.Cells["Title"].Value.ToString();
                var description = moviesDgv.CurrentRow.Cells["Description"].Value.ToString();
                var yearReleased = Convert.ToInt32(moviesDgv.CurrentRow.Cells["YearReleased"].Value);
                var updatedMovie = new MovieOriginal(title, description, yearReleased);
                OnUpdateMovie?.Invoke(movieId, updatedMovie);
            };
            copiesDgv.CellEndEdit += (sender, args) =>
            {
                var movieCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);
                var commentary = copiesDgv.CurrentRow.Cells["Commentary"].Value.ToString();
                var pricePerDay = Convert.ToInt32(copiesDgv.CurrentRow.Cells["PricePerDay"].Value);
                var updatedMovieCopy = new MovieCopy(Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value),
                    commentary, pricePerDay);
                OnUpdateMovieCopy?.Invoke(movieCopyId, updatedMovieCopy);
            };

            moviesDgv.DataError += (sender, args) => ShowDataError();
            copiesDgv.DataError += (sender, args) => ShowDataError();

            moviesDgv.SelectionChanged += (sender, args) =>
                OnMovieSelectionChanged?.Invoke(CurrentMovieId);
            copiesDgv.SelectionChanged += (sender, args) =>
                OnMovieCopySelectionChanged?.Invoke(Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value));
        }

        public void ShowDataError()
        {
            MessageBox.Show("Неправильный формат данных");
        }

        public void RedrawMovies(List<MovieOriginal> moviesList)
        {
            this.BeginInvoke(new Action(() => // без this.BeingInvoke ошибка reentrant call to SetCurrentCellAddressCore (решение https://stackoverflow.com/questions/27626158/reentrant-call-to-setcurrentcelladdresscore-in-event-handlers-only-where-cel)
            {
                moviesDgv.DataSource = moviesList;

                moviesDgv.Columns["Id"].ReadOnly = true;
                moviesDgv.Columns["Copies"].Visible = false;

                moviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                moviesDgv.Columns["Id"].HeaderText = "ID";
                moviesDgv.Columns["Title"].HeaderText = "Название";
                moviesDgv.Columns["Description"].HeaderText = "Описание";
                moviesDgv.Columns["YearReleased"].HeaderText = "Год выпуска";

                moviesDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                moviesDgv.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                moviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                moviesDgv.Columns["YearReleased"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }));
        }

        public void RedrawCopies(List<MovieCopy> movieCopiesList)
        {
            this.BeginInvoke(new Action(() =>
            {
                copiesDgv.DataSource = movieCopiesList;

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

                if (movieCopiesList.Count == 0
                ) // нет копий вообще - очищаем список Leasing'ов. Иначе сохраняются прокаты последнего выбранного фильма. Сильно костыльно?
                {
                    leasingsDgv.DataSource = null;
                }
            }));
        }

        public void RedrawLeasings(List<Leasing> leasingsList, List<Customer> customers)
        {
            int currentMovieCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);
            var movieCopyLeasingInfo =
                from leasing in leasingsList
                where leasing.MovieCopyId == currentMovieCopyId && leasing.ReturnDate == null
                join customer in customers on leasing.CustomerId equals customer.Id
                select new
                {
                    id = leasing.Id,
                    startDate = leasing.StartDate,
                    expectedEndDate = leasing.ExpectedEndDate,
                    totalPrice = leasing.TotalPrice,
                    customerName = customer.Name
                };

            leasingsDgv.DataSource = movieCopyLeasingInfo.ToList();

            leasingsDgv.Columns["id"].Visible = false;

            leasingsDgv.Columns["StartDate"].HeaderText = "Дата начала";
            leasingsDgv.Columns["ExpectedEndDate"].HeaderText = "Ожидаемый возврат";
            leasingsDgv.Columns["TotalPrice"].HeaderText = "Итоговая цена";
            leasingsDgv.Columns["CustomerName"].HeaderText = "Клиент";

            leasingsDgv.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void moviesDgv_SelectionChanged(object sender, EventArgs e)
        {
            if (moviesDgv.CurrentRow != null)
            {
                newMovieCopyButton.Enabled = true;
            }
            else
            {
                newMovieCopyButton.Enabled = false;
            }
        }

        private void copiesDgv_SelectionChanged(object sender, EventArgs e)
        {
            if (copiesDgv.SelectedRows.Count > 0)
            {
                if ((bool)copiesDgv.CurrentRow.Cells["Available"].Value == false) // нельзя изменять цену за день если копия на данный момент в пользовании
                {
                    copiesDgv.Columns["PricePerDay"].ReadOnly = true;
                }
                else
                {
                    copiesDgv.Columns["PricePerDay"].ReadOnly = false;
                }
            }
        }

        private void copiesDgv_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (copiesDgv.SelectedCells.Count > 0)
                {
                    if ((bool)copiesDgv.CurrentRow.Cells["Available"].Value == true)
                    {
                        copiesContextMenu.Items[0].Enabled = true; // "Прокат"
                    }
                    else
                    {
                        copiesContextMenu.Items[0].Enabled = false; // "Прокат"
                    }
                }
                else
                {
                    copiesContextMenu.Items[0].Enabled = false; // "Прокат"
                }
            }
        }

        private void leasingsDgv_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (leasingsDgv.SelectedCells.Count > 0)
                {
                    if ((bool)copiesDgv.CurrentRow.Cells["Available"].Value == false)
                    {
                        leasingContextMenu.Items[0].Enabled = true; // "Вернуть"
                    }
                    else
                    {
                        leasingContextMenu.Items[0].Enabled = false; // "Вернуть"
                    }
                }
                else
                {
                    leasingContextMenu.Items[0].Enabled = false; // "Вернуть"
                }
            }
        }
    }
}
