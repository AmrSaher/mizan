using Microsoft.EntityFrameworkCore;
using Ardalis.Specification.EntityFrameworkCore;
using Mizan.Domain.Entities.FundDomain;
using Mizan.Domain.Repositories;

namespace Mizan.Infrastructure.Persistence.Repositories
{
    public sealed class FundRepository : RepositoryBase<Fund>, IFundRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FundRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Fund?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Fund>()
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Fund>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Fund>()
                .ToListAsync(cancellationToken);
        }

        public void Add(Fund fund)
        {
            _dbContext.Set<Fund>().Add(fund);
        }

        public void Update(Fund fund)
        {
            _dbContext.Set<Fund>().Update(fund);
        }

        public void Delete(Fund fund)
        {
            _dbContext.Set<Fund>().Remove(fund);
        }
    }
}
