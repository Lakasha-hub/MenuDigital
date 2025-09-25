using Aplication.DTOs;
using Aplication.DTOs.Dish;
using Aplication.DTOs.Order;
using Aplication.Exceptions;
using Aplication.Interfaces.Command;
using Aplication.Interfaces.Querys;
using Aplication.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderCommand _command;
        private readonly IOrderQuery _query;
        private readonly IDishQuery _dishQuery;

        public OrderService(IOrderQuery query, IOrderCommand command, IDishQuery dishQuery)
        {
            _query = query;
            _command = command;
            _dishQuery = dishQuery;
        }

        public async Task<OrderCreateResponse> Create(OrderRequest req)
        {
            Order order = new Order
            {
                DeliveryType = req.Delivery.Id,
                DeliveryTo = !string.IsNullOrEmpty(req.Delivery.To) ? req.Delivery.To.Trim() : "",
                Notes = !string.IsNullOrEmpty(req.Notes) ? req.Notes.Trim() : "",
                OrderItems = new List<OrderItem>(),
                OverallStatus = 1,
                CreateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                UpdateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            };

            decimal total = 0;
            foreach (var item in req.Items)
            {
                var dish = _dishQuery.GetDishById(item.Id);
                if (dish == null || !dish.Available)
                {
                    throw new BusinessException("El plato especificado no existe o no está disponible");
                }
                total += dish.Price * item.Quantity;

                order.OrderItems.Add(new OrderItem
                {
                    Dish = dish.DishId,
                    Quantity = item.Quantity,
                    Notes = !string.IsNullOrEmpty(item.Notes) ? item.Notes.Trim() : "",
                    Status = 1,
                    CreateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                });
            }

            order.Price = total;
            await _command.InsertOrder(order);

            return new OrderCreateResponse
            {
                OrderNumber = Convert.ToInt32(order.OrderId),
                CreatedAt = order.CreateDate.ToLocalTime(),
                TotalAmount = Convert.ToDouble(order.Price)
            };
        }

        public async Task<List<OrderDetailsResponse>> GetAll(OrderFilterRequest filter)
        {
            if (filter.From.HasValue && filter.To.HasValue && filter.From > filter.To)
                throw new BusinessException("Rango de fechas inválido");

            var orders = await _query.GetAllOrders(filter);
            var result = orders.Select(o => new OrderDetailsResponse
            {
                OrderNumber = Convert.ToInt32(o.OrderId),
                TotalAmount = Convert.ToDouble(o.Price),
                DeliveryTo = o.DeliveryTo,
                Notes = o.Notes,
                Status = new GenericResponse
                {
                    Id = o.StatusDb.Id,
                    Name = o.StatusDb.Name
                },
                DeliveryType = new GenericResponse
                {
                    Id = o.DeliveryTypeDb.Id,
                    Name = o.DeliveryTypeDb.Name,
                },
                Items = o.OrderItems.Select(oi => new OrderItemResponse
                {
                    Id = Convert.ToInt32(oi.OrderItemId),
                    Quantity = oi.Quantity,
                    Notes = oi.Notes,
                    Status = new GenericResponse
                    {
                        Id = oi.Status,
                        Name = oi.StatusDb.Name
                    },
                    Dish = new DTOs.Dish.DishShortResponse
                    {
                        Id = oi.Dish,
                        Name = oi.DishDb.Name,
                        Image = oi.DishDb.ImageUrl,
                    }
                }).ToList(),
                CreatedAt = o.CreateDate.ToLocalTime(),
                UpdatedAt = o.UpdateDate.ToLocalTime()
            }).OrderByDescending(o => o.CreatedAt);

            return result.ToList();
        }

        public async Task<OrderUpdateResponse> Update(int id, OrderUpdateRequest req)
        {
            var order = _query.GetOrderById(id);
            if (order == null)
                throw new NotFoundException("Orden no encontrada");

            if (order.OverallStatus == 5)
                throw new BusinessException("No se puede modificar una orden que se encuentra cerrada");

            if (req.Items == null || req.Items.Count == 0)
                throw new BusinessException("La Orden debe contener al menos un item");

            foreach(var item in req.Items)
            {
                var dish = _dishQuery.GetDishById(item.Id);
                if (dish == null || !dish.Available)
                {
                    throw new BusinessException("El plato especificado no existe o no está disponible");
                }

                var itemInOrder = order.OrderItems.FirstOrDefault(oi => oi.Dish == item.Id);

                if (itemInOrder != null)
                {
                    itemInOrder.Quantity += item.Quantity;
                    itemInOrder.Notes = !string.IsNullOrEmpty(item.Notes) ? item.Notes.Trim() : itemInOrder.Notes;
                }
                else
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        Quantity = item.Quantity,
                        Notes = !string.IsNullOrEmpty(item.Notes) ? item.Notes.Trim() : "",
                        Dish = item.Id,
                        Status = 1,
                        CreateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    });
                }
                order.Price += dish.Price * item.Quantity;
            }
            order.UpdateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            await _command.UpdateOrder(order);
            return new OrderUpdateResponse
            {
                OrderNumber = Convert.ToInt32(order.OrderId),
                TotalAmount = Convert.ToDouble(order.Price),
                UpdateAt = order.UpdateDate.ToLocalTime()
            };
        }

        public async Task<OrderDetailsResponse> GetById(int id)
        {
            var order = _query.GetOrderById(id);
            if (order == null)
                throw new NotFoundException("Orden no encontrada");

            var result = new OrderDetailsResponse
            {
                OrderNumber = Convert.ToInt32(order.OrderId),
                TotalAmount = Convert.ToDouble(order.Price),
                DeliveryTo = order.DeliveryTo,
                Notes = order.Notes,
                Status = new GenericResponse
                {
                    Id = order.StatusDb.Id,
                    Name = order.StatusDb.Name,
                },
                DeliveryType = new GenericResponse
                {
                    Id = order.DeliveryTypeDb.Id,
                    Name = order.DeliveryTypeDb.Name,
                },
                Items = order.OrderItems.Select(oi => new OrderItemResponse
                {
                    Id = Convert.ToInt32(oi.OrderItemId),
                    Quantity = oi.Quantity,
                    Notes = oi.Notes,
                    Status = new GenericResponse
                    {
                        Id = oi.StatusDb.Id,
                        Name = oi.StatusDb.Name,
                    },
                    Dish = new DishShortResponse
                    {
                        Id = oi.Dish,
                        Name = oi.DishDb.Name,
                        Image = oi.DishDb.ImageUrl
                    }
                }).ToList(),
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            };
            return result;
        }

        public async Task<OrderUpdateResponse> UpdateItem(int orderId, int itemId , OrderItemUpdateRequest details)
        {
            var order = _query.GetOrderById(orderId);
            if (order == null)
                throw new NotFoundException("Orden no encontrada");

            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.OrderItemId == itemId);
            if (orderItem == null)
                throw new NotFoundException("Orden no encontrada");

            await _command.UpdateOrderItem(new UpdateOrderItemParams
            {
                OrderId = orderId,
                OrderItemId = itemId,
                Status = details.Status,
            });

            return new OrderUpdateResponse
            {
                OrderNumber = Convert.ToInt32(order.OrderId),
                TotalAmount = Convert.ToDouble(order.Price),
                UpdateAt = order.UpdateDate.ToLocalTime(),
            };
        }
    }
}
