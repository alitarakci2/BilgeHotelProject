using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Areas.Admin.AdminVMClasses
{
    public class RoomVM
    {
        public List<Room> Rooms { get; set; }
        public Room Room { get; set; }
        public List<RoomType> RoomTypes { get; set; }



    }
}