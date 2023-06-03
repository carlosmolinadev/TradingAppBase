namespace Domain.Entities
{
    public class TradeOrder
    {
        public TradeOrder()
        {
        }

        public TradeOrder(decimal quantity, decimal activationPrice, decimal filledPrice, decimal fee, decimal realizedProfit, DateTime createdDate, DateTime closedDate, string exchangeTransaction, int orderActivationParent, int tradeId, int sideId, int typeId, int statusId)
        {
            Quantity = quantity;
            ActivationPrice = activationPrice;
            FilledPrice = filledPrice;
            Fee = fee;
            RealizedProfit = realizedProfit;
            CreatedDate = createdDate;
            ClosedDate = closedDate;
            ExchangeTransaction = exchangeTransaction;
            OrderActivationParent = orderActivationParent;
            TradeId = tradeId;
            SideId = sideId;
            TypeId = typeId;
            StatusId = statusId;
        }

        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal ActivationPrice { get; set; }
        public decimal FilledPrice { get; set; }
        public decimal Fee { get; set; }
        public decimal RealizedProfit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public string? ExchangeTransaction { get; set; }
        public int OrderActivationParent { get ; set; }
        public int TradeId { get; set; }
        public int SideId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
    }
}
