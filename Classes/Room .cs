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
        public int Id { get; private set; }
        public string Type { get; private set; }
        public decimal PricePerNight { get; private set; }
        public bool IsAvailable { get; private set; }

        public Room(int id, string type, decimal price)
        {
            Id = id;
            Type = type;
            PricePerNight = price;
            IsAvailable = true;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Номер {Id}: тип {Type}, цена {PricePerNight:C}, доступен: {IsAvailable}");
        }

        public void SetAvailability(bool available)
        {
            IsAvailable = available;
        }
    }
}
