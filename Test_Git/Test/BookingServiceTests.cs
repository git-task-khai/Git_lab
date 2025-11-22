using GitClassLibrary.Classes;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test
{
    public class BookingServiceTests
    {
        [Fact]
        public void RoomIsUnavailable_WhenOverlapExists()
        {
            var room = new Room(1, "Single", 100);
            var bookings = new List<Booking>
        {
            new Booking(room, "Ivan", new DateTime(2025,1,1), new DateTime(2025,1,10))
        };

            var service = new BookingService();

            bool available = service.IsRoomAvailable(
                bookings,
                1,
                new DateTime(2025, 1, 5),
                new DateTime(2025, 1, 6)
            );

            Assert.False(available);
        }

        [Fact]
        public void RoomIsAvailable_WhenNoOverlap()
        {
            var room = new Room(1, "Single", 100);
            var bookings = new List<Booking>
        {
            new Booking(room, "Ivan", new DateTime(2025,1,1), new DateTime(2025,1,10))
        };

            var service = new BookingService();

            bool available = service.IsRoomAvailable(
                bookings,
                1,
                new DateTime(2025, 2, 1),
                new DateTime(2025, 2, 5)
            );

            Assert.True(available);
        }
    }
}
