using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using ZLMClaims.Models;
using ZLMClaims.Services;

namespace ZLMClaimsTest.Services;

public class RepairCompanyServiceTest
{
    [Fact]
    public async Task GetRepairCompaniesAsync_ReturnsRepairCompanies()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var expectedRepairCompanies = new List<RepairCompany>
    {
        new RepairCompany { Id = 1, Name = "Company 1" },
        new RepairCompany { Id = 2, Name = "Company 2" }
    };

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(@"[
            { ""id"": 1, ""name"": ""Company 1"", ""Email"": ""company1@example.com"", ""Street"":""Street1"", ""Housenumber"": ""1"", ""City"": ""City1"", ""Zipcode"": ""1111""},
            { ""id"": 2, ""name"": ""Company 2"", ""Email"": ""company2@example.com"", ""Street"":""Street2"", ""Housenumber"": ""2"", ""City"": ""City2"", ""Zipcode"": ""2222""}
        ]")
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var repairCompanyService = new RepairCompanyService(httpClient);

        // Act
        var repairCompanies = await repairCompanyService.GetRepairCompaniesAsync();

        // Assert
        Assert.Equal(expectedRepairCompanies.Count, repairCompanies.Count());
        Assert.Equal(expectedRepairCompanies[0].Id, repairCompanies.First().Id);
        Assert.Equal(expectedRepairCompanies[0].Name, repairCompanies.First().Name);
        Assert.Equal(expectedRepairCompanies[0].Email, repairCompanies.First().Email);
        Assert.Equal(expectedRepairCompanies[0].Street, repairCompanies.First().Street);
        Assert.Equal(expectedRepairCompanies[0].Housenumber, repairCompanies.First().Housenumber);
        Assert.Equal(expectedRepairCompanies[0].City, repairCompanies.First().City);
        Assert.Equal(expectedRepairCompanies[0].Zipcode, repairCompanies.First().Zipcode);
        Assert.Equal(expectedRepairCompanies[1].Id, repairCompanies.Skip(1).First().Id);
        Assert.Equal(expectedRepairCompanies[1].Name, repairCompanies.Skip(1).First().Name);
        Assert.Equal(expectedRepairCompanies[1].Email, repairCompanies.Skip(1).First().Email);
        Assert.Equal(expectedRepairCompanies[1].Street, repairCompanies.Skip(1).First().Street);
        Assert.Equal(expectedRepairCompanies[1].Housenumber, repairCompanies.Skip(1).First().Housenumber);
        Assert.Equal(expectedRepairCompanies[1].City, repairCompanies.Skip(1).First().City);
        Assert.Equal(expectedRepairCompanies[1].Zipcode, repairCompanies.Skip(1).First().Zipcode);
    }
}
