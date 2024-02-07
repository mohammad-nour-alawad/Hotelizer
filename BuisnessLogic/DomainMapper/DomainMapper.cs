using AutoMapper;
using BuisnessLogic.Dtos;
using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.DomainMapper
{
    public class DomainMapper : Profile
    {
        public DomainMapper()
        {
            CreateMap<Booking, BookingDto>();
            CreateMap<Catergory, CatergoryDto>();
            CreateMap<FoodItem, FoodItemDto>();
            CreateMap<FoodItemType, FoodItemTypeDto>();
            CreateMap<Hotel, HotelDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<RoomSpecification, RoomSpecificationDto>();
            CreateMap<ServiceCategoryBooking, ServiceCategoryBookingDto>();
            CreateMap<ServiceCategory, ServiceCategoryDto>();
            CreateMap<Service, ServiceDto>();
            CreateMap<Specification, SpecificationDto>();
            CreateMap<ApplicationUser, ApplicationUserDto>();


            CreateMap<BookingDto,Booking>();
            CreateMap<CatergoryDto,Catergory>();
            CreateMap<FoodItemDto, FoodItem>();
            CreateMap<FoodItemTypeDto,FoodItemType>();
            CreateMap<HotelDto,Hotel>();
            CreateMap<OrderDto, Order>();
            CreateMap<RoomDto,Room>();
            CreateMap<RoomSpecificationDto, RoomSpecification>();
            CreateMap<ServiceCategoryBookingDto, ServiceCategoryBooking>();
            CreateMap<ServiceCategoryDto, ServiceCategory>();
            CreateMap<ServiceDto,Service>();
            CreateMap<SpecificationDto,Specification>();
            CreateMap<ApplicationUserDto, ApplicationUser>();
        }
    }
}
