using Application.Contracts.Persistance;
using Application.Contracts.TradingSystem;
using Application.Models.Persistance;
using Application.Requests;
using Application.Responses;
using Domain.Entities;

namespace Application.Features.Commands
{
    public class TradeCommand
    {
        private readonly IRepositoryAsync<Trade> _tradeRepository;
        private readonly IRepositoryAsync<TradeOrder> _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TradeCommand(IRepositoryAsync<Trade> tradeRepository, IRepositoryAsync<TradeOrder> orderRepository, IUnitOfWork unitOfWork)
        {
            _tradeRepository = tradeRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TradeResponse> RegisterTrade(TradeRequest tradeRequest)
        {
            var response = new TradeResponse();

            try
            {
                if(tradeRequest.Trades is not null && tradeRequest.Trades.Any())
                {
                    using (_unitOfWork.BeginTransactionAsync())
                    {
                        foreach (var trade in tradeRequest.Trades)
                        {
                            trade.Id = await _tradeRepository.AddAsync(trade);
                            if(trade.Id > 0 && trade.Orders.Any())
                            {
                                foreach (var order in trade.Orders)
                                {
                                    order.Id = await _orderRepository.AddAsync(order);
                                }
                                
                            }
                            response.Trades.Append(trade);
                        }

                        await _unitOfWork.CommitAsync();
                        
                        response.Success = true;
                    }
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
