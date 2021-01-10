using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class LeasingForm : Form, ILeasingView
    {
        private MovieOriginal _currentMovie;
        private MovieCopy _currentMovieCopy;

        public MovieOriginal currentMovie
        {
            get => _currentMovie;
            set => _currentMovie = value;
        }

        public MovieCopy currentMovieCopy
        {
            get => _currentMovieCopy;
            set => _currentMovieCopy = value;
        }

        public event Action<Leasing> OnLeaseMovieCopy;

        public LeasingForm()
        {
            InitializeComponent();

            leaseButton.Click += (sender, args) =>
            {
                var startDate = startDatePicker.Value.Date;
                var endDate = endDatePicker.Value.Date;
                var customerId = Convert.ToInt32(customersComboBox.SelectedValue);
                var movieCopyId = currentMovieCopy.Id;
                var pricePerDay = currentMovieCopy.PricePerDay;
                Leasing leasing = new Leasing(startDate, endDate, customerId, movieCopyId, pricePerDay);
                OnLeaseMovieCopy?.Invoke(leasing);
            };
        }

        public new void Show()
        {
            ShowDialog();
        }

        public bool ConfirmNewLeasing()
        {
            if (customersComboBox.SelectedIndex > -1)
            {
                if (startDatePicker.Value.Date < endDatePicker.Value.Date)
                {
                    Leasing leasing = new Leasing(startDatePicker.Value.Date, endDatePicker.Value.Date, Convert.ToInt32(customersComboBox.SelectedValue),
                                        currentMovieCopy.Id, currentMovieCopy.PricePerDay);
                    DialogResult result = MessageBox.Show($"Прокат {currentMovie.Title}, {currentMovieCopy.Commentary} " +
                        $"с {startDatePicker.Value.Date} по {endDatePicker.Value.Date} за {leasing.TotalPrice.ToString()}?", "Прокат", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Дата окончания проката не может быть раньше даты начала");
                }
            }
            else
            {
                MessageBox.Show("Укажите клиента");
            }

            return false;
        }

        public void PopulateWithCustomers(List<Customer> customers)
        {
            customersComboBox.DataSource = customers;
            customersComboBox.DisplayMember = "Name";
            customersComboBox.ValueMember = "Id";
        }

        private void LeasingForm_Load(object sender, EventArgs e)
        {
            startDatePicker.Value = DateTime.Now;
            endDatePicker.Value = DateTime.Now.AddDays(2);

            movieNameLabel.Text = currentMovie.Title;
            movieCommentLabel.Text = currentMovieCopy.Commentary;
        }

        private void customersComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            string id = (((Customer)e.ListItem).Id).ToString();
            string name = ((Customer)e.ListItem).Name;
            e.Value = id + ", " + name;
        }

        private void startDatePicker_ValueChanged(object sender, EventArgs e)
        {
            setPriceLabelText();
        }

        private void endDatePicker_ValueChanged(object sender, EventArgs e)
        {
            setPriceLabelText();
        }

        private void setPriceLabelText()
        {
            if (endDatePicker.Value.Date > startDatePicker.Value.Date)
            {
                decimal price = (int)((endDatePicker.Value.Date - startDatePicker.Value.Date).TotalDays) *
                                currentMovieCopy.PricePerDay;
                priceLabel.Text = "Цена: " + price.ToString();
            }
            else
            {
                priceLabel.Text = "Цена: - ";
            }
        }
    }
}
