using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;
using System.IO;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class ImportMoviesForm : Form, IImportMoviesView
    {
        public event Action OnSelectNewFile;
        public event Action OnUploadMovies;
        public ImportMoviesForm()
        {
            InitializeComponent();

            chooseFilePathButton.Click += (sender, args) => OnSelectNewFile?.Invoke();
            uploadMoviesButton.Click += (sender, args) => OnUploadMovies?.Invoke();
        }

        public new void Show()
        {
            ShowDialog();
        }

        public bool SkipWronglyDeclaredMovie(string[] movieValues)
        {
            DialogResult result = MessageBox.Show($"Неверно объявлен фильм: {string.Join(", ", movieValues)}. \r Продолжить?", "Ошибка", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return false;
            }
            moviesListView.Items.Clear();
            return true;
        }

        public string ChooseFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                moviesListView.Items.Clear();
                return openFileDialog.FileName;
            }
            return "";
        }

        public void RedrawMovies(List<MovieOriginal> movies)
        {
            if (movies.Count > 0)
            {
                uploadMoviesButton.Enabled = true;
                foreach (var movie in movies)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = $"{movie.Title}, {movie.Description}, {movie.YearReleased} года выпуска";
                    moviesListView.Items.Add(item);
                }
            }
            else
            {
                uploadMoviesButton.Enabled = false;
            }
        }

        public bool ConfirmUploadMovies()
        {
            DialogResult result = MessageBox.Show("Загрузить полученные фильмы в базу данных?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
    }
}
