using Mizan.Application.DTOs.FundDTOs;
using Mizan.Application.Responses;
using Mizan.Domain.Exceptions;
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

        public async Task<PaginatedResponse<IEnumerable<FundReadDTO>>> GetFunds(FundsFilter filter)
        {
            var funds = await _fundRepo.ListAsync(new FundsSpecification(filter));
            var count = await _fundRepo.CountAsync();

            if (funds == null || !funds.Any())
            {
                return PaginatedResponse<IEnumerable<FundReadDTO>>.Success(Enumerable.Empty<FundReadDTO>(), 0, filter.Take!.Value, filter.Skip!.Value);
            }

            return PaginatedResponse<IEnumerable<FundReadDTO>>.Success(funds.Select(FundReadDTO.FromEntity), count, filter.Take!.Value, filter.Skip!.Value);
        }

        public async Task<BaseResponse<FundReadDTO>> GetFund(Guid id)
        {
            var fund = await _fundRepo.GetByIdAsync(id);

            if (fund == null)
            {
                throw new NotFoundException("Fund not found.");
            }

            return BaseResponse<FundReadDTO>.Success(FundReadDTO.FromEntity(fund));
        }

        public async Task<PaginatedResponse<IEnumerable<FundLookupDTO>>> GetFundLookups(FundLookupsFilter filter)
        {
            var funds = await _fundRepo.ListAsync(new FundLookupsSpecification(filter));
            var count = await _fundRepo.CountAsync();

            if (funds == null || !funds.Any())
            {
                return PaginatedResponse<IEnumerable<FundLookupDTO>>.Success(Enumerable.Empty<FundLookupDTO>(), 0, filter.Take!.Value, filter.Skip!.Value);
            }

            return PaginatedResponse<IEnumerable<FundLookupDTO>>.Success(funds.Select(FundLookupDTO.FromEntity), count, filter.Take!.Value, filter.Skip!.Value);
        }
    }
}
