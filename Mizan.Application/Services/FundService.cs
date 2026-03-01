using Mizan.Application.DTOs.FundDTOs;
using Mizan.Domain.Repositories;

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
    }
}
