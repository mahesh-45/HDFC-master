using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.Departments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class DepartmentController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var departments = await _unitOfWork.Departments.ActiveToListAsync();

            var departmentDtoList = _mapper.Map<List<Department>, List<DepartmentDto>>(departments.OrderBy(s => s.Name).ToList());
            return Ok(departmentDtoList);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _unitOfWork.Departments.ToListAsync();

            var departmentDtoList = _mapper.Map<List<Department>, List<DepartmentDto>>(departments.OrderBy(s => s.Name).ToList());
            return Ok(departmentDtoList);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var department = await _unitOfWork.Departments.SingleAsync(uid);
            var departmentDto = _mapper.Map<Department, DepartmentDto>(department);
            return Ok(departmentDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]DepartmentCreateViewModel input)
        {
            var user = User.GetDetails();
            var department = new Department(input.Name, input.Code , input.Status, user.Id);

            if (await _unitOfWork.Departments.AnyAsync(department))
            {
                return BadRequest("Department Already Exists");
            }

            _unitOfWork.Departments.Add(department);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(department);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]DepartmentEditViewModel input)
        {
            var user = User.GetDetails();
            var department = await _unitOfWork.Departments.SingleAsync(uid);

            department.Update(input.Name, input.Code, input.Status, user.Id);

            if (await _unitOfWork.Departments.AnyAsync(department))
            {
                return BadRequest("Department Already Exists");
            }

            _unitOfWork.Departments.Update(department);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(department);
        }
    }
}
