using Domain.Entities;
using System.Collections.ObjectModel;

namespace Application.Responses
{
    public class TradeResponse : BaseResponse
    {
        public Trade Trade { get; set; }
    }
}
