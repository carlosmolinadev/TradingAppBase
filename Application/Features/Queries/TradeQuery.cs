using Application.Contracts.Persistance;
using Application.Models.Persistance;
using Application.Responses;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Application.Features.Queries
{
    public class TradeQuery
    {
        private readonly IRepositoryAsync<Trade> _tradeRepository;
        private readonly IRepositoryAsync<TradeOrder> _orderRepository;

        public TradeQuery(IRepositoryAsync<Trade> tradeRepository, IRepositoryAsync<TradeOrder> orderRepository)
        {
            _tradeRepository = tradeRepository;
            _orderRepository = orderRepository;
        }

        public async Task<TradeResponse> GetTradeByIdAsync(int tradeId)
        {
            var response = new TradeResponse();
            var trade = await _tradeRepository.GetByIdAsync(tradeId);
            if(trade is not null)
            {
                response.Trades =  new List<Trade>
                {
                    trade
                }.AsReadOnly() ;
            }
            return response;
        }

        public async Task<TradeOrderResponse> GetOrderById(int tradeId)
        {
            var response = new TradeOrderResponse();
            try
            {
                var order = await _orderRepository.GetByIdAsync(tradeId);
                if (order is not null)
                {
                    response.TradeOrder = new List<TradeOrder>
                    {
                        order
                    };
                }
            }
            catch (Exception e)
            {

                response.Message = e.Message;
            }
            return response;
        }

        public async Task<TradeResponse> GetOrdersByTradeAsync(int tradeId, QueryFilter orderFilter)
        {
            var response = new TradeResponse();

            try
            {
                var trade = await _tradeRepository.GetByIdAsync(tradeId);
                if (trade != null)
                {
                    var tradeCondition = new QueryCondition { Column = "trade_id", Operator = "=", Value = trade.Id.ToString() };
                    orderFilter.Condition.Add(tradeCondition);
                    var orders = await _orderRepository.GetFilteredAsync(orderFilter);
                    trade.Orders = orders;
                    response.Trades = new List<Trade>
                    {
                        trade
                    }.AsReadOnly();
                }
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<TradeOrderResponse> GetConditionalOrdersAsync(int orderId)
        {
            var response = new TradeOrderResponse();

            try
            {
                QueryFilter filter = new QueryFilter
                {
                    Condition = new List<QueryCondition>
                        {
                            new QueryCondition { Column = "activation_order", Operator = "=", Value = orderId.ToString() },
                        }
                };

                var orders = await _orderRepository.GetFilteredAsync(filter);

                response.Success = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
