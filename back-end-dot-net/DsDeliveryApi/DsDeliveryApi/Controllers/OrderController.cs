using DsDeliveryApi.Dto;
using DsDeliveryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace com.devsuperior.dsdeliver.controllers
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
        public ActionResult<List<OrderDTO>> FindAll()
        {
            List<OrderDTO> list = _service.FindAll();
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Insert([FromBody] OrderDTO dto)
        {
            dto = await _service.Insert(dto);
            Uri uri = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{dto.Id}");
            return Created(uri, dto);
        }

        [HttpPut("{id}/delivered")]
        public async Task<ActionResult<OrderDTO>> SetDelivered([FromRoute] long id)
        {
            OrderDTO dto = await _service.SetDelivered(id);
            return Ok(dto);
        }
    }
}
