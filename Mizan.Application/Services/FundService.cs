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

        public async Task<IEnumerable<FundListDTO>> GetFunds()
        {
            var funds = await _fundRepo.GetAllAsync();

            if (funds == null || funds.Count() == 0)
            {
                return Enumerable.Empty<FundListDTO>();
            }

            return funds.Select(FundListDTO.FromEntity);
        }
    }
}
