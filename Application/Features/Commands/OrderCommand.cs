using Application.Contracts.Persistance;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands
{
    public class OrderCommand
    {
        private readonly IRepositoryAsync<TradeOrder> _tradeOrderRepository;

        public OrderCommand(IRepositoryAsync<TradeOrder> tradeOrderRepository)
        {
            _tradeOrderRepository = tradeOrderRepository;
        }

    }
}
