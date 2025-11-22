using GitClassLibrary.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitClassLibrary.Classes
{
    public class HotelManagement : IHotelManagement
    {
        private readonly IDataStorage storage;
        private List<INumber> rooms;
        private List<Booking> bookings;

        public HotelManagement(IDataStorage dataStorage)
        {
            this.storage = dataStorage;

            rooms = storage.LoadRooms();
            bookings = storage.LoadBookings();
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

            booking.CancelBooking();
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
                                  $"з {b.StartDate:dd.MM.yyyy} до {b.EndDate:dd.MM.yyyy}, сума: {b.CalculateTotal():C}");
            }
            Console.WriteLine("---------------------------\n");
        }

        private void SaveData()
        {
            // Використовуємо ін'єктований об'єкт сховища.
            storage.SaveData(rooms, bookings);
        }

        public void CheckInGuest()
        {
            Console.Write("Заселення по броні? (так/ні): ");
            string choice = Console.ReadLine().ToLower();

            if (choice == "так")
            {
                Console.Write("ID номера: ");
                if (!int.TryParse(Console.ReadLine(), out int roomId)) return;

                var booking = bookings.FirstOrDefault(b => b.Room.Id == roomId &&
                                                           b.StartDate <= DateTime.Today &&
                                                           b.EndDate >= DateTime.Today &&
                                                           b.Status == BookingStatus.Reserved);

                if (booking == null)
                {
                    Console.WriteLine("Бронь на сьогодні не знайдено!");
                    return;
                }

                Console.Write($"Підтвердити заселення гостя {booking.GuestName}? (так/ні): ");
                if (Console.ReadLine().ToLower() == "так")
                {
                    booking.CheckIn();
                    SaveData();
                    Console.WriteLine($"Гість {booking.GuestName} заселений в номер {booking.Room.Id}");
                }
            }
            else
            {
                Console.Write("Введіть ID номера: ");
                if (!int.TryParse(Console.ReadLine(), out int roomId)) return;

                var room = rooms.FirstOrDefault(r => r.Id == roomId && r.IsAvailable);
                if (room == null)
                {
                    Console.WriteLine("Номер недоступний!");
                    return;
                }

                Console.Write("Ім’я гостя: ");
                string guest = Console.ReadLine();
                Console.Write("Дата виїзду (рррр-мм-дд): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime end)) return;

                var booking = new Booking(room, guest, DateTime.Today, end);
                booking.CheckIn();
                bookings.Add(booking);
                SaveData();

                Console.WriteLine($"Гість {guest} заселений в номер {room.Id} з сьогодні до {end:dd.MM.yyyy}");
            }
        }

        public void CheckOutGuest()
        {
            var todayGuests = bookings.Where(b => b.Status == BookingStatus.CheckedIn &&
                                                   b.EndDate >= DateTime.Today).ToList();
            if (!todayGuests.Any())
            {
                Console.WriteLine("Сьогодні немає заселених гостей, які заселені.");
                return;
            }

            Console.WriteLine("Заселені номери:");
            foreach (var b in todayGuests)
                Console.WriteLine($"{b.Room.Id}: {b.GuestName}");

            Console.Write("Введіть ID номера для виселення: ");
            if (!int.TryParse(Console.ReadLine(), out int roomId)) return;

            var booking = todayGuests.FirstOrDefault(b => b.Room.Id == roomId);
            if (booking == null)
            {
                Console.WriteLine("Номер не знайдено серед заселених.");
                return;
            }

            Console.Write($"Підтвердити виселення гостя {booking.GuestName}? (так/ні): ");
            if (Console.ReadLine().ToLower() == "так")
            {
                booking.CheckOut();
                SaveData();
                Console.WriteLine($"Гість {booking.GuestName} виселений. Сума до сплати: {booking.CalculateTotal():C}");
            }
        }
    }
}
