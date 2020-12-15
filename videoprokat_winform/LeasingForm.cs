using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;

namespace videoprokat_winform
{
    public partial class LeasingForm : Form
    {
        MovieCopy currentCopy;
        MovieOriginal currentMovie;
        public LeasingForm(MovieCopy movieCopy)
        {
            InitializeComponent();
            currentCopy = movieCopy;
        }

        private void LeasingForm_Load(object sender, EventArgs e)
        {
            startDatePicker.Value = DateTime.Now;
            endDatePicker.Value = DateTime.Now.AddDays(2);

            using (VideoprokatContext db = new VideoprokatContext())
            {
                currentMovie = db.MoviesOriginal.First(m => m.Id == currentCopy.MovieId);
                movieNameLabel.Text = currentMovie.Title;
                movieCommentLabel.Text = currentCopy.Commentary;

                var customers = db.Customers.ToList();
                customersComboBox.DataSource = customers;
                customersComboBox.DisplayMember = "Name";
                customersComboBox.ValueMember = "Id";
            }
        }

        private void leaseButton_Click(object sender, EventArgs e)
        {
            if (customersComboBox.SelectedIndex > -1)
            {
                if (startDatePicker.Value.Date < endDatePicker.Value.Date)
                {
                    using (VideoprokatContext db = new VideoprokatContext())
                    {
                        int customerId = Convert.ToInt32(customersComboBox.SelectedValue);
                        int movieCopyId = currentCopy.Id;
                        Customer owner = db.Customers.First(r => r.Id == customerId);
                        MovieCopy movieCopy = db.MoviesCopies.First(r => r.Id == movieCopyId);
                        movieCopy.Available = false;

                        Leasing leasing = new Leasing(startDatePicker.Value.Date, endDatePicker.Value.Date, customerId,
                            movieCopyId, currentCopy.PricePerDay);

                        DialogResult result = MessageBox.Show($"Прокат {currentMovie.Title}, {currentCopy.Commentary} " +
                            $"с {startDatePicker.Value.Date} по {endDatePicker.Value.Date} за {leasing.TotalPrice.ToString()}?", "Прокат", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            db.LeasedCopies.Add(leasing);
                            db.SaveChanges();
                            this.Close();
                        }
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
        }

        private void customersComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            string id = (((Customer)e.ListItem).Id).ToString();
            string name = ((Customer)e.ListItem).Name;
            e.Value = id + ", " + name;
        }

        private void endDatePicker_ValueChanged(object sender, EventArgs e)
        {
            setPriceLabelText();
        }

        private void startDatePicker_ValueChanged(object sender, EventArgs e)
        {
            setPriceLabelText();
        }
        private void setPriceLabelText()
        {
            if (endDatePicker.Value.Date > startDatePicker.Value.Date)
            {
                decimal price = (int)((endDatePicker.Value.Date - startDatePicker.Value.Date).TotalDays) *
                                currentCopy.PricePerDay;
                priceLabel.Text = "Цена: " + price.ToString();
            }
            else
            {
                priceLabel.Text = "Цена: - ";
            }
        }
    }
}
