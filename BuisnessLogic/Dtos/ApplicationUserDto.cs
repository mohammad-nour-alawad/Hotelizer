using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.Dtos
{
    public class ApplicationUserDto : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
