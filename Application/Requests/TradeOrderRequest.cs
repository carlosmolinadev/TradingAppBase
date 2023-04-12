using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests
{
    public class TradeOrderRequest : BaseRequest
    {
        public Exchange Exchange { get; set; }
        public Derivate Derivate { get; set; }
        public ICollection<TradeOrder> Orders { get; set; }

    }
}
