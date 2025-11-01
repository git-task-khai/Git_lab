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
        private INumber room;
        private int nights;
        private bool isActive;

        public Booking(INumber room, int nights)
        {
            BookRoom(room, nights);
        }

        public void BookRoom(INumber room, int nights)
        {
            this.room = room;
            this.nights = nights;
            this.room.SetAvailability(false);
            isActive = true;
            Console.WriteLine($"Номер {this.room.Id} забронирован на {this.nights} ночей.");
        }

        public void CancelBooking()
        {
            if (!this.isActive) return;
            this.room.SetAvailability(true);
            this.isActive = false;
            Console.WriteLine($"Бронь на номер {this.room.Id} отменена.");
        }

        public void ChangeBooking(INumber newRoom, int nights)
        {
            CancelBooking();
            BookRoom(newRoom, nights);
        }

        public decimal CalculateTotal()
        {
            return this.room.PricePerNight * this.nights;
        }
    }
}
