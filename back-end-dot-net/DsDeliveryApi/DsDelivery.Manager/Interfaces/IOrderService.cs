using DsDelivery.Core.Shared.Dto.Order;

namespace DsDelivery.Manager.Interfaces;

public interface IOrderService
{
    Task<List<OrderDTO>> GetAllAsync();
    Task<OrderDTO> GetByIdAsync(int id);
    Task<OrderDTO> InsertAsync(CreateOrderDTO dto);
    Task<OrderDTO> SetDeliveredAsync(int id);
}
