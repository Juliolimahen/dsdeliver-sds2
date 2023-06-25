using DsDeliveryApi.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDeliveryApi.Services
{
    public interface IProductService
    {
        Task Delete(int id);
        Task<List<ProductDTO>> FindAll();
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);
        Task<ProductDTO> Insert(ProductDTO dto);
        Task<ProductDTO> Update(int id, ProductDTO dto);
    }
}
