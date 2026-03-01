using Microsoft.EntityFrameworkCore;
using Mizan.Domain.Entities.StockDomain;
using Mizan.Domain.Repositories;

namespace Mizan.Infrastructure.Persistence.Repositories
{
    public sealed class StockIRContactRepository : IStockIRContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockIRContactRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StockIRContact?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<StockIRContact>()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<StockIRContact>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<StockIRContact>()
                .ToListAsync(cancellationToken);
        }

        public void Add(StockIRContact contact)
        {
            _dbContext.Set<StockIRContact>().Add(contact);
        }

        public void Update(StockIRContact contact)
        {
            _dbContext.Set<StockIRContact>().Update(contact);
        }

        public void Delete(StockIRContact contact)
        {
            _dbContext.Set<StockIRContact>().Remove(contact);
        }
    }
}
