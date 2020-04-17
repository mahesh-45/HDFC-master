using AutoMapper;
using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Procurements;
using HDFC.Core.Entities.Budgeting;
using HDFC.Core.Enums;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Budgets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Procurements
{
    public class BudgetsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BudgetsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var budgets = await _unitOfWork.Budgets.ToListAsync();
            if (budgets.Count == 0)
                return Ok(null);
            var budgetDtoList = _mapper.Map<List<Budget>, List<BudgetDto>>(budgets.OrderBy(s => s.Name).ToList());
            return Ok(budgetDtoList);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetBudgets(SearchDto searchDto)
        {
            var budgets = await _unitOfWork.Budgets.GetList(searchDto);
            if (budgets == null)
                return Ok(null);
            return Ok(budgets);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var budget = await _unitOfWork.Budgets.GetBudgets(uid);
            var budgetDto = _mapper.Map<Budget, BudgetDto>(budget);
            return Ok(budgetDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]BudgetCreateViewModel input)
        {
            var user = User.GetDetails();
            var data = _mapper.Map<BudgetCreateViewModel, Budget>(input);
            var budget = new Budget(input.Name, input.Description, input.BudgetHeadId, input.BudgetCategoryId, input.DepartmentId, input.CurrencyId, user.Id);
            string referenceId = await _unitOfWork.NumberingSequences.GenerateReferenceId(NumberingSequenceTypeEnum.Budget);
            if (referenceId == null)
            {
                return BadRequest("ReferenceId not generated");
            }

            budget.SetBudgetData(input.BudgetType, referenceId, input.DivisionId, input.SubDivisionId, input.PersonInChargeId, input.Justification);
            budget.SetBudgetAmountData(input.StartDate, input.EndDate, input.TransactionDate, input.BasicAmount, input.TaxAmount);
            budget.CalculateTotalAmount();
            budget.AddSpendLimits(data.BudgetSpendLimits.ToList(), user.Id);
            budget.AddCostCodes(data.BudgetCostCodes.ToList(), user.Id);
            budget.SetBaseId(null);
            budget.SetRevisionNumber(0);
            budget.SetActive();
            if (await _unitOfWork.Budgets.AnyAsync(budget))
            {
                return BadRequest("Budget Already Exists");
            }

            _unitOfWork.Budgets.Add(budget);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(budget);
        }

        [HttpPut]
        [ValidateModel]
        public async Task<IActionResult> Update([FromBody]BudgetEditViewModel input)
        {
            var user = User.GetDetails();
            var data = _mapper.Map<BudgetEditViewModel, Budget>(input);
            var previousBudget = await _unitOfWork.Budgets.GetBudgets(input.Uid);
            previousBudget.SetInActive();
            _unitOfWork.Budgets.Update(previousBudget);
            await _unitOfWork.CompleteAsync(user.Id);
            var budget = new Budget(input.Name, input.Description, input.BudgetHeadId, input.BudgetCategoryId, input.DepartmentId, input.CurrencyId, user.Id);
            budget.SetBudgetData(input.BudgetType, previousBudget.ReferenceId, input.DivisionId, input.SubDivisionId, input.PersonInChargeId, input.Justification);
            budget.SetBudgetAmountData(input.StartDate, input.EndDate, input.TransactionDate, input.BasicAmount, input.TaxAmount);
            budget.CalculateTotalAmount();
            budget.AddSpendLimits(data.BudgetSpendLimits.ToList(), user.Id);
            budget.AddCostCodes(data.BudgetCostCodes.ToList(), user.Id);
            if (previousBudget != null)
            {
                if (previousBudget.BaseId > 0)
                {
                    budget.SetBaseId(previousBudget.BaseId);
                }
                else
                {
                    budget.SetBaseId(previousBudget.Id);
                }
                budget.SetRevisionNumber(previousBudget.RevisionNumber + 1);

                budget.SetActive();
            }
            if (await _unitOfWork.Budgets.AnyAsync(budget))
            {
                return BadRequest("budget Already Exists.");
            }
            _unitOfWork.Budgets.Add(budget);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(budget);
        }

        [HttpDelete("{uid}/deleteSubBudgets/{subBudgetId}")]
        public async Task<IActionResult> DeleteSubBudgets(string uid, int subBudgetId)
        {
            var userId = User.GetDetails();
            var budget = await _unitOfWork.Budgets.GetBudgets(uid);
            budget.RemoveSubBudgets(subBudgetId, userId.Id);
            _unitOfWork.Budgets.Update(budget);
            await _unitOfWork.CompleteAsync(userId.Id);
            return Ok("Sub-Budgets-Deleted");
        }


        [HttpDelete("{uid}/deleteBudgetSpendLimit/{budgetSpendLimit}")]
        public async Task<IActionResult> DeleteBudgetSpendLimit(string uid, int budgetSpendLimit)
        {
            var userId = User.GetDetails();
            var budget = await _unitOfWork.Budgets.GetBudgets(uid);
            budget.RemoveBudgetSpendLimits(budgetSpendLimit, userId.Id);
            _unitOfWork.Budgets.Update(budget);
            await _unitOfWork.CompleteAsync(userId.Id);
            return Ok("BudgetsSpendLimit--Deleted");
        }

        [HttpPut("Hold/{budgetId:long}")]
        public async Task<IActionResult> HoldBudget(long budgetId)
        {
            var user = User.GetDetails();
            var budget = await _unitOfWork.Budgets.SingleAsync(budgetId);
            budget.Hold(user.Id);
            _unitOfWork.Budgets.Update(budget);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(budget);
        }

    }
}
