using ProjetcGit.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcGit.Classes
{
    public class Booking : IBooking
    {
        public INumber Room { get; set; }
        public string GuestName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Booking() { }

        public Booking(INumber room, string guestName, DateTime start, DateTime end)
        {
            Room = room;
            GuestName = guestName;
            StartDate = start;
            EndDate = end;

            Room.SetAvailability(false);
        }

        public decimal CalculateTotal()
        {
            int nights = (EndDate - StartDate).Days;
            if (nights < 0) nights = 0;
            return nights * Room.PricePerNight;
        }

        public bool Overlaps(DateTime start, DateTime end)
        {
            return StartDate < end && start < EndDate;
        }

        public void CancelBooking()
        {
            Room.SetAvailability(true);
        }

        public override string ToString()
        {
            return $"Гість: {GuestName}, Номер: {Room.Id} ({Room.Type}), " +
                   $"{StartDate:dd.MM.yyyy} → {EndDate:dd.MM.yyyy}, Сума: {CalculateTotal()}";
        }
    }
}
