using GitClassLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class BookingService
    {
        public bool IsRoomAvailable(List<Booking> bookings, int roomId, DateTime start, DateTime end)
        {
            return !bookings.Any(b =>
                b.Room.Id == roomId &&
                b.Overlaps(start, end)
            );
        }
    }
}