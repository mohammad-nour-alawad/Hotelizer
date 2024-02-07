using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class RoomDto
    {
        public RoomDto()
        {
            Booking = new HashSet<BookingDto>();
        }

        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public int HotelId { get; set; }

        public string ImageUrl { get; set; }
        public virtual CatergoryDto Category { get; set; }
        public virtual HotelDto Hotel { get; set; }
        public virtual ICollection<BookingDto> Booking { get; set; }
    }
}
