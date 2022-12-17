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
    public class SalaryController : Controller
    {

        EmployeeRepository _emRep;

        public SalaryController()
        {
            _emRep= new EmployeeRepository();
        }
        




      
        public ActionResult EmployeeList(int? id)
        {
            EmployeeVM empvm = id == null ? new EmployeeVM
            {
                Employees = _emRep.GetActives()
            } : new EmployeeVM { Employees = _emRep.Where(x => x.ID == id) };

            return View(empvm);




        }

        public ActionResult PaySalary(int id)
        {
            EmployeeVM empvm = new EmployeeVM { Employee = _emRep.Find(id) };
            return View(empvm);



        }


        [HttpPost]
        public ActionResult PaySalary(Employee employee) 
        {
            _emRep.GetSalary(employee);
            _emRep.Update(employee);


            return RedirectToAction("EmployeeList");



        }

        


    }
}