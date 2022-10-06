using Microsoft.Playwright;

namespace TestProject.Base;

/// <summary>
/// Base class for UI tests
/// </summary>
[TestClass]
public class UiTestBase
{
    protected const string WebsiteUrl = "https://alza.cz";
    protected IBrowser Browser { get; private set; }
    protected IPage Page { get; private set; }
    
    /// <summary>
    /// Test initialize
    /// </summary>
    [TestInitialize]
    public async  Task Initialize()
    {
        var playwright = await Playwright.CreateAsync();
        Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false, 
            Args = new[] { "--start-maximized" }
        });
        Page = await Browser.NewPageAsync(new BrowserNewPageOptions()
        {
            ViewportSize = new ViewportSize { Width = 1280, Height = 1024 }
        });
    }
    
    /// <summary>
    /// Test cleanup
    /// </summary>
    [TestCleanup]
    public async  Task Cleanup()
    {
        if (Browser is not null)
        {
            await Browser.CloseAsync();
        }
    }
}