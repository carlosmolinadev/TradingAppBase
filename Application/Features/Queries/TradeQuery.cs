using Application.Contracts.Persistance;
using Application.Models.Persistance;
using Application.Responses;
using Domain.Entities;

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
            var trade = await _tradeRepository.SelectByIdAsync(tradeId);
            if(trade is not null)
            {
                //response.Trades =  new List<Trade>
                //{
                //    trade
                //}.AsReadOnly() ;
            }
            return response;
        }

        public async Task<TradeOrderResponse> GetOrderById(int tradeId)
        {
            var response = new TradeOrderResponse();
            try
            {
                var order = await _orderRepository.SelectByIdAsync(tradeId);
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

        public async Task<TradeResponse> GetOrdersByTradeAsync(int tradeId, QueryParameter orderFilter)
        {
            var response = new TradeResponse();

            try
            {
                var trade = await _tradeRepository.SelectByIdAsync(tradeId);
                if (trade != null)
                {
                    var tradeCondition = new QueryCondition("trade_id", trade.Id.ToString());
                    orderFilter.Condition.Add(tradeCondition);
                    var orders = await _orderRepository.SelectByParameterAsync(orderFilter);
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
                //QueryParameter filter = new QueryParameter
                //{
                //    Condition = new List<QueryCondition>
                //        {
                //            new QueryCondition { Column = "activation_order", Operator = "=", Value = orderId.ToString() },
                //        }
                //};

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
