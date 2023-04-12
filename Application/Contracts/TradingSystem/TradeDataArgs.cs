using Application.Models.TradingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.TradingSystem
{
    public class TradeDataArgs : EventArgs
    {
        public bool DataAssigned { get; set; }

        public TradeDataArgs(AccountTradeData accountTradeData) { }
    }
}
