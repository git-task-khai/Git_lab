using GitClassLibrary.Classes;
using System;
using Xunit;

namespace Test
{
    public class BookingTests
    {
        [Fact]
        public void CalculateTotal_ShouldReturnCorrectPrice()
        {
            var room = new Room(1, "Deluxe", 200);
            var booking = new Booking(
                room,
                "Ivan",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 4)
            );

            var total = booking.CalculateTotal();

            Assert.Equal(600m, total);
        }

        [Fact]
        public void Overlaps_ReturnsTrue_WhenDatesOverlap()
        {
            var room = new Room(1, "Single", 100);
            var booking = new Booking(
                room, "Ivan",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 10)
            );

            bool result = booking.Overlaps(new DateTime(2025, 1, 5), new DateTime(2025, 1, 7));

            Assert.True(result);
        }

        [Fact]
        public void Overlaps_ReturnsFalse_WhenDatesDoNotOverlap()
        {
            var room = new Room(1, "Single", 100);
            var booking = new Booking(
                room, "Ivan",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 10)
            );

            bool result = booking.Overlaps(new DateTime(2025, 2, 1), new DateTime(2025, 2, 5));

            Assert.False(result);
        }

        [Fact]
        public void CancelBooking_SetsRoomAvailable()
        {
            var room = new Room(1, "Single", 150);
            var booking = new Booking(
                room, "Ivan",
                DateTime.Today,
                DateTime.Today.AddDays(3)
            );

            booking.CancelBooking();

            Assert.True(room.IsAvailable);
            Assert.Equal(BookingStatus.CheckedOut, booking.Status);
        }

        [Fact]
        public void CheckIn_ChangesStatus_AndMakesRoomUnavailable()
        {
            var room = new Room(1, "Single", 150);
            var booking = new Booking(room, "Ivan", DateTime.Today, DateTime.Today.AddDays(3));

            booking.CheckIn();

            Assert.Equal(BookingStatus.CheckedIn, booking.Status);
            Assert.False(room.IsAvailable);
        }
    }
}