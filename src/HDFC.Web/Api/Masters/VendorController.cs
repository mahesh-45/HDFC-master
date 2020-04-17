using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HDFC.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Dtos.Masters.Employee;
using HDFC.Core.Dtos.Masters.Vendor;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Entities.Masters.Employee;
using HDFC.Core.Entities.Masters.Vendor;
using HDFC.Web.Helpers;
using HDFC.Web.ViewModels.Masters.Vendor;

namespace HDFC.Web.Api.Masters
{
    public class VendorController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VendorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vendors = await _unitOfWork.Vendors.ActiveToListAsync();
            if (vendors.Count == 0)
                return Ok(null);
            var vendorDtosList = _mapper.Map<List<Vendor>, List<VendorDto>>(vendors.OrderBy(s => s.Name).ToList());
            return Ok(vendorDtosList);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await _unitOfWork.Vendors.ActiveToListAsync();
            if (vendors.Count == 0)
                return Ok(null);
            var vendorDtosList = _mapper.Map<List<Vendor>, List<VendorDto>>(vendors.OrderBy(s => s.Name).ToList());
            return Ok(vendorDtosList);
        }

        [HttpGet("list/{sort}/{order}/{page}/{pageSize}/{search}")]
        public async Task<IActionResult> GetList(string sort, string order, int page, int pageSize, string search)
        {
            var vendors = await _unitOfWork.Vendors.GetList(sort, order, page, pageSize, search);
            if (vendors.Total_count == 0)
                return Ok(null);
            return Ok(vendors);
        }
        [HttpGet("{uid}")]
        public async Task<IActionResult> Get(string uid)
        {
            var vendor = await _unitOfWork.Vendors.SingleAsync(uid);
            var vendorDto = _mapper.Map<Vendor, VendorDto>(vendor);
            return Ok(vendorDto);
        }
        [HttpPost("invite")]
        [ValidateModel]

        public async Task<IActionResult> InviteVendor([FromBody]VendorInvitationViewModel input)
        {
            var user = User.GetDetails();
            var vendorInvitation = new Vendor(input.Name, input.VendorType, user.Id);
            var vendorUsers = new List<VendorUser>() { new VendorUser(input.Email, " ", "") };
            vendorInvitation.AddVendorUsers(vendorUsers, user.Id);

            if (await _unitOfWork.Vendors.AnyAsync(vendorInvitation))
            {
                return BadRequest("Vendor Already Exists.");
            }

            _unitOfWork.Vendors.Add(vendorInvitation);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(vendorInvitation);
        }
        [HttpPost("registration")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]VendorCreateViewModel input)
        {
            var user = User.GetDetails();
            var data = _mapper.Map<VendorCreateViewModel, Vendor>(input);
            var vendor = new Vendor(input.Name, input.ContactPerson, true, input.Status, input.ProprietorName, user.Id);

            vendor.SetContactDetailsAndIdentity(input.MobileNo, input.LandlineNo, input.PANNo, input.PANHolderName, input.AadharNo);
            vendor.SetVendorTypeAndStatus(input.VendorType, input.VendorStatus);
            vendor.AddVendorUsers(data.VendorUsers.ToList(), user.Id);
            if (await _unitOfWork.Vendors.AnyAsync(vendor))
            {
                return BadRequest("Vendor Already Exists");
            }

            _unitOfWork.Vendors.Add(vendor);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(vendor);
        }

        [HttpPut("{uid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(string uid, [FromBody]VendorEditViewModel input)
        {
            var user = User.GetDetails();
            var vendor = await _unitOfWork.Vendors.SingleAsync(input.Uid);
            vendor.Update(input.Name, input.ContactPerson, input.IsCompositeTaxable, input.Status, input.ProprietorName, user.Id);
            vendor.SetContactDetailsAndIdentity(input.MobileNo, input.LandlineNo, input.PANNo, input.PANHolderName, input.AadharNo);
            vendor.SetVendorTypeAndStatus(input.VendorType, input.VendorStatus);

            if (await _unitOfWork.Vendors.AnyAsync(vendor))
            {
                return BadRequest("Vendor Already Exists.");
            }

            _unitOfWork.Vendors.Update(vendor);
            await _unitOfWork.CompleteAsync(user.Id);
            return Ok(vendor);
        }
    }
}
