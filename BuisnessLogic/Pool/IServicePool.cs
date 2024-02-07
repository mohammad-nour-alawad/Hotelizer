using BuisnessLogic.AppServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Pool
{
    public interface IServicePool
    {
        public RoomService roomService { get; }
        public BookingService bookingService { get; }
        public RestaurantService restaurantService { get; }
    }
}
