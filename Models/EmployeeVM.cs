using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDirect.Models
{
    public class EmployeeVM
    {
        public List<Employee> Employees = new List<Employee>();
        //public Employee Employee = new Employee();
        public int EmployeeID { get; set; }
        public int WorkDays  {get; set; }
        
        public double VacationTaken { get; set; }
        public double VacationDays { get; set; }
        public string EditType { get; set; }
    }
}