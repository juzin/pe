using Microsoft.Playwright;

namespace TestProject;

[TestClass]
public class BasketTest : TestBase
{
    [TestMethod]
    public async Task AddProductsToBasketTest()
    {
        //Open website
        await Page.GotoAsync(WebsiteUrl);
        //Accept cookies
        await Page.ClickAsync(PageObject.AcceptCookieButton);
        //Click on computers notebooks category
        await Page.ClickAsync(PageObject.ComputersNotebooksCategory);
        //Choose notebooks
        await Page.ClickAsync(PageObject.NotebooksCategory);
        //Filter from most expensive
        await Page.ClickAsync(PageObject.MostExpensiveFilterButton);

        //Wait for loading spinner
        var spinner = Page.Locator(PageObject.LoadingSpinner);
        await spinner.WaitForAsync( new LocatorWaitForOptions()
        {
            State = WaitForSelectorState.Hidden
        });
        
        //Check there are 2 and more products after filter
        var products = await Page.QuerySelectorAllAsync(PageObject.FirstRowProducts);
        Assert.IsTrue(products.Count > 1, "There are no 2 products available");
        
        //First product
        var firstProductNameElement = await products[0].QuerySelectorAsync(PageObject.ProductName);
        Assert.IsNotNull(firstProductNameElement, "First product name field is null");
        var firstProductName = await firstProductNameElement.InnerTextAsync();
        Assert.IsFalse(string.IsNullOrEmpty(firstProductName), "First product name is empty");
        
        //Add first product to the basket
        var firstProductBuyButton = await products[0].QuerySelectorAsync(PageObject.AddProductToBasketButton);
        Assert.IsNotNull(firstProductBuyButton, "First product buy button is not present");

        //Click on buy button
        await firstProductBuyButton.ClickAsync();

        //Go back o shopping
        await Page.ClickAsync(PageObject.GoBackToShoppingButton);
        
        //Wait for products to be available
        await Page.WaitForSelectorAsync(PageObject.FirstRowProducts);
        products = await Page.QuerySelectorAllAsync(PageObject.FirstRowProducts);
        Assert.IsTrue(products.Count > 1, "There is less than 2 products");
        
        var secondProductNameElement = await products[1].QuerySelectorAsync(PageObject.ProductName);
        
        Assert.IsNotNull(secondProductNameElement, "Second product name field is null");
        var secondProductName = await secondProductNameElement.InnerTextAsync();
        Assert.IsFalse(string.IsNullOrEmpty(secondProductName), "Second product name is empty");
        
        //Add second product to the basket
        var secondProductBuyButton = await products[1].QuerySelectorAsync(PageObject.AddProductToBasketButton);
        Assert.IsNotNull(secondProductBuyButton, "Second product buy button is not present");
        
        //Click on buy button
        await secondProductBuyButton.ClickAsync();
        
        //Go to basket
        await Page.ClickAsync(PageObject.GoToShoppingBagButton);
        
        //Wai for basket loaded
        await Page.WaitForSelectorAsync(PageObject.BasketProductsFrame);

        //Check first product is in basket
        var firstBasketProduct = Page.Locator(PageObject.BasketProductName, new PageLocatorOptions() { HasTextString = firstProductName});
        Assert.IsTrue(await firstBasketProduct.IsVisibleAsync());
        
        //Check second product is in basket
        var secondBasketProduct = Page.Locator(PageObject.BasketProductName, new PageLocatorOptions() { HasTextString = secondProductName});
        Assert.IsTrue(await secondBasketProduct.IsVisibleAsync());
    }
}