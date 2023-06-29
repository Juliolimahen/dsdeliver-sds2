using AutoMapper;
using DsDelivery.Core.Shared;
using DsDelivery.Core.Shared.Dto;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDelivery.WebApi.Controllers;

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
    public async Task<IActionResult> InsertAsync(ProductDTO dto)
    {
        try
        {
            var insertedProduct = await _service.InsertAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { productId = insertedProduct.Id }, insertedProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}
