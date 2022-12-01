using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminVMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{

    public class RoomTypeController : Controller
    {
        RoomTypeRepository _rtRep;

        public RoomTypeController()
        {
            _rtRep= new RoomTypeRepository();
        }

        public ActionResult RoomTypeList(int? id)
        {
            RoomTypeVM rtvm = id == null ? new RoomTypeVM
            {
                RoomTypes = _rtRep.GetActives()
            } : new RoomTypeVM { RoomTypes = _rtRep.Where(x => x.ID == id) };

            return View(rtvm);



        }

        public ActionResult AddRoomType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRoomType(RoomType roomType)
        {
            _rtRep.Add(roomType);
            return RedirectToAction("RoomTypeList");
        }

        public ActionResult UpdateRoomType(int id)
        {
            RoomTypeVM rtvm = new RoomTypeVM { RoomType = _rtRep.Find(id) };
            return View(rtvm);
        }

        [HttpPost]
        public ActionResult UpdateRoomType(RoomType roomType)
        {
            _rtRep.Update(roomType);
            return RedirectToAction("RoomTypeList");
        }

        public ActionResult DeleteRoomType(int id)
        {
            _rtRep.Delete(_rtRep.Find(id));
            return RedirectToAction("RoomTypeList");
        }


    }
}