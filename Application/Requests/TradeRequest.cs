﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests
{
    public class TradeRequest
    {
        public ICollection<Trade>? Trades { get; set; }
        
    }
}
