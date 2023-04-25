
using Application.Contracts.TradingSystem;
using Application.Responses;
using Domain.Entities;

namespace Infrastructure.TradeSystem.Bots
{
    public abstract class BaseBot 
    {
        //private readonly ITradeCommand _tradeCommand;
        //private readonly ITradeQuery _tradeQuery;
        //private readonly IAccountCommand _accountCommand;
        //private readonly IAccountQuery _accountQuery;
        //private readonly IOrderCommand _orderCommand;

        //public BaseBot(ITradeCommand tradeCommand, ITradeQuery tradeQuery, IAccountCommand accountCommand, IAccountQuery accountQuery, IOrderCommand orderCommand)
        //{
        //    _tradeCommand = tradeCommand;
        //    _tradeQuery = tradeQuery;
        //    _accountCommand = accountCommand;
        //    _accountQuery = accountQuery;
        //    _orderCommand = orderCommand;
        //}
        protected abstract void SetUpTrade();

        protected abstract void AddTrade();
        public virtual Task<TradeResponse> CancelOrderAsync(Exchange exchange, string derivate, TradeOrder tradeOrder)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TradeResponse> PlaceOrderAsync(Exchange exchange, string derivate, TradeOrder tradeOrder)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TradeResponse> UpdateOrderAsync(Exchange exchange, string derivate, TradeOrder tradeOrder)
        {
            throw new NotImplementedException();
        }
    }
}
