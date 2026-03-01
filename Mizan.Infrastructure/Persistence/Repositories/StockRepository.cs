using Microsoft.EntityFrameworkCore;
using Mizan.Domain.Entities.StockDomain;
using Mizan.Domain.Repositories;

namespace Mizan.Infrastructure.Persistence.Repositories
{
    public sealed class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Stock?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Stock>()
                .Include(s => s.DailyQuotes)
                .Include(s => s.IRContacts)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Stock>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Stock>()
                .Include(s => s.DailyQuotes)
                .Include(s => s.IRContacts)
                .ToListAsync(cancellationToken);
        }

        public void Add(Stock stock)
        {
            _dbContext.Set<Stock>().Add(stock);
        }

        public void Update(Stock stock)
        {
            _dbContext.Set<Stock>().Update(stock);
        }

        public void Delete(Stock stock)
        {
            _dbContext.Set<Stock>().Remove(stock);
        }
    }
}
