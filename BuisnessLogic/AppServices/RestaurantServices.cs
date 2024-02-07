using System;
using System.Collections.Generic;
using DBConnection.Models;
using DBConnection.Repos;
using DBConnection.UnitOfWork;
using BuisnessLogic.Dtos;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using System.Linq;

namespace BuisnessLogic.AppServices
{
    public class RestaurantService : BaseService.BaseService<
         OrderDto, IOrderRepo, Order>
    {
        public RestaurantService(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork,
                httpContextAccessor)
        {

        }

        public override IOrderRepo DbRepo => UnitOfWork.OrderRepo;
        public IFoodItemRepo FoodRepo => UnitOfWork.FoodItemRepo;
        public IFoodItemTypeRepo FoodTypeRepo => UnitOfWork.FoodItemTypeRepo;


        public override OrderDto Create(OrderDto dto)
        {
            return base.Create(dto);
        }
    

        public override OrderDto Update(OrderDto dto)
        {
            return base.Update(dto);
        }

        public List<OrderDto> GetAll(Expression<Func<Order, bool>> condition = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, string includeProperties = "")
        {
            return Mapper.Map<List<OrderDto>>(DbRepo.GetAll(condition, orderBy, includeProperties));
        }


        public List<FoodItemDto> GetAllFooditems(Expression<Func<FoodItem, bool>> condition = null, Func<IQueryable<FoodItem>, IOrderedQueryable<FoodItem>> orderBy = null, string includeProperties = "")
        {
            return Mapper.Map<List<FoodItemDto>>(FoodRepo.GetAll(condition, orderBy, includeProperties));
        }

       
    }
}
