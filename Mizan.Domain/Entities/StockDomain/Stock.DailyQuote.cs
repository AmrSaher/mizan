using Mizan.Domain.Enums;
using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.StockDomain
{
    // https://www.egx.com.eg/en/CompanyDetails.aspx?ISIN={isin}
    public sealed class StockDailyQuote : BaseEntity, IAggregateRoot
    {
        public long TradedVolume { get; private set; }
        public DateOnly CouponPaymentDate { get; private set; }
        public int CouponNumber { get; private set; }
        public DateOnly TradingDate { get; private set; }
        public decimal PriceEarningRatio { get; private set; }
        public decimal DividendYield { get; private set; }

        public Money TradedValue { get; private set; }
        public Money ClosingPrice { get; private set; }
        public Money CashDividends { get; private set; } // L.E./$
        public Money MarketCap { get; private set; }

        public Guid StockId { get; private set; }
        public Stock Stock { get; }

        private StockDailyQuote() { }

        public StockDailyQuote(
            long tradedVolume,
            long tradedValue,
            decimal closingPrice,
            decimal cashDividends,
            long marketCap,
            DateOnly couponPaymentDate,
            decimal priceEarningRatio,
            decimal dividendYield,
            int couponNumber,
            Currency currency,
            DateOnly tradingDate,
            Guid stockId
        )
        {
            SetTradedVolume(tradedVolume);
            SetTradedValue(tradedValue, currency);
            SetClosingPrice(closingPrice, currency);
            SetCashDividends(cashDividends, currency);
            SetMarketCap(marketCap, currency);
            SetCouponPaymentDate(couponPaymentDate);
            SetPriceEarningRatio(priceEarningRatio);
            SetDividendYield(dividendYield);
            SetCouponNumber(couponNumber);
            SetTradingDate(tradingDate);
            SetStockId(stockId);
        }

        public void SetTradedVolume(long tradedVolume)
        {
            TradedVolume = tradedVolume;
        }

        public void SetTradedValue(long tradedValue, Currency currency)
        {
            TradedValue = new Money(amount: tradedValue, currency: currency);
        }

        public void SetClosingPrice(decimal closingPrice, Currency currency)
        {
            ClosingPrice = new Money(amount: closingPrice, currency: currency);
        }

        public void SetCashDividends(decimal cashDividends, Currency currency)
        {
            CashDividends = new Money(amount: cashDividends, currency: currency);
        }

        public void SetMarketCap(long marketCap, Currency currency)
        {
            MarketCap = new Money(amount: marketCap, currency: currency);
        }

        public void SetCouponPaymentDate(DateOnly couponPaymentDate)
        {
            CouponPaymentDate = couponPaymentDate;
        }

        public void SetPriceEarningRatio(decimal priceEarningRatio)
        {
            PriceEarningRatio = priceEarningRatio;
        }

        public void SetDividendYield(decimal dividendYield)
        {
            DividendYield = dividendYield;
        }

        public void SetCouponNumber(int couponNumber)
        {
            CouponNumber = couponNumber;
        }

        public void SetTradingDate(DateOnly tradingDate)
        {
            TradingDate = tradingDate;
        }

        public void SetStockId(Guid stockId)
        {
            StockId = stockId;
        }
    }
}
