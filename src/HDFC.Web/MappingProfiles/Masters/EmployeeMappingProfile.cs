using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Dtos.Masters.Employee;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Entities.Masters.Employee;

namespace HDFC.Web.MappingProfiles.Masters
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();

        }
    }
}
