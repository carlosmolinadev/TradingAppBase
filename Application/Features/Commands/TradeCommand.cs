using Application.Contracts.Persistance;
using Application.Contracts.TradingSystem;
using Application.Requests;
using Application.Responses;
using Domain.Entities;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Application.Features.Commands
{
    public class TradeCommand
    {
        private readonly IRepositoryAsync<Trade> _tradeRepository;
        private readonly IRepositoryAsync<TradeOrder> _orderRepository;
        private readonly ITradeManager _tradeManager;

        public TradeCommand(IRepositoryAsync<Trade> tradeRepository, IRepositoryAsync<TradeOrder> orderRepository, ITradeManager tradeManager)
        {
            _tradeRepository = tradeRepository;
            _orderRepository = orderRepository;
            _tradeManager = tradeManager;
        }

        public async Task<TradeResponse> RegisterTrade(TradeRequest tradeRequest)
        {
            var response = new TradeResponse();
            var trade = new Trade
            {
                Id = 2,
                RiskReward = 2.0M,
                LateEntry = 32,
                CandleCloseEntry = true,
                Attempt = 32,
                PercentageEntry = true,
                Symbol = "lorem ipsum",
                CreatedDate = DateTime.Now,
                TradeStrategyId = 1,
                AccountId = 1
            };

            try
            {
                //await _tradeRepository.AddAsync(trade);
                //var s = await _tradeRepository.LoadAsync<Trade, dynamic>("get_trade_info", new { tradeId = 2 });
                await _tradeRepository.UpdateAsync(trade);
                //_tradeManager.AddTrade(tradeRequest);
                if (tradeRequest.Trade is not null)
                {
                    
                    //await _tradeManager.AddTrade(trade);
                    //response.Trade = (await _tradeRepository.LoadAsync<TradeDB, object>("get_trade_by_id", new { Trade = 1 })).Single();

                    //var trade = await _tradeRepository.SelectByIdAsync(1);

                    response.Success = true;
                }

            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<TradeOrderResponse> PlaceTradeOrderAsync(TradeOrderRequest tradeOrderRequest)
        {
            var response = new TradeOrderResponse();
            if(tradeOrderRequest.Orders.Any())
            {
                try
                {
                    foreach (var order in tradeOrderRequest.Orders)
                    {
                        
                        await _orderRepository.AddAsync(order);
                    }

                    response.Success = true;
                }
                catch (Exception e)
                {
                    response.Message = e.Message;
                }
            } 
            
            return response;
        }


    }

}
