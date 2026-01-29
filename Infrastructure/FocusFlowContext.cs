using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class FocusFlowContext(DbContextOptions<FocusFlowContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<FlowTask> Tasks { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Priority> Priorities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasIndex(x => x.Name).IsUnique();
        
        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(
                new User { Id = 1, Username = "test", Name = "Test", Password = "1234", Email = "test@test.com" }
            );

        modelBuilder.Entity<Priority>()
            .HasData(
                new Priority { Id = 1, Name = "High" },
                new Priority { Id = 2, Name = "Medium" },
                new Priority { Id = 3, Name = "Low" }
            );

        modelBuilder.Entity<Status>()
            .HasData(
                new Priority { Id = 1, Name = "Todo" },
                new Priority { Id = 2, Name = "In Progress" },
                new Priority { Id = 3, Name = "Done" }
            );

        modelBuilder.Entity<Project>()
            .HasData(
                new Project { Id = 1, Name = "Demo Project 1", Description = "Test Description" },
                new Project { Id = 2, Name = "Demo Project 2", Description = "Test Description" },
                new Project { Id = 3, Name = "Demo Project 3", Description = "Test Description" },
                new Project { Id = 4, Name = "Demo Project 4", Description = "Test Description" }
            );
            
        modelBuilder.Entity<FlowTask>()
            .HasData(
                new FlowTask
                {
                    Id = 1,
                    Title = "Design database schema",
                    Description = "Create initial ER diagram and tables",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    PriorityId = 1,
                    StatusId = 1,
                    ProjectId = null,
                    AssignedUserId = 1
                },
                new FlowTask
                {
                    Id = 2,
                    Title = "Implement authentication",
                    Description = "JWT-based login and registration",
                    DueDate = DateTime.UtcNow.AddDays(10),
                    PriorityId = 1,
                    StatusId = 2,
                    ProjectId = 1,
                    AssignedUserId = 1
                },
                new FlowTask
                {
                    Id = 3,
                    Title = "Create task management API",
                    Description = "CRUD endpoints for FlowTask",
                    DueDate = DateTime.UtcNow.AddDays(14),
                    PriorityId = 2,
                    StatusId = 1,
                    ProjectId = 1,
                    AssignedUserId = 1
                },
                new FlowTask
                {
                    Id = 4,
                    Title = "Write unit tests",
                    Description = "Cover services",
                    DueDate = DateTime.UtcNow.AddDays(20),
                    PriorityId = 3,
                    StatusId = 1,
                    ProjectId = 1,
                    AssignedUserId = null
                },
                new FlowTask
                {
                    Id = 5,
                    Title = "Deploy to staging",
                    Description = "Dockerize and deploy to staging environment",
                    DueDate = DateTime.UtcNow.AddDays(25),
                    PriorityId = 2,
                    StatusId = 3,
                    ProjectId = 1,
                    AssignedUserId = null
                });
    }
}