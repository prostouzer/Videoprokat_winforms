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
        public ReturnForm(Leasing leasing)
        {
            InitializeComponent();
            currentLeasing = leasing;
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            MovieCopy currentCopy;
            Client currentOwner;
            MovieOriginal currentMovie;
            using (VideoprokatContext db = new VideoprokatContext())
            {
                currentOwner = db.Clients.First(o => o.Id == currentLeasing.ClientId);
                currentCopy = db.MoviesCopies.First(c => c.Id == currentLeasing.MovieCopyId);
                currentMovie = db.MoviesOriginal.First(o => o.Id == currentCopy.MovieId);
            }
            ownerNameLabel.Text = currentOwner.Name + ", рейтинг " + currentOwner.Rating.ToString();
            movieNameLabel.Text = currentMovie.Title;
            movieCommentLabel.Text = currentCopy.Commentary;

            startDate.Value = currentLeasing.LeasingStartDate;
            returnDate.Value = DateTime.Now;
            returnDate.MinDate = currentLeasing.LeasingStartDate;
            expectedEndLabel.Text = "Ожидается: " + currentLeasing.LeasingExpectedEndDate.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO RETURN BUTTON
        }

        private void returnDate_ValueChanged(object sender, EventArgs e)
        {
            if (returnDate.Value.Date > currentLeasing.LeasingExpectedEndDate.Date) // delayed return
            {
                returnDateLabel.ForeColor = Color.Red;
            }
            else if (returnDate.Value.Date == currentLeasing.LeasingExpectedEndDate.Date) // on time
            {
                returnDateLabel.ForeColor = SystemColors.ControlText;
            }
            else // early return
            {
                returnDateLabel.ForeColor = Color.Green;
            }
        }
    }
}
