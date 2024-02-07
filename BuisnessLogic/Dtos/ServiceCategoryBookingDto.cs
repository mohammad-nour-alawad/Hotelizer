using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class ServiceCategoryBookingDto
    {
        public int Id { get; set; }
        public int ServiceCategoryId { get; set; }
        public int BookingId { get; set; }

        public virtual BookingDto Booking { get; set; }
        public bool isConsumed { get; set; }
        public virtual ServiceCategoryDto ServiceCategory { get; set; }
    }
}
