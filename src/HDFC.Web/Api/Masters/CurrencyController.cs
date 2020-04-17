using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.Currencies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class CurrencyController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurrencyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currencies = await _unitOfWork.Currencies.ActiveToListAsync();

            var currencyDtoList = _mapper.Map<List<Currency>, List<CurrencyDto>>(currencies.OrderBy(s => s.Name).ToList());
            return Ok(currencyDtoList);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var currencies = await _unitOfWork.Currencies.ToListAsync();

            var currencyDtosList = _mapper.Map<List<Currency>, List<CurrencyDto>>(currencies.OrderBy(s => s.Name).ToList());
            return Ok(currencyDtosList);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var currency = await _unitOfWork.Currencies.SingleAsync(uid);
            var currencyDto = _mapper.Map<Currency, CurrencyDto>(currency);
            return Ok(currencyDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]CurrencyCreateViewModel input)
        {
            var user = User.GetDetails();
            var currency = new Currency(input.Name,input.Code, input.Description, input.Status, user.Id);

            if (await _unitOfWork.Currencies.AnyAsync(currency))
            {
                return BadRequest("Currency Already Exists");
            }

            _unitOfWork.Currencies.Add(currency);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(currency);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]CurrencyEditViewModel input)
        {
            var user = User.GetDetails();
            var currency = await _unitOfWork.Currencies.SingleAsync(uid);

            currency.Update(input.Name, input.Code, input.Description, input.Status, user.Id);

            if (await _unitOfWork.Currencies.AnyAsync(currency))
            {
                return BadRequest("Currency Already Exists");
            }

            _unitOfWork.Currencies.Update(currency);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(currency);
        }
    }
}
