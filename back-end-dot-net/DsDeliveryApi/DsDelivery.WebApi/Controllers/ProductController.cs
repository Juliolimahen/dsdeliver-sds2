using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Operation = SerilogTimings.Operation;
using Microsoft.AspNetCore.Authorization;

namespace DsDelivery.WebApi.Controllers;

[ApiController]
[Route("products")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService service, ILogger<ProductController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Retorna todos produtos cadastrados na base.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        using (Operation.Time("GetAllProducts"))
        {
            try
            {
                IEnumerable<ProductDTO> list = await _service.GetAllAsync();
                if (!list.Any())
                {
                    return NotFound();
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao obter todos os produtos.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    /// <summary>
    /// Retorna um produto consultado pelo id.
    /// </summary>
    /// <param name="productId" example="1">Id do produto.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int productId)
    {
        using (Operation.Time("GetProductById"))
        {
            try
            {
                var product = await _service.GetByIdAsync(productId);
                if (product.Id == 0)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao obter o produto por ID.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    /// <summary>
    /// Insere um novo produto. 
    /// </summary>
    /// <param name="productDTO"></param>
    [HttpPost]
    [Authorize(Roles = "Presidente, Lider, Diretor")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert(CreateProductDTO productDTO)
    {
        using (Operation.Time("InsertProduct"))
        {
            try
            {
                ProductDTO insertedProduct;
                _logger.LogInformation("Objeto recebido {@dto}", productDTO);
                insertedProduct = await _service.InsertAsync(productDTO);
                return CreatedAtAction(nameof(GetById), new { id = insertedProduct.Id }, insertedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao inserir o produto.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    /// <summary>
    /// Altera um produto.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="productDTO"></param>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Presidente, Lider, Diretor")]
    [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, UpdateProductDTO productDTO)
    {
        using (Operation.Time("UpdateProduct"))
        {
            try
            {
                var product = await _service.UpdateAsync(productDTO);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao atualizar o produto.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    /// <summary>
    /// Exclui um produto.
    /// </summary>
    /// <param name="id" example="1"></param>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Presidente, Lider, Diretor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        using (Operation.Time("DeleteProduct"))
        {
            try
            {
                var productRemoved = await _service.DeleteAsync(id);
                if (productRemoved == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao excluir o produto.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
