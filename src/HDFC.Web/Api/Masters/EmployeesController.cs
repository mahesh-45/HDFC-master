using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Dtos.Masters.Employee;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Entities.Masters.Employee;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HDFC.Web.Api.Masters
{
    public class EmployeesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _unitOfWork.Employees.ActiveToListAsync();

            var employeeDtosList = _mapper.Map<List<Employee>, List<EmployeeDto>>(employees.OrderBy(s => s.FirstName).ToList());
            return Ok(employeeDtosList);

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _unitOfWork.Employees.ToListAsync();

            var employeeDtosList = _mapper.Map<List<Employee>, List<EmployeeDto>>(employees.OrderBy(s => s.FirstName).ToList());
            return Ok(employeeDtosList);
        }

        [HttpGet("list/{search}")]
        public async Task<IActionResult> GetList(string search)
        {
            var employees = await _unitOfWork.Employees.GetList(search);

            return Ok(employees);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var employee = await _unitOfWork.Employees.SingleAsync(uid);
            var employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetByUserId(long userId)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeByUserId(userId);
            var employeeDto = _mapper.Map<Employee, EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]EmployeeCreateViewModel input)
        {
            var user = User.GetDetails();

            var employee = new Employee(input.FirstName, input.MiddleName, input.LastName, input.Title, input.UserId);
            employee.SetEmployeePersonalDetails(input.EmployeeNumber, input.Gender, input.PanNumber, input.DateOfBirth, input.MaritalStatus, input.OfficialEmail, input.Contact1, input.Contact2, input.Band);
            employee.SetEmployeeOrganizationalDetails(input.LocationName, input.SupervisorEmpCode, input.HireDate, input.RecordStatus, input.CountryId, input.City, input.Region, input.ActualTerminationDate, input.EmployeeStatus);
            if (await _unitOfWork.Employees.AnyAsync(employee))
            {
                return BadRequest("Employee Already Exists");
            }



            _unitOfWork.Employees.Add(employee);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(employee);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]EmployeeEditViewModel input)
        {
            var user = User.GetDetails();
            var employee = await _unitOfWork.Employees.SingleAsync(uid);
            employee.UpdateDetails(input.FirstName, input.MiddleName, input.LastName, input.Title, input.UserId);
            employee.SetEmployeePersonalDetails(input.EmployeeNumber, input.Gender, input.PanNumber, input.DateOfBirth, input.MaritalStatus, input.OfficialEmail, input.Contact1, input.Contact2, input.Band);
            employee.SetEmployeeOrganizationalDetails(input.LocationName, input.SupervisorEmpCode, input.HireDate, input.RecordStatus, input.CountryId, input.City, input.Region, input.ActualTerminationDate, input.EmployeeStatus);
            if (await _unitOfWork.Employees.AnyAsync(employee))
            {
                return BadRequest("Employee Already Exists.");
            }

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(employee);
        }
    }
}
