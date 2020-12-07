using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;

namespace videoprokat_winform
{
    public partial class MovieCopyForm : Form
    {
        MovieOriginal currentMovie;
        public MovieCopyForm(MovieOriginal movie)
        {
            InitializeComponent();

            currentMovie = movie;
        }
        private void MovieCopyForm_Load(object sender, EventArgs e)
        {
            movieNameLabel.Text = currentMovie.Title;
        }
        private void leaseButton_Click(object sender, EventArgs e)
        {
            if (commentTextBox.Text.Trim() != "" && priceNumericUpDown.Value > 0)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Создать новую копию фильма " + currentMovie.Title + ", " + commentTextBox.Text.Trim() + 
                    ", с ценой " + priceNumericUpDown.Value.ToString() + " руб. за день?", "Новая копия", MessageBoxButtons.YesNo) ;
                if (dialogResult == DialogResult.Yes)
                {
                    MovieCopy copy = new MovieCopy();
                    copy.MovieId = currentMovie.Id;
                    copy.Available = true;
                    copy.Commentary = commentTextBox.Text;
                    copy.PricePerDay = priceNumericUpDown.Value;
                    using (VideoprokatContext db = new VideoprokatContext())
                    {
                        db.MoviesCopies.Add(copy);
                        db.SaveChanges();
                    }
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Заполните поле комментария");
            }
        }
    }
}
