using Bogus;
using Bogus.DataSets;
using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {

        protected override void Seed(MyContext context)
        {
            AppUser au = new AppUser();
            au.UserName = "ali";
            au.Password = DantexCrypt.Crypt("123");
            au.Email = "ormaninhayaleti@gmail.com";
            au.Role = ENTITIES.Enums.UserRole.Admin;
            au.Active= true;
            context.AppUsers.Add(au);
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser();
                ap.UserName = new Internet("tr").UserName();
                ap.Password = new Internet("tr").Password();
                ap.Email = new Internet("tr").Email();
                context.AppUsers.Add(ap);

            }

            context.SaveChanges();

            for (int i = 2; i < 12; i++)
            {
                AppUserProfile apu = new AppUserProfile();
                apu.ID= i;
                apu.FirstName = new Name("tr").FirstName();
                apu.LastName = new Name("tr").LastName();
                apu.Adress = new Address("tr").FullAddress();
                apu.IdentityNumber = new Randomizer().Long(10000000000,99999999999).ToString();
                apu.PhoneNumber = new PhoneNumbers().PhoneNumber();
                context.Profiles.Add(apu);
            }
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                RoomType rt= new RoomType();
                rt.TypeName = new Lorem("tr").Word();
                rt.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                rt.GuestCapacity = new Randomizer().Int(1, 5);
                rt.ImagePath = new Images().Nightlife();

                for (int j = 0; j < 30; j++)
                {
                    Room r= new Room();

                    r.FloorNumber = new Randomizer().Int(1, 4);
                    r.RoomNumber = new Randomizer().Int(1, 20).ToString();
                    r.RoomStatus = new Randomizer().Enum<RoomStatus>();
                    r.Description= new Lorem("tr").Sentence(10);
                    rt.Rooms.Add(r);


                }

                context.RoomTypes.Add(rt);
                context.SaveChanges();

            }







        }



    }
}
