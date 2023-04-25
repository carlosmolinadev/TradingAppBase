using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public decimal RiskReward { get; set; }
        public int LateEntry { get; set; }
        public bool CandleCloseEntry { get; set; }
        public int Attempt { get; set; }
        public bool PercentageEntry { get; set; }
        public string? Symbol { get; set; }
        public int AccountId { get; set; }
        [NotMapped]
        public DateTime? CreatedDate { get; set; }

        [NotMapped]
        public IList<TradeOrder>? Orders { get; set; } = new List<TradeOrder>();
        [NotMapped]
        public IList<TakeProfit> TakeProfitOrders { get; set; } = new List<TakeProfit>();
        [NotMapped]
        public decimal StopLossOrder { get; set; }
    }

    public class TakeProfit
    {
        public decimal TradePercentage { get; set; }
        public decimal TakeProfitOrder { get; set; }
    }

}
