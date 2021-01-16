namespace videoprokat_winform.Models
{
    public class Customer
    {
        private const float DefaultRating = 100;
        private float _rating;

        public int Id { get; set; }
        public string Name { get; set; }
        public float Rating
        {
            get => _rating;
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

        private Customer() { } // для чтения для Entity Framework; private не даст вызвать пустой конструктор из кода
        public Customer(string name)
        {
            Name = name;
            Rating = DefaultRating;
        }

        public Customer(string name, float rating)
        {
            Name = name;
            Rating = rating;
        }
    }
}
