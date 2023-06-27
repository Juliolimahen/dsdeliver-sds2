using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared;
using DsDeliveryApi.Repositories;

namespace DsDelivery.Manager.Interfaces
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;

        public OrderService(IMapper mapper, IOrderRepository repository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            List<Order> orders = await _repository.FindOrdersWithProducts();

            List<OrderDTO> dtoList = orders.Select(order =>
            {
                List<ProductDTO> productDTOs = order.OrderProducts
                    .Select(op => new ProductDTO
                    {
                        Id = op.Product.Id,
                        Name = op.Product.Name,
                        Price = op.Product.Price,
                        Description = op.Product.Description,
                        ImageUri = op.Product.ImageUri
                    })
                    .ToList();

                return new OrderDTO
                {
                    Id = order.Id,
                    Address = order.Address,
                    Latitude = order.Latitude,
                    Longitude = order.Longitude,
                    Moment = order.Moment,
                    Status = order.Status.ToString(),
                    Total = order.GetTotal(),
                    Products = productDTOs
                };
            }).ToList();

            return dtoList;
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            Order order = await _repository.GetByIdAsync(id);
            OrderDTO orderDto = _mapper.Map<OrderDTO>(order);
            return orderDto;
        }

        public async Task<OrderDTO> SetDeliveredAsync(int id)
        {
            Order order = await _repository.GetByIdAsync(id);
            order.Status = OrderStatus.DELIVERED;
            order = await _repository.UpdateAsync(order);
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<OrderDTO> InsertAsync(OrderDTO dto)
        {
            Order order = _mapper.Map<Order>(dto);
            order.Moment = DateTime.Now;
            order.Status = OrderStatus.PENDING;
            order.OrderProducts = new List<OrderProduct>(); // Inicializa a lista OrderProducts

            foreach (ProductDTO p in dto.Products)
            {
                Product product = await _productRepository.GetByIdAsync(p.Id);
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

            order = await _repository.AddAsync(order);

            return _mapper.Map<OrderDTO>(order);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}