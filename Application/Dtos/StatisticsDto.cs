namespace Application.Dtos;

public class StatisticsDto
{
    public string ProjectName { get; set; }
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int OverdueTasks { get; set; }
}