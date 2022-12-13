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
    public class RegisterController : Controller
    {



        AppUserRepository _apRep;
        ProfileRepository _proRep;

        public RegisterController()
        {
            _apRep= new AppUserRepository();
            _proRep= new ProfileRepository();
        }


        public ActionResult RegisterNow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNow(AppUser appUser, AppUserProfile userProfile)
        {
            appUser.Password = DantexCrypt.Crypt(appUser.Password);

            if (_apRep.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanici ismi daha once alinmis.";
                return View();
            }
            else if (_apRep.Any(x => x.Email == appUser.Email))
            {
                ViewBag.ZatenVar = "Email zaten kayitli.";
                return View();
            }

            
            string gonderilecekEmail = "Hesabinizi aktif edebilmek icin  https://localhost:44340/Register/Activation/" + appUser.ActivationCode + " linkine tiklayabilirsiniz.";

            MailService.Send(appUser.Email, body: gonderilecekEmail, subject: "Hesap Aktivasyon!!");
            _apRep.Add(appUser);

            if (!string.IsNullOrEmpty(userProfile.FirstName.Trim()) || !string.IsNullOrEmpty(userProfile.LastName.Trim()))
            {
                userProfile.ID = appUser.ID;
                _proRep.Add(userProfile);
            }

            return View("RegisterOK");
        }

        public ActionResult Activation(Guid id)
        {
            AppUser aktifEdilecek = _apRep.FirstOrDefault(x => x.ActivationCode == id);
            if (aktifEdilecek != null)
            {
                aktifEdilecek.Active = true;
                _apRep.Update(aktifEdilecek);
                TempData["HesapAktifMi"] = "Hesabınız aktif hale getirildi";
                return RedirectToAction("Login", "Home");
            }


            TempData["HesapAktifMi"] = "Hesabınız bulunamadı";

            return RedirectToAction("Login", "Home");
        }

        public ActionResult RegisterOK()
        {
            return View();
        }








    }
}