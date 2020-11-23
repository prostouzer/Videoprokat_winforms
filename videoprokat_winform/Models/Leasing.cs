using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace videoprokat_winform.Models
{
    class Leasing
    {
        public int Id { get; set; }
        public DateTime LeasingStartDate { get; set; }
        public DateTime LeasingExpectedEndDate { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; } // если клиент вернет позже положенного
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
    }
}
