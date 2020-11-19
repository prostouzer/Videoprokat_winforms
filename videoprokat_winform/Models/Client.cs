using System;
using System.Collections.Generic;
using System.Text;

namespace videoprokat_winform.Models
{
    class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Rating { get; set; }
        public ICollection<Leasing> LeasedCopies { get; set; } // почему не использовать list сразу, ведь его можно сразу присвоить?

        public void LeaseCopy(Leasing leasedCopy)
        {
            if (LeasedCopies != null)
            {
                LeasedCopies.Add(leasedCopy);
            }
            else
            {
                LeasedCopies = new List<Leasing>();
                LeasedCopies.Add(leasedCopy);
            }
        }
    }
}
