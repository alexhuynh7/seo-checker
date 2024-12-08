using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using SEOAutoWebApi.Cache;
using SEOAutoWebApi.Features;
using SEOAutoWebApi.Infrastructure.Enums;
using SEOAutoWebApi.Infrastructure.Interface;
using SEOAutoWebApi.Infrastructure.Services;
using SEOAutoWebApi.Models;
using System.Net;
using System.Net.Http;

namespace SEOAuto.Web.Test.Feature
{
    public class SearchRequestTests
    {
        [Fact]
        public async Task SearchRequest_WhenCalled_ReturnSuccess()
        {
            // Arrange
            var request = new SearchRequest.Command
            {
                Keyword = "example",
                Url = "http://example.com",
                BrowserType = BrowserType.Google
            };

            var mockCacheService = new Mock<ICacheService>();
            var mockSearchService = new Mock<ISearchServiceFactory>();
            var mockValidator = new Mock<IValidator<SearchRequest.Command>>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("example") });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            var googleSearchService = new GoogleSearchService(client);
            mockSearchService.Setup(m => m.GetSearchService(BrowserType.Google)).Returns(googleSearchService);

            // Act
            var handler = new SearchRequest.Handler(mockCacheService.Object, mockSearchService.Object, mockValidator.Object);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<ResponseModel>(result);
            Assert.Equal(StatusCodeReturnType.Success, result.Code);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetSupportBrowsersAsync_ShouldReturnOkResult_WhenRequestIsValid()
        {
            // Arrange
            var request = new GetSupportBrowsersRequest();
            var expectedResponse = new ResponseModel
            {
                Data = new List<SupportBrowserModel>
                {
                    new() { BrowserType = BrowserType.All, BrowserName = "All" },
                    new() { BrowserType = BrowserType.Google, BrowserName = "Google" },
                    new() { BrowserType = BrowserType.Bing, BrowserName = "Bing" }
                }
            };
            var mockCacheService = new Mock<ICacheService>();
            //var _mockMediator = new Mock<IMediator>();
            //_mockMediator.Setup(m => m.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResponse);

            // Act
            var handler = new GetSupportBrowsersRequest.Handler(mockCacheService.Object);
            var result = await handler.Handle();

            // Assert
            var returnValue = Assert.IsType<ResponseModel>(result);
            List<SupportBrowserModel> resultList = Enumerable.ToList<SupportBrowserModel>(returnValue.Data);
            Assert.Equal(expectedResponse.Data.Count, resultList.Count);
        }
    }
}