﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class MovieCopyForm : Form, IMovieCopyView
    {
        public MovieOriginal _currentMovie;
        public MovieOriginal currentMovie { get => _currentMovie; set => _currentMovie = value; }
        public event Action<MovieCopy> OnAddMovieCopy;
        public MovieCopyForm()
        {
            InitializeComponent();

            leaseButton.Click += (sender, args) => OnAddMovieCopy?.Invoke(new MovieCopy(currentMovie.Id, commentTextBox.Text.Trim(), priceNumericUpDown.Value));
        }

        public new void Show()
        {
            this.ShowDialog();
        }

        public bool ConfirmNewMovieCopy()
        {
            if (commentTextBox.Text.Trim() != "" && priceNumericUpDown.Value > 0)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Создать новую копию фильма " + currentMovie.Title + ", " + commentTextBox.Text.Trim() +
                    ", с ценой " + priceNumericUpDown.Value.ToString() + " руб. за день?", "Новая копия", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) return true;
            }
            else
            {
                MessageBox.Show("Заполните поле комментария");
            }
            return false;
        }

        private void MovieCopyForm_Load(object sender, EventArgs e)
        {
            movieNameLabel.Text = currentMovie.Title;
        }

        //private void leaseButton_Click(object sender, EventArgs e)
        //{
        //    if (commentTextBox.Text.Trim() != "" && priceNumericUpDown.Value > 0)
        //    {
        //        DialogResult dialogResult;
        //        dialogResult = MessageBox.Show("Создать новую копию фильма " + currentMovie.Title + ", " + commentTextBox.Text.Trim() +
        //            ", с ценой " + priceNumericUpDown.Value.ToString() + " руб. за день?", "Новая копия", MessageBoxButtons.YesNo);
        //        if (dialogResult == DialogResult.Yes)
        //        {
        //            MovieCopy copy = new MovieCopy(currentMovie.Id, commentTextBox.Text, priceNumericUpDown.Value);
        //            db.MoviesCopies.Add(copy);
        //            db.SaveChanges();
        //            this.Close();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Заполните поле комментария");
        //    }
        //}
    }
}
