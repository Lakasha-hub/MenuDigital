using Aplication.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderCreateResponse> Create(OrderRequest order);
        Task<List<OrderDetailsResponse>> GetAll(OrderFilterRequest filter);
        Task<OrderUpdateResponse> Update(int id, OrderUpdateRequest order);
        Task<OrderDetailsResponse> GetById(int id);
        Task<OrderUpdateResponse> UpdateItem(int orderId, int itemId, OrderItemUpdateRequest details);
    }
}
