using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace DBConnection.Repos
{
    public class RoomSpecificationRepo : BaseRepo<RoomSpecification>, IRoomSpecificationRepo
    {
        public RoomSpecificationRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}
