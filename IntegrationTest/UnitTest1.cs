using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace IntegrationTest
{
    [TestClass]
    public class UnitTest1
    {
        private static TestContext _testContext;
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _testContext = testContext;
            _factory = new WebApplicationFactory<Program>();
        }
        [TestMethod]
        public async Task Get_All_Books_ShouldReturnOkResponse()
        {
            var client = _factory.CreateClient();
            var response = client.GetAsync("api/Book");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}