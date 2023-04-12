using Domain.Entities;

namespace Application.Models.TradingSystem
{
    public class BaseTradeData
    {
        public bool IsAssigned { get; set; }
        public IList<Trade> Trades { get; set; } = new List<Trade>();
    }
}
