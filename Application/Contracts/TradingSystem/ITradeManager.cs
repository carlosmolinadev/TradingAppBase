using Application.Models.TradingSystem;
using Application.Requests;
using Application.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.TradingSystem
{
    public interface ITradeManager
    {

        public bool AddTrade(TradeRequest tradeRequest);


    }
}
