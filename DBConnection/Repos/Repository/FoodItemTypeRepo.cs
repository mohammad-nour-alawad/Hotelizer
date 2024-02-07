using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Repos
{
    public class FoodItemTypeRepo : BaseRepo<FoodItemType>, IFoodItemTypeRepo
    {
        public FoodItemTypeRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}