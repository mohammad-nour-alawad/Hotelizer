using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBConnection.Models
{
    public partial class FoodItem
    {
        public int Id { get; set; }
        public int? FoodItemTypeId { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile Imagefile { get; set; }

        public virtual FoodItemType FoodItemType { get; set; }
    }
}
