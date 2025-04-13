using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _config;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _config = _mapper.ConfigurationProvider;
        }

        public Task<List<OrderDto>> GetAllAsync()
        {
            return _orderRepository.Entity.ProjectTo<OrderDto>(_config)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public Task<OrderDto?> GetByIdAsync(string id)
        {
            return _orderRepository.Entity.Where(o => o.Id == id)
                            .ProjectTo<OrderDto>(_config)
                            .AsNoTracking()
                            .FirstOrDefaultAsync();
        }

        public async Task<OrderDto> CreateAsync(OrderCreateRequest request)
        {
            var order = _mapper.Map<Order>(request);
            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateAsync(string id, OrderUpdateRequest request)
        {
            var order = _mapper.Map<Order>(request);
            _orderRepository.Update(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task DeleteAsync(string id)
        {
            var order = await _orderRepository.Entity.Where(o => o.Id == id)
                                                    .FirstOrDefaultAsync();
            if (order == null)
            {
                throw new NotFoundException($"Order with id {id} not found.");   
            }

            _orderRepository.Delete(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}