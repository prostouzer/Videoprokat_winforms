﻿using System;
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
        }

        VideoprokatContext db;
        private void MainForm_Load(object sender, EventArgs e)
        {
            db = new VideoprokatContext();
            db.MoviesOriginal.Load();

            moviesDgv.DataSource = db.MoviesOriginal.Local.ToBindingList();
            moviesDgv.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            moviesDgv.Columns["Copies"].Visible = false;

            moviesDgv.Columns["Id"].ReadOnly = true;
        }

        private void moviesDgv_SelectionChanged(object sender, EventArgs e) // из оригинального фильма загружаем в таблицу его копии
        {
            int CurrentMovieId = Convert.ToInt32(moviesDgv.CurrentRow.Cells["Id"].Value);

            var movieCopies = (from r in db.MoviesCopies
                               where r.MovieId == CurrentMovieId
                               select r).ToList();

            copiesDgv.DataSource = movieCopies;

            copiesDgv.Columns["Id"].ReadOnly = true;
            copiesDgv.Columns["MovieId"].Visible = false;
            copiesDgv.Columns["Movie"].Visible = false;
        }

        private void copiesDgv_SelectionChanged(object sender, EventArgs e) // из копии фильма загружаем в таблицу инфу по аренде
        {
            if (copiesDgv.SelectedCells.Count > 0)
            {
                int CurrentMovieCopyId = Convert.ToInt32(copiesDgv.CurrentRow.Cells["Id"].Value);

                var movieCopyLeasingInfo = (from r in db.LeasedCopies
                                            where r.MovieCopy.Id == CurrentMovieCopyId
                                            select r).ToList();

                leasingsDgv.DataSource = movieCopyLeasingInfo;

                leasingsDgv.Columns["Id"].Visible = false;
                leasingsDgv.Columns["MovieCopy"].Visible = false;
                leasingsDgv.Columns["MovieCopyId"].Visible = false;
                leasingsDgv.Columns["Client"].Visible = false;
                leasingsDgv.Columns["ClientId"].Visible = false;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void moviesDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }

        private void moviesDgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неправильный формат данных");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
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

        private void leasingsDgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
