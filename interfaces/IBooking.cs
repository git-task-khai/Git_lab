using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcGit.interfaces
{
    public interface IBooking
    {
        INumber Room { get; }
        string GuestName { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }

        decimal CalculateTotal();
        bool Overlaps(DateTime start, DateTime end);
    }
}
