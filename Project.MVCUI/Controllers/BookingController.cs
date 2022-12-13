using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class BookingController : Controller
    {
        BookingRepository _bRep;
        RoomTypeRepository _rtRep;
        RoomRepository _rRep;
        BookingManagementRepository _bmRep;


        public BookingController()
        {
            _bRep = new BookingRepository();
            _rtRep = new RoomTypeRepository();
            _rRep = new RoomRepository();
            _bmRep = new BookingManagementRepository();

        }





        public ActionResult BookingList(int? page, int? roomTypeID)
        {
            PaginationVM pavm = new PaginationVM
            {
                //PagedRooms = roomTypeID == null ? _rRep.GetActives().ToPagedList(page ?? 1, 9) : _rRep.Where(x => x.RoomTypeID == roomTypeID).ToPagedList(page ?? 1, 9),
                RoomTypes = _rtRep.GetActives(),
                PagedRoomTypes = roomTypeID == null ? _rtRep.GetActives().ToPagedList(page ?? 1, 9) : _rtRep.Where(x => x.ID == roomTypeID).ToPagedList(page ?? 1, 9)


            };
            if (roomTypeID != null) TempData["rtID"] = roomTypeID;


            return View(pavm);
        }
    }
}