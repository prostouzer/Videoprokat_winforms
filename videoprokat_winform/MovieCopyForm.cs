using System;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class MovieCopyForm : Form, IMovieCopyView
    {
        public MovieOriginal CurrentMovie { get; set; }

        public event Action<MovieCopy> OnAddMovieCopy;
        public MovieCopyForm()
        {
            InitializeComponent();

            addMovieButton.Click += (sender, args) =>
            {
                var movieId = CurrentMovie.Id;
                var commentary = commentTextBox.Text.Trim();
                var pricePerDay = priceNumericUpDown.Value;
                var copy = new MovieCopy(movieId, commentary, pricePerDay);
                OnAddMovieCopy?.Invoke(copy);
            };
        }

        public new void Show()
        {
            ShowDialog();
        }

        private void MovieCopyForm_Load(object sender, EventArgs e)
        {
            movieNameLabel.Text = CurrentMovie.Title;
        }

        public bool ConfirmNewMovieCopy()
        {
            if (commentTextBox.Text.Trim() != "" && priceNumericUpDown.Value > 0)
            {
                var dialogResult = MessageBox.Show("Создать новую копию фильма " + CurrentMovie.Title + ", " + commentTextBox.Text.Trim() +
                                                            ", с ценой " + priceNumericUpDown.Value + " руб. за день?", "Новая копия", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) 
                    return true;
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
            return false;
        }
    }
}
