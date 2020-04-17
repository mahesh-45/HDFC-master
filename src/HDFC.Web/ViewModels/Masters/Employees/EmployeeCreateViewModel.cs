using HDFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.ViewModels.Masters.Employee
{
    public class EmployeeCreateViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string EmployeeNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PanNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        [EmailAddress]
        public string OfficialEmail { get; set; }
        [Required]
        public string Contact1 { get; set; }
        [Required]
        public string Contact2 { get; set; }
        [Required]
        public string Band { get; set; }
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string SupervisorEmpCode { get; set; }
        [Required]
        public DateTime? HireDate { get; set; }
        [Required]
        public string RecordStatus { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public DateTime? ActualTerminationDate { get; set; }
        [Required]
        public EmployeeStatusEnum EmployeeStatus { get; set; }
        
        public long? UserId { get; set; }
    }
}
