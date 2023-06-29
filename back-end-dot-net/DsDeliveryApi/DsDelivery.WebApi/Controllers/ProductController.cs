using AutoMapper;
using DsDelivery.Core.Shared;
using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDelivery.WebApi.Controllers;

[Route("products")]
[Produces("application/json")]
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
        List<ProductDTO> list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetByIdAsync(int productId)
    {
        try
        {
            var product = await _service.GetByIdAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> InsertAsync(ProductDTO dto)
    {
        try
        {
            var insertedProduct = await _service.InsertAsync(dto);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
