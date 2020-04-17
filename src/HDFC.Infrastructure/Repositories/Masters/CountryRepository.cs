using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Repositories.Masters;
using HDFC.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories.Masters
{
    public class CountryRepository : ActiveRepository<Country>, ICountryRepository
    {
        public CountryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync(Country country)
        {
            if (country.Id > 0)
            {
                return await _dbContext.Countries.AnyAsync(c => c.Id != country.Id && c.Name == country.Name);
            }
            else
            {
                return await _dbContext.Countries.AnyAsync(c => c.Name == country.Name);
            }
        }

        public async Task<CountryListDto> GetList(string sort, string order, int page, int pageSize, string search)
        {
            try
            {
                CountryListDto res = new CountryListDto();

                var SkipPage = page * pageSize;

                List<CountryDto> countries = await (from a in _dbContext.Countries
                                                    where (search != "null" ? (a.Name.Contains(search) || a.Code.Contains(search)) : true)
                                                    select new CountryDto()
                                                    {
                                                        Code = a.Code,
                                                        Name = a.Name,
                                                        Status = a.Status,
                                                        CreatedDate = a.CreatedDate,
                                                        Id = a.Id,
                                                        Uid = a.Uid

                                                    }).ToListAsync();
                res.Total_count = countries.Count();
                switch (sort + "_" + order)
                {
                    case "name_desc":
                        res.Items = countries.Skip(SkipPage).Take(pageSize).OrderByDescending(s => s.Name).ToList();
                        break;
                    case "name_asc":
                        res.Items = countries.Skip(SkipPage).Take(pageSize).OrderBy(s => s.Name).ToList();
                        break;
                    case "code_desc":
                        res.Items = countries.Skip(SkipPage).Take(pageSize).OrderByDescending(s => s.Code).ToList();
                        break;
                    case "code_asc":
                        res.Items = countries.Skip(SkipPage).Take(pageSize).OrderBy(s => s.Code).ToList();
                        break;
                    case "createdDate_desc":
                        res.Items = countries.Skip(SkipPage).Take(pageSize).OrderByDescending(s => s.CreatedDate).ToList();
                        break;
                    case "createdDate_asc":
                        res.Items = countries.Skip(SkipPage).Take(pageSize).OrderBy(s => s.CreatedDate).ToList();
                        break;
                }

                //sort = char.ToUpper(sort[0]) + sort.Substring(1);
                //if (order == "asc")
                //{
                //    Accounts.Items = query.Skip(SkipPage).Take(100).OrderBy(x => x.GetType().GetProperty(sort).GetValue(x, null)).ToList();
                //}
                //else
                //{
                //    Accounts.Items = query.Skip(SkipPage).Take(100).OrderByDescending(x => x.GetType().GetProperty(sort).GetValue(x, null)).ToList();
                //}

                return res;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
