using Application.Dtos;
using Application.Services.Implementations;
using Domain.Model;
using Domain.Repositories;
using Moq;

namespace Tests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private Mock<IProjectRepository> _mockProjectRepository;
        private ProjectService _service;

        [SetUp]
        public void Setup()
        {
            _mockProjectRepository = new Mock<IProjectRepository>();
            _service = new ProjectService(_mockProjectRepository.Object);
        }

        [Test]
        public async Task Create_ShouldReturnCreatedProject()
        {
            var project = new Project { Id = 1, Name = "Test Project" };

            _mockProjectRepository
                .Setup(r => r.AddAsync(It.IsAny<Project>()))
                .Callback<Project>(u => project = u)
                .ReturnsAsync((Project u) => u);
            
            var result = await _service.AddAsync(new ProjectDto{Name = "Test Project"});

            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(project.Id));
            
            _mockProjectRepository.Verify(r => r.AddAsync(project), Times.Once);
            _mockProjectRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task GetById_ShouldReturnProject_WhenExists()
        {
            var project = new Project { Id = 1, Name = "Existing Project" };
            _mockProjectRepository.Setup(r => r.FindAsync(1))
                .ReturnsAsync(project);
        
            var result = await _service.GetByIdAsync(1);
        
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }
        
        [Test]
        public async Task GetById_ShouldReturnNull_WhenNotExists()
        {
            _mockProjectRepository.Setup(r => r.FindAsync(2))
                     .ReturnsAsync((Project?)null);
            
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await _service.GetByIdAsync(2);
            });

            Assert.That(ex.Message, Is.EqualTo("Project with id 2 not found"));
        }
        
        [Test]
        public async Task GetAll_ShouldReturnAllProjects()
        {
            var projects = new List<Project>
            {
                new() { Id = 1, Name = "Project 1" },
                new() { Id = 2, Name = "Project 2" }
            };
            
            _mockProjectRepository.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(projects);
        
            var result = await _service.GetAllAsync();
        
            Assert.That(result.Count(), Is.EqualTo(2));
        }
        
        [Test]
        public async Task Update_ShouldReturnUpdatedProject()
        {
            var project = new Project { Id = 1, Name = "Old Name" };
            _mockProjectRepository.Setup(r => r.FindAsync(1))
                .ReturnsAsync(project);
            
            _mockProjectRepository.Setup(r => r.Update(project))
                     .Returns(project);
        
            var result = await _service.UpdateAsync(1, new ProjectDto { Name = "New Name" });
        
            Assert.IsNotNull(result);
            _mockProjectRepository.Verify(r => r.Update(project), Times.Once);
        }
        
        [Test]
        public async Task Delete_ShouldCallRepository()
        {
            var project = new Project { Id = 1};
            
            _mockProjectRepository.Setup(r => r.FindAsync(1))
                .ReturnsAsync(project);
            _mockProjectRepository.Setup(r => r.Delete(project));
        
            await _service.DeleteAsync(1);
        
            _mockProjectRepository.Verify(r => r.Delete(project), Times.Once);
        }
    }
}