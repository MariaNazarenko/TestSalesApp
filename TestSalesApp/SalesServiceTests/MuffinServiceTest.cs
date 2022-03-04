using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SalesApi.Models;
using SalesApi.Services;
using System;
using Xunit;

namespace SalesApiTests
{
    public class MuffinServiceTest
    {
        private readonly SalesService _salesService;
        private readonly Mock<ILogger<SalesService>> _logger;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IConfigurationSection> _confSection;
        private readonly DataContext _dataContext;
        private readonly string conectionString = "User ID=postgres;Password=qwerty;Host=localhost;Port=5432;Database=postgres;Pooling=true;";


        public MuffinServiceTest()
        {
            _logger = new Mock<ILogger<SalesService>>();
            _confSection = new Mock<IConfigurationSection>();
            _confSection.SetupGet(m => m[It.Is<string>(s => s == "PostgresString")]).Returns(conectionString);
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(_confSection.Object);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _dataContext = new DataContext(_configuration.Object);
            _salesService = new SalesService(_logger.Object, _dataContext);
        }

        [Fact]
        public void Create()
        {
            var result = _salesService.Create();
            result.Should().NotBeNull();
        }

        [Fact]
        public void Buy()
        {
            _salesService.Buy(0).Should().BeFalse();
            _salesService.Buy(-1).Should().BeFalse();
            _salesService.Buy(1).Should().BeTrue();
            _salesService.Buy(4).Should().BeFalse();
        }

        [Fact]
        public void GetReport()
        {
            var result = _salesService.GetReport();
            result.Should().NotBeNull();
        }
    }
}