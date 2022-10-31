using ToDoListAPIFront.Models;

namespace ToDoListAPIFront.Services;

public interface IAssignmentService
{
    List<Assignment> Assignments { get; set; }
    List<Assignment> FilteredAssignments { get; set; }

    Task GetAssignments(DateTime? day = null);

    Task<Assignment> GetAssignment(Guid id);

    Task CreateAssignment(Assignment assignment);

    Task UpdateAssignment(Assignment assignment, Guid id);

    Task DeleteAssignment(Guid id);

    void FilterAssignments(DateTime? day);
}