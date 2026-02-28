using Mizan.Domain.Enums;
using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities
{
    public sealed record Money : IValueObject, IEquatable<Money>
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        internal Money(decimal amount, Currency currency)
        {
            SetAmount(amount);
            SetCurrency(currency);
        }

        private void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        private void SetCurrency(Currency currency)
        {
            Currency = currency;
        }
    }
}
