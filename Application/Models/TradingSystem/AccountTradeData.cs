namespace Application.Models.TradingSystem
{
    public class AccountTradeData
    {
        public int AccountId { get; set; }
        public StandardTradeData StandardTradeData { get; set; }
        public FakeBreakoutTradeData FakeBreakoutTradeData { get; set; }
        public FibTradeData FibTradeData { get; set; }
    }
}
