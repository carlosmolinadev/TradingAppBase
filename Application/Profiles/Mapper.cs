using Domain.Entities;

namespace Application.Profiles
{
    public static class Mapper
    {
        //public static TradeDTO ToTradeDTO(Trade tradeDB)
        //{
        //    return new TradeDTO
        //    {
        //        RiskReward = tradeDB.RiskReward,
        //        LateEntry = tradeDB.LateEntry,
        //        CandleCloseEntry = tradeDB.CandleCloseEntry,
        //        Attempt = tradeDB.Attempt,
        //        PercentageEntry = tradeDB.PercentageEntry,
        //        Symbol = tradeDB.Symbol,
        //        CreatedDate = tradeDB.CreatedDate,
        //    };
        //}

        //public static Trade ToTradeDB()
        //{
        //    return new Trade(id: default, riskReward: tradeDTO.RiskReward, lateEntry: tradeDTO.LateEntry, candleCloseEntry: tradeDTO.CandleCloseEntry, attempt: tradeDTO.Attempt, percentageEntry: tradeDTO.PercentageEntry, symbol: tradeDTO.Symbol, createdDate: tradeDTO.CreatedDate, tradeStrategyId: tradeDTO.TradeStrategy.Id, accountId: tradeDTO.Account.Id);
        //}
    }
}


//public static class Mapper<TSource, TDestination>
//{
//    public static TDestination Map(TSource source)
//    {
//        var destination = Activator.CreateInstance<TDestination>();

//        var sourceProperties = typeof(TSource).GetProperties();
//        var destinationProperties = typeof(TDestination).GetProperties();

//        foreach (var sourceProperty in sourceProperties)
//        {
//            var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);
//            if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType && destinationProperty.CanWrite)
//            {
//                destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
//            }
//        }

//        return destination;
//    }
//}