using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcGit.interfaces
{
    public interface IHotelManagement
    {
        void AddRoom(INumber room);
        void CheckIn(string guestName, INumber room, int nights);
        void CheckOut(string guestName);
        void ShowReport();
    }
}
