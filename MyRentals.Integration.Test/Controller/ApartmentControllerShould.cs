namespace MyRentals.Integration.Test.Controller
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentAssertions;
    using MyRentals.Integration.Test.Fixtures;
    using MyRentals.Web;
    using MyRentals.Web.Models;
    using Xunit;

    public class ApartmentControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient unAuthorisedClient;
        private readonly HttpClient authorisedNonAdminClient;

        public ApartmentControllerShould(CustomWebApplicationFactory<Startup> fixture)
        {
            this.unAuthorisedClient = fixture.GetUnAuthorisedClient();
            this.authorisedNonAdminClient = fixture.GetAuthorisedNonAdmin();
        }

        [Fact]
        public async Task GetAll()
        {
            using var response = await this.unAuthorisedClient.GetAsync("/apartment/");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<IEnumerable<SaveApartment>>(body);
            a.Should().NotBeNull();
            a.Should().NotBeEmpty();
        }

        [Fact]
        public async Task AddUnauthorised()
        {
            using var response = await this.unAuthorisedClient.PostAsJsonAsync<object>("/apartment/", null);
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        //[Fact]
        //public async Task AddAuthorised()
        //{
        //    using var response = await this.authorisedNonAdminClient.PostAsJsonAsync("/apartment/", new SaveApartment());
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //}
    }
}
