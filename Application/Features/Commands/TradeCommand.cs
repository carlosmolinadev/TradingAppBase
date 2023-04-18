using Application.Contracts.Persistance;
using Application.Contracts.TradingSystem;
using Application.Models.Persistance;
using Application.Requests;
using Application.Responses;
using Domain.Entities;
using System.Diagnostics;

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
                using (_unitOfWork.BeginTransactionAsync())
                {
                    if(tradeRequest.Trade is not null)
                    {
                        tradeRequest.Trade.Id = await _tradeRepository.AddAsync(tradeRequest.Trade);

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
