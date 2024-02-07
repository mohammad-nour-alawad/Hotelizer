using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Repos
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        public OrderRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}