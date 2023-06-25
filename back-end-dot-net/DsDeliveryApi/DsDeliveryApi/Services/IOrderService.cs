using DsDeliveryApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDeliveryApi.Services
{
    public interface IOrderService
    {
        Task Delete(int id);
        List<OrderDTO> FindAll();
        Task<List<OrderDTO>> GetAll();
        Task<OrderDTO> GetById(int id);
        Task<OrderDTO> Insert(OrderDTO dto);
        Task<OrderDTO> SetDelivered(long id);
        Task<OrderDTO> Update(int id, OrderDTO dto);
    }
}
