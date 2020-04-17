using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.CostCodes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class CostCodeController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CostCodeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var costCodes = await _unitOfWork.CostCodes.ActiveToListAsync();
            
            var costCodeDtoList = _mapper.Map<List<CostCode>, List<CostCodeDto>>(costCodes.OrderBy(s => s.Name).ToList());
            return Ok(costCodeDtoList);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var costCodes = await _unitOfWork.CostCodes.ToListAsync();
            
            var costCodeDtosList = _mapper.Map<List<CostCode>, List<CostCodeDto>>(costCodes.OrderBy(s => s.Name).ToList());
            return Ok(costCodeDtosList);
        }

        [HttpGet("list/{search}")]
        public async Task<IActionResult> GetList(string search)
        {

            var costCodes = await _unitOfWork.CostCodes.GetList( search);

            
            return Ok(costCodes);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var costCode = await _unitOfWork.CostCodes.SingleAsync(uid);
            var costCodeDto = _mapper.Map<CostCode, CostCodeDto>(costCode);
            return Ok(costCodeDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]CostCodeCreateViewModel input)
        {            
            var user = User.GetDetails();
            var costCode = new CostCode(input.Code, input.Name, input.BHEmpCode, input.BH, input.ADGroup, input.ADEmpCode, 
                                        input.Head, input.Status, user.Id);

            if (await _unitOfWork.CostCodes.AnyAsync(costCode))
            {
               return BadRequest("Cost Code Already Exists");
            }

            _unitOfWork.CostCodes.Add(costCode);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(costCode);
        }
           
    

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]CostCodeEditViewModel input)
        {
            var user = User.GetDetails();
            var costCode = await _unitOfWork.CostCodes.SingleAsync(uid);

            costCode.Update(input.Code, input.Name, input.BHEmpCode, input.BH,input.ADGroup, input.ADEmpCode, input.Head, 
                            input.Status, user.Id);
            if (await _unitOfWork.CostCodes.AnyAsync(costCode))
            {
                return BadRequest("Cost Code Already Exists");
            }

            _unitOfWork.CostCodes.Update(costCode);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(costCode);
        }

    }
}
