using BlossomAvenue.Infrastrcture.Database;
using BlossomAvenue.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlossomAvenue.Infrastrcture.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlossomAvenueDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
