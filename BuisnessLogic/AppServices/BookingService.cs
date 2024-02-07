using BuisnessLogic.Dtos;
using DBConnection.Models;
using DBConnection.Repos;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BuisnessLogic.ViewModels;

namespace BuisnessLogic.AppServices
{
    public class BookingService : BaseService.BaseService<BookingDto, IBookingRepo, Booking>
    {
        public BookingService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, httpContextAccessor)
        {
        }

        public override IBookingRepo DbRepo => UnitOfWork.BookingRepo;
        public IRoomSpecificationRepo RoomRepo => UnitOfWork.RoomSpecificationRepo;
        public IServiceCategoryRepo serviceCategoryRepo => UnitOfWork.ServiceCategoryRepo;
        public IServiceCategoryBookingRepo serviceCategoryBookingRepo => UnitOfWork.ServiceCategoryBookingRepo;


        public override BookingDto Create(BookingDto dto)
        {
            return base.Create(dto);
        }
        public ServiceCategoryBookingDto CreateServiceCategoryBooking(ServiceCategoryBookingDto dto)
        {
            ServiceCategoryBooking dbEntity = Mapper.Map<ServiceCategoryBooking>(dto);
            dbEntity = serviceCategoryBookingRepo.Insert(dbEntity);
            UnitOfWork.Save();
            return Mapper.Map<ServiceCategoryBookingDto>(dbEntity);
        }
        public override BookingDto Update(BookingDto dto)
        {
            return base.Update(dto);
        }
        public List<BookingDto> GetAll(Expression<Func<Booking, bool>> condition = null)
        {
            return Mapper.Map<List<BookingDto>>(DbRepo.GetAll(condition, x => x.OrderByDescending(s => s.FromDate), includeProperties: "Room"));
        }
        public List<BookingDto> GetAllBookings(Expression<Func<Booking, bool>> condition = null, string includeProperties = "")
        {
            return Mapper.Map<List<BookingDto>>(DbRepo.GetAll(condition, x => x.OrderByDescending(s => s.FromDate), includeProperties: "Room"));
        }
        public List<ServiceCategoryBookingDto> GetAllServiceCategory(Expression<Func<ServiceCategoryBooking, bool>> condition = null, string includeProperties = "")
        {
            return Mapper.Map<List<ServiceCategoryBookingDto>>(serviceCategoryBookingRepo.GetAll(condition, x => x.OrderByDescending(s => s.Booking.FromDate), includeProperties: "Booking,ServiceCategory,ServiceCategory.Service,Booking.ApplicationUser,Booking.Room"));
        }
        public ServiceCategoryDto GetServiceCategory(int sid, int cid)
        {
            var res = serviceCategoryRepo.GetAll(e => e.CategoryId == cid && e.ServiceId == sid);
            return (Mapper.Map<List<ServiceCategoryDto>>(res))[0];
        }
        public float RecalculateBill(int bookid)
        {
            var book = serviceCategoryBookingRepo.GetAll(x => x.BookingId == bookid, includeProperties: "ServiceCategory,ServiceCategory.Category").ToList();
            float sum = 0;
            foreach (var sc in book)
            {
                sum += sc.ServiceCategory.Cost;
            }
            if (book.Count != 0)
            {
                sum += book[0].ServiceCategory.Category.BasePrice;
            }
            return sum;
        }

        public List<ServiceCategoryDto> GetAllServiceCategoryForCatId(int cid)
        {
            var res = serviceCategoryRepo.GetAll(e => e.CategoryId == cid, includeProperties: "Service,Category");
            return (Mapper.Map<List<ServiceCategoryDto>>(res));
        }

        public void DeleteServiceCategoryBooking(int id)
        {
            serviceCategoryBookingRepo.Delete(id);
            UnitOfWork.Save();
        }

