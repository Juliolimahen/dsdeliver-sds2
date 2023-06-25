using AutoMapper;
using DsDeliveryApi.Dto;
using DsDeliveryApi.Models;
using DsDeliveryApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ProductDTO>> FindAll()
        {
            List<Product> list = await repository.FindAllByOrderByNameAsc();
            List<ProductDTO> dtoList = list.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
            return dtoList;
        }


        public Task<List<ProductDTO>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductDTO> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductDTO> Insert(ProductDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductDTO> Update(int id, ProductDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}