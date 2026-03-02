using Microsoft.Playwright;
using Mizan.Domain.Enums;
using Mizan.Domain.Repositories;
using Mizan.Domain.Entities.FundDomain;

namespace Mizan.Infrastructure.Scraper.Scrapers
{
    public class EFGHermesScraper
    {
        private readonly IFundRepository _fundRepo;

        public EFGHermesScraper(IFundRepository fundRepo)
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

            await page.GotoAsync("https://efgholding.com/en/our-services/mutual-funds", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle // Wait for all JS to finish
            });

            // Wait for a specific element to load (crucial for JS sites)
            await page.WaitForSelectorAsync("table.w-full.border-collapse");

            var fundTypes = await page.Locator("table tbody").AllAsync();

            int i = 1;

            foreach (var fundType in fundTypes)
            {
                FundType type = FundType.NONE;

                switch (i)
                {
                    case 1:
                    case 2:
                        type = FundType.Equity;
                        break;
                    case 3:
                    case 4:
                        type = FundType.MoneyMarket;
                        break;
                    case 5:
                    case 6:
                        type = FundType.FixedIncome;
                        break;
                    case 7:
                        type = FundType.Balanced;
                        break;
                    case 8:
                        type = FundType.PreciousMetals;
                        break;
                    default:
                        break;
                }

                var funds = await fundType.Locator("tr").AllAsync();

                foreach (var fund in funds)
                {
                    var name = (await fund.Locator("td:nth-child(1) a").InnerTextAsync()).Trim();
                    decimal nav = decimal.Parse((await fund.Locator("td:nth-child(2)").InnerTextAsync()).Trim());

                    await _fundRepo.AddAsync(new Fund(
                        name: name,
                        navAmount: nav,
                        navCurrency: Currency.EGP,
                        provider: FundProvider.EFGHermes,
                        type: type,
                        ricTicker: null,
                        bbgTicker: null,
                        subscriptionFrequency: FundFrequency.NONE,
                        redemptionFrequency: FundFrequency.NONE
                    ));

                    await _fundRepo.SaveChangesAsync();

                    Console.WriteLine($"Name: {name}, NAV: {nav}, Type: {type}");
                    Console.WriteLine("-------------------------------------------------------------------------------------------");
                }

                i++;
            }
        }
    }
}
