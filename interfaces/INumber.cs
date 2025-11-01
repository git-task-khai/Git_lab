using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcGit.interfaces
{
    public interface INumber
    {
        int Id { get; }
        string Type { get; }
        decimal PricePerNight { get; }
        bool IsAvailable { get; }

        void ShowInfo();
        void SetAvailability(bool available);
    }
}
