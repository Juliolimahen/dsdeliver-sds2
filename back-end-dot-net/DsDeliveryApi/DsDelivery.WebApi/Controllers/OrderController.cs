using DsDelivery.Core.Shared.Dto.Order;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DsDelivery.WebApi.Controllers;

[Route("orders")]
[Produces("application/json")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService service, ILogger<OrderController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Retorno todos os pedidos. 
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            List<OrderDTO> list = await _service.GetAllAsync();
            if (!list.Any())
                return NotFound();

            return Ok(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao recuperar todos os pedidos.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retorna um pedido consultado pelo id.
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            OrderDTO orderDto = await _service.GetByIdAsync(id);

            if (orderDto.Id == 0)
                return NotFound();

            return Ok(orderDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao recuperar o pedido por ID.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Cria um novo pedido. 
    /// </summary>
    /// <param name="dto"></param>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder(CreateOrderDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            OrderDTO orderDto = await _service.InsertAsync(dto);

            return CreatedAtAction(nameof(GetOrderById), new { id = orderDto.Id }, orderDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar um pedido.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Marca o pedido como entregue. 
    /// </summary>
    /// <param name="id" example="1"></param>
    [HttpPut("{id}/delivered")]
    [ProducesResponseType(typeof(OrderDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderDTO>> SetDelivered([FromRoute] int id)
    {
        try
        {
            OrderDTO dto = await _service.SetDeliveredAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao definir o pedido como entregue.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
