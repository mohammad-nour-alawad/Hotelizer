using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Repos
{
    public class CategoryRepo : BaseRepo<Catergory>, ICategoryRepo
    {
        public CategoryRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}