using System;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class MovieCopyForm : Form, IMovieCopyView
    {
        private MovieOriginal _currentMovie;
        public MovieOriginal CurrentMovie
        {
            get => _currentMovie;
            set => _currentMovie = value;
        }
        public event Action<MovieCopy> OnAddMovieCopy;
        public MovieCopyForm()
        {
            InitializeComponent();

            addMovieButton.Click += (sender, args) =>
            {
                var movieId = _currentMovie.Id;
                var commentary = commentTextBox.Text.Trim();
                var pricePerDay = priceNumericUpDown.Value;
                var copy = new MovieCopy(movieId, commentary, pricePerDay);
                OnAddMovieCopy?.Invoke(copy);
            };
        }

        public new void Show()
        {
            this.ShowDialog();
        }

        private void MovieCopyForm_Load(object sender, EventArgs e)
        {
            movieNameLabel.Text = CurrentMovie.Title;
        }

        public bool ConfirmNewMovieCopy()
        {
            if (commentTextBox.Text.Trim() != "" && priceNumericUpDown.Value > 0)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Создать новую копию фильма " + CurrentMovie.Title + ", " + commentTextBox.Text.Trim() +
                    ", с ценой " + priceNumericUpDown.Value.ToString() + " руб. за день?", "Новая копия", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) 
                    return true;
            }
            else
            {
                MessageBox.Show("Заполните поле комментария");
            }
            return false;
        }
    }
}
