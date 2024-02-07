using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace DBConnection.Repos
{
    public class ServiceCategoryBookingRepo : BaseRepo<ServiceCategoryBooking>, IServiceCategoryBookingRepo
    {
        public ServiceCategoryBookingRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}
