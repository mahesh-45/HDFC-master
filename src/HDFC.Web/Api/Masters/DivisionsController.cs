using AutoMapper;
using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters.Division;
using HDFC.Core.Entities.Masters.Division;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.Division;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class DivisionsController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DivisionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var divisions = await _unitOfWork.Divisions.ToListAsync();
            if (divisions.Count == 0)
                return Ok(null);
            var budgetDtoList = _mapper.Map<List<Division>, List<DivisionDto>>(divisions.OrderBy(s => s.Name).ToList());
            return Ok(budgetDtoList);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetDivisions(SearchDto searchDto)
        {
            var divisions = await _unitOfWork.Divisions.GetList(searchDto);
            if (divisions == null)
                return Ok(null);
            return Ok(divisions);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var division = await _unitOfWork.Divisions.GetDivision(uid);
            var divisionDto = _mapper.Map<Division, DivisionDto>(division);
            return Ok(divisionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]DivisionCreateViewModel input)
        {
            var user = User.GetDetails();
            var data = _mapper.Map<DivisionCreateViewModel, Division>(input);
            var division = new Division(input.Name, input.Code, input.Status, user.Id);
            if (await _unitOfWork.Divisions.AnyAsync(division))
            {
                return BadRequest("Division Already Exists");
            }
            division.AddSubDivisions(data.SubDivisions.ToList(), user.Id);
            _unitOfWork.Divisions.Add(division);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(division);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Update([FromBody]DivisionEditViewModel input)
        {
            var user = User.GetDetails();
            var data = _mapper.Map<DivisionEditViewModel, Division>(input);
            var division = await _unitOfWork.Divisions.GetDivision(input.Uid);
            division.Update(input.Name, input.Code, input.Status, user.Id);
            division.AddSubDivisions(data.SubDivisions.ToList(), user.Id);           
            if (await _unitOfWork.Divisions.AnyAsync(division))
            {
                return BadRequest("Division Already Exists.");
            }
            _unitOfWork.Divisions.Update(division);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(division);
        }

        [HttpDelete("{uid}/deleteSubDivision/{subDivisionId}")]
        public async Task<IActionResult> DeleteSubDivisions(string uid, int subDivisionId)
        {
            var userId = User.GetDetails();
            var previousDivision = await _unitOfWork.Divisions.GetDivision(uid);
            previousDivision.RemoveSubDivision(subDivisionId, userId.Id);
            _unitOfWork.Divisions.Update(previousDivision);
            await _unitOfWork.CompleteAsync(userId.Id);
            return Ok("SubDivision Deleted");
        }

    }
}
