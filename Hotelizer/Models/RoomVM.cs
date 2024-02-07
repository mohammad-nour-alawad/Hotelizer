using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotelizer.Models
{
    public class RoomVM
    {
        public List<int> HotelIds { get; set; }
        public List<int> CategoryIds { get; set; } 
    }
}
