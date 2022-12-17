using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Booking:BaseEntity
    {
        public int DurationInDays { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public int GuestCount { get; set; }
        public double Payment { get; set; }

        public bool IsPaid { get; set; }


        public int? AppUserID { get; set; }
        public int? RoomTypeID { get; set; }

        public decimal TotalPrice { get; set; }

        //Relational Properties

        public virtual AppUser Customer { get; set; }
        public virtual RoomType RoomType { get; set; }

        public virtual List<BookingManagement> BookingManagement { get; set; }



    }
}
