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
        public RoomType RoomType { get; set; }

        public List<RoomType> RoomTypes { get; set; }
        public IPagedList<RoomType> PagedRoomTypes { get; set; }






    }
}