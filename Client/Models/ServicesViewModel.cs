using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class ServicesViewModel
    {
        public int bookid { get; set; }
        public string todelete { get; set; }
        public string tocreate { get; set; }

    }
}
