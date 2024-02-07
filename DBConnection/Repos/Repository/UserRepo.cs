using DBConnection.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnection.Repos
{
    public class UserRepo : BaseRepo<ApplicationUser>,IUserRepo
    {

        public UserRepo(fiveSeasonDBContext db ) : base(db)
        {
        }

    }
}