using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlossomAvenue.Service.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        Task CommitAsync();
        void Rollback();
    }
}
