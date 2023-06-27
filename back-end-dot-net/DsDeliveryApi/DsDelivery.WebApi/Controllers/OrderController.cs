using AutoMapper;
using DsDelivery.Core.Shared;
using DsDelivery.Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DsDelivery.WebApi.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrderController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper= mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> FindAll()
        {
            List<OrderDTO> list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<CreateOrderDTO>> Insert([FromBody] CreateOrderDTO dto)
        {
            dto = await _service.InsertAsync(dto);
            OrderDTO orderDTO= _mapper.Map<OrderDTO>(dto);
            Uri uri = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{orderDTO.Id}");
            return Created(uri, dto);
        }

        [HttpPut("{id}/delivered")]
        public async Task<ActionResult<OrderDTO>> SetDelivered([FromRoute] int id)
        {
            OrderDTO dto = await _service.SetDeliveredAsync(id);
            return Ok(dto);
        }
    }
}
