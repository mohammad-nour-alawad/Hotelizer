using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DBConnection.Models
{
    public partial class Catergory
    {
        public Catergory()
        {
            Room = new HashSet<Room>();
            ServiceCategory = new HashSet<ServiceCategory>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public float BasePrice { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile Imagefile { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<ServiceCategory> ServiceCategory { get; set; }
    }
}
