using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class HotelDto
    {
        public HotelDto()
        {
            Room = new HashSet<RoomDto>();
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public string Location { get; set; }

        public virtual ICollection<RoomDto> Room { get; set; }
    }
}
