using Ardalis.Specification;
using Mizan.Domain.Entities.FundDomain;

namespace Mizan.Domain.Repositories
{
    public interface IFundRepository : IRepositoryBase<Fund>
    {
        Task<Fund?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Fund>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(Fund fund);
        void Update(Fund fund);
        void Delete(Fund fund);
    }
}
