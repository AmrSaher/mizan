using Mizan.Application.Primitives;
using Mizan.Domain.Entities.FundDomain;
using Mizan.Domain.Enums;

namespace Mizan.Application.DTOs.FundDTOs
{
    public sealed class FundLookupDTO : IInquiryDTO<Fund, FundLookupDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public FundProvider Provider { get; set; }

        public static FundLookupDTO FromEntity(Fund entity)
        {
            return new FundLookupDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Provider = entity.Provider
            };
        }
    }
}
