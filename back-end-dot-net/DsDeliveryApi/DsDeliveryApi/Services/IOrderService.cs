using DsDeliveryApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDeliveryApi.Services
{
    public interface IOrderService
    {
        Task Delete(int id);
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task<OrderDTO> InsertAsync(OrderDTO dto);
        Task<OrderDTO> SetDeliveredAsync(int id);
        Task<OrderDTO> UpdateAsync(int id, OrderDTO dto);
    }
}
