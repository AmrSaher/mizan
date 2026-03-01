using Microsoft.EntityFrameworkCore;
using Ardalis.Specification.EntityFrameworkCore;
using Mizan.Domain.Entities.StockDomain;
using Mizan.Domain.Repositories;

namespace Mizan.Infrastructure.Persistence.Repositories
{
    public sealed class StockDailyQuoteRepository : RepositoryBase<StockDailyQuote>, IStockDailyQuoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockDailyQuoteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StockDailyQuote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<StockDailyQuote>()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<StockDailyQuote>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<StockDailyQuote>()
                .ToListAsync(cancellationToken);
        }

        public void Add(StockDailyQuote quote)
        {
            _dbContext.Set<StockDailyQuote>().Add(quote);
        }

        public void Update(StockDailyQuote quote)
        {
            _dbContext.Set<StockDailyQuote>().Update(quote);
        }

        public void Delete(StockDailyQuote quote)
        {
            _dbContext.Set<StockDailyQuote>().Remove(quote);
        }
    }
}
