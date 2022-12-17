using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class RoomType:BaseEntity
    {



        public string TypeName { get; set; }

        public int GuestCapacity { get; set; }

        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
         
        public bool IsMiniBar { get; set; }

        public bool IsBalcony { get; set; }

        public bool IsAirConditioner { get; set; }
        public bool IsWifi { get; set; }
        public bool IsTv { get; set; }
        public bool IsHairDryer { get; set; }


        //Relational Properties

        public virtual List<Booking> Bookings { get; set; }

        public virtual List<Room> Rooms { get; set; }

        public virtual List<BookingManagement> BookingManagements { get; set; }



    }
}
