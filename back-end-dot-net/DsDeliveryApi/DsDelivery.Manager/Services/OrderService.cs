using AutoMapper;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Data.Repositories;
using DsDelivery.Manager.Interfaces;


namespace DsDelivery.Manager.Services;

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
                Status = order.Status,
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

    public async Task<OrderDTO> InsertAsync(CreateOrderDTO dto)
    {
        Order order = new Order
        {
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Moment = DateTime.Now,
            Status = OrderStatus.PENDING,
            OrderProducts = new List<OrderProduct>()
        };

        foreach (var dtoProduct in dto.Products)
        {
            Product product = await _productRepository.GetByIdAsync(dtoProduct.Id);
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