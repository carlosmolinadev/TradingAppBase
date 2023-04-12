using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public TradeBot TradeBot { get; set; }
        public bool LateEntry { get; set; }
        public int Attempt { get; set; }
        public string? Symbol { get; set; }
        public bool IsPercentage { get; set; }
        public decimal RiskReward { get; set; }
        public bool CandleCloseEntry { get; set; }
        public decimal StopLossOrder { get; set; }
        public ICollection<TakeProfit>? TakeProfitOrders { get; set; }
        public int AccountId { get; set; }
        public DateTime? CreatedDate { get; set; }

        [NotMapped]
        public ICollection<TradeOrder>? Orders { get; set; }

    }

    public class TakeProfit
    {
        public decimal TradePercentage { get; set; }
        public decimal TakeProfitOrder { get; set; }
    }

}
