using System;
using System.Drawing;
using System.Windows.Forms;
using videoprokat_winform.Models;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class ReturnForm : Form, IReturnView
    {
        public MovieOriginal CurrentMovie { get; set; }

        public MovieCopy CurrentMovieCopy { get; set; }

        public Customer CurrentCustomer { get; set; }

        public Leasing CurrentLeasing { get; set; }

        public decimal FineMultiplier { get; set; } = 2;

        private decimal TotalPriceChange => GetTotalPriceChangeAndDisplay();

        public event Action<DateTime> OnReturnEarly;
        public event Action OnReturnOnTime;
        public event Action<DateTime, decimal> OnReturnDelayed;

        public ReturnForm()
        {
            InitializeComponent();

            returnButton.Click += (sender, args) =>
            {
                if (returnDatePicker.Value.Date < CurrentLeasing.ExpectedEndDate.Date) OnReturnEarly?.Invoke(returnDatePicker.Value.Date);
                if (returnDatePicker.Value.Date == CurrentLeasing.ExpectedEndDate.Date) OnReturnOnTime?.Invoke();
                if (returnDatePicker.Value.Date > CurrentLeasing.ExpectedEndDate.Date) OnReturnDelayed?.Invoke(returnDatePicker.Value.Date, FineMultiplier);
            };
        }
        public new void Show()
        {
            ShowDialog();
        }

        public bool ConfirmReturnEarly()
        {
            var dialogResult = MessageBox.Show($"Возврат {CurrentMovie.Title}, {CurrentMovieCopy.Commentary}, ВЕРНУТЬ {-1 * TotalPriceChange}",
                         "Ранний возврат", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmReturnOnTime()
        {
            var dialogResult = MessageBox.Show($"Возврат {CurrentMovie.Title}, {CurrentMovieCopy.Commentary} в срок",
                "Возврат в срок", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmReturnDelayed()
        {
            var dialogResult = MessageBox.Show($"Возврат {CurrentMovie.Title}, {CurrentMovieCopy.Commentary}, ШТРАФ {TotalPriceChange}",
                             "Поздний возврат", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private decimal GetTotalPriceChangeAndDisplay()
        {
            if (returnDatePicker.Value.Date == CurrentLeasing.ExpectedEndDate.Date) // вернули вовремя
            {
                totalPriceChangeLabel.Visible = false;
                returnDateLabel.ForeColor = SystemColors.ControlText;
                return -1;
            }
            else if (returnDatePicker.Value.Date > CurrentLeasing.ExpectedEndDate.Date) // вернули позже
            {
                returnDateLabel.ForeColor = Color.Red;
                totalPriceChangeLabel.ForeColor = Color.Red;
                totalPriceChangeLabel.Visible = true;

                var daysDiff = (returnDatePicker.Value.Date - CurrentLeasing.ExpectedEndDate.Date).TotalDays;
                var totalPriceChange = (CurrentMovieCopy.PricePerDay * (decimal)daysDiff) * FineMultiplier;// просрочили - платите больше за каждый день просрочки
                totalPriceChangeLabel.Text = "Штраф: " + totalPriceChange;
                return totalPriceChange;
            }
            else // вернули раньше
            {
                returnDateLabel.ForeColor = Color.Green;
                totalPriceChangeLabel.ForeColor = Color.Green;
                totalPriceChangeLabel.Visible = true;

                var daysDiff = (CurrentLeasing.ExpectedEndDate.Date - returnDatePicker.Value.Date).TotalDays;
                var totalPriceChange = CurrentMovieCopy.PricePerDay * (decimal)-daysDiff; // вернули раньше - возвращай меньше
                totalPriceChangeLabel.Text = "Возврат: " + (-1 * totalPriceChange); // -1 чтобы не было ~"Возврат -100"
                return totalPriceChange;
            }
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            ownerNameLabel.Text = CurrentCustomer.Name + ", рейтинг " + CurrentCustomer.Rating;
            movieNameLabel.Text = CurrentMovie.Title;
            fineFormulaLabel.Text = $"Штраф = {FineMultiplier} * цена/день * количество просроченных дней";

            movieCommentLabel.Text = CurrentMovieCopy.Commentary;

            startDatePicker.Value = CurrentLeasing.StartDate;
            returnDatePicker.Value = DateTime.Now;
            returnDatePicker.MinDate = CurrentLeasing.StartDate;
            expectedEndLabel.Text = "Ожидается: " + CurrentLeasing.ExpectedEndDate.ToShortDateString();
        }

        private void returnDatePicker_ValueChanged(object sender, EventArgs e)
        {
            GetTotalPriceChangeAndDisplay();
        }
    }
}
