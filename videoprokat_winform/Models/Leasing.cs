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
        public MovieCopy MovieCopy { get; set; } // нужно ли, если используется только параметр movieCopy? movieCopyId для связи в entity framework недостаточно?
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // нужен ли этот конструктор, если с передачей MovieCopy он создает лишние записи в таблице MoviesCopies? см. кнопку добавления на LeasingForm
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
        public Leasing() { }
        public decimal GetExpectedTotalPrice(DateTime leasingStartDate, DateTime leasingExpectedEndDate, MovieCopy movieCopy)
        {
            return (Convert.ToDecimal((leasingExpectedEndDate - leasingStartDate).TotalDays)) * movieCopy.PricePerDay;
        }
        public void ReturnDelayed(DateTime leasingExpectedEndDate, DateTime leasingReturnDate, MovieCopy movieCopy, decimal multiplier) // multiplier > 1 
        {
            int totalDays = Convert.ToInt32((leasingReturnDate - leasingExpectedEndDate).TotalDays);
            TotalPrice = (totalDays * (movieCopy.PricePerDay * multiplier));
            movieCopy.Available = true;
            ReturnDate = leasingReturnDate;
        }
        public void ReturnEarly(DateTime leasingStartDate, DateTime leasingReturnDate, MovieCopy movieCopy)
        {
            int totalDays = Convert.ToInt32((leasingReturnDate - leasingStartDate).TotalDays);
            TotalPrice = totalDays * movieCopy.PricePerDay;
            movieCopy.Available = true;
            ReturnDate = leasingReturnDate;
        }

        public void EndLeasing(DateTime returnDate, decimal multiplier = 0)
        {
            MovieCopy.Available = true;
            if (returnDate > LeasingExpectedEndDate) // вернули позже
            {
                ReturnDelayed(LeasingExpectedEndDate, returnDate, MovieCopy, multiplier);
            }
            else if (returnDate < LeasingExpectedEndDate) // вернули раньше
            {
                ReturnEarly(LeasingStartDate, returnDate, MovieCopy);
            }
            else // вернули в срок
            {
                // при returnDate = LeasingExpectedEndDate значение изначально присвоено в конструкторе
                MovieCopy.Available = true;
                ReturnDate = LeasingExpectedEndDate;
            }
        }
    }
}
