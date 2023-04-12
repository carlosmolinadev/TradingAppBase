using Application.Models.TradingSystem;
using Application.Requests;
using Domain.Entities;


namespace Infrastructure.TradeSystem
{
    public class TradeServiceManager
    {
        private Dictionary<int, AccountTradeData> _accountTradeData = new();


        public AccountTradeData GetTradeAccountData(int AccountId)
        {
            if (_accountTradeData.TryGetValue(AccountId, out AccountTradeData accountData))
            {
                return accountData;
            }
            return null;
        }

        private void AddTrade(AccountTradeData accountData, TradeRequest tradeRequest)
        {
            //var account = GetTradeAccountData(tradeRequest.Trades.AccountId);
            //switch (tradeRequest.TradeBot)
            //{
            //    case TradeBot.Standard:
            //        accountData.StandardTradeData.Trades.Add(tradeRequest.Trade);
            //        break;
            //    case TradeBot.FakeBreakout:
            //        accountData.FakeBreakoutTradeData.Trades.Add(tradeRequest.Trade);
            //        break;
            //    case TradeBot.Fib:
            //        accountData.FibTradeData.Trades.Add(tradeRequest.Trade);
            //        break;
            //}
        }

        private void UpdateTrade(AccountTradeData accountData, TradeRequest tradeRequest)
        {
            //switch (tradeRequest.TradeBot)
            //{
            //    case TradeBot.Standard:
            //        accountData.StandardTradeData.Trades.Remove(tradeRequest.Trade);
            //        break;
            //    case TradeBot.FakeBreakout:
            //        accountData.FakeBreakoutTradeData.Trades.Remove(tradeRequest.Trade);
            //        break;
            //    case TradeBot.Fib:
            //        accountData.FibTradeData.Trades.Remove(tradeRequest.Trade);
            //        break;
            //}
        }

        private void DeleteTrade(AccountTradeData accountData, TradeRequest tradeRequest)
        {
            //switch (tradeRequest.TradeBot)
            //{
            //    case TradeBot.Standard:
            //        accountData.StandardTradeData.Trades.Add(tradeRequest.Trade);
            //        break;
            //    case TradeBot.FakeBreakout:
            //        accountData.FakeBreakoutTradeData.Trades.Add(tradeRequest.Trade);
            //        break;
            //    case TradeBot.Fib:
            //        accountData.FibTradeData.Trades.Add(tradeRequest.Trade);
            //        break;
            //}
        }

    }
    

}
