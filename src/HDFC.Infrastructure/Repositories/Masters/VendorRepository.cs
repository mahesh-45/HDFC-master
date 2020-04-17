using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using HDFC.Core.Dtos.Masters.Vendor;
using HDFC.Core.Entities.Masters.Vendor;
using HDFC.Infrastructure.Persistence;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HDFC.Infrastructure.Repositories.Masters
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        public VendorRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<List<Vendor>> ActiveToListAsync()
        {
            return await _dbContext.Set<Vendor>().Where(s => s.VendorStatus == Core.Enums.VendorStatuEnum.Active).ToListAsync();
        }

        public async Task<bool> AnyAsync(Vendor vendor)
        {
            if (vendor.Id > 0)
            {
                return await _dbContext.Vendors.AnyAsync(c =>
                    c.Id != vendor.Id && c.Name == vendor.Name && c.ContactPerson == vendor.ContactPerson);
            }
            else
            {
                return await _dbContext.Vendors.AnyAsync(c =>
                    c.Name == vendor.Name && c.ContactPerson == vendor.ContactPerson);
            }
        }

        public async Task<VendorListDto> GetList(string sort, string order, int page, int pageSize, string search)
        {
            try
            {
                VendorListDto res = new VendorListDto();
                var SkipPage = page * pageSize;

                List<VendorDto> vendors = await (from a in _dbContext.Vendors
                                                 where (search != null ? (a.Name.Contains(search) || a.ContactPerson.Contains(search)) : true)
                                                 select new VendorDto()
                                                 {
                                                     Name = a.Name,
                                                     ContactPerson = a.ContactPerson,
                                                     IsCompositeTaxable = a.IsCompositeTaxable,
                                                     ProprietorName = a.ProprietorName,
                                                     Status = a.Status,
                                                 }
                            ).ToListAsync();
                res.Total_count = vendors.Count();
                switch (sort + "_" + order)
                {
                    case "name_desc":
                        res.Items = vendors.Skip(SkipPage).Take(pageSize).OrderByDescending(s => s.Name).ToList();
                        break;
                    case "name_asc":
                        res.Items = vendors.Skip(SkipPage).Take(pageSize).OrderBy(s => s.Name).ToList();
                        break;
                    case "createdDate_desc":
                        res.Items = vendors.Skip(SkipPage).Take(pageSize).OrderByDescending(s => s.CreatedDate).ToList();
                        break;
                    case "createdDate_asc":
                        res.Items = vendors.Skip(SkipPage).Take(pageSize).OrderBy(s => s.CreatedDate).ToList();
                        break;
                }

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
