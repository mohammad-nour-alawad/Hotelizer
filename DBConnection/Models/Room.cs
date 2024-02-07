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
    public partial class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        [Required]
        [DisplayName("Room Number")]
        public int FloorNumber { get; set; }
        [Required]
        [DisplayName("Status")]
        public string Status { get; set; }
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Hotel")]
        public int HotelId { get; set; }

        public string ImageUrl { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile Imagefile { get; set; }
        public virtual Catergory Category { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
