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
using videoprokat_winform.Presenters;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class MainForm : Form, IMainFormView
    {
        private MainFormPresenter _presenter;
        public DataGridView MoviesDgv { get => moviesDgv; set => moviesDgv = value; }
        public DataGridView CopiesDgv { get => copiesDgv; set => copiesDgv = value; }
        public DataGridView LeasingsDgv { get => leasingsDgv; set => leasingsDgv = value; }
        public string filter { get => mainMenu.Items[0].Text; set => mainMenu.Items[0].Text = value; }

        public new void Show()
        {
            Application.Run(this);
        }
        public MainForm()
        {
            InitializeComponent();

            _presenter = new MainFormPresenter(this);
            mainMenu.Items[0].TextChanged += _presenter.FilterMovies; // "Поиск"
            mainMenu.Items[1].Click += _presenter.OpenCustomersForm; // "Клиенты"
            mainMenu.Items[2].Click += _presenter.OpenImportMoviesForm; // "Импорт фильмов"

            copiesContextMenu.Items[0].Click += _presenter.OpenLeasingForm; // "Прокат"

            leasingContextMenu.Items[0].Click += _presenter.OpenReturnForm; // "Вернуть"

            _presenter.db = new VideoprokatContext();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            _presenter.RedrawMoviesDgv();
        }
        private void moviesDgv_SelectionChanged(object sender, EventArgs e) // из оригинального фильма загружаем в таблицу его копии
        {
            if (MoviesDgv.CurrentRow != null)
            {
                _presenter.RedrawCopiesDgv();
                newMovieCopyButton.Enabled = true;
            }
            else
            {
                newMovieCopyButton.Enabled = false;
            }
        }
        private void copiesDgv_SelectionChanged(object sender, EventArgs e) // из копии фильма загружаем в таблицу инфу по аренде
        {
            if (CopiesDgv.SelectedRows.Count > 0)
            {
                _presenter.RedrawLeasingsDgv();

                if ((bool)CopiesDgv.CurrentRow.Cells["Available"].Value == false) // нельзя изменять цену за день если копия на данный момент в пользовании
                {
                    CopiesDgv.Columns["PricePerDay"].ReadOnly = true;
                }
                else
                {
                    CopiesDgv.Columns["PricePerDay"].ReadOnly = false;
                }

            }
        }

        private void moviesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _presenter.db.SaveChanges();
        }

        private void moviesDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void copiesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _presenter.db.SaveChanges();
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
                if (CopiesDgv.SelectedCells.Count > 0)
                {
                    if ((bool)CopiesDgv.CurrentRow.Cells["Available"].Value == true)
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
                if (LeasingsDgv.SelectedCells.Count > 0)
                {
                    if ((bool)CopiesDgv.CurrentRow.Cells["Available"].Value == false)
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
            _presenter.OpenMovieCopyForm();
        }

        private void newMovieButton_Click(object sender, EventArgs e)
        {
            _presenter.OpenMovieForm();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _presenter.db.Dispose();
        }
    }
}
