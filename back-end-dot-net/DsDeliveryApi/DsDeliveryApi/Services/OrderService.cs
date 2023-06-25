using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DsDeliveryApi.Dto;
using DsDeliveryApi.Models;
using DsDeliveryApi.Repositories;

namespace DsDeliveryApi.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IProductRepository productRepository;

        public OrderService(IOrderRepository repository, IProductRepository productRepository)
        {
            this.repository = repository;
            this.productRepository = productRepository;
        }

        public List<OrderDTO> FindAll()
        {
            List<Order> list = repository.FindOrdersWithProducts();
            return list.Select(x => new OrderDTO(x)).ToList();
        }

        public Task<List<OrderDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDTO> SetDelivered(long id)
        {
            Order order = await repository.GetByIdAsync(id);
            order.Status = OrderStatus.DELIVERED;
            order = await repository.AddAsync(order);
            return new OrderDTO(order);
        }

        public async Task<OrderDTO> Insert(OrderDTO dto)
        {
            Order order = new Order(
                id: null,
                address: dto.Address,
                latitude: dto.Latitude,
                longitude: dto.Longitude,
                moment: DateTime.Now,
                status: OrderStatus.PENDING
            );

            foreach (ProductDTO p in dto.Products)
            {
                Product product = await productRepository.GetByIdAsync(p.Id);
                order.Products.Add(product);
            }

            order = await repository.AddAsync(order);

            return new OrderDTO(order);
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