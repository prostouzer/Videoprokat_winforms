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
        public MainForm()
        {
            InitializeComponent();

            mainMenu.Items[0].TextChanged += FilterMovies; // "Поиск"
            mainMenu.Items[1].Click += OpenClientsForm; // "Клиенты"
            mainMenu.Items[2].Click += OpenImportMoviesForm;

            copiesContextMenu.Items[0].Click += OpenLeaseForm; // "Прокат"
            copiesContextMenu.Items[1].Click += OpenCreateCopyForm; // "Новая копия"

            leasingContextMenu.Items[0].Click += OpenReturnForm; // "Вернуть"
        }
        void FilterMovies(object sender, EventArgs e)
        {
            using (VideoprokatContext db = new VideoprokatContext())
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
        }
        void OpenClientsForm(object sender, EventArgs e)
        {
            ClientsForm form = new ClientsForm();
            form.ShowDialog();
        }
        void OpenImportMoviesForm(object sender, EventArgs e)
        {
            ImportMoviesForm form = new ImportMoviesForm();
            form.ShowDialog();
            RedrawMoviesDgv();
        }
        void OpenLeaseForm(object sender, EventArgs e)
        {
            using (VideoprokatContext db = new VideoprokatContext())
            {
                int currentCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);
                MovieCopy movieCopy = db.MoviesCopies.First(c => c.Id == currentCopyId);
                LeasingForm form = new LeasingForm(movieCopy);
                form.ShowDialog();
            }
            RedrawCopiesDgv();
            RedrawLeasingsDgv();
        }
        void OpenCreateCopyForm(object sender, EventArgs e)
        {
            using (VideoprokatContext db = new VideoprokatContext())
            {
                int currentMovieId = Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);
                MovieOriginal movie = db.MoviesOriginal.First(m => m.Id == currentMovieId);
                MovieCopyForm form = new MovieCopyForm(movie);
                form.ShowDialog();
            }
            RedrawCopiesDgv();
        }
        void OpenReturnForm(object sender, EventArgs e)
        {
            using (VideoprokatContext db = new VideoprokatContext())
            {
                int CurrentLeasingId = Convert.ToInt32(leasingsDgv.CurrentRow.Cells["Id"].Value);
                Leasing leasing = db.LeasedCopies.First(l => l.Id == CurrentLeasingId);
                ReturnForm form = new ReturnForm(leasing);
                form.ShowDialog();
            }
            RedrawCopiesDgv();
            RedrawLeasingsDgv();
        }

        private void RedrawMoviesDgv()
        {
            using (VideoprokatContext db = new VideoprokatContext())
            {
                db.MoviesOriginal.Load();
                moviesDgv.DataSource = db.MoviesOriginal.Local.ToBindingList();

                moviesDgv.Sort(moviesDgv.Columns["Title"], ListSortDirection.Ascending);
            }
        }
        private void RedrawCopiesDgv()
        {
            using (VideoprokatContext db = new VideoprokatContext())
            {
                int CurrentMovieId = Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);

                var movieCopies = (from r in db.MoviesCopies
                                   where r.MovieId == CurrentMovieId
                                   select r).ToList();

                copiesDgv.DataSource = movieCopies;
            }
        }
        private void RedrawLeasingsDgv()
        {
            using (VideoprokatContext db = new VideoprokatContext())
            {
                int CurrentMovieCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);

                var movieCopyLeasingInfo = (from r in db.LeasedCopies
                                            where r.MovieCopy.Id == CurrentMovieCopyId && r.ReturnDate == null
                                            select r).ToList();

                leasingsDgv.DataSource = movieCopyLeasingInfo;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //using (VideoprokatContext db = new VideoprokatContext())
            //{
            //    db.MoviesOriginal.Load();
            //    moviesDgv.DataSource = db.MoviesOriginal.Local.ToBindingList();
            //}
            RedrawMoviesDgv();

            moviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDgv.Columns["Copies"].Visible = false;

            moviesDgv.Columns["Id"].ReadOnly = true;

            moviesDgv.Columns["Id"].HeaderText = "ID";
            moviesDgv.Columns["Title"].HeaderText = "Название";
            moviesDgv.Columns["Description"].HeaderText = "Описание";
            moviesDgv.Columns["YearReleased"].HeaderText = "Год выпуска";

            moviesDgv.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            moviesDgv.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDgv.Columns["YearReleased"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void moviesDgv_SelectionChanged(object sender, EventArgs e) // из оригинального фильма загружаем в таблицу его копии
        {
            if (moviesDgv.CurrentRow != null)
            {
                RedrawCopiesDgv();

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
        }
        private void copiesDgv_SelectionChanged(object sender, EventArgs e) // из копии фильма загружаем в таблицу инфу по аренде
        {
            if (copiesDgv.SelectedRows.Count > 0)
            {
                RedrawLeasingsDgv();

                leasingsDgv.Columns["Id"].Visible = false;
                leasingsDgv.Columns["MovieCopy"].Visible = false;
                leasingsDgv.Columns["MovieCopyId"].Visible = false;
                leasingsDgv.Columns["Client"].Visible = false;
                leasingsDgv.Columns["ClientId"].Visible = false;

                leasingsDgv.Columns["LeasingStartDate"].HeaderText = "Дата начала";
                leasingsDgv.Columns["LeasingExpectedEndDate"].HeaderText = "Ожидаемый возврат";
                leasingsDgv.Columns["ReturnDate"].HeaderText = "Фактический возврат";
                leasingsDgv.Columns["TotalPrice"].HeaderText = "Итоговая цена";
                leasingsDgv.Columns["ClientName"].HeaderText = "Клиент";

                leasingsDgv.Columns["ClientName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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
            DataGridViewRow currentRow = moviesDgv.CurrentRow;
            int currentMovieId = (int)currentRow.Cells["Id"].Value;
            using (VideoprokatContext db = new VideoprokatContext())
            {
                MovieOriginal currentMovie = db.MoviesOriginal.First(m => m.Id == currentMovieId);
                currentMovie.Title = currentRow.Cells["Title"].Value.ToString();
                currentMovie.Description = currentRow.Cells["Description"].Value.ToString();
                currentMovie.YearReleased = Convert.ToInt32(currentRow.Cells["YearReleased"].Value);

                db.SaveChanges();
            }
        }

        private void moviesDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void copiesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow currentRow = copiesDgv.CurrentRow;
            int currentCopyId = (int)currentRow.Cells["Id"].Value;
            using (VideoprokatContext db = new VideoprokatContext())
            {
                MovieCopy currentCopy = db.MoviesCopies.First(m => m.Id == currentCopyId);
                currentCopy.Commentary = currentRow.Cells["Commentary"].Value.ToString();
                currentCopy.PricePerDay = (decimal)currentRow.Cells["PricePerDay"].Value;

                db.SaveChanges();
            }
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
                        copiesContextMenu.Items[0].Enabled = true; // leasing button
                    }
                    else
                    {
                        copiesContextMenu.Items[0].Enabled = false; // leasing button
                    }
                }
                else
                {
                    copiesContextMenu.Items[0].Enabled = false; // leasing button
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
                        leasingContextMenu.Items[0].Enabled = true; // return button
                    }
                    else
                    {
                        leasingContextMenu.Items[0].Enabled = false; // return button
                    }
                }
            }
        }
    }
}
