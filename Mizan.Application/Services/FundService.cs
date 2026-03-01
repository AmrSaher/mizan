using Mizan.Application.DTOs.FundDTOs;
using Mizan.Domain.Filters;
using Mizan.Domain.Repositories;
using Mizan.Domain.Specifications;

namespace Mizan.Application.Services
{
    public sealed class FundService
    {
        private readonly IFundRepository _fundRepo;

        public FundService(IFundRepository fundRepo)
        {
            _fundRepo = fundRepo;
        }

        public async Task<IEnumerable<FundReadDTO>> GetFunds()
        {
            var funds = await _fundRepo.GetAllAsync();

            if (funds == null || funds.Count() == 0)
            {
                return Enumerable.Empty<FundReadDTO>();
            }

            return funds.Select(FundReadDTO.FromEntity);
        }

        public async Task<FundReadDTO> GetFund(Guid id)
        {
            var fund = await _fundRepo.GetByIdAsync(id);

            if (fund == null)
            {
                throw new Exception("Not Found Exception");
            }

            return FundReadDTO.FromEntity(fund);
        }

        public async Task<IEnumerable<FundLookupDTO>> GetFundLookups(FundLookupsFilter filter)
        {
            var funds = await _fundRepo.ListAsync(new FundLookupsSpecification(filter));

            if (funds == null || funds.Count() == 0)
            {
                return Enumerable.Empty<FundLookupDTO>();
            }

            return funds.Select(FundLookupDTO.FromEntity);
        }
    }
}
