using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    //[AllowAnonymous]
    public class CountriesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await _unitOfWork.Countries.ActiveToListAsync();
            if (countries.Count == 0)
                return Ok(null);
            var countryDtosList = _mapper.Map<List<Country>, List<CountryDto>>(countries.OrderBy(s => s.Name).ToList());
            return Ok(countryDtosList);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _unitOfWork.Countries.ToListAsync();
            if (countries.Count == 0)
                return Ok(null);
            var countryDtosList = _mapper.Map<List<Country>, List<CountryDto>>(countries.OrderBy(s => s.Name).ToList());
            return Ok(countryDtosList);
        }

        [HttpGet("list/{sort}/{order}/{page}/{pageSize}/{search}")]
        public async Task<IActionResult> GetList(string sort, string order, int page, int pageSize, string search)
        {
            var countries = await _unitOfWork.Countries.GetList(sort, order, page, pageSize, search);
            if (countries == null)
                return Ok(null);
            return Ok(countries);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var country = await _unitOfWork.Countries.SingleAsync(uid);
            var countryDto = _mapper.Map<Country, CountryDto>(country);
            return Ok(countryDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]CountryDto input)
        {
            var user = User.GetDetails();
            var country = new Country(input.Name, input.Code, input.Status, user.Id);
            if (await _unitOfWork.Countries.AnyAsync(country))
            {
                return BadRequest("Country Already Exists");
            }

            //This is a sample line of code

            _unitOfWork.Countries.Add(country);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(country);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]CountryDto input)
        {
            var user = User.GetDetails();
            var country = await _unitOfWork.Countries.SingleAsync(uid);
            country.Update(input.Name, input.Code, input.Status, user.Id);

            if (await _unitOfWork.Countries.AnyAsync(country))
            {
                return BadRequest("Country Already Exists.");
            }

            _unitOfWork.Countries.Update(country);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(country);
        }
    }
}
