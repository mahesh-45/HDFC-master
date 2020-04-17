using HDFC.Core.Dtos.Masters;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HDFC.Core.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(UserDto userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
