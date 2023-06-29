using DsDelivery.Core.Shared;
using DsDelivery.Core.Shared.Dto.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDelivery.Manager.Interfaces;

public interface IProductService
{
    Task DeleteAsync(int id);
    Task<List<ProductDTO>> GetAllAsync();
    Task<ProductDTO> GetByIdAsync(int id);
    Task<ProductDTO> InsertAsync(ProductDTO dto);
    Task<ProductDTO> UpdateAsync(int id, ProductDTO dto);
}
