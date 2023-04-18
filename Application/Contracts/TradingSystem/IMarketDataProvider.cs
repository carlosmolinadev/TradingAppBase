using Application.Models.Broker;
using Domain.Entities;

namespace Application.Contracts.Broker
{
    public interface IMarketDataProvider
    {
        Task<decimal> GetPriceBySymbol(string symbol, Exchange exchange);
        Task StartDataProviderAsync(string symbol, string derivates, Exchange exchange);
        Task StopDataProviderAsync(string symbol, Exchange exchange);
    }
}