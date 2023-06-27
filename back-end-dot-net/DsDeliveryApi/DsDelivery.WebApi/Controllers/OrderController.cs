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

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> FindAll()
        {
            List<OrderDTO> list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Insert([FromBody] OrderDTO dto)
        {
            dto = await _service.InsertAsync(dto);
            Uri uri = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{dto.Id}");
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
