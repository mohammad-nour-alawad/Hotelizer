using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class BookingDto
    {
        
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
        //[Remote(action: "VerifyBookingDate", controller: "Booking", AdditionalFields = "ToDate")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        //[Remote(action: "VerifyBookingDate", controller: "Booking", AdditionalFields = "FromDate")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public float Bill { get; set; }
        public virtual ApplicationUserDto ApplicationUser { get; set; }
        public virtual RoomDto Room { get; set; }

    }
}
