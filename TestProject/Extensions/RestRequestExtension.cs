using RestSharp;

namespace TestProject.Extensions;

public static class RestRequestExtension
{
    public static void AddAcceptApplicationJsonHeader(this RestRequest request)
    {
        request.AddHeader("Accept", "application/json");
    }
}