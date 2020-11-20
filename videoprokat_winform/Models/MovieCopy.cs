﻿using System;
using System.Collections.Generic;
using System.Text;

namespace videoprokat_winform.Models
{
    class MovieCopy
    {
        public int Id { get; set; }
        public string Commentary { get; set; }
        public bool Available { get; set; }
        public decimal PricePerDay { get; set; }

        public int MovieId { get; set; }
        public MovieOriginal Movie { get; set; }
    }
}