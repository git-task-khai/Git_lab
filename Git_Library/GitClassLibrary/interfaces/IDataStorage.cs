using GitClassLibrary.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitClassLibrary.interfaces
{
    public interface IDataStorage
    {
        List<INumber> LoadRooms();
        List<Booking> LoadBookings();
        void SaveData(List<INumber> rooms, List<Booking> bookings);
    }
}
