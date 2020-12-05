using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace videoprokat_winform.Models
{
    public class Leasing
    {
        public int Id { get; set; }
        public DateTime LeasingStartDate { get; set; }
        public DateTime LeasingExpectedEndDate { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        // лучший способ вывода имени? через id?
        public string ClientName { get; set; }
        public int MovieCopyId { get; set; }
        public MovieCopy MovieCopy { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

        //public Leasing() { }
        //public decimal GetExpectedTotalPrice(DateTime leasingStartDate, DateTime leasingExpectedEndDate, MovieCopy movieCopy)
        public decimal GetExpectedTotalPrice(decimal pricePerDay)
        {
            return Convert.ToDecimal((LeasingExpectedEndDate.Date - LeasingStartDate.Date).TotalDays) * pricePerDay;
        }
        public void ReturnOnTime()
        {
            MovieCopy.Available = true;
            ReturnDate = LeasingExpectedEndDate;
        }
        public void ReturnDelayed(DateTime leasingReturnDate, decimal fineMultiplier = 2) // multiplier > 1 
        {
            double delayedDaysDiff = (leasingReturnDate.Date - LeasingExpectedEndDate.Date).TotalDays;
            decimal totalPriceChange = (MovieCopy.PricePerDay * (decimal)delayedDaysDiff) * fineMultiplier; // getting MORE money from leasing
            TotalPrice += totalPriceChange;
            MovieCopy.Available = true;
            ReturnDate = leasingReturnDate;
        }
        public void ReturnEarly(DateTime leasingReturnDate)
        {
            double daysDiff = (LeasingExpectedEndDate.Date - leasingReturnDate.Date).TotalDays;
            decimal totalPriceChange = MovieCopy.PricePerDay * (decimal)daysDiff; // getting LESS money from leasing
            TotalPrice -= totalPriceChange;
            MovieCopy.Available = true;
            ReturnDate = leasingReturnDate;
        }
        //public void EndLeasing(DateTime returnDate, decimal multiplier = 0)
        //{
        //    MovieCopy.Available = true;
        //    if (returnDate > LeasingExpectedEndDate) // вернули позже
        //    {
        //        ReturnDelayed(LeasingExpectedEndDate, returnDate, MovieCopy, multiplier);
        //    }
        //    else if (returnDate < LeasingExpectedEndDate) // вернули раньше
        //    {
        //        ReturnEarly(LeasingStartDate, returnDate, MovieCopy);
        //    }
        //    else // вернули в срок
        //    {
        //        // при returnDate = LeasingExpectedEndDate значение изначально присвоено в конструкторе
        //        MovieCopy.Available = true;
        //        ReturnDate = LeasingExpectedEndDate;
        //    }
        //}
    }
}
