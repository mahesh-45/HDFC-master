using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Dtos.Masters.Vendor;
using HDFC.Core.Entities.Masters.Vendor;

namespace HDFC.Web.MappingProfiles.Masters
{
    public class VendorMappingProfile: Profile
    {
        public VendorMappingProfile()
        {
            CreateMap<Vendor, VendorDto>()
                .ForMember(dest => dest.VendorAttachments, opt => opt.MapFrom(x => x.VendorAttachments)).ReverseMap()
                .ForMember(dest => dest.VendorBankDetails, opt => opt.MapFrom(x => x.VendorBankDetails)).ReverseMap()
                .ForMember(dest => dest.VendorLocations, opt => opt.MapFrom(x => x.VendorLocations)).ReverseMap()
                .ForMember(dest => dest.VendorMSMEDetails, opt => opt.MapFrom(x => x.VendorMSMEDetails)).ReverseMap()
                .ForMember(dest => dest.VendorUsers, opt => opt.MapFrom(x => x.VendorUsers)).ReverseMap(); 

            CreateMap<VendorAttachment, VendorAttachmentDto>().ReverseMap();
            CreateMap<VendorBankDetail, VendorBankDetailDto>().ReverseMap();
            CreateMap<VendorLocation, VendorLocationDto>().ReverseMap();
            CreateMap<VendorMSMEDetail, VendorMSMEDetailDto>().ReverseMap();
            CreateMap<VendorUser, VendorUserDto>().ReverseMap();
        }
    }
}
