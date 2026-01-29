using Application.Services.Implementations;
using Domain.Model;
using Domain.Repositories;
using Moq;

namespace Tests
{
    [TestFixture]
    public class ReferenceDataServiceTests
    {
        private Mock<IPriorityRepository> _mockPriorityRepository;
        private Mock<IStatusRepository> _mockStatusRepository;
        private ReferenceDataService _service;

        [SetUp]
        public void Setup()
        {
            _mockPriorityRepository = new Mock<IPriorityRepository>();
            _mockStatusRepository = new Mock<IStatusRepository>();
            _service = new ReferenceDataService(_mockPriorityRepository.Object, _mockStatusRepository.Object);
        }
        
        [Test]
        public async Task GetAll_ShouldReturnAllPriorities()
        {
            var priorities = new List<Priority>
            {
                new() { Id = 1, Name = "High" },
                new() { Id = 2, Name = "Low" }
            };
            
            _mockPriorityRepository.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(priorities);

            var result = await _service.GetPrioritiesAsync();
        
            Assert.That(result.Count(), Is.EqualTo(2));
        }
        
        [Test]
        public async Task GetAll_ShouldReturnAllStatuses()
        {
            var statuses = new List<Status>
            {
                new() { Id = 1, Name = "Done" },
                new() { Id = 2, Name = "Todo" }
            };
            
            _mockStatusRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(statuses);

            var result = await _service.GetStatusesAsync();
        
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}