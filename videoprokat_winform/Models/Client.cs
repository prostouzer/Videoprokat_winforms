using System;
using System.Collections.Generic;
using System.Text;

namespace videoprokat_winform.Models
{
    class Client
    {
        float _rating;
        public int Id { get; set; }
        public string Name { get; set; }
        public float Rating
        {
            get { return _rating; }
            set
            {
                if (_rating > 100)
                {
                    _rating = 100;
                }
                else if (_rating < 0)
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
