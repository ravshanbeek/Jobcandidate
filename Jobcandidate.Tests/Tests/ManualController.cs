using Jobcandidate.Api.Controllers;
using Jobcandidate.Application;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using Jobcandidate.Api;
using Jobcandidate.Application.Services;

namespace Jobcandidate.Tests
{
    public class ManualControllerTests
    {
        private readonly Mock<IManualService> _mockManualService;
        private readonly ManualController _controller;

        public ManualControllerTests()
        {
            _mockManualService = new Mock<IManualService>();
            _controller = new ManualController(_mockManualService.Object);
        }

        [Fact]
        public void GetPreferWay_ReturnsOkWithPreferWayList()
        {
            // Arrange
            var preferWayList = new List<PreferWayDto>
            {
                new PreferWayDto { Id = 1, Name = "Call" },
                new PreferWayDto { Id = 2, Name = "Email" }
            };

            _mockManualService.Setup(service => service.GetPrefer())
                .Returns(preferWayList);

            // Act
            var result = _controller.GetPreferWay();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Response>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Success", response.Message);
            Assert.NotNull(response.Data);
        }
    }
}
