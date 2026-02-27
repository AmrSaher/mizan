using Mizan.Domain.Primitives;

namespace Mizan.Domain.Entities.StockDomain
{
    public sealed class StockDailyQuote : BaseEntity, IAggregateRoot
    {
        public long TradedVolume { get; private set; }
        public long TradedValue { get; private set; }
        public decimal ClosingPrice { get; private set; }
        public decimal CashDividends { get; private set; } // L.E./$
        public long MarketCap { get; private set; }
        public DateOnly CouponPaymentDate { get; private set; }
        public decimal PriceEarningRatio { get; private set; }
        public decimal DividendYield { get; private set; }
        public int CouponNumber { get; private set; }
        public DateOnly TradingDate { get; private set; }

        public Guid StockId { get; private set; }
        public Stock Stock { get; }

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
            DateOnly tradingDate,
            Guid stockId
        )
        {
            SetTradedVolume(tradedVolume);
            SetTradedValue(tradedValue);
            SetClosingPrice(closingPrice);
            SetCashDividends(cashDividends);
            SetMarketCap(marketCap);
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

        public void SetTradedValue(long tradedValue)
        {
            TradedValue = tradedValue;
        }

        public void SetClosingPrice(decimal closingPrice)
        {
            ClosingPrice = closingPrice;
        }

        public void SetCashDividends(decimal cashDividends)
        {
            CashDividends = cashDividends;
        }

        public void SetMarketCap(long marketCap)
        {
            MarketCap = marketCap;
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
