using System;
using System.Collections.Generic;
using System.Text;

namespace videoprokat_winform.Models
{
    public class MovieOriginal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearReleased { get; set; }

        public ICollection<MovieCopy> Copies { get; set; }
    }
}
