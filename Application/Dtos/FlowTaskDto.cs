namespace Application.Dtos;

public class FlowTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public int PriorityId { get; set; }
    public string? PriorityName { get; set; }
    public int StatusId { get; set; }
    public string? StatusName { get; set; }
    public int? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public int? AssignedUserId { get; set; }
    public string? AssignedUser { get; set; }
}