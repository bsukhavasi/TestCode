using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDirect.Models
{
    public class Employee
    {
        
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name { get; set; }
        public string EmployeeTypeCD { get; set; }

        private int _workingDays;
        [Required]
        [Range(0, 260, ErrorMessage = "Can only be between 0 .. 260")]
        public int WorkDays {
            get { return _workingDays; }
            set {
                _workingDays = value;
                switch (this.EmployeeTypeCD)
                {
                    case ("H")://Hourly paid Employee
                        this.VacationDays = (_workingDays * 0.0383)- VacationTaken;
                        break;
                    case ("E"):// Employee
                        this.VacationDays = ((_workingDays *  0.0578)) - VacationTaken;
                        break;
                    case ("M")://Manager
                        this.VacationDays = (_workingDays * 0.1154)- VacationTaken;
                        break;
                }
            }
        }
        [DisplayFormat(DataFormatString = "{0:#.#}")]
        public double VacationDays { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.#}")]
        public double VacationTaken { get; set; }


    }
}