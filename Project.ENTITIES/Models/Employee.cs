using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Employee:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal BaseSalary { get; set; }

        public SalaryType SalaryType { get; set; }

        public decimal TotalSalary { get; set; }
        public int? WorkedHours { get; set; }








    }
}
