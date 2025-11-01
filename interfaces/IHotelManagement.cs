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
        void ShowReport();

        void BookRoom(string guestName, int roomId, DateTime start, DateTime end);
        void CancelBooking(string guestName);
        void ShowBookings();
        void CheckInGuest();
        void CheckOutGuest();
    }
}
