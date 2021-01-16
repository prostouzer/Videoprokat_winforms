namespace videoprokat_winform.Models
{
    public class MovieCopy
    {
        public int Id { get; set; }
        public string Commentary { get; set; }
        public bool Available { get; set; }
        public decimal PricePerDay { get; set; }

        public int MovieId { get; set; }
        public MovieOriginal Movie { get; set; }

        private MovieCopy() { }
        public MovieCopy(int movieId, string commentary, decimal pricePerDay)
        {
            MovieId = movieId;
            Commentary = commentary;
            PricePerDay = pricePerDay;
            Available = true;
        }
    }
}
