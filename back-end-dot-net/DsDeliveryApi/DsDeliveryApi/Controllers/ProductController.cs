using DsDeliveryApi.Dto;
using DsDeliveryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.devsuperior.dsdeliver.controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> FindAll()
        {
            List<ProductDTO> list = await _service.FindAll();
            return Ok(list);
        }
    }
}
