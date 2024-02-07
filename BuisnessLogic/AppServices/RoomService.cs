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
using BuisnessLogic.ViewModels;
using System.Threading.Tasks;
using AutoMapper;

namespace BuisnessLogic.AppServices
{
    public class RoomService : BaseService.BaseService<
         RoomSpecificationDto, IRoomSpecificationRepo, RoomSpecification>
    {
        public RoomService(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork,
                httpContextAccessor)
        {

        }


        public override IRoomSpecificationRepo DbRepo => UnitOfWork.RoomSpecificationRepo;
        public IRoomRepo roomRepo => UnitOfWork.RoomRepo;
        public ICategoryRepo categoryRepo => UnitOfWork.CategoryRepo;
        public IServiceCategoryRepo serviceCategoryRepo => UnitOfWork.ServiceCategoryRepo;
        public ISpecificationRepo specificationRepo => UnitOfWork.SpecificationRepo;
        public IServiceRepo serviceRepo => UnitOfWork.ServiceRepo;




        public override RoomSpecificationDto Create(RoomSpecificationDto dto)
        {
            return base.Create(dto);
        }

        public override RoomSpecificationDto Update(RoomSpecificationDto dto)
        {
            return base.Update(dto);
        }

        public RoomSpecificationDto getRoomDetails(int id)
        {
            List<RoomSpecificationDto> rooms = Mapper.Map<List<RoomSpecificationDto>>(DbRepo.GetAll(x => x.RoomId == id, includeProperties: "Spec,Room,Room.Category,Room.Category.ServiceCategory,Room.Category.ServiceCategory.Service"));
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
        
        public List<RoomSpecificationDto> getSimilarRooms(RoomSpecificationDto room)
        {
            var Similar_rooms = GetAllRooms(m => (room.Spec.Type.Contains(m.Spec.Type)
                                               || m.Room.Category.Name == room.Room.Category.Name)
                                               && m.Room.Id != room.RoomId );

            return Similar_rooms.OrderBy(x=> x.Room.FloorNumber).ToList();
            
        }
        

        public List<RoomSpecificationDto> GetAll()
        {
            return Mapper.Map<List<RoomSpecificationDto>>(DbRepo.GetAll());
        }

        public List<RoomSpecificationDto> AvailablePeriod(DateTime start, DateTime end, int category_id, int specification_id)
        {
            List<RoomSpecificationDto> allrooms = GetAllRooms( m=> m.Room.CategoryId == category_id && m.SpecId == specification_id  );
            List<RoomSpecificationDto> allavailablerooms = new List<RoomSpecificationDto>();
            if (allrooms.Count != 0)
            {
                foreach (var room in allrooms)
                {
                    List<BookingDto> allbookings = new List<BookingDto>();
                    allbookings = room.Room.Booking.Where(x => x.Status == "Accepted").ToList();
                    bool noConflict = true;
                        foreach (var bo in allbookings)
                        {
                            DateTime fromP = bo.FromDate;
                            DateTime toP = bo.ToDate;   
                            if(  (start >= fromP && start <= toP ) || (end >= fromP && end <= toP) || ((fromP >= start && toP <=  end)))
                            {
                              noConflict = false;
                            }
                        }

                        if (noConflict)
                        {
                            allavailablerooms.Add(room);
                        }
                }
            }
            return allavailablerooms;
        }

        public List<ServiceCategoryDto> GetAllServices(Expression<Func<ServiceCategory, bool>> condition = null, Func<IQueryable<ServiceCategory>, IOrderedQueryable<ServiceCategory>> orderBy = null)
        {
            List<ServiceCategoryDto> services = Mapper.Map<List<ServiceCategoryDto>>(serviceCategoryRepo.GetAll(condition, orderBy, includeProperties: "Category,Service"));
            return services;
        }

        public List<ServiceDto> GetServices(Expression<Func<Service, bool>> condition = null, Func<IQueryable<Service>, IOrderedQueryable<Service>> orderBy = null)
        {
            List<ServiceDto> services = Mapper.Map<List<ServiceDto>>(serviceRepo.GetAll(condition, orderBy));
            return services;
        }

        public List<CatergoryDto> GetAllCategories(Expression<Func<Catergory, bool>> condition = null, Func<IQueryable<Catergory>, IOrderedQueryable<Catergory>> orderBy = null, string includeProperties = "")
        {

            List<CatergoryDto> categories = Mapper.Map<List<CatergoryDto>>(categoryRepo.GetAll(condition, orderBy, includeProperties));
            return categories;
        }
        public List<SpecificationDto> GetAllSpecifications(Expression<Func<Specification, bool>> condition = null, Func<IQueryable<Specification>, IOrderedQueryable<Specification>> orderBy = null, string includeProperties = "")
        {

            List<SpecificationDto> specifications = Mapper.Map<List<SpecificationDto>>(specificationRepo.GetAll(condition, orderBy, includeProperties));
            return specifications;
        }
        public List<RoomSpecificationDto> GetAllRooms(Expression<Func<RoomSpecification, bool>> condition = null, Func<IQueryable<RoomSpecification>, IOrderedQueryable<RoomSpecification>> orderBy = null)
        {
            List<RoomSpecificationDto> rooms = Mapper.Map<List<RoomSpecificationDto>>(DbRepo.GetAll(condition, orderBy, includeProperties: "Spec,Room,Room.Category,Room.Category.ServiceCategory,Room.Category.ServiceCategory.Service,Room.Booking"));
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
            return roomid_spec.Values.ToList();

        }

        public List<RoomSpecificationDto> SearchRooms(string searchValue, int floornumber)
        {
            var rooms = GetAllRooms(m => m.Spec.Type.ToLower().Contains(searchValue.ToLower())
                                                || m.Room.Category.Name.ToLower().Contains(searchValue.ToLower())
                                                || m.Room.FloorNumber == floornumber);

            return rooms;
        }
    }
}
