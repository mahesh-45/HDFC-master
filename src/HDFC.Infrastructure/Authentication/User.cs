using Microsoft.AspNetCore.Identity;

namespace HDFC.Infrastructure.Authentication
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
