using Application.Dtos;
using Application.Exceptions;
using Application.Services.Implementations;
using Domain.Model;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UserService _service;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _service = new UserService(_mockUserRepository.Object);
        }

        [Test]
        public async Task Register_ShouldReturnCreatedUser()
        {
            var user = new User { Id = 1, Username = "Test User" };

            _mockUserRepository
                .Setup(r => r.AddAsync(It.IsAny<User>()))
                .Callback<User>(u => user = u)
                .ReturnsAsync((User u) => u);
            
            var result = await _service.RegisterAsync(new UserRegisterDto{Username = "Test User"});

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(user.Id));
            
            _mockUserRepository.Verify(r => r.AddAsync(user), Times.Once);
            _mockUserRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        
        [Test]
        public void Register_Duplicate_ShouldThrowException()
        {
            _mockUserRepository.Setup(r => r.UserExists(It.IsAny<string>()))
                .ReturnsAsync(true);
            
            var ex = Assert.ThrowsAsync<DataConflictException>(async () =>
            {
                await _service.RegisterAsync(new UserRegisterDto{Username = "Test User"});
            });

            Assert.That(ex.Message, Is.EqualTo("Username already exists"));
        }
    }
}