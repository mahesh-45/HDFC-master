using HDFC.Core.Dtos.Masters;
using System;
using System.Security.Claims;

namespace HDFC.Web.Helpers
{
    public static class UserExtensions
    { 

        public static UserDto GetDetails(this ClaimsPrincipal principal)
        {
            var user = new UserDto();
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));
            user.Id = long.Parse(principal.FindFirst(i => i.Type == "id")?.Value);
            user.Email = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            return user;
        }
    }

}
