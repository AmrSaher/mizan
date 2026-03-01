using Mizan.Domain.Enums;
using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.FundDomain
{
    // https://azimut.eg/funds
    // https://www.beltoneholding.com/the-investment-bank/asset-management
    // https://efgholding.com/en/our-services/mutual-funds
    // https://www.cicapital.com/fundprice
    public sealed class Fund : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public FundType Type { get; private set; }
        public FundProvider Provider { get; private set; }

        public Money NAV { get; private set; }

        public FundTickerSymbol? RICTicker { get; private set; }
        public FundTickerSymbol? BBGTicker { get; private set; }
        
        public FundLiquidityProfile? Liquidity { get; private set; }

        private Fund() { }

        public Fund(
            string name,
            FundType type,
            FundProvider provider,
            decimal navAmount,
            Currency navCurrency,
            string? ricTicker,
            string? bbgTicker,
            FundFrequency? subscriptionFrequency,
            FundFrequency? redemptionFrequency
        )
        {
            SetName(name);
            SetType(type);
            SetProvider(provider);
            SetNAV(amount: navAmount, currency: navCurrency);
            SetRIC(ricTicker);
            SetBBGTicker(bbgTicker);
            SetLiquidityProfile(subscriptionFrequency: subscriptionFrequency, redemptionFrequency: redemptionFrequency);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetType(FundType type)
        {
            Type = type;
        }

        public void SetProvider(FundProvider provider)
        {
            Provider = provider;
        }

        public void SetNAV(decimal amount, Currency currency)
        {
            NAV = new Money(amount: amount, currency: currency);
        }

        public void SetRIC(string? ricTicker)
        {
            RICTicker = new FundTickerSymbol(value: ricTicker);
        }

        public void SetBBGTicker(string? bbgTicker)
        {
            BBGTicker = new FundTickerSymbol(value: bbgTicker);
        }

        public void SetLiquidityProfile(FundFrequency? subscriptionFrequency, FundFrequency? redemptionFrequency)
        {
            Liquidity = new FundLiquidityProfile(subscriptionFrequence: subscriptionFrequency, redemptionFrequency: redemptionFrequency);
        }
    }
}
