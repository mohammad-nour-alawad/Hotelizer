using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Repos
{
    public class FoodItemRepo : BaseRepo<FoodItem>, IFoodItemRepo
    {
        public FoodItemRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}