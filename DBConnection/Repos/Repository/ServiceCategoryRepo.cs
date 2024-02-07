using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace DBConnection.Repos
{
    public class ServiceCategoryRepo : BaseRepo<ServiceCategory>, IServiceCategoryRepo
    {
        public ServiceCategoryRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}
