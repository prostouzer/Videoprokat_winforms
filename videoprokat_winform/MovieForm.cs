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
