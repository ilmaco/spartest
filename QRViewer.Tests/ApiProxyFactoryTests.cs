using Microsoft.Extensions.Configuration;
using Moq;
using QRViewer.Services;
using System.Net;
using System.Net.Http;

using Xunit;

namespace QRViewer.Tests
{
    public class ApiProxyFactoryTests
    {
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly Mock<IConfigurationSection> useProxySection;
        private readonly Mock<IConfigurationSection> hostSection;
        private readonly Mock<IConfigurationSection> portSection;
        private readonly Mock<IConfigurationSection> byPassSection;
        private readonly Mock<IConfigurationSection> useDefalutCredSection;
        private readonly Mock<IConfigurationSection> userSection;
        private readonly Mock<IConfigurationSection> passwordSection;
        private readonly string host = "host";
        private readonly int port = 1;
        private readonly bool bypassOnLocal = true;       
        private readonly string userName = "user";
        private readonly string password = "pass";
        bool useDefaultCredential = true;

        public ApiProxyFactoryTests()
        {
            mockConfiguration = new Mock<IConfiguration>();

            useProxySection = new Mock<IConfigurationSection>();
            useProxySection.Setup(a => a.Value).Returns("true");
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:UseProxy")).Returns(useProxySection.Object);

            hostSection = new Mock<IConfigurationSection>();
            hostSection.Setup(a => a.Value).Returns(host);
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:Host")).Returns(hostSection.Object);

            portSection = new Mock<IConfigurationSection>();
            portSection.Setup(a => a.Value).Returns(port.ToString());
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:Port")).Returns(portSection.Object);

            byPassSection = new Mock<IConfigurationSection>();
            byPassSection.Setup(a => a.Value).Returns(bypassOnLocal.ToString());
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:BypassOnLocal")).Returns(byPassSection.Object);

            useDefalutCredSection = new Mock<IConfigurationSection>();
            useDefalutCredSection.Setup(a => a.Value).Returns(useDefaultCredential.ToString());
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:UseDefaultCredential")).Returns(useDefalutCredSection.Object);

            userSection = new Mock<IConfigurationSection>();
            userSection.Setup(a => a.Value).Returns(userName);
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:UserName")).Returns(userSection.Object);
            
            passwordSection = new Mock<IConfigurationSection>();
            passwordSection.Setup(a => a.Value).Returns(password);
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:Password")).Returns(passwordSection.Object);

        }

        [Fact]
        public void GivenAProxyConfigVerifyIsSetCorrect()
        {    
            var apiHandler = new ApiProxyFactory(mockConfiguration.Object);
            WebProxy result = (WebProxy)apiHandler.GetWebProxy();            
            Assert.NotNull(result);
            Assert.Equal(result.BypassProxyOnLocal, bypassOnLocal);
            Assert.Equal(result.Address.Host, host);
            Assert.Equal(result.Address.Port, port);
            Assert.Equal(result.UseDefaultCredentials, useDefaultCredential);
        }

        [Fact]
        public void GivenAProxyConfigIgnoreVerifyIsSetCorrect()
        {
            var useNoProxySection = new Mock<IConfigurationSection>();
            useNoProxySection.Setup(a => a.Value).Returns("false");
            mockConfiguration.Setup(a => a.GetSection("ChartApi:Proxy:UseProxy")).Returns(useNoProxySection.Object);

            var apiHandler = new ApiProxyFactory(mockConfiguration.Object);
            WebProxy result = (WebProxy)apiHandler.GetWebProxy();
            Assert.Null(result);
        }
    }
}

