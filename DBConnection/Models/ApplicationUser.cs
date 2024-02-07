using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DBConnection.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Booking = new HashSet<Booking>();
        }
        [Required]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        public string LastName { get; set; }
        [NotMapped]
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public override string Email { get; set; }



        public virtual ICollection<Booking> Booking { get; set; }
    }
}
