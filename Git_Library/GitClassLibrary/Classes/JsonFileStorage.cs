using GitClassLibrary.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitClassLibrary.Classes
{
    public class JsonFileStorage : IDataStorage
    {
        private readonly string roomsFile;
        private readonly string bookingsFile;

        // Конструктор приймає конкретні файлові шляхи
        public JsonFileStorage(string roomsFile, string bookingsFile)
        {
            this.roomsFile = roomsFile;
            this.bookingsFile = bookingsFile;
        }

        public List<INumber> LoadRooms()
        {
            // Використовуємо статичний клас JsonStorage
            var loadedRooms = JsonStorage.LoadFromJson<List<Room>>(roomsFile);
            // Використання оператора 'as' може бути чистішим для уникнення помилок
            return loadedRooms?.Cast<INumber>().ToList() ?? new List<INumber>();
        }

        public List<Booking> LoadBookings()
        {
            // Використовуємо статичний клас JsonStorage
            return JsonStorage.LoadFromJson<List<Booking>>(bookingsFile) ?? new List<Booking>();
        }

        public void SaveData(List<INumber> rooms, List<Booking> bookings)
        {
            // Використовуємо статичний клас JsonStorage
            JsonStorage.SaveToJson(rooms, roomsFile);
            JsonStorage.SaveToJson(bookings, bookingsFile);
        }
    }
}
