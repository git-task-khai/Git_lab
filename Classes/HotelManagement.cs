using ProjetcGit.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcGit.Classes
{
    public class HotelManagement : IHotelManagement
    {
        private List<INumber> rooms;
        private Dictionary<string, IBooking> bookings;

        public void AddRoom(INumber room)
        {
            this.rooms.Add(room);
            Console.WriteLine($"Добавлен номер {room.Id}");
        }

        public void CheckIn(string guestName, INumber room, int nights)
        {
            if (!room.IsAvailable)
            {
                Console.WriteLine("Номер недоступен!");
                return;
            }

            var booking = new Booking(room, nights);
            this.bookings[guestName] = booking;
            Console.WriteLine($"{guestName} заселён в номер {room.Id}");
        }

        public void CheckOut(string guestName)
        {
            if (this.bookings.TryGetValue(guestName, out var booking))
            {
                booking.CancelBooking();
                Console.WriteLine($"{guestName} выселен. Сумма к оплате: {booking.CalculateTotal():C}");
                this.bookings.Remove(guestName);
            }
            else
            {
                Console.WriteLine("Гость не найден!");
            }
        }

        public void ShowReport()
        {
            Console.WriteLine("\n--- Отчёт о номерах ---");
            foreach (var room in this.rooms)
                room.ShowInfo();
            Console.WriteLine("------------------------\n");
        }
    }
}
