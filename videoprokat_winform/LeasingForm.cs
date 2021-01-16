using System;
using System.Collections.Generic;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class LeasingForm : Form, ILeasingView
    {
        public MovieOriginal CurrentMovie { get; set; }
        public MovieCopy CurrentMovieCopy { get; set; }

        public event Action<Leasing> OnLeaseMovieCopy;

        public LeasingForm()
        {
            InitializeComponent();

            leaseButton.Click += (sender, args) =>
            {
                var startDate = startDatePicker.Value.Date;
                var endDate = endDatePicker.Value.Date;
                var customerId = Convert.ToInt32(customersComboBox.SelectedValue);
                var movieCopyId = CurrentMovieCopy.Id;
                var pricePerDay = CurrentMovieCopy.PricePerDay;
                var leasing = new Leasing(startDate, endDate, customerId, movieCopyId, pricePerDay);
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
                    var leasing = new Leasing(startDatePicker.Value.Date, endDatePicker.Value.Date, Convert.ToInt32(customersComboBox.SelectedValue),
                                        CurrentMovieCopy.Id, CurrentMovieCopy.PricePerDay);
                    var result = MessageBox.Show($"Прокат {CurrentMovie.Title}, {CurrentMovieCopy.Commentary} " +
                        $"с {startDatePicker.Value.Date} по {endDatePicker.Value.Date} за {leasing.TotalPrice}?", "Прокат", MessageBoxButtons.YesNo);
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

        public void RedrawCustomers(List<Customer> customers)
        {
            customersComboBox.DataSource = customers;
            customersComboBox.DisplayMember = "Name";
            customersComboBox.ValueMember = "Id";
        }

        private void LeasingForm_Load(object sender, EventArgs e)
        {
            startDatePicker.Value = DateTime.Now;
            endDatePicker.Value = DateTime.Now.AddDays(2);

            movieNameLabel.Text = CurrentMovie.Title;
            movieCommentLabel.Text = CurrentMovieCopy.Commentary;
        }

        private void customersComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            var id = ((Customer)e.ListItem).Id.ToString();
            var name = ((Customer)e.ListItem).Name;
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
                var price = (int)(endDatePicker.Value.Date - startDatePicker.Value.Date).TotalDays *
                                CurrentMovieCopy.PricePerDay;
                priceLabel.Text = "Цена: " + price;
            }
            else
            {
                priceLabel.Text = "Цена: - ";
            }
        }
    }
}
