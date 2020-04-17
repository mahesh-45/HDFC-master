using System.Collections.Generic;

namespace HDFC.Core.Dtos.Masters
{
    public class UserDto : BaseEntityDto
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<RoleDto> Roles { get; set; }
    }

}
