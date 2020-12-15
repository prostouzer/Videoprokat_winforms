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

namespace videoprokat_winform
{
    public partial class MainForm : Form
    {
        private VideoprokatContext db;
        public MainForm()
        {
            InitializeComponent();

            mainMenu.Items[0].TextChanged += FilterMovies; // "Поиск"
            mainMenu.Items[1].Click += OpenCustomersForm; // "Клиенты"
            mainMenu.Items[2].Click += OpenImportMoviesForm;

            copiesContextMenu.Items[0].Click += OpenLeaseForm; // "Прокат"

            leasingContextMenu.Items[0].Click += OpenReturnForm; // "Вернуть"

            db = new VideoprokatContext();
        }
        void FilterMovies(object sender, EventArgs e)
        {
            var filteredData = db.MoviesOriginal.Where(m => m.Title.Contains(searchBox.Text)).ToList();
            if (filteredData.Count() > 0 && searchBox.Text != "")
            {
                moviesDgv.DataSource = filteredData;
            }
            else
            {
                RedrawMoviesDgv();
            }
        }
        void OpenCustomersForm(object sender, EventArgs e)
        {
            CustomersForm form = new CustomersForm(db);
            form.ShowDialog();
        }
        void OpenImportMoviesForm(object sender, EventArgs e)
        {
            ImportMoviesForm form = new ImportMoviesForm(db);
            form.ShowDialog();
            RedrawMoviesDgv();
        }
        void OpenLeaseForm(object sender, EventArgs e)
        {
            int currentCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);
            MovieCopy movieCopy = db.MoviesCopies.First(c => c.Id == currentCopyId);
            LeasingForm form = new LeasingForm(db, movieCopy);
            form.ShowDialog();
            RedrawCopiesDgv();
            RedrawLeasingsDgv();
        }
        void OpenReturnForm(object sender, EventArgs e)
        {
            int currentLeasingId = Convert.ToInt32(leasingsDgv.CurrentRow.Cells["Id"].Value);
            Leasing leasing = db.LeasedCopies.First(l => l.Id == currentLeasingId);
            ReturnForm form = new ReturnForm(db, leasing);
            form.ShowDialog();
            RedrawCopiesDgv();
            RedrawLeasingsDgv();
        }

        private void RedrawMoviesDgv()
        {
            db.MoviesOriginal.Load();
            moviesDgv.DataSource = db.MoviesOriginal.Local.ToBindingList();
            moviesDgv.Columns["Id"].ReadOnly = true;
            moviesDgv.Columns["Copies"].Visible = false;

            moviesDgv.Sort(moviesDgv.Columns["Title"], ListSortDirection.Ascending);

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
        private void RedrawCopiesDgv()
        {
            int CurrentMovieId = Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);

            var movieCopies = (from r in db.MoviesCopies
                               where r.MovieId == CurrentMovieId
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
        private void RedrawLeasingsDgv()
        {
            int CurrentMovieCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);

            var movieCopyLeasingInfo = from leasing in db.LeasedCopies
                                       where leasing.MovieCopyId == CurrentMovieCopyId && leasing.ReturnDate == null
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
        private void MainForm_Load(object sender, EventArgs e)
        {
            RedrawMoviesDgv();
        }
        private void moviesDgv_SelectionChanged(object sender, EventArgs e) // из оригинального фильма загружаем в таблицу его копии
        {
            if (moviesDgv.CurrentRow != null)
            {
                RedrawCopiesDgv();
                newMovieCopyButton.Enabled = true;
            }
            else
            {
                newMovieCopyButton.Enabled = false;
            }
        }
        private void copiesDgv_SelectionChanged(object sender, EventArgs e) // из копии фильма загружаем в таблицу инфу по аренде
        {
            if (copiesDgv.SelectedRows.Count > 0)
            {
                RedrawLeasingsDgv();

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

        private void moviesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }

        private void moviesDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void copiesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }

        private void copiesDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void leasingsDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
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
        private void newMovieCopyButton_Click(object sender, EventArgs e)
        {
            int currentMovieId = Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);
            MovieOriginal movie = db.MoviesOriginal.First(m => m.Id == currentMovieId);
            MovieCopyForm form = new MovieCopyForm(db, movie);
            form.ShowDialog();
            RedrawCopiesDgv();
        }

        private void newMovieButton_Click(object sender, EventArgs e)
        {
            MovieForm form = new MovieForm(db);
            form.ShowDialog();
            RedrawMoviesDgv();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }
    }
}
