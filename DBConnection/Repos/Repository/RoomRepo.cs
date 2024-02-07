using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace DBConnection.Repos
{
    public class RoomRepo : BaseRepo<Room>, IRoomRepo
    {
        public RoomRepo(fiveSeasonDBContext db) : base(db)
        {
        }


    }
}
