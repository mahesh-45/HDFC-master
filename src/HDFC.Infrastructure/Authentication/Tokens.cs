using HDFC.Core.Dtos.Masters;
using HDFC.Core.Interfaces;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Authentication
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions,
            JsonSerializerSettings serializerSettings, UserDto user)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                token = await jwtFactory.GenerateEncodedToken(user, identity),
                expiry = (int)jwtOptions.ValidFor.TotalSeconds,
                userDisplayName = user.FirstName + " " + user.LastName,
            };


            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
