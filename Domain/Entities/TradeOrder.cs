namespace Domain.Entities
{
    public class TradeOrder
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal ActivationPrice { get; set; }
        public decimal FilledPrice { get; set; }
        public decimal Fee { get; set; }
        public decimal RealizedProfit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public long? ExchangeTransaction { get; set; }
        public int? ParentId { get ; set; }
        public int SideId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int TradeId { get; set; }
    }
}
