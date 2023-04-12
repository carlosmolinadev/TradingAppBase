using Application.Profiles.DTOs;
using Application.Responses;
using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients;
using Domain.Entities;

namespace Infrastructure.Broker
{
    public class BrokerClientService
    {
        private readonly IBinanceClient _binanceClient;

        public BrokerClientService(IBinanceClient binanceClient)
        {
            _binanceClient = binanceClient;
        }

        public async Task<TradeOrderResponse> CreateOrder(Exchange exchange, Derivate derivate, OrderType orderType, OrderParameter orderParameter)
        {
            var response = new TradeOrderResponse();
            switch (exchange)
            {
                case Exchange.Binance:
                    if (derivate == Derivate.Futures)
                    {
                        response = await CreateFuturesBinanceOrder(orderType, orderParameter);
                    }
                    if (derivate == Derivate.Coin)
                    {
                        response = await CreateCoinBinanceOrder(orderType, orderParameter);
                    }
                    break;
                case Exchange.BingX:
                    break;
                case Exchange.Okex:
                    break;
                default:
                    break;
            }

            return response;
        }

        private async Task<TradeOrderResponse> CreateFuturesBinanceOrder(OrderType orderType, OrderParameter orderParameter)
        {
            var response = new TradeOrderResponse();

            var orderData = await _binanceClient.UsdFuturesApi.Trading.PlaceOrderAsync(
            orderParameter.Symbol,
            (Binance.Net.Enums.OrderSide)orderParameter.Side,
            type: (FuturesOrderType)orderType,
            orderParameter.Quantity,
            stopPrice: orderParameter.TradePrice,
            timeInForce: TimeInForce.GoodTillCanceled
            );

            if (orderData.Success)
            {
                response.Success = true;
            }
            return response;
        }

        private async Task<TradeOrderResponse> CreateCoinBinanceOrder(OrderType orderType, OrderParameter orderParameter)
        {
            var response = new TradeOrderResponse();

            var orderData = await _binanceClient.CoinFuturesApi.Trading.PlaceOrderAsync(
            orderParameter.Symbol,
            (Binance.Net.Enums.OrderSide)orderParameter.Side,
            type: (FuturesOrderType)orderType,
            orderParameter.Quantity,
            stopPrice: orderParameter.TradePrice,
            timeInForce: TimeInForce.GoodTillCanceled
            );

            if (orderData.Success)
            {
                response.Success = true;
                
            }
            return response;
        }
    }
}
