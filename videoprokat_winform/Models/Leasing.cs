using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace videoprokat_winform.Models
{
    class Leasing
    {
        public int Id { get; set; }
        //[Column(TypeName = "datetime2")]
        public DateTime LeasingStartDate { get; set; }
        //[Column(TypeName = "datetime2")]
        public DateTime LeasingExpectedEndDate { get; set; }
        //[Column(TypeName = "datetime2")] // для совместимости c datetime'ом SQL
        public Nullable<DateTime> ReturnDate { get; set; } // если клиент вернет позже положенного
        public decimal TotalPrice { get; set; }

        public MovieCopy MovieCopy { get; set; } // нужно ли, если используется только параметр movieCopy?
        public Client Owner { get; set; }

        public Leasing(MovieCopy movieCopy, Client owner, DateTime leasingStartDate, DateTime leasingExpectedEndDate)
        {
            LeasingStartDate = leasingStartDate;
            LeasingExpectedEndDate = leasingExpectedEndDate;
            MovieCopy = movieCopy;
            Owner = owner;
            movieCopy.Available = false;
            TotalPrice = GetExpectedTotalPrice(leasingStartDate, leasingExpectedEndDate, movieCopy);
        }
        public decimal GetExpectedTotalPrice(DateTime leasingStartDate, DateTime leasingExpectedEndDate, MovieCopy movieCopy)
        {
            return (Convert.ToDecimal((leasingExpectedEndDate - leasingStartDate).TotalDays))*movieCopy.PricePerDay;
        }
    }
}
