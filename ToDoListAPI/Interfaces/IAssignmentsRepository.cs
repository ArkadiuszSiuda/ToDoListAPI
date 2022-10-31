using ToDoListAPI.Dtos;

namespace ToDoListAPI.Interfaces;

public interface IAssignmentsRepository
{
    Task<List<AssignmentDto>> GetAssignments();

    Task<AssignmentDto> GetAssignment(Guid id);

    Task<AssignmentDto> CreateAssignment(AssignmentDto assignmentDto);

    Task<AssignmentDto> UpdateAssignment(AssignmentDto assignmentDto, Guid id);

    Task DeleteAssignment(Guid id);
}