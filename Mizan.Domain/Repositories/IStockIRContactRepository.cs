using Ardalis.Specification;
using Mizan.Domain.Entities.StockDomain;

namespace Mizan.Domain.Repositories
{
    public interface IStockIRContactRepository : IRepositoryBase<StockIRContact>
    {
        Task<StockIRContact?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<StockIRContact>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(StockIRContact contact);
        void Update(StockIRContact contact);
        void Delete(StockIRContact contact);
    }
}
