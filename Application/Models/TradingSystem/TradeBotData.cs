using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.TradingSystem
{

    public class StandardTradeData : BaseTradeData
    {
        public StandardBotParameters? Parameters { get; set; }
    }

    public class StandardBotParameters
    {
    }

    public class FakeBreakoutTradeData : BaseTradeData
    {
        public FakeBreakoutBotParameters? Parameters { get; set; }
    }

    public class FakeBreakoutBotParameters
    {
    }

    public class FibTradeData : BaseTradeData
    {
        public FibBotParameters? Parameters { get; set; }
    }

    public class FibBotParameters
    {
    }
}
