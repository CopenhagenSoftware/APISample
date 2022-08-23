using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CopenhagenSoftware.Api.Tests
{
    public sealed class EndpointTests
    {
        [Fact]
        public async Task GetReturns200OK()
        {
            using var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var response = await client.GetAsync(new Uri("/", UriKind.Relative));

            Assert.True(
                response.IsSuccessStatusCode,
                $"Actual status code: {response.StatusCode}");
        }

        [Fact]
        public async Task HealthCheckReturnsHealthy()
        {
            using var factory = new WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var response = await client.GetAsync(new Uri("/healthz", UriKind.Relative));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("Healthy", content);
        }
    }
}