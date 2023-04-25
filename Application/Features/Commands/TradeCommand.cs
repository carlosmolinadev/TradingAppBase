using Application.Contracts.Persistance;
using Application.Contracts.TradingSystem;
using Application.Models.Persistance;
using Application.Profiles.DTOs;
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
        private readonly IUnitOfWork _unitOfWork;

        public TradeCommand(IRepositoryAsync<Trade> tradeRepository, IRepositoryAsync<TradeOrder> orderRepository, IUnitOfWork unitOfWork)
        {
            _tradeRepository = tradeRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }
        private struct ExchangeParameter
        {
            public int ExchangeId;
        }

        public struct test
        {
            public string another { get; set; }
        }
        public async Task<TradeResponse> RegisterTrade(TradeRequest tradeRequest)
        {
            var response = new TradeResponse();
            
            try
            {
                if (tradeRequest.Trade is not null)
                {
                    //List<ExchangeDTO> test = new List<ExchangeDTO>();
                    var res = await _tradeRepository.UpdateAsync(tradeRequest.Trade);
                    //var test = await _orderRepository.LoadAsync<string, dynamic>("get_customer", null);
                    //var queryFilter = new QueryFilter("activation_price", "=", "5", 5, 5);
                    //var result = _orderRepository.GetFilteredAsync(queryFilter);
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
                        
                        await _orderRepository.InsertAsync(order);
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
