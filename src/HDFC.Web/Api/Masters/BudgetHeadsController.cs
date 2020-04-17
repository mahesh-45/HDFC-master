using AutoMapper;
using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.BudgetHead;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class BudgetHeadsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BudgetHeadsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var budgetHeads = await _unitOfWork.BudgetHeads.ActiveToListAsync();
            if (budgetHeads.Count == 0)
                return Ok(null);
            var budgetHeadDtoList = _mapper.Map<List<BudgetHead>, List<BudgetHeadDto>>(budgetHeads.OrderBy(s => s.Name).ToList());
            return Ok(budgetHeadDtoList);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetList([FromBody]SearchDto searchDto)
        {
            var budgetHeads = await _unitOfWork.BudgetHeads.GetList(searchDto);
            return Ok(budgetHeads);
        }


        [HttpGet("GetTree")]
        public async Task<IActionResult> GetTree()
        {
            var budgetHeads = await _unitOfWork.BudgetHeads.GetParents();
            if (budgetHeads.Count == 0)
                return BadRequest("No BudgetHeads exist");

            var budgetHeadDto = new BudgetHeadDto();
            List<BudgetHeadDto> budgetHeadDtoList = new List<BudgetHeadDto>();

            foreach (var itemType in budgetHeads)
            {
                List<BudgetHeadDto> childbudgetHeadDtoList = new List<BudgetHeadDto>();
                childbudgetHeadDtoList = await GetChildBudgetHead(itemType.Id);
                budgetHeadDto = _mapper.Map<BudgetHead, BudgetHeadDto>(itemType);
                budgetHeadDto.ChildBudgetHead = childbudgetHeadDtoList;
                budgetHeadDtoList.Add(budgetHeadDto);
            }
            return Ok(budgetHeadDtoList);
        }

        private async Task<List<BudgetHeadDto>> GetChildBudgetHead(long parentId)
        {
            var budgetHeadDto = new BudgetHeadDto();
            List<BudgetHeadDto> budgetHeadDtoList = new List<BudgetHeadDto>();

            var childBudgetHeadDto = new BudgetHeadDto();
            var childBudgetHead = await _unitOfWork.BudgetHeads.GetChildren(parentId);
            foreach (var cc in childBudgetHead)
            {
                List<BudgetHeadDto> childBudgetHeadDtoList = new List<BudgetHeadDto>();
                childBudgetHeadDtoList = await GetChildBudgetHead(cc.Id);
                budgetHeadDto = _mapper.Map<BudgetHead, BudgetHeadDto>(cc);
                budgetHeadDto.ChildBudgetHead = childBudgetHeadDtoList;
                budgetHeadDtoList.Add(budgetHeadDto);
            }
            return budgetHeadDtoList;
        }




        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var budgetHead = await _unitOfWork.BudgetHeads.SingleAsync(uid);
            var budgetHeadDto = _mapper.Map<BudgetHead, BudgetHeadDto>(budgetHead);
            return Ok(budgetHeadDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]BudgetHeadCreateViewModel input)
        {
            var user = User.GetDetails();
            var budgetHead = new BudgetHead(input.Name, input.Code, input.ParentId, input.Status, input.DepartmentId, input.CostCodeId, user.Id);

            if (await _unitOfWork.BudgetHeads.AnyAsync(budgetHead))
            {
                return BadRequest("Budget Head Already Exists");
            }

            _unitOfWork.BudgetHeads.Add(budgetHead);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(budgetHead);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]BudgetHeadEditViewModel input)
        {
            var user = User.GetDetails();
            var budgetHead = await _unitOfWork.BudgetHeads.SingleAsync(input.Uid);
            budgetHead.Update(input.Name, input.Code, input.ParentId, input.Status, input.DepartmentId, input.CostCodeId, user.Id);

            if (await _unitOfWork.BudgetHeads.AnyAsync(budgetHead))
            {
                return BadRequest("Budget Head Already Exists");
            }

            _unitOfWork.BudgetHeads.Update(budgetHead);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(budgetHead);
        }

    }
}
