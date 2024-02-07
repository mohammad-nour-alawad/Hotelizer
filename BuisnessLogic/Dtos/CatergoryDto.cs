using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class CatergoryDto
    {
        public CatergoryDto()
        {
            Room = new HashSet<RoomDto>();
            ServiceCategory = new HashSet<ServiceCategoryDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public float BasePrice { get; set; }
        public virtual ICollection<RoomDto> Room { get; set; }
        public virtual ICollection<ServiceCategoryDto> ServiceCategory { get; set; }
    }
}
