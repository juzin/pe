using RestSharp;

namespace TestProject.Base;

/// <summary>
/// Base class for Api tests
/// </summary>
[TestClass]
public class ApiTestBase
{
    protected const string ApiKey = "xxx";

    protected string ApiToken = "yyyyyyyyyy";
    protected Uri BaseAddress { get; } = new ("https://api.trello.com/");
    
    /// <summary>
    /// Rest client
    /// </summary>
    protected RestClient Client { get; private set; }
    
    [TestInitialize]
    public void TestInitialize()
    {
        Client = new RestClient(BaseAddress);
    }
}