using Mizan.Domain.Entities.StockDomain;

namespace Mizan.Domain.Repositories
{
    public interface IStockDailyQuoteRepository
    {
        Task<StockDailyQuote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockDailyQuote>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(StockDailyQuote quote);
        void Update(StockDailyQuote quote);
        void Delete(StockDailyQuote quote);
    }
}
