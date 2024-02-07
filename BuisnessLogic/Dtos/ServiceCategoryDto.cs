using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class ServiceCategoryDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
        public float Cost { get; set; }

        public virtual CatergoryDto Category { get; set; }
        public virtual ServiceDto Service { get; set; }
    }
}
