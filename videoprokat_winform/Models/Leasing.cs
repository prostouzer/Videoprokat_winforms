using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace videoprokat_winform.Models
{
    class Leasing
    {
        DateTime _returnDate;

        public int Id { get; set; }
        public DateTime LeasingStartDate { get; set; }
        public DateTime LeasingExpectedEndDate { get; set; }
        public Nullable<DateTime> ReturnDate 
        {
            get { return _returnDate; }
            set 
            {
                if (ReturnDate > LeasingExpectedEndDate)
                {

                }
            }
        } // если клиент вернет позже положенного
        public decimal TotalPrice { get; set; }
        // лучший способ вывода содержимого id свойства?
        public string ClientName { get; set; }

        public int MovieCopyId { get; set; }
        public MovieCopy MovieCopy { get; set; } // нужно ли, если используется только параметр movieCopy?
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public Leasing(MovieCopy movieCopy, Client owner, DateTime leasingStartDate, DateTime leasingExpectedEndDate)
        {
            LeasingStartDate = leasingStartDate;
            LeasingExpectedEndDate = leasingExpectedEndDate;
            MovieCopy = movieCopy;
            Client = owner;
            ClientName = owner.Name;
            movieCopy.Available = false;
            TotalPrice = GetExpectedTotalPrice(leasingStartDate, leasingExpectedEndDate, movieCopy);
        }
        public Leasing()
        {

        }
        public decimal GetExpectedTotalPrice(DateTime leasingStartDate, DateTime leasingExpectedEndDate, MovieCopy movieCopy)
        {
            return (Convert.ToDecimal((leasingExpectedEndDate - leasingStartDate).TotalDays)) * movieCopy.PricePerDay;
        }
        public decimal GetDelayedTotalPrice(DateTime leasingExpectedEndDate, DateTime leasingReturnDate, MovieCopy movieCopy, decimal multiplier) // multiplier > 1 
        {
            int totalDays = Convert.ToInt32((leasingReturnDate - leasingExpectedEndDate).TotalDays);
            return (totalDays * (movieCopy.PricePerDay * multiplier));
        }
        public decimal GetEarlyTotalPrice(DateTime leasingStartDate, DateTime leasingReturnDate, MovieCopy movieCopy)
        {
            int totalDays = Convert.ToInt32((leasingReturnDate - leasingStartDate).TotalDays);
            return totalDays * movieCopy.PricePerDay;
        }

        public void EndLeasing(DateTime returnDate, decimal multiplier = 0)
        {
            MovieCopy.Available = true;
            if (returnDate > LeasingExpectedEndDate) // вернули позже
            {
                TotalPrice = GetDelayedTotalPrice(LeasingExpectedEndDate, returnDate, MovieCopy, multiplier);
            }
            else if (returnDate < LeasingExpectedEndDate) // вернули раньше
            {
                TotalPrice = GetEarlyTotalPrice(LeasingStartDate, returnDate, MovieCopy);
            }
            // при returnDate = LeasingExpectedEndDate значение изначально присвоено в конструкторе
        }
    }
}
