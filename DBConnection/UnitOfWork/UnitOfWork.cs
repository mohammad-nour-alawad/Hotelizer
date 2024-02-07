using System;
using System.Threading.Tasks;
using DBConnection.Models;
using DBConnection.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DBConnection.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public fiveSeasonDBContext context { get; }
        public IRoomRepo RoomRepo { get; }
        public ICategoryRepo CategoryRepo { get; set; }
        public ISpecificationRepo SpecificationRepo{ get; }
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

        public UnitOfWork(fiveSeasonDBContext db )
        {
            this.context = db;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            RoomRepo = new RoomRepo(context);
            CategoryRepo = new CategoryRepo(context);
            SpecificationRepo = new SpecificationRepo(context);
            ServiceRepo = new ServiceRepo(context);
            ServiceCategoryRepo = new ServiceCategoryRepo(context);
            HotelRepo = new HotelRepo(context);
            RoomSpecificationRepo = new RoomSpecificationRepo(context);
            BookingRepo = new BookingRepo(context);
            UserRepo = new UserRepo(context);
            FoodItemRepo = new FoodItemRepo(context);
            FoodItemTypeRepo = new FoodItemTypeRepo(context);
            OrderRepo = new OrderRepo(context);
            ServiceCategoryBookingRepo = new ServiceCategoryBookingRepo(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}