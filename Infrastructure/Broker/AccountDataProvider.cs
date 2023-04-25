using Application.Models.Broker;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Futures.Socket;
using CryptoExchange.Net.Sockets;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Infrastructure.Broker
{
    public class AccountDataProvider : IAccountDataProvider
    {
        private IBinanceSocketClient? _socketClient;
        private IBinanceClient? _client;
        private System.Timers.Timer? aTimer;
        private Dictionary<string, UpdateSubscription> _subscriptions = new Dictionary<string, UpdateSubscription>();

        private string? _listenKeyFutures;
        private string? _listenKeyCoin;

        public AccountDataProvider(IBinanceClient client, IBinanceSocketClient socketClient)
        {
            _client = client;
            _socketClient = socketClient;
        }

        public event EventHandler<AccountOrderUpdate>? RaiseUpdateOrder;

        protected virtual void OnRaiseUpdateOrder(BinanceFuturesStreamOrderUpdateData e)
        {
            var accountUpdate = new AccountOrderUpdate
            {
                Symbol = e.Symbol,
                Side = (int)e.Side,
                Quantity = e.Quantity,
                Price = e.Price,
                AveragePrice = e.AveragePrice,
                StopPrice = e.StopPrice,
                ExecutionType = (ExecutionType)e.ExecutionType,
                Status = (int)e.Status,
                OrderId = e.OrderId,
                QuantityOfLastFilledTrade = e.QuantityOfLastFilledTrade,
                AccumulatedQuantityOfFilledTrades = e.AccumulatedQuantityOfFilledTrades,
                PriceLastFilledTrade = e.PriceLastFilledTrade,
                Fee = e.Fee,
                FeeAsset = e.FeeAsset,
                UpdateTime = e.UpdateTime,
                TradeId = e.TradeId,
                BidNotional = e.BidNotional,
                AskNotional = e.AskNotional,
                BuyerIsMaker = e.BuyerIsMaker,
                IsReduce = e.IsReduce,
                PushedConditionalOrder = e.PushedConditionalOrder,
                ActivationPrice = e.ActivationPrice,
                CallbackRate = e.CallbackRate,
                RealizedProfit = e.RealizedProfit
            };
            RaiseUpdateOrder?.Invoke(this, accountUpdate);
        }

        public async Task StartAccountDataProviderAsync()
        {

            await SetUserUpdates();
        }

        private async Task SetUserUpdates()
        {
            if (_subscriptions.Count == 0 && _client is not null && _socketClient is not null)
            {
                _listenKeyFutures = _client.UsdFuturesApi.Account.StartUserStreamAsync().Result.Data;
                var futuresUserUpdate = await _socketClient.UsdFuturesStreams.SubscribeToUserDataUpdatesAsync(_listenKeyFutures, onLeverageUpdate =>
                {
                }, onMarginUpdate =>
                {
                }, onAccountUpdate =>
                {
                }, onOrderUpdate =>
                {
                    OnRaiseUpdateOrder(onOrderUpdate.Data.UpdateData);
                }, onStrategyUpdate =>
                {
                }, onGridUpdate =>
                {
                }, onListenKeyExpired =>
                {
                    _listenKeyFutures = _client.UsdFuturesApi.Account.StartUserStreamAsync().Result.Data;
                });
                if (futuresUserUpdate.Success)
                {
                    _subscriptions.TryAdd("FUTURES", futuresUserUpdate.Data);
                }

                _listenKeyCoin = _client.CoinFuturesApi.Account.StartUserStreamAsync().Result.Data;
                var coinUserUpdate = await _socketClient.CoinFuturesStreams.SubscribeToUserDataUpdatesAsync(_listenKeyCoin, onLeverageUpdate =>
                {
                }, onMarginUpdate =>
                {
                }, onAccountUpdate =>
                {
                }, onOrderUpdate =>
                {
                    OnRaiseUpdateOrder(onOrderUpdate.Data.UpdateData);
                }, onListenKeyExpired =>
                {
                    _listenKeyCoin = _client.CoinFuturesApi.Account.StartUserStreamAsync().Result.Data;
                });
                if (coinUserUpdate.Success)
                {
                    _subscriptions.TryAdd("COIN", coinUserUpdate.Data);
                }

                SetTimer();
            }
        }

        public async Task StopAccountDataProviderAsync(string derivates)
        {
            var unsubscribe = _subscriptions[derivates];
            if (unsubscribe is not null && _socketClient is not null)
            {
                await _socketClient.UnsubscribeAsync(unsubscribe);
                _subscriptions.Remove(derivates);
            }
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(900000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private async void OnTimedEvent(object? o, ElapsedEventArgs e)
        {

            var futures = await _client!.UsdFuturesApi.Account.KeepAliveUserStreamAsync(_listenKeyFutures!);
            var coin = await _client.CoinFuturesApi.Account.KeepAliveUserStreamAsync(_listenKeyCoin!);

            if (!futures.Success)
            {
                _listenKeyFutures = _client.UsdFuturesApi.Account.StartUserStreamAsync().Result.Data;
            }
            if (!coin.Success)
            {
                _listenKeyCoin = _client.CoinFuturesApi.Account.StartUserStreamAsync().Result.Data;
            }

        }
    }
}
