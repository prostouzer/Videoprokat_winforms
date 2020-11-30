using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;
using System.Linq;

namespace videoprokat_winform
{
    public partial class ReturnForm : Form
    {
        Leasing currentLeasing;
        MovieCopy currentCopy;
        string MovieOriginalTitle;
        Client currentOwner;
        VideoprokatContext db = new VideoprokatContext();
        public ReturnForm(Leasing leasing)
        {
            InitializeComponent();

            currentLeasing = db.LeasedCopies.First(l => l.Id == leasing.Id);
            currentCopy = db.MoviesCopies.First(c => c.Id == leasing.MovieCopyId);
            MovieOriginalTitle = leasing.MovieCopy.Movie.Title;
            currentOwner = db.Clients.First(o => o.Id == leasing.ClientId);
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            ownerNameLabel.Text = currentOwner.Name + ", рейтинг " + currentOwner.Rating.ToString();
            movieNameLabel.Text = MovieOriginalTitle;

            movieCommentLabel.Text = currentCopy.Commentary;

            startDatePicker.Value = currentLeasing.LeasingStartDate;
            returnDatePicker.Value = DateTime.Now;
            returnDatePicker.MinDate = currentLeasing.LeasingStartDate;
            expectedEndLabel.Text = "Ожидается: " + currentLeasing.LeasingExpectedEndDate.ToShortDateString();
        }

        decimal totalPriceChange;
        double daysDiff;
        decimal fineMultiplier = 2;
        private void returnButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult;
            if (returnDatePicker.Value.Date == currentLeasing.LeasingExpectedEndDate) // on time
            {
                dialogResult = MessageBox.Show("Возврат " + MovieOriginalTitle + ", " + currentCopy.Commentary + " в срок",
                    "Возврат в срок", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    currentLeasing.ReturnOnTime();
                    db.SaveChanges();
                    this.Close();
                }
            }
            else if (returnDatePicker.Value > currentLeasing.LeasingExpectedEndDate) // delayed
            {
                dialogResult = MessageBox.Show("Возврат " + MovieOriginalTitle + ", " + currentCopy.Commentary + ", ШТРАФ " + totalPriceChange.ToString(),
                     "Поздний возврат", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    currentLeasing.ReturnDelayed(returnDatePicker.Value, fineMultiplier);
                    db.SaveChanges();
                    this.Close();
                }
            }
            else // early
            {
                dialogResult = MessageBox.Show("Возврат " + MovieOriginalTitle + ", " + currentCopy.Commentary + ", ВЕРНУТЬ " + (-1 * totalPriceChange).ToString(),
                     "Ранний возврат", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    currentLeasing.ReturnEarly(returnDatePicker.Value);
                    db.SaveChanges();
                    this.Close();
                }
            }
        }

        private void returnDate_ValueChanged(object sender, EventArgs e)
        {
            if (returnDatePicker.Value.Date == currentLeasing.LeasingExpectedEndDate.Date) // on time
            {
                totalPriceChangeLabel.Visible = false;
                returnDateLabel.ForeColor = SystemColors.ControlText;
            }
            else if (returnDatePicker.Value.Date > currentLeasing.LeasingExpectedEndDate.Date) // delayed
            {
                returnDateLabel.ForeColor = Color.Red;
                totalPriceChangeLabel.ForeColor = Color.Red;
                totalPriceChangeLabel.Visible = true;

                daysDiff = (returnDatePicker.Value.Date - currentLeasing.LeasingExpectedEndDate.Date).TotalDays;
                totalPriceChange = (currentCopy.PricePerDay * (decimal)daysDiff) * fineMultiplier;// getting MORE money from leasing
                totalPriceChangeLabel.Text = "Штраф: " + totalPriceChange.ToString();
            }
            else // early
            {
                returnDateLabel.ForeColor = Color.Green;
                totalPriceChangeLabel.ForeColor = Color.Green;
                totalPriceChangeLabel.Visible = true;

                daysDiff = (currentLeasing.LeasingExpectedEndDate.Date - returnDatePicker.Value.Date).TotalDays;
                totalPriceChange = currentCopy.PricePerDay * (decimal)-daysDiff; // getting LESS money from leasing
                totalPriceChangeLabel.Text = "Возврат: " + (-1 * totalPriceChange).ToString(); // -1 to prevent ~"Возврат -100"
            }
        }

        private void ReturnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Dispose();
        }
    }
}
