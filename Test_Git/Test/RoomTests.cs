using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitClassLibrary.Classes;
using Xunit;

namespace Test
{
    public class RoomTests
    {
        [Fact]
        public void Room_IsAvailable_ShouldBeTrue_ByDefault()
        {
            var room = new Room(1, "Single", 100m);

            Assert.True(room.IsAvailable);
        }

        [Fact]
        public void SetAvailability_ChangesStatus()
        {
            var room = new Room(1, "Single", 100m);

            room.SetAvailability(false);

            Assert.False(room.IsAvailable);
        }
    }
}
