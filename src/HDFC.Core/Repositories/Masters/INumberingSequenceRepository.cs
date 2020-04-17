using HDFC.Core.Dtos.Common;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Enums;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories.Masters
{
    public interface INumberingSequenceRepository : IActiveRepository<NumberingSequence>
    {
        Task<bool> AnyAsync(NumberingSequence numberingSequence);
        Task<NumberingSequenceListDto> GetList(SearchDto searchDto);
        Task<string> GenerateReferenceId(NumberingSequenceTypeEnum numberingSequenceType);
    }
}
