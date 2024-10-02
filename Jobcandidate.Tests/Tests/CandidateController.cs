using Jobcandidate.Api.Controllers;
using Jobcandidate.Application;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Jobcandidate.Api;

namespace Jobcandidate.Tests
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateService> _mockCandidateService;
        private readonly CandidateController _controller;

        public CandidateControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidateController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task Send_ReturnsOk_WhenCandidateIsCreatedOrUpdated()
        {
            // Arrange
            var dto = new CandiateCreateOrModifyDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Comments = "Test comment",
                PreferWay = 1
            };

            var candidateDto = new CandiateDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                PhoneNumber = "1234567890",
                Comments = "Test comment"
            };

            _mockCandidateService.Setup(service => service.CreateOrModify(dto))
                .ReturnsAsync(candidateDto);

            // Act
            var result = await _controller.Send(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Response>(okResult.Value);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("Success", response.Message);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task Send_ReturnsBadRequest_WhenDtoIsInvalid()
        {
            // Arrange
            var dto = new CandiateCreateOrModifyDto(); // Empty DTO to trigger validation errors

            // Act
            var result = await _controller.Send(dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var badRequestValue = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            Assert.Equal(400, badRequestValue.Status);
            Assert.Equal("One or more validation errors occurred.", badRequestValue.Title);
            Assert.Contains("FirstName", badRequestValue.Errors);
            Assert.Contains("Email", badRequestValue.Errors);
            Assert.Contains("PhoneNumber", badRequestValue.Errors);

            // Check the specific error messages
            Assert.Equal("FirstName cannot be null", badRequestValue.Errors["FirstName"][0]);
            Assert.Equal("Email is not a valid email address", badRequestValue.Errors["Email"][0]);
            Assert.Equal("PhoneNumber is not a valid phone number", badRequestValue.Errors["PhoneNumber"][0]);
        }


    }
}
