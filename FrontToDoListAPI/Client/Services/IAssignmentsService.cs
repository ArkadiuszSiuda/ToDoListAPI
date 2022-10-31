using FrontToDoListAPI.Client.Models;

namespace FrontToDoListAPI.Client.Services;

public interface IAssignmentsService
{
    List<Assignments> Assignments { get; set; }

    Task GetAssignments();

    Task<Assignments> GetAssignment(Guid id);

    Task CreateAssignment(Assignments assignment);

    Task UpdateAssignment(Assignments assignment, Guid id);

    Task DeleteAssignment(Guid id);
}