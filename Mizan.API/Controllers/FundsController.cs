using Microsoft.AspNetCore.Mvc;
using Mizan.Application.DTOs.FundDTOs;
using Mizan.Application.Responses;
using Mizan.Application.Services;
using Mizan.Domain.Filters;

namespace Mizan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundsController : ControllerBase
    {
        private readonly FundService _fundService;

        public FundsController(FundService fundService)
        {
            _fundService = fundService;
        }

        [HttpGet]
        public async Task<BaseResponse<IEnumerable<FundReadDTO>>> GetFunds()
        {
            var result = await _fundService.GetFunds();

            return BaseResponse<IEnumerable<FundReadDTO>>.Success(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<BaseResponse<FundReadDTO>> GetFund([FromRoute] Guid id)
        {
            var result = await _fundService.GetFund(id);

            return BaseResponse<FundReadDTO>.Success(result);
        }

        [HttpGet("lookup")]
        public async Task<BaseResponse<IEnumerable<FundLookupDTO>>> GetFundLookups([FromQuery] FundLookupsFilter filter)
        {
            var result = await _fundService.GetFundLookups(filter);

            return BaseResponse<IEnumerable<FundLookupDTO>>.Success(result);
        }
    }
}
