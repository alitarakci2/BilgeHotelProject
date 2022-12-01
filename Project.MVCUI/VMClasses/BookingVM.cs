using Project.ENTITIES.Models;
using Project.MVCUI.ConsumerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class BookingVM
    {
        public Booking Booking { get; set; }
        public List<Booking> Bookings { get; set; }

        public PaymentDTO PaymentDTO { get; set; }







    }
}