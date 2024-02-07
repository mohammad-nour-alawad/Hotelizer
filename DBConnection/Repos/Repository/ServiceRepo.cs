using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace DBConnection.Repos
{
    public class ServiceRepo : BaseRepo<Service>, IServiceRepo
    {
        public ServiceRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}
