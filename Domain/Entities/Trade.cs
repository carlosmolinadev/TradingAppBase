using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Trade
    {
        public Trade(int id, decimal riskReward, int lateEntry, bool candleCloseEntry, int attempt, bool percentageEntry, string symbol, int accountId)
        {
            Id = id;
            RiskReward = riskReward;
            LateEntry = lateEntry;
            CandleCloseEntry = candleCloseEntry;
            Attempt = attempt;
            PercentageEntry = percentageEntry;
            Symbol = symbol;
            AccountId = accountId;
            CreatedDate = DateTime.Today;
        }

        public int Id { get; set; }
        public decimal RiskReward { get; set; }
        public int LateEntry { get; set; }
        public bool CandleCloseEntry { get; set; }
        public int Attempt { get; set; }
        public bool PercentageEntry { get; set; }
        public string Symbol { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedDate { get; set; }

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
