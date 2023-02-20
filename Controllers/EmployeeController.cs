using EmployeeDirect.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EmployeeDirect.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            EmployeeVM employeeVM = new EmployeeVM();
            if (TempData.ContainsKey("Employees"))
                employeeVM.Employees = JsonConvert.DeserializeObject<List<Employee>>((string)(TempData.Peek("Employees")));
            else
                employeeVM.Employees = GetEmployees();
            if (employeeVM.Employees == null)
                employeeVM.Employees = new List<Employee>();
            TempData["Employees"] = JsonConvert.SerializeObject(employeeVM.Employees);
            return View(employeeVM);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(EmployeeVM employeeVM,string Edit,string CloseEdit)
        {
            if (TempData.ContainsKey("Employees"))
                employeeVM.Employees = JsonConvert.DeserializeObject<List<Employee>>((string)(TempData.Peek("Employees")));

            if (!string.IsNullOrEmpty(Edit))
            {
                if(employeeVM.EditType =="V")
                {
                    string strvalidation;
                    strvalidation = CheckforMaxVacation(employeeVM);
                    if (!string.IsNullOrEmpty(strvalidation))                    
                    {
                        ViewBag.message = strvalidation;
                        return PartialView("EditEmployee", employeeVM);
                    }
                }
                foreach(Employee e in employeeVM.Employees)
                {
                    if(e.EmployeeID == employeeVM.EmployeeID)
                    {
                        if (employeeVM.EditType == "W")
                            e.WorkDays = employeeVM.WorkDays;
                        else if(employeeVM.EditType == "V")
                        {
                            e.VacationDays = e.VacationDays - employeeVM.VacationTaken;
                            e.VacationTaken = employeeVM.VacationTaken;
                        }
                            
                    }
                }    
            }
          
            TempData["Employees"] = JsonConvert.SerializeObject(employeeVM.Employees);
            return RedirectToAction("Index");
        }
        public ActionResult Work(int id, string editType)
        {
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>((string)(TempData.Peek("Employees")));
            EmployeeVM employeeVM = new EmployeeVM();
            Employee employee = employees.Find(e => e.EmployeeID == id);
            employeeVM.EmployeeID = employee.EmployeeID;
            employeeVM.WorkDays = employee.WorkDays;
            employeeVM.VacationDays = employee.VacationDays;
            employeeVM.EditType = editType;
            return PartialView("EditEmployee", employeeVM);
        }

        private string CheckforMaxVacation(EmployeeVM employeeVM)
        {
            Employee employee = employeeVM.Employees.Find(e => e.EmployeeID == employeeVM.EmployeeID);
            switch (employee.EmployeeTypeCD)
            {
                case ("H")://Hourly paid Employee
                    //We are checking max vacation days. But in addition to that  we need to consider How many vacation days avaialable to take vacation?
                    if (employeeVM.VacationTaken > 10 || employeeVM.VacationTaken >employee.VacationDays)
                    {
                        return "Max allowed vacation per Year is 10. Currently Available vacations days are " + employee.VacationDays + ". Please Correct the vacation days.";
                        
                    }
                    break;
                case ("E"):// Employee
                    if (employeeVM.VacationTaken > 15 || employeeVM.VacationTaken > employee.VacationDays)
                    {
                        return "Max allowed vacation per Year is 15. Currently Available vacations days are " + employee.VacationDays + ".  Please Correct the vacation days.";
                        
                    }
                    break;
                case ("M")://Manager
                    if (employeeVM.VacationTaken > 30 || employeeVM.VacationTaken > employee.VacationDays)
                    {
                        return "Max allowed vacation per Year is 30.. Currently Available vacations days are " + employee.VacationDays + ".  Please Correct the vacation days.";
                        
                    }
                    break;
            }
            return "";
        }     
       

        private List<Employee> GetEmployees()
        {
            
            List<Employee> employees = new List<Employee>();
            
                List<string> FirstNames = new List<string>()
    {
        "Sergio",
        "Daniel",
        "Carolina",
        "David",
        "Reina",
        "Saul",
        "Bernard",
        "Danny",
        "Dimas",
        "Yuri",
        "Ivan",
        "Laura","Tapia",
        "Gutierrez",
        "Rueda",
        "Galviz",
        "Yuli",
        "Rivera",
        "Mamami",
        "Saucedo",
        "Dominguez",
        "Johnson",
        "Williams",
        "Jones",
        "Brown",
        "David",
        "Miller","Sam","Hung","Scotty"
    };

   
            for(int i=0;i<30;i++)
            {
                Employee employee = new Employee();
                employee.EmployeeID = i+1;
                employee.Name = FirstNames[i];
                if(i<10)
                employee.EmployeeTypeCD = "E";
                else if(i <20)
                    employee.EmployeeTypeCD = "H";
                else if (i < 30)
                    employee.EmployeeTypeCD = "M";
                
                    employee.WorkDays = i + 100;
                employees.Add(employee);
            }
               
            return employees;
        }
    }
}