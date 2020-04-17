using HDFC.Core.Interfaces;
using HDFC.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories
{
    public interface IActiveRepository<T> : IRepository<T> where T : AggregateRoot, IStatus
    {
        Task<List<T>> ActiveToListAsync();
    }
}
