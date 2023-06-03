using Application.Contracts.Persistance;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries
{
    public class OrderQuery
    {
        private readonly IRepositoryAsync<OrderType> _orderTypeRepository;
        private readonly IRepositoryAsync<OrderSide> _orderSideRepository;

        public OrderQuery(IRepositoryAsync<OrderType> orderTypeRepository, IRepositoryAsync<OrderSide> orderSideRepository)
        {
            _orderTypeRepository = orderTypeRepository;
            _orderSideRepository = orderSideRepository;
        }
        public async Task<IEnumerable<OrderType>> GetAllOrderTypes()
        {
            return await _orderTypeRepository.SelectAllAsync();
        }

        public async Task<IEnumerable<OrderSide>> GetAllOrderSide()
        {
            return await _orderSideRepository.SelectAllAsync();
        }
    }
}
