using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace DBConnection.Repos
{
    public class HotelRepo : BaseRepo<Hotel>, IHotelRepo
    {
        public HotelRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}
