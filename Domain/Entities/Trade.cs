using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Trade : Entity<int>
    {
        public Trade() { }

        public Trade(int id, decimal riskReward, int lateEntry, bool candleCloseEntry, int attempt, bool percentageEntry, string symbol, DateTime createdDate, int tradeStrategyId, int accountId)
        {
            RiskReward = riskReward;
            LateEntry = lateEntry;
            CandleCloseEntry = candleCloseEntry;
            Attempt = attempt;
            PercentageEntry = percentageEntry;
            Symbol = symbol;
            CreatedDate = createdDate;
            TradeStrategyId = tradeStrategyId;
            AccountId = accountId;
        }
        public decimal RiskReward { get; set; }
        public int LateEntry { get; set; }
        [Key]
        public bool CandleCloseEntry { get; set; }
        [Key]
        public int Attempt { get; set; }
        public bool PercentageEntry { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int TradeStrategyId { get; set; }
        public int AccountId { get; set; }
        
    }
}
