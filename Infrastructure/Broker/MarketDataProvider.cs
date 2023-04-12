using Application.Models.Broker;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Sockets;
using Domain.Entities;
using Application.Contracts.Broker;

namespace Infrastructure.Broker
{
    public class MarketDataProvider : EventArgs, IMarketDataProvider
    {
        private IBinanceSocketClient _socketClient;
        private IBinanceClient _client;
        private Dictionary<string, UpdateSubscription> _subscriptions = new Dictionary<string, UpdateSubscription>();

        public MarketDataProvider(IBinanceSocketClient socketClient, IBinanceClient client)
        {
            _socketClient = socketClient;
            _client = client;
        }

        public event EventHandler<MarketStreamData>? BinanceMarketData;
        //public delegate Task RaiseLastKlineData<MarketStreamData>(object? sender, MarketStreamData e);
        //public event RaiseLastKlineData<MarketStreamData>? LastKlineData;

        protected virtual void OnKlineData(IBinanceStreamKlineData e)
        {
            MarketStreamData binanceDataKline = new()
            {
                Symbol = e.Symbol,
                ClosePrice = e.Data.ClosePrice,
                Final = e.Data.Final
            };

            BinanceMarketData?.Invoke(this, binanceDataKline);
        }

        public async Task StartDataProviderAsync(string symbol, Derivate derivates, Exchange exchange)
        {
            await SetFuturesKlineData(symbol, derivates);
        }

        public async Task<decimal> GetPriceBySymbol(string symbol)
        {
            var data = await _client.UsdFuturesApi.ExchangeData.GetKlinesAsync(symbol, KlineInterval.ThirtyMinutes, limit: 1);
            return data.Data.First().ClosePrice;
        }

        private async Task SetFuturesKlineData(string symbol, Derivate derivates)
        {
            try
            {
                if (_subscriptions.ContainsKey(symbol))
                {
                    return;
                }
                else
                {
                    if (derivates == Derivate.Futures)
                    {

                        var subResult = await _socketClient.UsdFuturesStreams.SubscribeToKlineUpdatesAsync(symbol, KlineInterval.ThirtyMinutes, streamData =>
                        {
                            OnKlineData(streamData.Data);
                        });
                        if (subResult.Success)
                        {
                            _subscriptions.TryAdd(symbol, subResult.Data);
                        }
                    }
                    if (derivates == Derivate.Coin)
                    {
                        var subResult = await _socketClient.CoinFuturesStreams.SubscribeToKlineUpdatesAsync(symbol, KlineInterval.ThirtyMinutes, streamData =>
                        {
                            OnKlineData(streamData.Data);
                        });
                        if (subResult.Success)
                        {
                            _subscriptions.TryAdd(symbol, subResult.Data);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task StopDataProviderAsync(string symbol, Exchange exchange)
        {
            var unsubscribe = _subscriptions[symbol];
            if (unsubscribe != null)
            {
                await _socketClient.UnsubscribeAsync(unsubscribe);
                _subscriptions.Remove(symbol);
            }

        }

        public Task<decimal> GetPriceBySymbol(string symbol, Exchange exchange)
        {
            throw new NotImplementedException();
        }

    }
}
