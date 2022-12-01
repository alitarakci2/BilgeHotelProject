using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class BookingManagement:BaseEntity
    {
        public int BookingID { get; set; }
        public int RoomID { get; set; }

        public int IsCheckedOut { get; set; }

        public decimal? ExtraPayment { get; set; }



        //Relational Properties

        public virtual Room Room { get; set; }
        public virtual Booking Booking { get; set; }







    }
}
