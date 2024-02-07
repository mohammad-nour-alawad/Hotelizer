using DBConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnection.Repos
{
    public class BookingRepo : BaseRepo<Booking>,IBookingRepo
    {
        public BookingRepo(fiveSeasonDBContext db) : base(db)
        {
        }
    }
}