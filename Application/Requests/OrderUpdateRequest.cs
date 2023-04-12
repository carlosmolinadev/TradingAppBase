using Application.Models.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests
{
    public class OrderUpdateRequest
    {
        public AccountOrderUpdate AccountOrderUpdate { get; set; }
        public TradeOrderRequest TradeOrderRequest { get; set; }
    }
}
