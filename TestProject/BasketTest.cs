using Microsoft.Playwright;
using TestProject.Base;

namespace TestProject;

[TestClass]
public class BasketTest : UiTestBase
{
    [TestMethod]
    public async Task AddProductsToBasketTest()
    {
        //Open website
        await Page.GotoAsync(WebsiteUrl);
        //Accept cookies
        await Page.ClickAsync(Locators.AcceptCookieButton);
        //Click on computers notebooks category
        await Page.ClickAsync(Locators.ComputersNotebooksCategory);
        //Choose notebooks
        await Page.ClickAsync(Locators.NotebooksCategory);
        //Filter from most expensive
        await Page.ClickAsync(Locators.MostExpensiveFilterButton);

        //Wait for loading spinner
        var spinner = Page.Locator(Locators.LoadingSpinner);
        await spinner.WaitForAsync( new LocatorWaitForOptions()
        {
            State = WaitForSelectorState.Hidden
        });
        
        //Check there are 2 and more products after filter
        var products = await Page.QuerySelectorAllAsync(Locators.FirstRowProducts);
        Assert.IsTrue(products.Count > 1, "There are no 2 products available");
        
        //First product
        var firstProductNameElement = await products[0].QuerySelectorAsync(Locators.ProductName);
        Assert.IsNotNull(firstProductNameElement, "First product name field is null");
        var firstProductName = await firstProductNameElement.InnerTextAsync();
        Assert.IsFalse(string.IsNullOrEmpty(firstProductName), "First product name is empty");
        
        //Add first product to the basket
        var firstProductBuyButton = await products[0].QuerySelectorAsync(Locators.AddProductToBasketButton);
        Assert.IsNotNull(firstProductBuyButton, "First product buy button is not present");

        //Click on buy button
        await firstProductBuyButton.ClickAsync();

        //Go back o shopping
        await Page.ClickAsync(Locators.GoBackToShoppingButton);
        
        //Wait for products to be available
        await Page.WaitForSelectorAsync(Locators.FirstRowProducts);
        products = await Page.QuerySelectorAllAsync(Locators.FirstRowProducts);
        Assert.IsTrue(products.Count > 1, "There is less than 2 products");
        
        var secondProductNameElement = await products[1].QuerySelectorAsync(Locators.ProductName);
        
        Assert.IsNotNull(secondProductNameElement, "Second product name field is null");
        var secondProductName = await secondProductNameElement.InnerTextAsync();
        Assert.IsFalse(string.IsNullOrEmpty(secondProductName), "Second product name is empty");
        
        //Add second product to the basket
        var secondProductBuyButton = await products[1].QuerySelectorAsync(Locators.AddProductToBasketButton);
        Assert.IsNotNull(secondProductBuyButton, "Second product buy button is not present");
        
        //Click on buy button
        await secondProductBuyButton.ClickAsync();
        
        //Go to basket
        await Page.ClickAsync(Locators.GoToShoppingBagButton);
        
        //Wai for basket loaded
        await Page.WaitForSelectorAsync(Locators.BasketProductsFrame);

        //Check first product is in basket
        var firstBasketProduct = Page.Locator(Locators.BasketProductName, new PageLocatorOptions() { HasTextString = firstProductName});
        Assert.IsTrue(await firstBasketProduct.IsVisibleAsync());
        
        //Check second product is in basket
        var secondBasketProduct = Page.Locator(Locators.BasketProductName, new PageLocatorOptions() { HasTextString = secondProductName});
        Assert.IsTrue(await secondBasketProduct.IsVisibleAsync());
    }
}