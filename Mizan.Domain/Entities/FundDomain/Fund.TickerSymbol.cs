using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.FundDomain
{
    public sealed record FundTickerSymbol : IValueObject, IEquatable<FundTickerSymbol>
    {
        public string? Value { get; set; }

        private FundTickerSymbol() { }

        internal FundTickerSymbol(string? value)
        {
            SetValue(value);
        }

        private void SetValue(string? value)
        {
            Value = value;
        }
    }
}
