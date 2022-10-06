namespace TestProject;

public static class Locators
{
    /// <summary>
    /// //a[contains(@class, 'cookie') and contains(@class, 'accept')]
    /// </summary>
    public const string AcceptCookieButton = "//a[contains(@class, 'cookie') and contains(@class, 'accept')]";

    /// <summary>
    /// //a[@href='/pocitace-a-notebooky']
    /// </summary>
    public const string ComputersNotebooksCategory = "//a[@href='/pocitace-a-notebooky']";
    
    /// <summary>
    /// //a[contains(@href, '/notebooky/')]
    /// </summary>
    public const string NotebooksCategory = "//a[contains(@href, '/notebooky/')]";

    /// <summary>
    /// //a[@id='hlNejdrazsiSeo']/parent::li
    /// </summary>
    public const string MostExpensiveFilterButton = "//a[@id='hlNejdrazsiSeo']/parent::li";

    /// <summary>
    /// //span[contains(@class, 'circle') and contains(@class, 'loader')]
    /// </summary>
    public const string LoadingSpinner = "//span[contains(@class, 'circle') and contains(@class, 'loader')]";

    /// <summary>
    /// //div[contains(@class, 'firstRow') and contains(@class, 'box')]
    /// </summary>
    public const string FirstRowProducts = "//div[contains(@class, 'firstRow') and contains(@class, 'box')]";
    
    /// <summary>
    /// Product name inside product from <see cref="FirstRowProducts"/>
    /// //a[contains(@class, 'browsinglink') and contains(@class, 'name')]
    /// </summary>
    public const string ProductName = "//a[contains(@class, 'browsinglink') and contains(@class, 'name')]";

    /// <summary>
    /// //span[contains(@class, 'cannotChangeQuantity')]
    /// </summary>
    public const string AddProductToBasketButton = "//span[contains(@class, 'cannotChangeQuantity')]";

    /// <summary>
    /// #varBBackButton
    /// </summary>
    public const string GoBackToShoppingButton = "#varBBackButton";

    /// <summary>
    /// #varBToBasketButton
    /// </summary>
    public const string GoToShoppingBagButton = "#varBToBasketButton";

    /// <summary>
    /// #o1basket
    /// </summary>
    public const string BasketProductsFrame = "#o1basket";

    /// <summary>
    /// //a[@class='mainItem']
    /// </summary>
    public const string BasketProductName = "//a[@class='mainItem']";
}