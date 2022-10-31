namespace ToDoListAPI.Entities;

public abstract class OwnedEntity
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
}