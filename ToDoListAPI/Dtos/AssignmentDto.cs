namespace ToDoListAPI.Dtos;

public class AssignmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public DateTime Added { get; set; }

    public DateTime Deadline { get; set; }
}