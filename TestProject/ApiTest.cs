using System.Net;
using RestSharp;
using TestProject.Base;
using TestProject.Dtos;
using TestProject.Extensions;

namespace TestProject;

[TestClass]
public class ApiTest : ApiTestBase
{
    /// <summary>
    /// Create new board resource
    /// </summary>
    /// <param name="boardName"></param>
    /// <returns></returns>
    private string CreateBoardResource(string boardName) => $"1/boards/?name={boardName}&key={ApiKey}&token={ApiToken}";
    
    /// <summary>
    /// GET, UPDATE, DELETE board resource
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string BoardResource(string id) => $"1/boards/{id}?key={ApiKey}&token={ApiToken}";

    /// <summary>
    /// Create list on Board resource
    /// </summary>
    /// <param name="boardId"></param>
    /// <param name="listName"></param>
    /// <returns></returns>
    private string CreateListResource(string boardId, string listName) => $"1/boards/{boardId}/lists?name={listName}&key={ApiKey}&token={ApiToken}";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="boardId"></param>
    /// <returns></returns>
    private string GetBoardListsResource(string boardId) => $"1/boards/{boardId}/lists?key={ApiKey}&token={ApiToken}";
    
    [TestMethod]
    public async Task BoardCrudOperationsTest()
    {
        //Create Board
        var expectedName = "Test board1";
        var response = await CreateNewBoard(expectedName);
        Assert.IsNotNull(response.Data, "Response data is null");
        Assert.IsFalse(string.IsNullOrEmpty(response.Data.Id), "Board id is null or empty");
        Assert.AreEqual(expectedName, response.Data.Name, "Created board has different name than expected");
        
        //Update
        var expectedDescription = "Test description 1";
        var expectedNewName = "New board name1";
        var request = new RestRequest(BoardResource(response.Data.Id));
        request.AddAcceptApplicationJsonHeader();
        request.AddJsonBody(new BoardDto()
        {
            Name = expectedNewName,
            Desc = expectedDescription
        });

        var updateResponse = await Client.ExecutePutAsync<BoardDto>(request);

        Assert.AreEqual(HttpStatusCode.OK, updateResponse.StatusCode);
        Assert.IsNotNull(updateResponse.Data);
        Assert.AreEqual(expectedNewName, updateResponse.Data.Name);
        Assert.AreEqual(expectedDescription, updateResponse.Data.Desc);

        //Delete
        var deleteResponse = await DeleteBoard(response.Data.Id);
        Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
        
        //Verify that board does not exist
        request = new RestRequest(BoardResource(response.Data.Id));
        request.AddAcceptApplicationJsonHeader();

        var getBoardResponse = await Client.ExecuteGetAsync(request);
        Assert.AreEqual(HttpStatusCode.NotFound, getBoardResponse.StatusCode);
    }

    [TestMethod]
    public async Task AddListToBoard()
    {
        //Create Board
        var expectedName = "Board with lists";
        var response = await CreateNewBoard(expectedName);
        Assert.IsNotNull(response.Data);

        //Create list
        var expectedListName = "New TODO list1";
        var request = new RestRequest(CreateListResource(response.Data.Id, expectedListName));
        request.AddAcceptApplicationJsonHeader();

        var createListResponse = await Client.ExecutePostAsync<ListDto>(request);
        
        Assert.AreEqual(HttpStatusCode.OK, createListResponse.StatusCode);
        Assert.IsNotNull(createListResponse.Data);
        Assert.AreEqual(expectedListName, createListResponse.Data.Name);
        
        //Get lists ond board
        request = new RestRequest(GetBoardListsResource(response.Data.Id));
        request.AddAcceptApplicationJsonHeader();

        //Check lists
        var getBoardListsResponse = await Client.ExecuteGetAsync<List<ListDto>>(request);
        Assert.AreEqual(HttpStatusCode.OK, getBoardListsResponse.StatusCode);
        Assert.IsNotNull(getBoardListsResponse.Data);
        Assert.IsNotNull(getBoardListsResponse.Data.Any(x => x.Name == expectedListName));

        //Delete Board
        var deleteResponse = await DeleteBoard(response.Data.Id);
        Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);
    }

    [TestMethod]
    public async Task UnAuthorizedUserTest()
    {
        var request = new RestRequest("1/boards/?name=NoUserBoard&key=xxx&token=yyy");
        request.AddAcceptApplicationJsonHeader();

        var response = await Client.ExecutePostAsync<BoardDto>(request);
        
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    /// <summary>
    /// Creates new board with specific name
    /// </summary>
    /// <param name="name">Board name</param>
    /// <returns></returns>
    private async Task<RestResponse<BoardDto>> CreateNewBoard(string name)
    {
        var request = new RestRequest(CreateBoardResource(name));
        request.AddAcceptApplicationJsonHeader();

        var response = await Client.ExecutePostAsync<BoardDto>(request);
        Assert.AreEqual(HttpStatusCode.OK ,response.StatusCode);

        return response;
    }

    /// <summary>
    /// Deletes board by id
    /// </summary>
    /// <param name="id">Board id</param>
    /// <returns></returns>
    private async Task<RestResponse> DeleteBoard(string id)
    {
        var request = new RestRequest(BoardResource(id))
        {
            Method = Method.Delete 
        };
        request.AddAcceptApplicationJsonHeader();
        
        var deleteResponse = await Client.ExecuteAsync(request);
        Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);

        return deleteResponse;
    }
}