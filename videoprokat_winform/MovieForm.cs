using System;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class MovieForm : Form, IMovieView
    {
        public event Action<MovieOriginal> OnAddMovie;

        public MovieForm()
        {
            InitializeComponent();

            movieButton.Click += (sender, args) =>
            {
                var title = movieTitleTextBox.Text.Trim();
                var description = movieDescriptionTextBox.Text.Trim();
                var yearReleased = Convert.ToInt32(yearReleasedNumericUpDown.Value);
                var movie = new MovieOriginal(title, description, yearReleased);
                OnAddMovie?.Invoke(movie);
            };
        }
        public new void Show()
        {
            ShowDialog();
        }
        public bool ConfirmNewMovie()
        {
            if (movieTitleTextBox.Text.Trim() != "")
            {
                var result = MessageBox.Show(movieDescriptionTextBox.Text.Trim() != "" ? $"Добавить фильм {movieTitleTextBox.Text.Trim()}, {movieDescriptionTextBox.Text.Trim()}, {yearReleasedNumericUpDown.Value} года?" 
                    : $"Добавить фильм {movieTitleTextBox.Text.Trim()}, {yearReleasedNumericUpDown.Value} года?", "Добавить фильм", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Укажите название фильма");
            }
            return false;
        }
    }
}
