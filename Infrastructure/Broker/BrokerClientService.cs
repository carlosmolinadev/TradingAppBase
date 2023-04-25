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

        public async Task<TradeOrderResponse> CreateOrder(Exchange exchange, string derivate, OrderType orderType, OrderParameter orderParameter)
        {
            var response = new TradeOrderResponse();
            switch (exchange.Value)
            {
                case "BINANCE":
                    if (derivate == "FUTURES")
                    {
                        response = await CreateFuturesBinanceOrder(orderType, orderParameter);
                    }
                    if (derivate == "COIN")
                    {
                        response = await CreateCoinBinanceOrder(orderType, orderParameter);
                    }
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
            (Binance.Net.Enums.OrderSide)orderParameter.Side.Id,
            type: (FuturesOrderType)orderType.Id,
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
            (Binance.Net.Enums.OrderSide)orderParameter.Side.Id,
            type: (FuturesOrderType)orderType.Id,
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
