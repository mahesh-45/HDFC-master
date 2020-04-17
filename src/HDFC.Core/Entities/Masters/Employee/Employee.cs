using HDFC.Core.Enums;
using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;
using System;

namespace HDFC.Core.Entities.Masters.Employee
{
    public class Employee : AggregateRoot
    {
        private Employee()
        {
        }

        public Employee(string firstName, string middleName, string lastName, string title, long? userId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Title = title;
            UserId = userId;
            UpdateAudit(Convert.ToInt32(userId));
        }

     
        public void SetEmployeePersonalDetails(string employeeNumber, string gender, string panNumber, DateTime dateOfBirth, string maritalStatus, string officialEmail, string contact1, string contact2, string band)
        {
            EmployeeNumber = employeeNumber;
            Gender = gender;
            PanNumber = panNumber;
            DateOfBirth = dateOfBirth;
            MaritalStatus = maritalStatus;
            OfficialEmail = officialEmail;
            Contact1 = contact1;
            Contact2 = contact2;
            Band = band;
           
        }
        public void SetEmployeeOrganizationalDetails(string locationName,string supervisorEmpCode, DateTime? hireDate, string recordStatus, int countryId, string city, string region, DateTime? actualTerminationDate, EmployeeStatusEnum employeeStatus)
        {
            LocationName = locationName;
            SupervisorEmpCode = supervisorEmpCode;
            HireDate = hireDate;
            RecordStatus = recordStatus;
            CountryId = countryId;
            City = city;
            Region = region;
            ActualTerminationDate = actualTerminationDate;
            EmployeeStatus = employeeStatus;
            
        }

        public void UpdateDetails(string firstName, string middleName, string lastName, string title,  long? userId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Title = title;
            UserId = userId;
            UpdateAudit(Convert.ToInt32(userId));
        }



        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }
        public string EmployeeNumber { get; private set; }
        public string Gender { get; private set; }
        public string PanNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string MaritalStatus { get; private set; }
        public string OfficialEmail { get; private set; }
        public string Contact1 { get; private set; }
        public string Contact2 { get; private set; }
        public string Band { get; private set; }
        public string LocationName { get; private set; }
        public string SupervisorEmpCode { get; private set; }
        public DateTime? HireDate { get; private set; }
        public string RecordStatus { get; private set; }
        public int CountryId { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public DateTime? ActualTerminationDate { get; private set; }
        public EmployeeStatusEnum EmployeeStatus { get; private set; }
        public long? UserId { get; private set; }

    }
}
