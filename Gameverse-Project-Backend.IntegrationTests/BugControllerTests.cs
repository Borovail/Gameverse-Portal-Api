using Back_End.Data;
using Back_End.Models.AuthModels;
using Back_End.Models.BugModes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Gameverse_Project_Backend.IntegrationTests
{
    public class BugControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public BugControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task GetBugs_ReturnsSuccess_WithData()
        {
            // Arrange - подготовка тестовых данных
           await AddTestBugToTestDB();

            // Act - выполнение запроса к эндпоинту
            var response = await _client.GetAsync("/api/bug/bugs");

            // Assert - проверка ответа
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Bug", responseString);
        }


        [Fact]
        public async Task GetBugById_WithValidId_ReturnsSuccess_WithData()
        {
            // Arrange - подготовка тестовых данных
            var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            string id;
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                context.Bugs.Add(new BugModel { Title = "Test Bug", Description = "A test bug", Status = "Open" });
                await context.SaveChangesAsync();
                id = (await context.Bugs.FirstAsync(b => b.Title == "Test Bug")).Id;
            }


            // Act - выполнение запроса к эндпоинту
            var response = await _client.GetAsync($"/api/bug/bug-by-id/{id}");

            // Assert - проверка ответа
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Bug", responseString);
        }

        [Fact]
        public async Task GetBugById_WithInValidId_ReturnsNotFound()
        {
            // Arrange - подготовка тестовых данных
            await AddTestBugToTestDB();

            // Act - выполнение запроса к эндпоинту
            var response = await _client.GetAsync($"/api/bug/bug-by-id{"92"}");

            // Assert - проверка ответа
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetBugById_WithInValidFormatId_ReturnsFailure()
        {
            // Arrange - подготовка тестовых данных
            await AddTestBugToTestDB();

            // Act - выполнение запроса к эндпоинту
            var response = await _client.GetAsync($"/api/bug/bug-by-id{"fsd"}");

            // Assert - проверка ответа
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Fact]
        //public async Task GetUserBug_ReturnSuccess_WithData()
        //{
        //    // Arrange - подготовка тестовых данных
        //    await AddTestBugToTestDB();

        //    // Act - выполнение запроса к эндпоинту
        //    var response = await _client.GetAsync($"/api/bug/bug-by-id{"fsd"}");

        //    // Assert - проверка ответа
        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}








        //[Fact]
        //public async Task CreateBug_ReturnsSuccess()
        //{
        //    // Arrange - подготовка данных для создания бага
        //    var bugModel = new CreateBugModel { Title = "New Bug", Description = "A new bug", Status = "New" };
        //    var content = new StringContent(JsonConvert.SerializeObject(bugModel), Encoding.UTF8, "application/json");

        //    // Act - выполнение запроса к эндпоинту
        //    var response = await _client.PostAsync("/api/bugs", content);

        //    // Assert - проверка ответа
        //    response.EnsureSuccessStatusCode();

        //    // Проверка, что баг добавлен в базу данных
        //    using (var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
        //        var bug = await context.Bugs.FirstOrDefaultAsync(b => b.Title == "New Bug");
        //        Assert.NotNull(bug);
        //    }
        //}




        //[Fact]
        //public async Task Register_ReturnsSuccess_WhenDataIsValid()
        //{
        //    var model = new RegisterModel
        //    {
        //        Name = "TestUser",
        //        Email = "test@example.com",
        //        Password = "Test1234!"
        //    };
        //    var content = JsonContent.Create(model);

        //    var response = await _client.PostAsync("api/account/register", content);

        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();

        //    // Предполагаем, что API возвращает токен в поле "token"
        //    var responseObject = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
        //    Assert.True(responseObject.ContainsKey("token"), "Token is missing in the response");
        //    Assert.False(string.IsNullOrEmpty(responseObject["token"]), "The token is empty");
        //}
        ///Helper functions

        private async Task AddTestBugToTestDB()
        {
            var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
                context.Bugs.Add(new BugModel { Title = "Test Bug", Description = "A test bug", Status = "Open" });
                await context.SaveChangesAsync();
            }
        }

    }














}