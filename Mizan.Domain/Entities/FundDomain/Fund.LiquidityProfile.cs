using Mizan.Domain.Enums;
using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.FundDomain
{
    public sealed record FundLiquidityProfile : IValueObject, IEquatable<FundLiquidityProfile>
    {
        public FundFrequency? SubscriptionFrequency { get; private set; }
        public FundFrequency? RedemptionFrequency { get; private set; }

        internal FundLiquidityProfile(FundFrequency? subscriptionFrequence, FundFrequency? redemptionFrequency)
        {
            SetSubscriptionFrequency(subscriptionFrequence);
            SetRedemptionFrequency(redemptionFrequency);
        }

        private void SetSubscriptionFrequency(FundFrequency? subscriptionFrequence)
        {
            SubscriptionFrequency = subscriptionFrequence;
        }

        private void SetRedemptionFrequency(FundFrequency? redemptionFrequency)
        {
            RedemptionFrequency = redemptionFrequency;
        }
    }
}
