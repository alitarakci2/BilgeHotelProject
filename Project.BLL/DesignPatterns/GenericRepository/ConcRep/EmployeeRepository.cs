using Project.BLL.DesignPatterns.GenericRepository.BaseRep;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.ConcRep
{
    public class EmployeeRepository:BaseRepository<Employee>
    {
        public decimal GetSalary(Employee emp)
        {
            if (emp.SalaryType == ENTITIES.Enums.SalaryType.Hourly)
            {
                emp.TotalSalary = Convert.ToDecimal(emp.BaseSalary * emp.WorkedHours);

                return emp.TotalSalary;
            }

            return emp.TotalSalary;


        }







    }
}
