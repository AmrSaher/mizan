using Mizan.Domain.Enums;
using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.StockDomain
{
    public sealed record StockBasicData : IValueObject, IEquatable<StockBasicData>
    {
        public string ISINCode { get; private set; }
        public string ReutersCode { get; private set; }
        public DateOnly ListingDate { get; private set; }
        public long ListedShares { get; private set; }
        public decimal ParValue { get; private set; }
        public Currency Currency { get; private set; }
        public string SecurityType { get; private set; }
        public StockSector Sector { get; private set; }

        internal StockBasicData(
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
            SetISINCode(isinCode);
            SetReutersCode(reutersCode);
            SetListingDate(listingDate);
            SetListedShares(listedShares);
            SetParValue(parValue);
            SetCurrency(currency);
            SetSecurityType(securityType);
            SetSector(sector);
        }

        private void SetISINCode(string isinCode)
        {
            ISINCode = isinCode;
        }

        private void SetReutersCode(string reutersCode)
        {
            ReutersCode = reutersCode;
        }

        private void SetListingDate(DateOnly listingDate)
        {
            ListingDate = listingDate;
        }

        private void SetListedShares(long listedShares)
        {
            ListedShares = listedShares;
        }

        private void SetParValue(decimal parValue)
        {
            ParValue = parValue;
        }

        private void SetCurrency(Currency currency)
        {
            Currency = currency;
        }

        private void SetSecurityType(string securityType)
        {
            SecurityType = securityType;
        }

        private void SetSector(StockSector sector)
        {
            Sector = sector;
        }
    }
}
