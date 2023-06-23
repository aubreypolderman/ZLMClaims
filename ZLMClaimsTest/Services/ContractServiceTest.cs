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

public class ContractServiceTest
{
    [Fact]
    public async Task GetAllContractsByPersonIdAsync_ReturnsContracts()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var expectedContracts = new List<Contract>
            {
                new Contract
                {
                    Id = 1,
                    UserId = 1,
                    Product = "Personenauto",
                    Make = "KIA",
                    Model = "Ceed 1.8",
                    LicensePlate = "HF067X",
                    DamageFreeYears = 10,
                    StartingDate = new DateTime(2022, 1, 1),
                    EndDate = new DateTime(2023, 1, 1),
                    AnnualPolicyPremium = 100.0
                },
                new Contract
                {
                    Id = 2,
                    UserId = 2,
                    Product = "Personenauto",
                    Make = "OPEL",
                    Model = "Astra 1.8",
                    LicensePlate = "69SXKZ",
                    DamageFreeYears = 15,
                    StartingDate = new DateTime(2022, 2, 1),
                    EndDate = new DateTime(2023, 2, 1),
                    AnnualPolicyPremium = 150.0
                }
            };

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(expectedContracts))
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var contractService = new ContractService(httpClient);

        // Act
        var contracts = await contractService.GetAllContractsByPersonIdAsync(1);

        // Assert
        Assert.Equal(expectedContracts.Count, contracts.Count());
        Assert.Equal(expectedContracts[0].Id, contracts.First().Id);
        Assert.Equal(expectedContracts[0].UserId, contracts.First().UserId);
        Assert.Equal(expectedContracts[0].Product, contracts.First().Product);
        Assert.Equal(expectedContracts[0].Make, contracts.First().Make);
        Assert.Equal(expectedContracts[0].Model, contracts.First().Model);
        Assert.Equal(expectedContracts[0].LicensePlate, contracts.First().LicensePlate);
        Assert.Equal(expectedContracts[0].DamageFreeYears, contracts.First().DamageFreeYears);
        Assert.Equal(expectedContracts[0].StartingDate, contracts.First().StartingDate);
        Assert.Equal(expectedContracts[0].EndDate, contracts.First().EndDate);
        Assert.Equal(expectedContracts[0].AnnualPolicyPremium, contracts.First().AnnualPolicyPremium);

        Assert.Equal(expectedContracts[1].Id, contracts.Skip(1).First().Id);
        Assert.Equal(expectedContracts[1].UserId, contracts.Skip(1).First().UserId);
        Assert.Equal(expectedContracts[1].Product, contracts.Skip(1).First().Product);
        Assert.Equal(expectedContracts[1].Make, contracts.Skip(1).First().Make);
        Assert.Equal(expectedContracts[1].Model, contracts.Skip(1).First().Model);
        Assert.Equal(expectedContracts[1].LicensePlate, contracts.Skip(1).First().LicensePlate);
        Assert.Equal(expectedContracts[1].DamageFreeYears, contracts.Skip(1).First().DamageFreeYears);
        Assert.Equal(expectedContracts[1].StartingDate, contracts.Skip(1).First().StartingDate);
        Assert.Equal(expectedContracts[1].EndDate, contracts.Skip(1).First().EndDate);
        Assert.Equal(expectedContracts[1].AnnualPolicyPremium, contracts.Skip(1).First().AnnualPolicyPremium);
    }

    [Fact]
    public async Task GetContractByIdAsync_ReturnsContract()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var expectedContract = new Contract
        {
            Id = 1,
            UserId = 1,
            Product = "Personenauto",
            Make = "KIA",
            Model = "Ceed 1.8",
            LicensePlate = "HF067X",
            DamageFreeYears = 10,
            StartingDate = new DateTime(2022, 1, 1),
            EndDate = new DateTime(2023, 1, 1),
            AnnualPolicyPremium = 100.0
        };

        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(expectedContract))
        };

        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var contractService = new ContractService(httpClient);

        // Act
        var contract = await contractService.GetContractByIdAsync(1);

        // Assert
        Assert.Equal(expectedContract.Id, contract.Id);
        Assert.Equal(expectedContract.UserId, contract.UserId);
        Assert.Equal(expectedContract.Product, contract.Product);
        Assert.Equal(expectedContract.Make, contract.Make);
        Assert.Equal(expectedContract.Model, contract.Model);
        Assert.Equal(expectedContract.LicensePlate, contract.LicensePlate);
        Assert.Equal(expectedContract.DamageFreeYears, contract.DamageFreeYears);
        Assert.Equal(expectedContract.StartingDate, contract.StartingDate);
        Assert.Equal(expectedContract.EndDate, contract.EndDate);
        Assert.Equal(expectedContract.AnnualPolicyPremium, contract.AnnualPolicyPremium);
    }
}
