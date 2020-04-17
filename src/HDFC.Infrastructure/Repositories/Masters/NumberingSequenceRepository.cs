using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using HDFC.Core.Enums;

namespace HDFC.Infrastructure.Repositories.Masters
{
    public class NumberingSequenceRepository : ActiveRepository<NumberingSequence>, INumberingSequenceRepository
    {
        public NumberingSequenceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync(NumberingSequence numberingSequence)
        {
            if (numberingSequence.Id > 0)
            {
                return await _dbContext.NumberingSequences.AnyAsync(c => c.Id != numberingSequence.Id && c.Prefix == numberingSequence.Prefix);
            }
            else
            {
                return await _dbContext.NumberingSequences.AnyAsync(c => c.Prefix == numberingSequence.Prefix);
            }
        }

        public async Task<NumberingSequenceListDto> GetList(SearchDto searchDto)
        {
            NumberingSequenceListDto res = new NumberingSequenceListDto();

            var SkipPage = searchDto.Page * searchDto.PageSize;

            List<NumberingSequenceDto> numberingSequences = await (from c in _dbContext.NumberingSequences
                                                                   where (searchDto.Search != null ? (c.Prefix.Contains(searchDto.Search)) : true)
                                                                   select new NumberingSequenceDto()
                                                                   {
                                                                       Prefix = c.Prefix,
                                                                       Number = c.Number,
                                                                       NumberingSequenceType = c.NumberingSequenceType,
                                                                       CreatedDate = c.CreatedDate,
                                                                       Status = c.Status,
                                                                       Id = c.Id,
                                                                       Uid = c.Uid

                                                                   }).OrderBy(a => a.Prefix).ToListAsync();
            res.Total_count = numberingSequences.Count();

            return res;
        }

        public async Task<string> GenerateReferenceId(NumberingSequenceTypeEnum numberingSequenceType)
        {
            var numberingSequence = await _dbContext.NumberingSequences.SingleOrDefaultAsync(e => e.NumberingSequenceType == numberingSequenceType);
            if (numberingSequence != null)
            {
                string Number = Convert.ToString(Convert.ToInt32(numberingSequence.Number) + 1);
                numberingSequence.Number = Number;
                return numberingSequence.Prefix + '-' + Number;
            }
            else
            {
                return null;
            }
        }
    }
}
