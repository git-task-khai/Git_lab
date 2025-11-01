using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcGit.interfaces
{
    public interface IBooking
    {
        void BookRoom(INumber room, int nights);
        void CancelBooking();
        void ChangeBooking(INumber newRoom, int nights);
        decimal CalculateTotal();
    }
}
