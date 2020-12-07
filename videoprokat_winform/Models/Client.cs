using System;
using System.Collections.Generic;
using System.Text;

namespace videoprokat_winform.Models
{
    public class Client
    {
        float _rating;
        public int Id { get; set; }
        public string Name { get; set; }
        public float Rating
        {
            get { return _rating; }
            set
            {
                if (value > 100)
                {
                    _rating = 100;
                }
                else if (value < 0)
                {
                    _rating = 0;
                }
                else
                {
                    _rating = value;
                }
            }
        }
    }
}
