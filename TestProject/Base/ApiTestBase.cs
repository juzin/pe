using RestSharp;

namespace TestProject.Base;

/// <summary>
/// Base class for Api tests
/// </summary>
[TestClass]
public class ApiTestBase
{
    protected const string ApiKey = "29ee192fbc2b3f319503d04049f74a1e";

    protected string ApiToken = "e5da56dfc7036b72675f299d3d128b95b472db2a2dec528ae764edacf59bf1ca";
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