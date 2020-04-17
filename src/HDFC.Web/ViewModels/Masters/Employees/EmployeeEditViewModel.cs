using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.Employee
{
    public class EmployeeEditViewModel :EmployeeCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
    }
}
