using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBConnection.Models
{
    public partial class RoomSpecification
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int SpecId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Specification Spec { get; set; }
    }
}
