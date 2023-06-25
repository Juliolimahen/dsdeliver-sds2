using AutoMapper;
using DsDeliveryApi.Dto;
using DsDeliveryApi.Models;
using DsDeliveryApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace DsDeliveryApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            List<Product> list = await repository.FindAllByOrderByNameAscAsync();
            List<ProductDTO> dtoList = list.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
            return dtoList;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            Product product = await repository.GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found");
            }

            ProductDTO productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public async Task<ProductDTO> InsertAsync(ProductDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);
            product = await repository.AddAsync(product);
            ProductDTO insertedDto = _mapper.Map<ProductDTO>(product);

            return insertedDto;
        }


        public async Task<ProductDTO> UpdateAsync(int id, ProductDTO dto)
        {
            // Verificar se o produto com o ID especificado existe no banco de dados
            Product existingProduct = await repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                // Produto não encontrado, você pode lançar uma exceção adequada ou retornar um valor nulo, dependendo do seu caso de uso.
                throw new NotFoundException("Produto não encontrado");
            }

            // Atualizar as propriedades do produto existente com os valores do DTO
            existingProduct.Name = dto.Name;
            existingProduct.Price = dto.Price;
            existingProduct.Description = dto.Description;
            existingProduct.ImageUri = dto.ImageUri;

            // Atualizar o produto no repositório
            existingProduct = await repository.UpdateAsync(existingProduct);

            // Mapear a entidade Product atualizada de volta para DTO
            ProductDTO updatedDto = _mapper.Map<ProductDTO>(existingProduct);

            return updatedDto;
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await repository.GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found");
            }

            await repository.RemoveAsync(product);
        }

    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}