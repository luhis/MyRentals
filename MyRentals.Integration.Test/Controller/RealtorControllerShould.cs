namespace MyRentals.Integration.Test.Controller
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using MyRentals.Integration.Test.Fixtures;
    using MyRentals.Web;
    using Xunit;

    public class RealtorControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient unAuthorisedClient;
        public RealtorControllerShould(CustomWebApplicationFactory<Startup> fixture)
        {
            this.unAuthorisedClient = fixture.GetUnAuthorisedClient();
        }

        [Fact]
        public async Task GetAllUnauthorised()
        {
            using var response = await this.unAuthorisedClient.GetAsync("/realtor/");
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

    }
}