        public RoomSpecificationDto getRoomDetails(int id)
        {
            List<RoomSpecificationDto> rooms = Mapper.Map<List<RoomSpecificationDto>>(RoomRepo.GetAll(x => x.RoomId == id, includeProperties: "Spec,Room,Room.Category,Room.Category.ServiceCategory,Room.Category.ServiceCategory.Service"));
            Dictionary<int, RoomSpecificationDto> roomid_spec = new Dictionary<int, RoomSpecificationDto>();
            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (!roomid_spec.ContainsKey(room.RoomId))
                {
                    roomid_spec.Add(room.RoomId, room);
                }
                else
                {
                    roomid_spec[room.RoomId].Spec.Type = roomid_spec[room.RoomId].Spec.Type + ", " + room.Spec.Type;
                }
            }
            return roomid_spec[id];
        }

        public ServiceDto GetServiceDetails(int serviceCategoryid)
        {
            var res = serviceCategoryRepo.GetAll(x => x.Service.Id == serviceCategoryid, includeProperties: "Service")
                                         .Select(x => x.Service)
                                         .ToList()[0];

            return Mapper.Map<ServiceDto>(res);
        }

        public List<RoomSpecificationDto> GetMostBooked()
        {
            var res = (DbRepo.GetAll(x => x.Status == "Accepted", includeProperties: "Room")
                                                .GroupBy(x => x.RoomId)
                                                .Select(group => new RoomRankViewModel
                                                {
                                                    Id = group.Key,
                                                    Count = group.Count()
                                                })
                                                ).ToList();

            res.Sort((x, y) => y.Count.CompareTo(x.Count));
            res = res.Take(5).ToList();
            List<RoomSpecificationDto> rooms = new List<RoomSpecificationDto>();
            foreach (var x in res)
                rooms.Add(getRoomDetails(x.Id));
            return rooms;
        }

        public List<ApplicationUserDto> GetTopUsers()
        {
            var res = (DbRepo.GetAll(x => x.Status == "Accepted", includeProperties: "ApplicationUser")
                                                .GroupBy(x => x.UserId)
                                                .Select(group => new TopUserViewModel
                                                {
                                                    Id = group.Key,
                                                    Count = group.Count()
                                                })
                                                ).ToList();

            res.Sort((x, y) => y.Count.CompareTo(x.Count));
            res = res.Take(5).ToList();
            List<ApplicationUserDto> users = new List<ApplicationUserDto>();
            foreach (var r in res)
            {
                var us = DbRepo.GetAll(x => x.UserId == r.Id, includeProperties: "ApplicationUser")
                               .Select(x => x.ApplicationUser)
                               .ToList()[0];

                var hh = Mapper.Map<ApplicationUserDto>(us);
                users.Add(hh);
            }
            return users;
        }
        public List<ServiceViewModel> GetMostRequestedServices()
        {
            var res = (serviceCategoryBookingRepo.GetAll(x => x.Booking.Status == "Accepted", includeProperties: "Booking,ServiceCategory")
                                                .GroupBy(x => x.ServiceCategory.ServiceId)
                                                .Select(group => new TopServiceViewModel
                                                {
                                                    Id = group.Key,
                                                    Count = group.Count()
                                                })
                                                ).ToList();

            List<ServiceViewModel> services = new List<ServiceViewModel>();
            foreach (var x in res)
            {
                services.Add(new ServiceViewModel()
                {
                    Name = GetServiceDetails(x.Id).Name,
                    Count = x.Count
                });
            }
            return services;
        }

        public void UpdateBill(int bookid)
        {
            float newbill = RecalculateBill(bookid);
            var book = GetAllBookings(x => x.Id == bookid).ToList()[0];
            book.Bill = newbill;
            Update(book);
        }
        public List<BookingDto> GetBookingforuser(string id)
        {
            return Mapper.Map<List<BookingDto>>(DbRepo.GetAll(x => x.UserId == id 
                                                                && x.Status == "Accepted"
                                                                && x.FromDate <= DateTime.Now
                                                                && x.ToDate >= DateTime.Now, 
                                                              x => x.OrderByDescending(s => s.FromDate),
                                                              includeProperties: "Room"));
        }

        public List<TopServiceUserViewModel> GetTopUsersForService(int id)
        {
            var res = (serviceCategoryBookingRepo.GetAll(x => x.Booking.Status == "Accepted" && x.ServiceCategory.ServiceId == id, includeProperties: "Booking,ServiceCategory")
                                                 .GroupBy(x => x.Booking.UserId)
                                                 .Select(group => new TopUserViewModel
                                                 {
                                                      Id = group.Key,
                                                     Count = group.Count()
                                                 })
                                                 ).ToList();

            res = res.Take(10).ToList();
            List<TopServiceUserViewModel> users = new List<TopServiceUserViewModel>();

            foreach (var r in res)
            {
                var us = DbRepo.GetAll(x => x.UserId == r.Id, includeProperties: "ApplicationUser")
                               .Select(x => x.ApplicationUser)
                               .ToList()[0];
                users.Add(new TopServiceUserViewModel()
                {
                    Name = us.FirstName + " " + us.LastName,
                    Count = r.Count
                });
            }

            return users;
        }
    }
}
