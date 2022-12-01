using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRepository _apRep;

        public HomeController()
        {
            _apRep = new AppUserRepository();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser yakalanan = _apRep.FirstOrDefault(x => x.UserName == appUser.UserName);
            if (yakalanan == null)
            {
                ViewBag.Kullanici = "Kullanici bulunamadı";
                return View();
            }

            string decrypted = DantexCrypt.DeCrypt(yakalanan.Password);
            if (appUser.Password == decrypted && yakalanan.Role == ENTITIES.Enums.UserRole.Admin)
            {
                if (!yakalanan.Active) return AktifKontrol();
                Session["admin"] = yakalanan;
                return RedirectToAction("RoomTypeList", "RoomType", new { area = "Admin" });
            }
            else if (yakalanan.Role == ENTITIES.Enums.UserRole.Customer && appUser.Password == decrypted)
            {
                if (!yakalanan.Active) return AktifKontrol();
                Session["customer"] = yakalanan;
                return RedirectToAction("BookingList", "Booking");

            }
            ViewBag.Kullanici = "Kullanıcı bulunamadı";
            return View();


        }

        private ActionResult AktifKontrol()
        {
            ViewBag.Kullanici = "Lutfen hesabinizi aktif hale getiriniz";
            return View("Login");
        }



    }
}