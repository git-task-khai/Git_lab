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
        private List<Booking> bookings;

        private readonly string roomsFile = @"D:\university\Git_Project\bin\Debug\rooms.json";
        private readonly string bookingsFile = @"D:\university\Git_Project\bin\Debug\bookings.json";

        public HotelManagement()
        {
            var loadedRooms = JsonStorage.LoadFromJson<List<Room>>(roomsFile);
            rooms = loadedRooms?.Cast<INumber>().ToList() ?? new List<INumber>();
            
            bookings = JsonStorage.LoadFromJson<List<Booking>>(bookingsFile) ?? new List<Booking>();
        }

        public void AddRoom(INumber room)
        {
            rooms.Add(room);
            SaveData();
            Console.WriteLine($"Додано номер {room.Id}");
        }

        public void ShowReport()
        {
            Console.WriteLine("\n--- Звіт про всі номери ---");
            foreach (var room in rooms)
                room.ShowInfo();
            Console.WriteLine("----------------------------\n");
        }

        public void BookRoom(string guestName, int roomId, DateTime start, DateTime end)
        {
            var room = rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                Console.WriteLine("Номер не знайдено!");
                return;
            }

            bool isAvailable = !bookings.Any(b =>
                b.Room.Id == roomId &&
                ((start >= b.StartDate && start < b.EndDate) ||
                 (end > b.StartDate && end <= b.EndDate) ||
                 (start <= b.StartDate && end >= b.EndDate)));

            if (!isAvailable)
            {
                Console.WriteLine("Цей номер вже заброньований на вибрані дати!");
                return;
            }

            var booking = new Booking(room, guestName, start, end);
            bookings.Add(booking);
            SaveData();

            Console.WriteLine($"{guestName} забронював номер {roomId} з {start:dd.MM.yyyy} до {end:dd.MM.yyyy}");
        }

        public void CancelBooking(string guestName)
        {
            var booking = bookings.FirstOrDefault(b => b.GuestName.Equals(guestName, StringComparison.OrdinalIgnoreCase));
            if (booking == null)
            {
                Console.WriteLine("Бронювання не знайдено!");
                return;
            }

            bookings.Remove(booking);
            SaveData();
            Console.WriteLine($"Бронювання для {guestName} скасовано.");
        }

        public void ShowBookings()
        {
            Console.WriteLine("\n--- Активні бронювання ---");
            if (bookings.Count == 0)
            {
                Console.WriteLine("Немає активних бронювань.");
                return;
            }

            foreach (var b in bookings)
            {
                Console.WriteLine($"Гість: {b.GuestName}, Номер: {b.Room.Id} ({b.Room.Type}), " +
                                  $"з {b.StartDate:dd.MM.yyyy} до {b.EndDate:dd.MM.yyyy}, сума: {b.CalculateTotal()}");
            }
            Console.WriteLine("---------------------------\n");
        }

        private void SaveData()
        {
            JsonStorage.SaveToJson(rooms, roomsFile);
            JsonStorage.SaveToJson(bookings, bookingsFile);
        }
    }
}
