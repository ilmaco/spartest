using Moq;
using Moq.Protected;
using QRViewer.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QRViewer.Tests
{
    public class ChartServiceTests
    {
        private readonly Mock<IApiHandler> mockApiHandler;
        public ChartServiceTests()
        {
            mockApiHandler = new Mock<IApiHandler>(); 
        }

        [Fact]
        public async Task GivenAUrlExpectedStatusSuccessAndValidResultAsync()
        {
            var baseUrl = "http://test.url";
            var testData = new byte[5] { 1, 2, 3, 4, 5 };

            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            httpMessageHandlerMock.Protected().
                        Setup<Task<HttpResponseMessage>>("SendAsync",
                          ItExpr.IsAny<HttpRequestMessage>(),
                          ItExpr.IsAny<CancellationToken>()).
                        ReturnsAsync(new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new ByteArrayContent(testData)
                        }).
                        Verifiable();
            mockApiHandler.Setup(m => m.GetHandler()).Returns(httpMessageHandlerMock.Object);

            var chartApiService = new ChartApiService(mockApiHandler.Object);

            var actual = await chartApiService.GetAsync(baseUrl);
            Assert.NotNull(actual);
            Assert.True(actual.Length == testData.Length);


            httpMessageHandlerMock.Protected().Verify(
              "SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(req =>
              req.Method == HttpMethod.Get &&
              req.RequestUri == new Uri($"{baseUrl}")),
              ItExpr.IsAny<CancellationToken>()
              );
        }

        [Fact]
        public async Task GivenInvalidAUrlExpectedThrowExceptionAsync()
        {
            var baseUrl = "http://test-invalid.url";
            var testData = new byte[5] { 1, 2, 3, 4, 5 };

            var apiHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            apiHandlerMock.Protected().
                        Setup<Task<HttpResponseMessage>>("SendAsync",
                          ItExpr.IsAny<HttpRequestMessage>(),
                          ItExpr.IsAny<CancellationToken>()).
                        ReturnsAsync(new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.InternalServerError
                        }).
                        Verifiable();
            mockApiHandler.Setup(m => m.GetHandler()).Returns(apiHandlerMock.Object);

            var chartApiService = new ChartApiService(mockApiHandler.Object);

            await Assert.ThrowsAsync<HttpRequestException>(() => chartApiService.GetAsync(baseUrl));

        }
    }
}
