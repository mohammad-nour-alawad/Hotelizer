using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBConnection.Models
{
    public partial class Booking
    {

        public int Id { get; set; }

        [DisplayName("Room Id")]
        public int RoomId { get; set; }
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("From Date")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DisplayName("To Date")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Bill")]
        public float Bill { get; set; }
        [DisplayName("User")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [DisplayName("Room")]
        public virtual Room Room { get; set; }
    
    }
}
