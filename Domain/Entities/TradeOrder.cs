namespace Domain.Entities
{
    public class TradeOrder
    {
        public int Id { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? FilledPrice { get; set; }
        public OrderSide OrderSideId { get; set; }
        public OrderType OrderTypeId { get; set; }
        public OrderStatus OrderStatusId { get; set; } = OrderStatus.New;
        public OrderCategory OrderCategory { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public decimal RealizedProfit { get; set;}
        public long? ActivationOrder { get; set; }
        public int TradeId { get; set; }
    }
}
