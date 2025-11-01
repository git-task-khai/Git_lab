using ProjetcGit.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcGit.Classes
{
    public class Room : INumber
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        public Room(int id, string type, decimal price)
        {
            Id = id;
            Type = type;
            PricePerNight = price;
            IsAvailable = true;
        }

        public Room()
        {

        }

        public void ShowInfo()
        {
            Console.WriteLine($"Номер {Id}: тип {Type}, ціна {PricePerNight}, доступний: {IsAvailable}");
        }

        public void SetAvailability(bool available)
        {
            IsAvailable = available;
        }
    }
}
