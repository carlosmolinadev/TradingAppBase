using Application.Contracts.Broker;
using Application.Contracts.TradingSystem;
using Application.Models.Broker;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TradeSystem.Bots
{
    public class FakeBreakoutBot : BaseBot
    {
        //public FakeBreakoutBot(ITradeCommand tradeCommand, ITradeQuery tradeQuery, IAccountCommand accountCommand, IAccountQuery accountQuery) : base(tradeCommand, tradeQuery, accountCommand, accountQuery)
        //{
        //}

        public event EventHandler<MarketStreamData>? BinanceMarketData;

        protected override void AddTrade()
        {
            throw new NotImplementedException();
        }

        protected override void SetUpTrade()
        {
            throw new NotImplementedException();
        }
    }
}
