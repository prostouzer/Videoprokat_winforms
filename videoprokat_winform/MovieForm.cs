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
    public partial class MovieForm : Form
    {
        public MovieForm()
        {
            InitializeComponent();
        }

        private void movieButton_Click(object sender, EventArgs e)
        {
            if (movieTitleTextBox.Text.Trim() != "")
            {
                using (VideoprokatContext db = new VideoprokatContext())
                {
                    MovieOriginal newMovie = new MovieOriginal
                    {
                        Title = movieTitleTextBox.Text.Trim(),
                        Description = movieDescriptionTextBox.Text.Trim(),
                        YearReleased = Convert.ToInt32(yearReleasedNumericUpDown.Value)
                    };
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
