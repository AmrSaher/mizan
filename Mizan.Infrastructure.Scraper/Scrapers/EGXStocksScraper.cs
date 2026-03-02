using Microsoft.Playwright;
using Mizan.Domain.Entities.StockDomain;
using Mizan.Domain.Enums;
using Mizan.Domain.Repositories;

namespace Mizan.Infrastructure.Scraper.Scrapers
{
    public class EGXStocksScraper
    {
        private readonly IStockRepository _stockRepo;

        public EGXStocksScraper(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task Run()
        {
            var exitCode = Program.Main(new[] { "install" });
            if (exitCode != 0)
            {
                throw new Exception($"Playwright browser install failed with exit code {exitCode}");
            }

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

            await page.GotoAsync("https://www.egx.com.eg/en/ListedStocks.aspx", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle // Wait for all JS to finish
            });

            // Wait for a specific element to load (crucial for JS sites)
            await page.WaitForSelectorAsync("tr.RowStyle");

            var stocks = await page.Locator("tr.RowStyle, tr.AlternatingRowStyle").AllAsync();

            foreach (var stock in stocks)
            {
                var name = (await stock.Locator("td:nth-child(1) table tbody tr td:nth-child(1) a span").InnerTextAsync()).Trim();
                var isinCode = (await stock.Locator("td[align=center]").InnerTextAsync()).Trim();
                var sector = (await stock.Locator("td").Last.InnerTextAsync()).Trim() switch
                {
                    "Banks" => StockSector.Banks,
                    "Basic Resources" => StockSector.BasicResources,
                    "Health Care & Pharmaceuticals" => StockSector.HealthCareAndPharmaceuticals,
                    "Industrial Goods , Services and Automobiles" => StockSector.IndustrialGoodsAndServicesAndAutomobiles,
                    "Real Estate" => StockSector.RealEstate,
                    "Travel & Leisure" => StockSector.TravelAndLeisure,
                    "Utilities" => StockSector.Utilities,
                    "IT , Media & Communication Services" => StockSector.ITAndMediaAndCommunicationServices,
                    "Food, Beverages and Tobacco" => StockSector.FoodAndBeveragesAndTobacco,
                    "Energy & Support Services" => StockSector.EnergyAndSupportServices,
                    "Trade & Distributors" => StockSector.TradeAndDistributors,
                    "Shipping & Transportation Services" => StockSector.ShippingAndTransportationServices,
                    "Education Services" => StockSector.EducationServices,
                    "Non-bank financial services" => StockSector.NonBankFinancialServices,
                    "Contracting & Construction Engineering" => StockSector.ContractingAndConstructionEngineering,
                    "Textile & Durables" => StockSector.TextileAndDurables,
                    "Building Materials" => StockSector.BuildingMaterials,
                    "Paper & Packaging" => StockSector.PaperAndPackaging,
                    _ => StockSector.None
                };

                var stockEntity = new Stock(
                    name: name,
                    provider: StockProvider.EGX
                );

                stockEntity.SetBasicData(isinCode: isinCode, "22dsd", new DateOnly(), 9999, 0.2m, Currency.EGP, "egypt", sector);

                await _stockRepo.AddAsync(stockEntity);

                await _stockRepo.SaveChangesAsync();

                Console.WriteLine($"Name: {name}, ISIN: {isinCode}, Sector: {sector}");
                Console.WriteLine("----------------------------");
            }
        }
    }
}
