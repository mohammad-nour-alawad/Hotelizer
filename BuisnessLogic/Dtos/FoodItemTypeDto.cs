using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BuisnessLogic.Dtos
{
    public partial class FoodItemTypeDto
    {
        public FoodItemTypeDto()
        {
            FoodItem = new HashSet<FoodItemDto>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<FoodItemDto> FoodItem { get; set; }
    }
}
