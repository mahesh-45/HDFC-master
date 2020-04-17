using AutoMapper;
using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.BudgetCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class BudgetCategoriesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BudgetCategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var budgetCategories = await _unitOfWork.BudgetCategories.ActiveToListAsync();
            if (budgetCategories.Count == 0)
                return Ok(null);
            var budgetCategoriesDtosList = _mapper.Map<List<BudgetCategory>, List<BudgetCategoryDto>>(budgetCategories.OrderBy(s => s.Name).ToList());
            return Ok(budgetCategoriesDtosList);
        }
        [HttpPost("search")]
        public async Task<IActionResult> GetList([FromBody]SearchDto searchDto)
        {
            var budgetHeads = await _unitOfWork.BudgetCategories.GetList(searchDto);
            if (budgetHeads.Total_count == 0)
                return Ok(null);
            return Ok(budgetHeads);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var budgetCategories = await _unitOfWork.BudgetCategories.SingleAsync(uid);
            var budgetCategoriesDto = _mapper.Map<BudgetCategory, BudgetCategoryDto>(budgetCategories);
            return Ok(budgetCategoriesDto);
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]BudgetCategoryCreateViewModel input)
        {
            var user = User.GetDetails();
            var budgetCategory = new BudgetCategory(input.Name, input.Code, input.Status, user.Id);

            if (await _unitOfWork.BudgetCategories.AnyAsync(budgetCategory))
            {
                return BadRequest("Budget Categories Already Exists");
            }

            _unitOfWork.BudgetCategories.Add(budgetCategory);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(budgetCategory);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromBody]BudgetCategoryEditViewModel input)
        {
            var user = User.GetDetails();
            var budgetCategory = await _unitOfWork.BudgetCategories.SingleAsync(input.Uid);
            budgetCategory.Update(input.Name, input.Code, input.Status, user.Id);

            if (await _unitOfWork.BudgetCategories.AnyAsync(budgetCategory))
            {
                return BadRequest("Budget Categories Already Exists");
            }

            _unitOfWork.BudgetCategories.Update(budgetCategory);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(budgetCategory);
        }
    }
}
