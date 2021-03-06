﻿using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.MappingProfiles.Masters
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
        }
    }
}
