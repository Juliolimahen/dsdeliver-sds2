﻿using DsDelivery.Core.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DsDelivery.Manager.Interfaces
{
    public interface IOrderService
    {
        Task Delete(int id);
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task<CreateOrderDTO> InsertAsync(CreateOrderDTO dto);
        Task<OrderDTO> SetDeliveredAsync(int id);
    }
}