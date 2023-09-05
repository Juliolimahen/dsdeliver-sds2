using DsDelivery.Core.Shared.Dto.Product;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Operation = SerilogTimings.Operation;
using Microsoft.AspNetCore.Authorization;

namespace DsDelivery.WebApi.Controllers
{
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

        [HttpGet]
        [Authorize(Roles = "Diretor")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            using (Operation.Time("GetAllProducts"))
            {
                try
                {
                    List<ProductDTO> list = await _service.GetAllAsync();
                    return Ok(list);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ocorreu um erro ao obter todos os produtos.");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

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

                    if (product == null)
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateProductDTO productDTO)
        {
            using (Operation.Time("UpdateProduct"))
            {
                try
                {
                    if (productDTO.Id == id)
                    {
                        var product = await _service.UpdateAsync(productDTO);
                        return Ok(product);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ocorreu um erro ao atualizar o produto.");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
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
}
