using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Room:BaseEntity
    {

        public Room()
        {
            BookingManagements = new List<BookingManagement>();
        }
    
        public string RoomNumber { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public int FloorNumber { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public int RoomTypeID { get; set; }

        //Relational Properties

        public virtual RoomType RoomType { get; set; }
        public virtual List<BookingManagement> BookingManagements { get; set; }






    }
}
