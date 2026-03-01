using Mizan.Application.Primitives;
using Mizan.Domain.Entities.FundDomain;
using Mizan.Domain.Enums;

namespace Mizan.Application.DTOs.FundDTOs
{
    public sealed class FundListDTO : IInquiryDTO<Fund, FundListDTO>
    {
        public string Name { get; set; }
        public FundType Type { get; set; }
        public FundProvider Provider { get; set; }
        public decimal NAV_Amount { get; set; }
        public Currency NAV_Currency { get; set; }
        public string RIC_Ticker { get; set; }
        public string BBG_Ticker { get; set; }
        public FundFrequency? SubscriptionFrequency { get; set; }
        public FundFrequency? RedemptionFrequency { get; set; }

        public static FundListDTO FromEntity(Fund entity)
        {
            return new FundListDTO
            {
                Name = entity.Name,
                Type = entity.Type,
                Provider = entity.Provider,
                NAV_Amount = entity.NAV.Amount,
                NAV_Currency = entity.NAV.Currency,
                RIC_Ticker = entity.RICTicker.Value,
                BBG_Ticker = entity.BBGTicker.Value,
                SubscriptionFrequency = entity.Liquidity.SubscriptionFrequency,
                RedemptionFrequency = entity.Liquidity.RedemptionFrequency
            };
        }
    }
}
