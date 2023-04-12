namespace Application.Profiles.DTOs
{
    public class FibParameter
    {
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public IList<OrderParameter>? FibOrders { get; set; }
    }
}
