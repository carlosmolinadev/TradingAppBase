namespace Application.Profiles.DTOs
{
    public class Pair
    {
        public decimal x { get; set; }
        public decimal y { get; set; }

        public Pair(decimal x, decimal y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
