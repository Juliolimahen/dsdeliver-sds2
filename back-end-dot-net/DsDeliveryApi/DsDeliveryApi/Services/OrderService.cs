using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AutoMapper;
using DsDeliveryApi.Dto;
using DsDeliveryApi.Models;
using DsDeliveryApi.Repositories;

namespace DsDeliveryApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository repository;
        private readonly IProductRepository productRepository;

        public OrderService(IMapper mapper, IOrderRepository repository, IProductRepository productRepository)
        {
            _mapper = mapper;
            this.repository = repository;
            this.productRepository = productRepository;
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            List<Order> orders = await repository.FindOrdersWithProducts();
            List<OrderDTO> orderDTOs = _mapper.Map<List<OrderDTO>>(orders);

            return orderDTOs;
        }

        public async Task<OrderDTO> GetById(int id)
        {
            Order order = await repository.GetByIdAsync(id);
            OrderDTO orderDto = _mapper.Map<OrderDTO>(order);
            return orderDto;
        }

        public async Task<OrderDTO> SetDelivered(long id)
        {
            Order order = await repository.GetByIdAsync(id);
            order.Status = OrderStatus.DELIVERED;
            order = await repository.AddAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> Insert(OrderDTO dto)
        {
            Order order = _mapper.Map<Order>(dto);
            order.Moment = DateTime.Now;
            order.Status = OrderStatus.PENDING;

            foreach (ProductDTO p in dto.Products)
            {
                Product product = await productRepository.GetByIdAsync(p.Id);
                if (product != null)
                {
                    OrderProduct orderProduct = new OrderProduct
                    {
                        Order = order,
                        Product = product
                    };
                    order.OrderProducts.Add(orderProduct);
                }
            }

            order = await repository.AddAsync(order);

            return _mapper.Map<OrderDTO>(order);
        }


        public Task<OrderDTO> Update(int id, OrderDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}