using System.Net;
using System.Text;
using Newtonsoft.Json;
using OpenSourceRecipes.Models;

[Collection("Sequential")]
public class CommentVoteTests
{

    private readonly HttpClient _client = SharedTestResources.Factory.CreateClient();


    // COMMENT VOTE TESTS
    [Fact]
    public async Task HandleCommentVote_ShouldSucceed()
    {
        // Arrange
        var newCommentVote = new
        {
            Upvote = true,
        };

        var newUser = new
        {
            Username = "seededuser",
            Password = "password"
        };

        var loginRequest = new HttpRequestMessage(HttpMethod.Post, "/api/login")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json")
        };

        var loginResponse = await _client.SendAsync(loginRequest);
        loginResponse.EnsureSuccessStatusCode();

        var userDetails = await loginResponse.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<GetLoggedInUserDto>(userDetails);

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(newCommentVote), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", "Bearer " + user?.Token);

        //Act
        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Upvote added.", content);

        var requestDownvote = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = false }), Encoding.UTF8, "application/json")
        };
        requestDownvote.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseDownvote = await _client.SendAsync(requestDownvote);

        var contentDownvote = await responseDownvote.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseDownvote.StatusCode);
        Assert.Equal("Down-vote added.", contentDownvote);

        // add another downvote to remove it
        var requestDownvote2 = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = false }), Encoding.UTF8, "application/json")
        };

        requestDownvote2.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseDownvote2 = await _client.SendAsync(requestDownvote2);

        var contentDownvote2 = await responseDownvote2.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseDownvote2.StatusCode);
        Assert.Equal("Down-vote removed.", contentDownvote2);

        // add another upvote
        var requestUpvote2 = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = true }), Encoding.UTF8, "application/json")
        };

        requestUpvote2.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseUpvote2 = await _client.SendAsync(requestUpvote2);

        var contentUpvote2 = await responseUpvote2.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseUpvote2.StatusCode);
        Assert.Equal("Upvote added.", contentUpvote2);

        // add another upvote to remove it
        var requestUpvote3 = new HttpRequestMessage(HttpMethod.Post, "/api/commentvotes/20")
        {
            Content = new StringContent(JsonConvert.SerializeObject(new { Upvote = true }), Encoding.UTF8, "application/json")
        };

        requestUpvote3.Headers.Add("Authorization", "Bearer " + user?.Token);

        var responseUpvote3 = await _client.SendAsync(requestUpvote3);

        var contentUpvote3 = await responseUpvote3.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, responseUpvote3.StatusCode);
        Assert.Equal("Upvote removed.", contentUpvote3);
    }
}
