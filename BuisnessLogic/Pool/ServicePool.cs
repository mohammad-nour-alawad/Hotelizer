using BuisnessLogic.AppServices;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Pool
{
    public class ServicePool : IServicePool
    {
        public RoomService roomService { get; }
        public RestaurantService restaurantService { get; }

        public BookingService bookingService { get; }

        public ServicePool(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            roomService = new RoomService(unitOfWork, httpContextAccessor);
            bookingService = new BookingService(unitOfWork, httpContextAccessor);
            restaurantService = new RestaurantService(unitOfWork, httpContextAccessor);
        }
    }
}
