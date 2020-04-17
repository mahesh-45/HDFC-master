using AutoMapper;
using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories;
using HDFC.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.Api.Masters
{
    public class NumberingSequencesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NumberingSequencesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var numberingSequences = await _unitOfWork.NumberingSequences.ActiveToListAsync();
            if (numberingSequences.Count == 0)
                return Ok(null);
            var numberingSequenceDtoList = _mapper.Map<List<NumberingSequence>, List<NumberingSequenceDto>>(numberingSequences.OrderBy(s => s.Prefix).ToList());
            return Ok(numberingSequenceDtoList);
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetList([FromBody]SearchDto searchDto)
        {
            var numberingSequences = await _unitOfWork.NumberingSequences.GetList(searchDto);
            return Ok(numberingSequences);
        }

        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var numberingSequence = await _unitOfWork.NumberingSequences.SingleAsync(uid);
            var numberingSequenceDto = _mapper.Map<NumberingSequence, NumberingSequenceDto>(numberingSequence);
            return Ok(numberingSequenceDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]NumberingSequenceDto input)
        {
            var user = User.GetDetails();
            var numberingSequence = new NumberingSequence(input.Prefix, input.Number, input.Status, input.NumberingSequenceType, user.Id);
            if (await _unitOfWork.NumberingSequences.AnyAsync(numberingSequence))
            {
                return BadRequest("Numbering Sequence Already Exists");
            }

            _unitOfWork.NumberingSequences.Add(numberingSequence);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(numberingSequence);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]NumberingSequenceDto input)
        {
            var user = User.GetDetails();
            var numberingSequence = await _unitOfWork.NumberingSequences.SingleAsync(input.Uid);
            numberingSequence.Update(input.Prefix, input.Number, input.Status, input.NumberingSequenceType, user.Id);

            if (await _unitOfWork.NumberingSequences.AnyAsync(numberingSequence))
            {
                return BadRequest("Numbering Sequence Already Exists");
            }

            _unitOfWork.NumberingSequences.Update(numberingSequence);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(numberingSequence);
        }
    }
}
