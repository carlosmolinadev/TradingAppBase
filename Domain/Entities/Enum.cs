namespace Domain.Entities
{
    public enum OrderSide
    {
        Buy,
        Sell
    }

    public enum MarketStructure
    {
        Range,
        Uptrend,
        Downtrend
    }

    public enum TradeBot
    {
        Standard,
        Grid,
        Fib,
        FakeBreakout
    }

    public enum Exchange
    {
        Binance,
        BingX,
        Okex
    }

    public enum Derivate
    {
        Spot,
        Futures,
        Coin
    }

    public enum OrderCategory
    {
        Activation,
        StopLoss,
        TakeProfit,
        TrailingStop
    }

    public enum OrderType
    {
        /// <summary>
        /// Limit orders will be placed at a specific price. If the price isn't available in the order book for that asset the order will be added in the order book for someone to fill.
        /// </summary>
        Limit,
        /// <summary>
        /// Market order will be placed without a price. The order will be executed at the best price available at that time in the order book.
        /// </summary>
        Market,
        /// <summary>
        /// Stop order. Execute a limit order when price reaches a specific Stop price
        /// </summary>
        Stop,
        /// <summary>
        /// Stop market order. Execute a market order when price reaches a specific Stop price
        /// </summary>
        StopMarket,
        /// <summary>
        /// Take profit order. Will execute a limit order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfit,
        /// <summary>
        /// Take profit market order. Will execute a market order when the price rises above a price to sell and therefor take a profit
        /// </summary>
        TakeProfitMarket,
        /// <summary>
        /// A trailing stop order will execute an order when the price drops below a certain percentage from its all time high since the order was activated
        /// </summary>
        TrailingStopMarket,
        /// <summary>
        /// A liquidation order
        /// </summary>
        Liquidation
    }

    public enum ExecutionType
    {
        /// <summary>
        /// New
        /// </summary>
        New,
        /// <summary>
        /// Canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// Replaced
        /// </summary>
        Replaced,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// Trade
        /// </summary>
        Trade,
        /// <summary>
        /// Expired
        /// </summary>
        Expired,
        /// <summary>
        /// Amendment
        /// </summary>
        Amendment
    }

    /// <summary>
    /// The status of an orderн
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Order is new
        /// </summary>
        New,
        /// <summary>
        /// Order is partly filled, still has quantity left to fill
        /// </summary>
        PartiallyFilled,
        /// <summary>
        /// The order has been filled and completed
        /// </summary>
        Filled,
        /// <summary>
        /// The order has been canceled
        /// </summary>
        Canceled,
        /// <summary>
        /// The order is in the process of being canceled  (currently unused)
        /// </summary>
        PendingCancel,
        /// <summary>
        /// The order has been rejected
        /// </summary>
        Rejected,
        /// <summary>
        /// The order has expired
        /// </summary>
        Expired,
        /// <summary>
        /// Liquidation with Insurance Fund
        /// </summary>
        Insurance,
        /// <summary>
        /// Counterparty Liquidation
        /// </summary>
        Adl
    }
}
