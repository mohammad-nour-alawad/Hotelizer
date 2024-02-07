using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace DBConnection.Repos
{
    public class SpecificationRepo : BaseRepo<Specification>, ISpecificationRepo
    {
        public SpecificationRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}
