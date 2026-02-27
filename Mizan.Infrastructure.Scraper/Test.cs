using Microsoft.Playwright;

namespace Mizan.Infrastructure.Scraper
{
    public static class Test
    {
        public static async Task Run()
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
                var name = await stock.Locator("td:nth-child(1) table tbody tr td:nth-child(1) a span").InnerTextAsync();
                var isinCode = await stock.Locator("td[align=center]").InnerTextAsync();
                var sector = await stock.Locator("td").Last.InnerTextAsync();

                Console.WriteLine($"Name: {name}, ISIN: {isinCode}, Sector: {sector}");
                Console.WriteLine("----------------------------");
            }
        }
    }
}
