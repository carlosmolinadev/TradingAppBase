namespace Application.Models.Broker
{
    public class MarketStreamData
    {
        /// <summary>
        /// The symbol the data is for
        /// </summary>
        public string? Symbol { get; set; }

        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// Is this kline final
        /// </summary>
        public bool Final { get; set; }
    }
}
