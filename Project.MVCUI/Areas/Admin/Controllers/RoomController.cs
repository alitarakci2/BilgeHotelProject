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
    public class RoomController : Controller
    {
        RoomRepository _rRep;
        RoomTypeRepository _rtRep;

        public RoomController()
        {
           _rRep= new RoomRepository();
            _rtRep= new RoomTypeRepository();
        }

        
        public ActionResult RoomList(int? id)
        {
            RoomVM rvm = new RoomVM
            {
                Rooms = id == null ? _rRep.GetAll() : _rRep.Where(x => x.RoomTypeID == id)
            };
            return View(rvm);
        }


        public ActionResult AddRoom()
        {
            RoomVM rvm = new RoomVM()
            {
                RoomTypes = _rtRep.GetActives()
            };

            return View(rvm);
        }


        [HttpPost]
        public ActionResult AddRoom(Room room)
        {
            _rRep.Add(room);
            return RedirectToAction("RoomList");
        }

        public ActionResult UpdateRoom(int id)
        {
            RoomVM rvm = new RoomVM
            {
                Room = _rRep.Find(id),
                RoomTypes = _rtRep.GetActives()
            };
            return View(rvm);
        }

        [HttpPost]
        public ActionResult UpdateRoom(Room room)
        {


            _rRep.Update(room);
            return RedirectToAction("RoomList");
        }

        public ActionResult DeleteRoom(int id)
        {
            _rRep.Delete(_rRep.Find(id));
            return RedirectToAction("RoomList");
        }


    }
}