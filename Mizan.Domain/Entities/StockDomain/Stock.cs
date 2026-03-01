using Mizan.Domain.Enums;
using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.StockDomain
{
    // https://www.egx.com.eg/en/ListedStocks.aspx
    public sealed class Stock : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public StockProvider Provider { get; private set; }

        public StockBasicData BasicData { get; private set; }

        private readonly List<StockDailyQuote> _dailyQuotes = new();
        public IReadOnlyCollection<StockDailyQuote> DailyQuotes => _dailyQuotes.AsReadOnly();

        private readonly List<StockIRContact> _irContacts = new();
        public IReadOnlyCollection<StockIRContact> IRContacts => _irContacts.AsReadOnly();

        private Stock() { }

        public Stock(string name, StockProvider provider)
        {
            SetName(name);
            SetProvider(provider);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetProvider(StockProvider provider)
        {
            Provider = provider;
        }

        public void SetBasicData(
            string isinCode,
            string reutersCode,
            DateOnly listingDate,
            long listedShares,
            decimal parValue,
            Currency currency,
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
