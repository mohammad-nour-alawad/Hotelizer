using DBConnection.Models;
using DBConnection.Repos;
using System.Threading.Tasks;

namespace DBConnection.UnitOfWork
{
    public interface IUnitOfWork
    {

        public fiveSeasonDBContext context { get; }
        public IRoomRepo RoomRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
        public ISpecificationRepo SpecificationRepo { get; }
        public IServiceRepo ServiceRepo { get; }
        public IServiceCategoryRepo ServiceCategoryRepo { get; }
        public IHotelRepo HotelRepo { get; }
        public IRoomSpecificationRepo RoomSpecificationRepo { get; }
        public IBookingRepo BookingRepo { get; }
        public IUserRepo UserRepo { get; }
        public IFoodItemRepo FoodItemRepo { get; }
        public IFoodItemTypeRepo FoodItemTypeRepo { get; }
        public IOrderRepo OrderRepo { get; }
        public IServiceCategoryBookingRepo ServiceCategoryBookingRepo { get; }

        public void Save();
        public Task<int> SaveAsync();
        public void Dispose();
    }
}