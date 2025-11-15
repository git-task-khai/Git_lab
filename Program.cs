using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using GitClassLibrary.interfaces;

namespace ProjetcGit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IHotelManagement hotel = new HotelManagement();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ГОТЕЛЕМ ===");
                Console.WriteLine("1. Додати номер");
                Console.WriteLine("2. Показати всі номери");
                Console.WriteLine("3. Забронювати номер");
                Console.WriteLine("4. Скасувати бронювання");
                Console.WriteLine("5. Показати всі бронювання");
                Console.WriteLine("6. Заселити гостя");
                Console.WriteLine("7. Виселити гостя");
                Console.WriteLine("8. Вийти");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddRoom(hotel);
                        break;

                    case "2":
                        hotel.ShowReport();
                        break;

                    case "3":
                        BookRoom(hotel);
                        break;

                    case "4":
                        CancelBooking(hotel);
                        break;

                    case "5":
                        hotel.ShowBookings();
                        break;

                    case "6":
                        hotel.CheckInGuest();
                        break;

                    case "7":
                        hotel.CheckOutGuest();
                        break;

                    case "8":
                        Console.WriteLine("Завершення роботи...");
                        return;

                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
                Console.ReadKey();
            }
        }

        static void AddRoom(IHotelManagement hotel)
        {
            Console.Write("Введіть ID номера: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введіть тип номера: ");
            string type = Console.ReadLine();

            Console.Write("Введіть ціну за ніч: ");
            decimal price = decimal.Parse(Console.ReadLine());

            hotel.AddRoom(new Room(id, type, price));
        }

        static void BookRoom(IHotelManagement hotel)
        {
            Console.Write("Ім’я гостя: ");
            string guest = Console.ReadLine();

            hotel.ShowReport();

            Console.Write("ID номера: ");
            int roomId = int.Parse(Console.ReadLine());

            Console.Write("Дата заїзду (рррр-мм-дд): ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            DateTime end;
            while (true)
            {
                Console.Write("Дата виїзду (рррр-мм-дд): ");
                end = DateTime.Parse(Console.ReadLine());

                if (end < start)
                {
                    Console.WriteLine("Дата виїзду не може бути раніше дати заїзду. Спробуйте ще раз.");
                }
                else
                {
                    break;
                }
            }

            hotel.BookRoom(guest, roomId, start, end);
        }

        static void CancelBooking(IHotelManagement hotel)
        {
            Console.Write("Введіть ім’я гостя для скасування броні: ");
            string guest = Console.ReadLine();

            hotel.CancelBooking(guest);
        }
    }
}
