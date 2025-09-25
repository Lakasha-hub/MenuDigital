using Aplication.DTOs;
using Aplication.DTOs.Dish;
using Aplication.Exceptions;
using Aplication.Interfaces.Command;
using Aplication.Interfaces.Querys;
using Aplication.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aplication.Services
{
    public class DishService : IDishService
    {
        private readonly IDishCommand _dishCommand;
        private readonly IDishQuery _dishQuery;
        private readonly ICategoryQuery _categoryQuery;
        private readonly IOrderQuery _orderQuery;

        public DishService(IDishCommand command, IDishQuery query, ICategoryQuery categoryQuery, IOrderQuery orderQuery)
        {
            _dishCommand = command;
            _dishQuery = query;
            _categoryQuery = categoryQuery;
            _orderQuery = orderQuery;
        }

        public async Task<List<DishResponse>> GetAll(DishFilterRequest filter)
        {
            var dishes = await _dishQuery.GetAllDishes(filter);

            var result = dishes.Select(d => new DishResponse
            {
                Id = d.DishId.ToString(),
                Name = d.Name,
                Description = d.Description,
                Price = (double)d.Price,
                IsActive = d.Available,
                Image = d.ImageUrl,
                Category = new GenericResponse
                {
                    Id = d.CategoryDb.Id,
                    Name = d.CategoryDb.Name
                },
                CreatedAt = d.CreateDate,
                UpdatedAt = d.UpdateDate,
            });
            return result.ToList();
        }

        public async Task<DishResponse> GetById(Guid id)
        {
            var dish = _dishQuery.GetDishById(id);
            if (dish == null)
            {
                throw new NotFoundException("Plato no encontrado");
            }

            var result = new DishResponse
            {
                Id = dish.DishId.ToString(),
                Name = dish.Name,
                Description = dish.Description,
                Price = (double)dish.Price,
                IsActive = dish.Available,
                Image = dish.ImageUrl,
                Category = new GenericResponse
                {
                    Id = dish.CategoryDb.Id,
                    Name = dish.CategoryDb.Name
                },
                CreatedAt = dish.CreateDate,
                UpdatedAt = dish.UpdateDate,
            };
            return result;
        }

        public async Task<DishResponse> Create(DishRequest req)
        {
            Dish dishExists = _dishQuery.GetDishByName(req.Name);
            if (dishExists != null)
                throw new ConflictException("Ya existe un plato con ese nombre");

            Category categoryExists = _categoryQuery.GetCategoryById(req.Category);
            if (categoryExists == null)
                throw new NotFoundException("La categoría no existe");

            if(req.Price <= 0)
                throw new BusinessException("El precio debe ser mayor a cero");

            if(!string.IsNullOrEmpty(req.Image) && !Uri.IsWellFormedUriString(req.Image, UriKind.Absolute))
                throw new BusinessException("La url no es válida");

            var newDish = new Dish
            {
                Name = req.Name.Trim(),
                Description = req.Description != null ? req.Description.Trim() : "",
                Price = (decimal)req.Price,
                Available = true,
                ImageUrl = req.Image != null ? req.Image.Trim() : "",
                Category = req.Category,
                CreateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                UpdateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            };

            await _dishCommand.InsertDish(newDish);

            DishResponse result = new DishResponse
            {
                Id = newDish.DishId.ToString(),
                Name = newDish.Name,
                Description = newDish.Description,
                Price = (double)newDish.Price,
                IsActive = newDish.Available,
                Image = newDish.ImageUrl,
                Category = new GenericResponse
                {
                    Id = categoryExists.Id,
                    Name = categoryExists.Name
                },
                CreatedAt = newDish.CreateDate,
                UpdatedAt = newDish.UpdateDate,
            };
            return result;
        }

        public async Task<DishResponse> Update(Guid id, DishUpdateRequest req)
        {
            var dish = _dishQuery.GetDishById(id);
            if (dish == null)
            {
                throw new NotFoundException("Plato no encontrado");
            }

            if (!string.IsNullOrEmpty(req.Name))
            {
                var dishWithSameName = _dishQuery.GetDishByName(req.Name);
                if(dishWithSameName != null && dishWithSameName.DishId != dish.DishId)
                {
                    throw new ConflictException("Ya existe un plato con ese nombre");
                }
            }

            if (req.Price <= 0)
            {
                throw new BusinessException("El precio debe ser mayor a cero");
            }

            Category categoryExists = _categoryQuery.GetCategoryById(req.Category);
            if (categoryExists == null)
            {
                throw new BusinessException("La categoría no existe");
            }

            if (!string.IsNullOrEmpty(req.Image) && !Uri.IsWellFormedUriString(req.Image, UriKind.Absolute))
            {
                throw new BusinessException("La url no es válida");
            }

            dish.Name = req.Name.Trim();
            dish.Description = req.Description != null ? req.Description.Trim() : dish.Description;
            dish.Price = (decimal)req.Price;
            dish.ImageUrl = req.Image != null ? req.Image.Trim() : dish.ImageUrl;
            dish.Available = req.IsActive;
            dish.Category = req.Category;
            dish.UpdateDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified); ;

            await _dishCommand.UpdateDish(dish);

            var query = _dishQuery.GetDishById(id);
            var result = new DishResponse
            {
                Id = query.DishId.ToString(),
                Name = query.Name,
                Description = query.Description,
                Price = (double)query.Price,
                IsActive = query.Available,
                Image = query.ImageUrl,
                Category = new GenericResponse
                {
                    Id = categoryExists.Id,
                    Name = categoryExists.Name
                },
                CreatedAt = query.CreateDate,
                UpdatedAt = query.UpdateDate,
            };
            return result;
        }

        public async Task<DishResponse> Delete(Guid id)
        {
            var dish = _dishQuery.GetDishById(id);
            if (dish == null)
                throw new NotFoundException("Plato no encontrado");

            var dishInOrder = await _orderQuery.IsDishInOrderActive(id);
            if (dishInOrder)
                throw new BusinessException("No se puede eliminar el plato porque está incluido en órdenes activas");

            await _dishCommand.DeleteDish(id);
            return new DishResponse
            {
                Id = dish.DishId.ToString(),
                Name = dish.Name,
                Description = dish.Description,
                Price = (double)dish.Price,
                IsActive = false,
                Image = dish.ImageUrl,
                Category = new GenericResponse
                {
                    Id = dish.CategoryDb.Id,
                    Name = dish.CategoryDb.Name
                },
                CreatedAt = dish.CreateDate,
                UpdatedAt = dish.UpdateDate,
            };
        }
    }
}
