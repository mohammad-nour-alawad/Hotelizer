using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBConnection.Models
{
    public partial class FoodItemType
    {
        public FoodItemType()
        {
            FoodItem = new HashSet<FoodItem>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<FoodItem> FoodItem { get; set; }
    }
}
