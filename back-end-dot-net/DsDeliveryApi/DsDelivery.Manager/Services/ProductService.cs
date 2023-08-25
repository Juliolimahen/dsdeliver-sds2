using AutoMapper;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Data.Repositories.Interfaces;

namespace DsDelivery.Manager.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductDTO>> GetAllAsync()
    {
        List<Product> list = await _repository.FindAllByOrderByNameAscAsync();
        List<ProductDTO> dtoList = list.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
        return dtoList;
    }

    public async Task<ProductDTO> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> InsertAsync(CreateProductDTO dto)
    {
        Product product = _mapper.Map<Product>(dto);
        product = await _repository.AddAsync(product);
        ProductDTO insertedDto = _mapper.Map<ProductDTO>(product);

        return insertedDto;
    }

    public async Task<ProductDTO> UpdateAsync(UpdateProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);
        product = await _repository.UpdateAsync(product);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> DeleteAsync(int id)
    {
        var product = await _repository.RemoveAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }
}
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}