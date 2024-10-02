using AutoMapper;
using Jobcandidate.Application;
using Jobcandidate.Domain;
using Jobcandidate.Shared;
using Moq;
using System.Linq.Expressions;

namespace Jobcandidate.Tests
{
    public class CandidateServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICandidateRepository> _mockCandidateRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CandidateService _service;

        public CandidateServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCandidateRepository = new Mock<ICandidateRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockUnitOfWork.SetupGet(uow => uow.CandidateRepository).Returns(_mockCandidateRepository.Object);

            _service = new CandidateService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateOrModify_ShouldUpdateCandidate_WhenCandidateExists()
        {
            // Arrange
            var existingCandidate = new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                LinkedInProfile = "https://linkedin.com/johndoe",
                GitHubProfile = "https://github.com/johndoe",
                Comments = "Existing candidate",
                PreferWay = PreferWay.Email,
                CallTimeInterval = "9AM-11AM"
            };

            var dto = new CandiateCreateOrModifyDto
            {
                FirstName = "John Updated",
                LastName = "Doe Updated",
                PhoneNumber = "0987654321",
                Email = "john.doe@example.com", // Same email
                LinkedInProfile = "https://linkedin.com/johndoeupdated",
                GitHubProfile = "https://github.com/johndoeupdated",
                Comments = "Updated candidate",
                PreferWay = 1,
                CallTimeInterval = "11AM-1PM"
            };

            _mockCandidateRepository
                .Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Candidate, bool>>>(), It.IsAny<bool>()))
                .Returns(new[] { existingCandidate }.AsQueryable());

            _mockMapper
                .Setup(m => m.Map<CandiateDto>(existingCandidate))
                .Returns(new CandiateDto
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    LinkedInProfile = dto.LinkedInProfile,
                    GitHubProfile = dto.GitHubProfile,
                    Comments = dto.Comments,
                    CallTimeInterval = dto.CallTimeInterval
                });

            // Act
            var result = await _service.CreateOrModify(dto);

            // Assert
            Assert.Equal(dto.FirstName, result.FirstName);
            Assert.Equal(dto.LastName, result.LastName);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
            Assert.Equal(dto.LinkedInProfile, result.LinkedInProfile);
            Assert.Equal(dto.GitHubProfile, result.GitHubProfile);
            Assert.Equal(dto.Comments, result.Comments);
            Assert.Equal(dto.CallTimeInterval, result.CallTimeInterval);

            _mockCandidateRepository.Verify(repo => repo.Update(existingCandidate), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateOrModify_ShouldCreateCandidate_WhenCandidateDoesNotExist()
        {
            // Arrange
            var dto = new CandiateCreateOrModifyDto
            {
                FirstName = "New",
                LastName = "User",
                PhoneNumber = "1234567890",
                Email = "new.user@example.com", // New email
                LinkedInProfile = "https://linkedin.com/newuser",
                GitHubProfile = "https://github.com/newuser",
                Comments = "New candidate",
                PreferWay = 1,
                CallTimeInterval = "9AM-11AM"
            };

            var newCandidate = new Candidate
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                LinkedInProfile = dto.LinkedInProfile,
                GitHubProfile = dto.GitHubProfile,
                Comments = dto.Comments,
                PreferWay = PreferWay.Email,
                CallTimeInterval = dto.CallTimeInterval
            };

            _mockCandidateRepository
                .Setup(repo => repo.FindByCondition(It.IsAny<Expression<Func<Candidate, bool>>>(), It.IsAny<bool>()))
                .Returns(Enumerable.Empty<Candidate>().AsQueryable());

            _mockCandidateRepository
                .Setup(repo => repo.Create(It.IsAny<Candidate>()))
                .ReturnsAsync(newCandidate);

            _mockMapper
                .Setup(m => m.Map<CandiateDto>(newCandidate))
                .Returns(new CandiateDto
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    LinkedInProfile = dto.LinkedInProfile,
                    GitHubProfile = dto.GitHubProfile,
                    Comments = dto.Comments,
                    CallTimeInterval = dto.CallTimeInterval
                });

            // Act
            var result = await _service.CreateOrModify(dto);

            // Assert
            Assert.Equal(dto.FirstName, result.FirstName);
            Assert.Equal(dto.LastName, result.LastName);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.PhoneNumber, result.PhoneNumber);
            Assert.Equal(dto.LinkedInProfile, result.LinkedInProfile);
            Assert.Equal(dto.GitHubProfile, result.GitHubProfile);
            Assert.Equal(dto.Comments, result.Comments);
            Assert.Equal(dto.CallTimeInterval, result.CallTimeInterval);

            _mockCandidateRepository.Verify(repo => repo.Create(It.IsAny<Candidate>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

    }
}
