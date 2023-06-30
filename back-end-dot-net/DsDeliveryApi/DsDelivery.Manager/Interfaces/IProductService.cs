using DsDelivery.Core.Shared;
using DsDelivery.Core.Shared.Dto.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDelivery.Manager.Interfaces;

public interface IProductService
{
    Task <ProductDTO> DeleteAsync(int id);
    Task<List<ProductDTO>> GetAllAsync();
    Task<ProductDTO> GetByIdAsync(int id);
    Task<ProductDTO> InsertAsync(CreateProductDTO dto);
    Task<ProductDTO> UpdateAsync(UpdateProductDTO productDTO);
}
