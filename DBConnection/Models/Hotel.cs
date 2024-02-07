using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBConnection.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Room = new HashSet<Room>();
        }
        [Required]
        public string Name { get; set; }
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
