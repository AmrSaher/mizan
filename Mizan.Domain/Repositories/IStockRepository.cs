using Ardalis.Specification;
using Mizan.Domain.Entities.StockDomain;

namespace Mizan.Domain.Repositories
{
    public interface IStockRepository : IRepositoryBase<Stock>
    {
        Task<Stock?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Stock>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(Stock stock);
        void Update(Stock stock);
        void Delete(Stock stock);
    }
}
