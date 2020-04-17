using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.Departments
{
    public class DepartmentEditViewModel : DepartmentCreateViewModel
    {
        [Required]
        public string Uid { get; set; }
    }
}
