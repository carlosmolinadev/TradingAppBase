using Domain.Entities;

namespace Application.Profiles.DTOs
{
    public class OrderParameter
    {
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
        public decimal TradePrice { get; set; }
        public string Symbol { get; set; }
        public OrderSide Side { get; set; }
        public decimal Quantity { get; set; }
    }
}
