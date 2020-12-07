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

                var clients = db.Clients.ToList();
                clientsComboBox.DataSource = clients;
                clientsComboBox.DisplayMember = "Name";
                clientsComboBox.ValueMember = "Id";
            }
        }

        private void leaseButton_Click(object sender, EventArgs e)
        {
            if (clientsComboBox.SelectedIndex > -1)
            {
                if (startDatePicker.Value.Date < endDatePicker.Value.Date)
                {
                    using (VideoprokatContext db = new VideoprokatContext())
                    {
                        int clientId = Convert.ToInt32(clientsComboBox.SelectedValue);
                        int movieCopyId = currentCopy.Id;
                        Client owner = db.Clients.First(r => r.Id == clientId);
                        MovieCopy movieCopy = db.MoviesCopies.First(r => r.Id == movieCopyId);
                        movieCopy.Available = false;

                        Leasing leasing = new Leasing();
                        leasing.LeasingStartDate = startDatePicker.Value.Date;
                        leasing.LeasingExpectedEndDate = endDatePicker.Value.Date;
                        leasing.ClientId = clientId;
                        leasing.MovieCopyId = movieCopyId;
                        leasing.TotalPrice = leasing.GetExpectedTotalPrice(currentCopy.PricePerDay);

                        DialogResult result = MessageBox.Show($"Прокат {currentMovie.Title}, {currentCopy.Commentary} " +
                            $"с {startDatePicker.Value.Date} по {endDatePicker.Value.Date} за {leasing.TotalPrice.ToString()}?", "Прокат", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            db.LeasedCopies.Add(leasing);
                            db.SaveChanges();
                        }
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Дата оканчания проката не может быть раньше даты начала");
                }
            }
            else
            {
                MessageBox.Show("Укажите клиента");
            }
        }

        private void clientsComboBox_Format(object sender, ListControlConvertEventArgs e)
        {
            string id = (((Client)e.ListItem).Id).ToString();
            string name = ((Client)e.ListItem).Name;
            e.Value = id + ", " + name;
        }

        private void endDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (endDatePicker.Value.Date > startDatePicker.Value.Date)
            {
                decimal price = (int)((endDatePicker.Value.Date - startDatePicker.Value.Date).TotalDays) * currentCopy.PricePerDay;
                priceLabel.Text = "Цена: " + price.ToString();
            }
            else
            {
                priceLabel.Text = "Цена: - ";
            }
        }

        private void startDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (startDatePicker.Value.Date > endDatePicker.Value.Date)
            {
                priceLabel.Text = "Цена: - ";
            }
        }
    }
}
