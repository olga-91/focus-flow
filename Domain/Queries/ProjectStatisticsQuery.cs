namespace Domain.Queries;

public class ProjectStatisticsQuery
{
    public string ProjectName { get; set; }
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int OverdueTasks { get; set; }
}