using Microsoft.Playwright;
using Mizan.Domain.Entities.FundDomain;
using Mizan.Domain.Enums;
using Mizan.Domain.Repositories;

namespace Mizan.Infrastructure.Scraper.Scrapers
{
    public class AzimutScraper
    {
        private readonly IFundRepository _fundRepo;

        public AzimutScraper(IFundRepository fundRepo)
        {
            _fundRepo = fundRepo;
        }

        public async Task Run()
        {
            using var playwright = await Playwright.CreateAsync();

            // Launch with specific args to hide automation
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] {
                    "--disable-blink-features=AutomationControlled",
                    "--no-sandbox"
                }
            });

            // Set a very specific User-Agent and Viewport
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
            });

            var page = await context.NewPageAsync();

            // Inject a script to "delete" the webdriver property
            await page.AddInitScriptAsync("delete Object.getPrototypeOf(navigator).webdriver");

            await page.GotoAsync("https://azimut.eg/funds", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle // Wait for all JS to finish
            });

            // Wait for a specific element to load (crucial for JS sites)
            await page.WaitForSelectorAsync(".funds-cont");

            var funds = await page.Locator(".row.zeromargin.graybg.mtop35.hideinmedia .col-12.fundspadding div.ng-star-inserted").AllAsync();

            foreach (var fund in funds)
            {
                var name = (await fund.Locator(".row.zeromargin.top5.rowalign .col-6.zeropadding .row.zeromargin.rowalign div:nth-child(1) p").InnerTextAsync()).Trim();
                var currency = (await fund.Locator(".row.zeromargin.top5.rowalign .col-6.zeropadding .row.zeromargin.rowalign div:nth-child(2) p").InnerTextAsync()).Trim();
                var type = (await fund.Locator(".row.zeromargin.top5.rowalign .col-6.zeropadding .row.zeromargin.rowalign div:nth-child(3) p").InnerTextAsync()).Trim();
                var ric = (await fund.Locator(".row.zeromargin.top5.rowalign .col-6.zeropadding .row.zeromargin.rowalign div:nth-child(4) p").InnerTextAsync()).Trim();
                var bbg = (await fund.Locator(".row.zeromargin.top5.rowalign .col-4.zeropadding .row.zeromargin.rowalign div:nth-child(1) p").InnerTextAsync()).Trim();
                var subscription = (await fund.Locator(".row.zeromargin.top5.rowalign .col-4.zeropadding .row.zeromargin.rowalign div:nth-child(2) p").InnerTextAsync()).Trim();
                var redemption = (await fund.Locator(".row.zeromargin.top5.rowalign .col-4.zeropadding .row.zeromargin.rowalign div:nth-child(3) p").InnerTextAsync()).Trim();
                decimal nav = decimal.Parse((await fund.Locator(".row.zeromargin.top5.rowalign .col-4.zeropadding .row.zeromargin.rowalign div:nth-child(4) p").InnerTextAsync()).Trim().Split(' ')[0]);

                await _fundRepo.AddAsync(new Fund(
                    name: name,
                    ricTicker: ric,
                    bbgTicker: bbg,
                    provider: FundProvider.Azimut,
                    navAmount: nav,
                    navCurrency: currency == "EGP" ? Currency.EGP : Currency.USD,
                    type: type switch
                    {
                        "Fixed Income" => FundType.FixedIncome,
                        "Real Estate" => FundType.RealEstate,
                        "Equity" => FundType.Equity,
                        "Money Market" => FundType.MoneyMarket,
                        "Precious Metals" => FundType.PreciousMetals,
                        "Balanced" => FundType.Balanced,
                        _ => FundType.NONE
                    },
                    subscriptionFrequency: subscription switch
                    {
                        "Weekly" => FundFrequency.Weekly,
                        "Closed" => FundFrequency.Closed,
                        "Daily" => FundFrequency.Daily,
                        _ => FundFrequency.NONE
                    },
                    redemptionFrequency: redemption switch
                    {
                        "Weekly" => FundFrequency.Weekly,
                        "Closed" => FundFrequency.Closed,
                        "Daily" => FundFrequency.Daily,
                        "Monthly" => FundFrequency.Monthly,
                        _ => FundFrequency.NONE
                    }
                ));

                await _fundRepo.SaveChangesAsync();

                Console.WriteLine($"Name: {name}, Currency: {currency}, Type: {type}, RIC: {ric}, BBG: {bbg}, Subscription: {subscription}, Redemption: {redemption}, NAV: {nav}");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
            }
        }
    }
}
