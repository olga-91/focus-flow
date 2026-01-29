using Application.Dtos;
using Application.Services.Implementations;
using Domain.Model;
using Domain.Repositories;
using Moq;

namespace Tests
{
    [TestFixture]
    public class FlowTaskServiceTests
    {
        private Mock<IFlowTaskRepository> _mockFlowTaskRepository;
        private FlowTaskService _service;

        [SetUp]
        public void Setup()
        {
            _mockFlowTaskRepository = new Mock<IFlowTaskRepository>();
            _service = new FlowTaskService(_mockFlowTaskRepository.Object);
        }

        [Test]
        public async Task Create_ShouldReturnCreatedFlowTask()
        {
            var flowTask = new FlowTask { Id = 1, Title = "Test FlowTask" };

            _mockFlowTaskRepository
                .Setup(r => r.AddAsync(It.IsAny<FlowTask>()))
                .Callback<FlowTask>(u => flowTask = u)
                .ReturnsAsync((FlowTask u) => u);
            
            var result = await _service.AddAsync(new FlowTaskDto{Title = "Test FlowTask"});

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(flowTask.Id));
            
            _mockFlowTaskRepository.Verify(r => r.AddAsync(flowTask), Times.Once);
            _mockFlowTaskRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetById_ShouldReturnFlowTask_WhenExists()
        {
            var flowTask = new FlowTask { Id = 1, Title = "Existing FlowTask" };
            _mockFlowTaskRepository.Setup(r => r.FindAsync(1))
                .ReturnsAsync(flowTask);
        
            var result = await _service.GetByIdAsync(1);
        
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }
        
        [Test]
        public void GetById_WhenNotExists_ShouldThrowException()
        {
            _mockFlowTaskRepository.Setup(r => r.FindAsync(2))
                     .ReturnsAsync((FlowTask?)null);
            
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _service.GetByIdAsync(2);
            });

            Assert.That(ex.Message, Is.EqualTo("FlowTask with id 2 not found"));
        }
        
        [Test]
        public async Task Filter_ShouldReturnAllFlowTasks()
        {
            var flowTasks = new List<FlowTask>
            {
                new() { Id = 1, Title = "FlowTask 1", Description =  "FlowTask 1", DueDate =  DateTime.Now,  },
                new() { Id = 2, Title = "FlowTask 2" }
            };
            
            _mockFlowTaskRepository.Setup(r => r.FilterAsync(It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<int?>()))
                     .ReturnsAsync(flowTasks);
        
            var result = await _service.FilterAsync(new FilterDto());
        
            Assert.That(result.Count(), Is.EqualTo(2));
        }
        
        [Test]
        public async Task Update_ShouldReturnUpdatedFlowTask()
        {
            var flowTask = new FlowTask { Id = 1, Title = "Old Name" };
            _mockFlowTaskRepository.Setup(r => r.FindAsync(1))
                .ReturnsAsync(flowTask);
            
            _mockFlowTaskRepository.Setup(r => r.Update(flowTask))
                     .Returns(flowTask);
        
            var result = await _service.UpdateAsync(1, new FlowTaskDto { Title = "New Name" });
        
            Assert.IsNotNull(result);
            _mockFlowTaskRepository.Verify(r => r.Update(flowTask), Times.Once);
        }
        
        [Test]
        public async Task Delete_ShouldCallRepository()
        {
            var flowTask = new FlowTask { Id = 1};
            
            _mockFlowTaskRepository.Setup(r => r.FindAsync(1))
                .ReturnsAsync(flowTask);
            _mockFlowTaskRepository.Setup(r => r.Delete(flowTask));
        
            await _service.DeleteAsync(1);
        
            _mockFlowTaskRepository.Verify(r => r.Delete(flowTask), Times.Once);
        }
    }
}