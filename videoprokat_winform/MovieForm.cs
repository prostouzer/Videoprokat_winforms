﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;

namespace videoprokat_winform
{
    public partial class MovieForm : Form
    {
        VideoprokatContext db;
        public MovieForm(VideoprokatContext context)
        {
            InitializeComponent();
            db = context;
        }

        private void movieButton_Click(object sender, EventArgs e)
        {
            if (movieTitleTextBox.Text.Trim() != "")
            {
                DialogResult result;
                if (movieDescriptionTextBox.Text.Trim() != "") // сообщение с описанием фильма
                {
                    result = MessageBox.Show($"Добавить фильм {movieTitleTextBox.Text.Trim()}, {movieDescriptionTextBox.Text.Trim()}, {yearReleasedNumericUpDown.Value} года?",
                        "Добавить фильм", MessageBoxButtons.YesNo);
                }
                else // сообщение без описания фильма, чтобы не выводил лишнюю запятую
                {
                    result = MessageBox.Show($"Добавить фильм {movieTitleTextBox.Text.Trim()}, {yearReleasedNumericUpDown.Value} года?",
                        "Добавить фильм", MessageBoxButtons.YesNo);
                }
                if (result == DialogResult.Yes)
                {
                    MovieOriginal newMovie = new MovieOriginal(movieTitleTextBox.Text.Trim(),
                        movieDescriptionTextBox.Text.Trim(), (int)yearReleasedNumericUpDown.Value);
                    db.MoviesOriginal.Add(newMovie);
                    db.SaveChanges();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Укажите название фильма");
            }
        }
    }
}