using DsDeliveryApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDeliveryApi.Services
{
    public interface IProductService
    {
        Task DeleteAsync(int id);
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> InsertAsync(ProductDTO dto);
        Task<ProductDTO> UpdateAsync(int id, ProductDTO dto);
    }
}
