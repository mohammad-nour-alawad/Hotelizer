using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Client.Models
{
    public class BookViewModel : IValidatableObject
    {
        public int RoomId { get; set; } = 1;
        public string UserId { get; set; }

        [Required(ErrorMessage = "Start date cannot be empty")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "End date cannot be empty")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(1).Date;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (FromDate < DateTime.Now.Date)
            {
                results.Add(new ValidationResult("Start date and time must be greater than current time", new[] { "FromDate" }));
            }

            if (ToDate.Date <= FromDate.Date)
            {
                results.Add(new ValidationResult("EndDateTime must be greater that StartDateTime", new[] { "ToDate" }));
            }

            if( RoomId == 0)
            {
                results.Add(new ValidationResult("Select a Room", new[] { "RoomId" }));
            }

            return results;
        }
        public string Status { get; set; }
        public float Bill { get; set; }
        public string services { get; set; }

        public int categoryID { get; set; }
        public int specificationID { get; set; }



    }
}
