using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using videoprokat_winform.Models;
using System.Linq;
using videoprokat_winform.Views;

namespace videoprokat_winform
{
    public partial class ReturnForm : Form, IReturnView
    {
        private MovieOriginal _currentMovie;
        private MovieCopy _currentCopy;
        private Customer _currentCustomer;
        private Leasing _currentLeasing;
        private decimal _fineMultiplier = 2;
        public MovieOriginal CurrentMovie { get => _currentMovie; set => _currentMovie = value; }
        public MovieCopy CurrentCopy { get => _currentCopy; set => _currentCopy = value; }
        public Customer CurrentCustomer { get => _currentCustomer; set => _currentCustomer = value; }
        public Leasing CurrentLeasing { get => _currentLeasing; set => _currentLeasing = value; }
        public decimal FineMultiplier { get => _fineMultiplier; set => _fineMultiplier = value; }

        private decimal TotalPriceChange { get => GetTotalPriceChangeAndDisplay(); }

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
            var dialogResult = MessageBox.Show($"Возврат {CurrentMovie.Title}, {CurrentCopy.Commentary}, ВЕРНУТЬ {(-1 * TotalPriceChange).ToString()}",
                         "Ранний возврат", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmReturnOnTime()
        {
            var dialogResult = MessageBox.Show($"Возврат {CurrentMovie.Title}, {CurrentCopy.Commentary} в срок",
                "Возврат в срок", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmReturnDelayed()
        {
            var dialogResult = MessageBox.Show($"Возврат {CurrentMovie.Title}, {CurrentCopy.Commentary}, ШТРАФ {TotalPriceChange.ToString()}",
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
                var totalPriceChange = (CurrentCopy.PricePerDay * (decimal)daysDiff) * FineMultiplier;// просрочили - платите больше за каждый день просрочки
                totalPriceChangeLabel.Text = "Штраф: " + totalPriceChange.ToString();
                return totalPriceChange;
            }
            else // вернули раньше
            {
                returnDateLabel.ForeColor = Color.Green;
                totalPriceChangeLabel.ForeColor = Color.Green;
                totalPriceChangeLabel.Visible = true;

                var daysDiff = (CurrentLeasing.ExpectedEndDate.Date - returnDatePicker.Value.Date).TotalDays;
                var totalPriceChange = CurrentCopy.PricePerDay * (decimal)-daysDiff; // вернули раньше - возвращай меньше
                totalPriceChangeLabel.Text = "Возврат: " + (-1 * totalPriceChange).ToString(); // -1 чтобы не было ~"Возврат -100"
                return totalPriceChange;
            }
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            ownerNameLabel.Text = CurrentCustomer.Name + ", рейтинг " + CurrentCustomer.Rating.ToString();
            movieNameLabel.Text = CurrentMovie.Title;
            fineFormulaLabel.Text = $"Штраф = {FineMultiplier.ToString()} * цена/день * количество просроченных дней";

            movieCommentLabel.Text = CurrentCopy.Commentary;

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
