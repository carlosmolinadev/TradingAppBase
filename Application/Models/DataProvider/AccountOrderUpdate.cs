using Domain.Entities;

namespace Application.Models.Broker
{
    public class AccountOrderUpdate
    {
        /// <summary>
        /// The symbol the order is for
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The new client order id
        /// </summary>

        public int Side { get; set; }

        /// <summary>
        /// The quantity of the order
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// The price of the order
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The average price of the order
        /// </summary>
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// The stop price of the order
        /// </summary>
        public decimal StopPrice { get; set; }
        /// <summary>
        /// The execution type
        /// </summary>
        public ExecutionType ExecutionType { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// The id of the order as assigned by Binance
        /// </summary>

        public long OrderId { get; set; }
        /// <summary>
        /// The quantity of the last filled trade of this order
        /// </summary>
        public decimal QuantityOfLastFilledTrade { get; set; }
        /// <summary>
        /// The quantity of all trades that were filled for this order
        /// </summary>
        public decimal AccumulatedQuantityOfFilledTrades { get; set; }
        /// <summary>
        /// The price of the last filled trade
        /// </summary>
        public decimal PriceLastFilledTrade { get; set; }
        /// <summary>
        /// The fee payed
        /// </summary>
        public decimal Fee { get; set; }
        /// <summary>
        /// The asset the fee was taken from
        /// </summary>
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// The time of the update
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// The trade id
        /// </summary>
        public long TradeId { get; set; }
        /// <summary>
        /// Bid Notional
        /// </summary>
        public decimal BidNotional { get; set; }
        /// <summary>
        /// Ask Notional
        /// </summary>
        public decimal AskNotional { get; set; }
        /// <summary>
        /// Whether the buyer is the maker
        /// </summary>
        public bool BuyerIsMaker { get; set; }
        /// <summary>
        /// Is this reduce only
        /// </summary>
        public bool IsReduce { get; set; }
        /// <summary>
        /// Original Order Type
        /// </summary>
        public OrderType OriginalType { get; set; }
        /// <summary>
        /// If Close-All, only pushed with conditional order
        /// </summary>
        public bool PushedConditionalOrder { get; set; }
        /// <summary>
        /// Activation Price, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        public decimal ActivationPrice { get; set; }
        /// <summary>
        /// Callback Rate, only pushed with TRAILING_STOP_MARKET order
        /// </summary>
        public decimal CallbackRate { get; set; }
        /// <summary>
        /// Realized profit of the trade
        /// </summary>
        public decimal RealizedProfit { get; set; }
    }
}
