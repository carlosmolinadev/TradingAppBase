using Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.TradingSystem
{
    public class TradeManager : ITradeManager
    {
        private IDictionary<string, TradeStrategy> _tradeStrategy = new Dictionary<string, TradeStrategy>();
        public bool AddTrade(TradeRequest tradeRequest)
        {
            var name = "FB1";
            //var name2 = "ASDASD";
            _tradeStrategy.TryAdd(name, new FakeBreakoutStrategy());
            
            //_tradeStrategy.TryAdd(name2, new StandardStrategy());
            if (_tradeStrategy.TryGetValue(name, out TradeStrategy test))
            {
                var check = test;
            }
            return true;
        }

        public bool AddFakeBreakoutTrade(TradeRequest tradeRequest)
        {
            return true;
        }
    }
}
