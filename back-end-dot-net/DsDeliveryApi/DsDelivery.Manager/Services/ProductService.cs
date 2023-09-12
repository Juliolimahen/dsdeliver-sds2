using AutoMapper;
using DsDelivery.Manager.Interfaces;
using DsDelivery.Core.Domain;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DsDelivery.Manager.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, IMapper mapper, ILogger<ProductService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        _logger.LogInformation("Chamada de negócio para buscar todos produtos.");

        var products = await _repository.FindAllByOrderByNameAscAsync();

        if (products == null)
        {
            return new List<ProductDTO>();
        }

        var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return productDTOs;
    }


    public async Task<ProductDTO> GetByIdAsync(int id)
    {
        _logger.LogInformation("Chamada de negócio para buscar um produto por Id.");
        var product = await _repository.GetByIdAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> InsertAsync(CreateProductDTO dto)
    {
        _logger.LogInformation("Chamada de negócio para inserir um produto.");

        Product product = _mapper.Map<Product>(dto);
        product = await _repository.AddAsync(product);
        ProductDTO insertedDto = _mapper.Map<ProductDTO>(product);

        return insertedDto;
    }

    public async Task<ProductDTO> UpdateAsync(UpdateProductDTO productDTO)
    {
        _logger.LogInformation("Chamada de negócio para atualizar um produto.");
        var product = _mapper.Map<Product>(productDTO);
        product = await _repository.UpdateAsync(product);
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<ProductDTO> DeleteAsync(int id)
    {
        _logger.LogInformation("Chamada de negócio para deletar um produto.");
        var product = await _repository.RemoveAsync(id);
        return _mapper.Map<ProductDTO>(product);
    }
}
