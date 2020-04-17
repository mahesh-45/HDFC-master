using System;
using System.Text;
using HDFC.Core.Enums;

namespace HDFC.Core.Dtos.Masters.Employee
{
    public class EmployeeDto : AggregateRootDto
    {
        public string FirstName { get;  set; }
        public string MiddleName { get;  set; }
        public string LastName { get;  set; }
        public string Title { get;  set; }
        public string EmployeeNumber { get;  set; }
        public string Gender { get;  set; }
        public string PanNumber { get;  set; }
        public DateTime DateOfBirth { get;  set; }
        public string MaritalStatus { get;  set; }
        public string OfficialEmail { get;  set; }
        public string Contact1 { get;  set; }
        public string Contact2 { get;  set; }
        public string Band { get;  set; }
        public string LocationName { get;  set; }
        public string SupervisorEmpCode { get;  set; }
        public DateTime? HireDate { get;  set; }
        public string RecordStatus { get;  set; }
        public int CountryId { get;  set; }
        public string City { get;  set; }
        public string Region { get;  set; }
        public DateTime? ActualTerminationDate { get;  set; }
        public EmployeeStatusEnum EmployeeStatus { get;  set; }
        public long? UserId { get;  set; }
    }
}
