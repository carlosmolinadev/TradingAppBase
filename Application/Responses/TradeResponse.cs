using Domain.Entities;
using System.Collections.ObjectModel;

namespace Application.Responses
{
    public class TradeResponse : BaseResponse
    {
        public IReadOnlyCollection<Trade> Trades { get; set; } = new Collection<Trade>();
    }
}
