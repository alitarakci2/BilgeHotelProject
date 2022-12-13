using PagedList;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class PaginationVM
    {
        public List<RoomType> RoomTypes { get; set; }

        public List<Room> Rooms { get; set; }
        public IPagedList<Room> PagedRooms { get; set; }
        public IPagedList<RoomType> PagedRoomTypes { get; set; }






    }
}