using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;
using System.IO;

namespace videoprokat_winform
{
    public partial class ImportMoviesForm : Form
    {
        public ImportMoviesForm()
        {
            InitializeComponent();
        }

        string filePath;
        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                moviesListView.Items.Clear();
                filePath = openFileDialog.FileName;

                List<MovieOriginal> movies = ExtractMoviesFromTxt(filePath);
                if (movies.Count > 0)
                {
                    loadMoviesButton.Enabled = true;

                    foreach (MovieOriginal movie in movies)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = $"{movie.Title}, {movie.Description}, {movie.YearReleased.ToString()} года выпуска";
                        moviesListView.Items.Add(item);
                    }
                }
                else
                {
                    loadMoviesButton.Enabled = false;
                }
            }
        }

        List<MovieOriginal> moviesList = new List<MovieOriginal>();
        private List<MovieOriginal> ExtractMoviesFromTxt(string path)
        {
            bool abort = false;
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream && !abort)
                {
                    var line = reader.ReadLine();
                    var movies = line.Split(';');
                    foreach (string movie in movies)
                    {
                        if (movie != "")
                        {
                            string[] movieValues = movie.Split(",,");

                            try
                            {
                                string title = movieValues[0];
                                string description = movieValues[1];
                                int yearReleased = Convert.ToInt32(movieValues[2]);

                                MovieOriginal newMovie = new MovieOriginal { Title = title, Description = description, YearReleased = yearReleased };

                                moviesList.Add(newMovie);
                            }
                            catch
                            {
                                DialogResult result = MessageBox.Show($"Неверно объявлен фильм: {string.Join(", ", movieValues)}. \r Продолжить?", "Ошибка", MessageBoxButtons.YesNo);
                                if (result == DialogResult.No)
                                {
                                    abort = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (abort == true)
                {
                    moviesList.Clear();
                }
            }
            return moviesList;
        }

        private void loadMoviesButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Загрузить полученные фильмы в базу данных?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (VideoprokatContext db = new VideoprokatContext())
                {
                    foreach (MovieOriginal movie in moviesList)
                    {
                        db.MoviesOriginal.Add(movie);
                    }
                    db.SaveChanges();
                }
                this.Close();
            }
        }
    }
}
