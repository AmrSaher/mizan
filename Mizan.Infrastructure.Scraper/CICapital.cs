using Microsoft.Playwright;

namespace Mizan.Infrastructure.Scraper
{
    public static class CICapital
    {
        public static async Task Run()
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

            await page.GotoAsync("https://www.cicapital.com/fundprice", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.NetworkIdle // Wait for all JS to finish
            });

            // Wait for a specific element to load (crucial for JS sites)
            await page.WaitForSelectorAsync("#page-content");

            var funds = (await page.Locator("#page-content tr").AllAsync()).ToList();

            funds.Remove(funds.First());

            var type = "";

            foreach (var fund in funds)
            {
                int index = 1;

                if ((await fund.Locator("td").AllAsync()).Count > 2)
                {
                    type = (await fund.Locator("td:nth-child(1)").InnerTextAsync()).Trim();
                    index++;
                }

                string name;

                try
                {
                    if ((await fund.Locator($"td:nth-child({index}) a").CountAsync()) == 0)
                    {
                        name = (await fund.Locator($"td:nth-child({index})").InnerTextAsync()).Trim();
                    }
                    else
                    {
                        name = (await fund.Locator($"td:nth-child({index}) a").InnerTextAsync()).Trim();
                    }
                } catch
                {
                    name = (await fund.Locator($"td:nth-child({index})").InnerTextAsync()).Trim();
                }

                string navS = (await fund.Locator($"td:nth-child({index + 1})").InnerTextAsync()).Trim();
                navS = string.IsNullOrEmpty(navS) ? "0" : navS;
                decimal nav = decimal.Parse(navS);

                Console.WriteLine($"Name: {name}, Type: {type}, NAV: {nav}");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
            }
        }
    }
}
