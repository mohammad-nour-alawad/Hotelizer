using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class RoomSpecificationDto
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public int SpecId { get; set; }

        public virtual RoomDto Room { get; set; }
        public virtual SpecificationDto Spec { get; set; }
    }
}
