﻿using System;

namespace videoprokat_winform.Models
{
    public class Leasing
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }

        public int MovieCopyId { get; set; }
        public MovieCopy MovieCopy { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        private Leasing() { }
        public Leasing(DateTime startDate, DateTime expectedEndDate, int customerId, int movieCopyId, decimal pricePerDay)
        {
            StartDate = startDate;
            ExpectedEndDate = expectedEndDate;
            TotalPrice = GetExpectedTotalPrice(pricePerDay);
            CustomerId = customerId;
            MovieCopyId = movieCopyId;
        }

        public decimal GetExpectedTotalPrice(decimal pricePerDay)
        {
            return Convert.ToDecimal((ExpectedEndDate.Date - StartDate.Date).TotalDays) * pricePerDay;
        }
        public void ReturnOnTime()
        {
            MovieCopy.Available = true;
            ReturnDate = ExpectedEndDate;
        }
        public void ReturnDelayed(DateTime leasingReturnDate, decimal fineMultiplier = 2) // fineMultiplier должен быть > 1
        {
            var delayedDaysDiff = (leasingReturnDate.Date - ExpectedEndDate.Date).TotalDays;
            var totalPriceChange = MovieCopy.PricePerDay * (decimal)delayedDaysDiff * fineMultiplier; // получаем БОЛЬШЕ денег за прокат (т.к. вернули позже)
            TotalPrice += totalPriceChange;
            MovieCopy.Available = true;
            ReturnDate = leasingReturnDate;
        }
        public void ReturnEarly(DateTime leasingReturnDate)
        {
            var daysDiff = (ExpectedEndDate.Date - leasingReturnDate.Date).TotalDays;
            var totalPriceChange = MovieCopy.PricePerDay * (decimal)daysDiff; // получаем МЕНЬШЕ денег за прокат (т.к. вернули раньше)
            TotalPrice -= totalPriceChange;
            MovieCopy.Available = true;
            ReturnDate = leasingReturnDate;
        }
    }
}
