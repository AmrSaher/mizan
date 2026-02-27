using Mizan.Domain.Enums;
using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.StockDomain
{
    public sealed class Stock : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public StockBasicData BasicData { get; private set; }

        private readonly List<StockDailyQuote> _dailyQuotes = new();
        public IReadOnlyCollection<StockDailyQuote> DailyQuotes => _dailyQuotes.AsReadOnly();

        private readonly List<StockIRContact> _irContacts = new();
        public IReadOnlyCollection<StockIRContact> IRContacts => _irContacts.AsReadOnly();

        public Stock(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetBasicData(
            string isinCode,
            string reutersCode,
            DateOnly listingDate,
            long listedShares,
            decimal parValue,
            string currency,
            string securityType,
            StockSector sector
        )
        {
            BasicData = new StockBasicData(
                isinCode: isinCode,
                reutersCode: reutersCode,
                listingDate: listingDate,
                listedShares: listedShares,
                parValue: parValue,
                currency: currency,
                securityType: securityType,
                sector: sector
            );
        }
    }
}
