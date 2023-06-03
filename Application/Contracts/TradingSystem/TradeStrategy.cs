using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.TradingSystem
{
    public abstract class TradeStrategy
    {
        public bool SetUpTrade()
        {
            return false;
        }
    }
}
