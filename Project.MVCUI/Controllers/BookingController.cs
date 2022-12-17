using PagedList;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.BookingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

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
                //PagedRooms = roomTypeID == null ? _rRep.GetActives().ToPagedList(page ?? 1, 9) : _rtRep.Where(x => x.RoomTypeID == roomTypeID).ToPagedList(page ?? 1, 9),
                RoomTypes = _rtRep.GetActives(),
                PagedRoomTypes = roomTypeID == null ? _rtRep.GetActives().ToPagedList(page ?? 1, 9) : _rtRep.Where(x => x.ID == roomTypeID).ToPagedList(page ?? 1, 9)


            };
            if (roomTypeID != null) TempData["rtID"] = roomTypeID;


            return View(pavm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] == null ? new Cart() : Session["scart"] as Cart;
            RoomType eklenecekUrun = _rtRep.Find(id);

            CartItem ci = new CartItem
            {
                ID = eklenecekUrun.ID,
                Name = eklenecekUrun.TypeName,
                Price = eklenecekUrun.UnitPrice,
                ImagePath = eklenecekUrun.ImagePath
            };

            c.SepeteEkle(ci);
            Session["scart"] = c;
            return RedirectToAction("BookingList");
        }

        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                CartPageVM cpvm = new CartPageVM();
                Cart c = Session["scart"] as Cart;
                cpvm.Cart = c;
                return View(cpvm);
            }
            TempData["bos"] = "Oda bulunmamaktadır";
            return RedirectToAction("BookingList");
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
                Cart c = Session["scart"] as Cart;
                c.SepettenCikar(id);
                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Tum rezervasyonlar temizlenmistir";
                    return RedirectToAction("BookingList");
                }
                return RedirectToAction("CartPage");
            }
            return RedirectToAction("BookingList");
        }


        public ActionResult ConfirmOrder()
        {
            AppUser currentUser;
            if (Session["member"] != null)
            {
                currentUser = Session["member"] as AppUser;
            }
            else TempData["anonim"] = "Kullanıcı üye degil";
            return View();


        }

        //https://localhost:44366/api/Payment/RecievePayment

        [HttpPost]
        public ActionResult ConfirmOrder(BookingVM bvm)
        {
            bool result;
            Cart sepet = Session["scart"] as Cart;
            bvm.Booking.TotalPrice = bvm.PaymentDTO.BookingPrice = sepet.TotalPrice;
           


            #region APISection

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44366/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", bvm.PaymentDTO);
                HttpResponseMessage sonuc;

                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception ex)
                {
                    TempData["baglantiRed"] = "Banka baglantiyi reddetti";
                    return RedirectToAction("BookingList");
                }

                if (sonuc.IsSuccessStatusCode) result = true;
                else result = false;

                if (result)
                {
                    if (Session["member"] != null)
                    {
                        AppUser kullanici = Session["member"] as AppUser;
                        bvm.Booking.AppUserID = kullanici.ID;
                        bvm.Booking.Customer.UserName = kullanici.UserName;
                    }
                    else
                    {
                        bvm.Booking.AppUserID = null;
                        bvm.Booking.Customer.UserName = TempData["anonim"].ToString();
                    }

                    _bRep.Add(bvm.Booking);

                    foreach (CartItem item in sepet.Sepetim)
                    {
                        BookingManagement bm = new BookingManagement();
                        bm.BookingID = bvm.Booking.ID;
                        bm.RoomTypeID = item.ID;
                        bm.RoomType.UnitPrice = item.SubTotal;

                        _bmRep.Add(bm);


                        //Product stokDus = _pRep.Find(item.ID);
                        //stokDus.UnitsInStock -= item.Amount;
                        //_pRep.Update(stokDus);

                    }
                    TempData["odeme"] = "Siparişiniz  bize ulasmıstır...Tesekkür ederiz";
                    MailService.Send(bvm.Booking.Customer.Email, body: $"Siparişiniz basarıyla alındı {bvm.Booking.TotalPrice}");

                    Session.Remove("scart");

                    return RedirectToAction("BookingList");

                }
                else
                {
                    Task<string> s = sonuc.Content.ReadAsStringAsync();
                    TempData["sorun"] = s.Result;
                    return RedirectToAction("BookingList");
                }

            }


            #endregion

        }


    }
}