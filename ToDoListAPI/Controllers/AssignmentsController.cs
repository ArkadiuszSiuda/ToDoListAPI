using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Dtos;
using ToDoListAPI.Interfaces;

namespace ToDoListAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssignmentsController : ControllerBase
{
    private readonly IAssignmentsRepository _assignmentsRepository;

    public AssignmentsController(IAssignmentsRepository assignmentsRepository)
    {
        _assignmentsRepository = assignmentsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<AssignmentDto>>> GetAssignments()
    {
        return Ok(await _assignmentsRepository.GetAssignments());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentDto>> GetAssignment(Guid id)
    {
        return await _assignmentsRepository.GetAssignment(id) is AssignmentDto r ? Ok(r) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<AssignmentDto>> CreateAssignment([FromBody] AssignmentDto assignmentDto)
    {
        var assignment = await _assignmentsRepository.CreateAssignment(assignmentDto);

        if (assignment == null)
        {
            return BadRequest();
        }

        return Ok(await _assignmentsRepository.GetAssignments());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AssignmentDto>> UpdateAssignment([FromRoute] Guid id, [FromBody] AssignmentDto assignmentDto)
    {
        var assignment = await _assignmentsRepository.UpdateAssignment(assignmentDto, id);

        if (assignment == null)
        {
            return BadRequest();
        }
        return Ok(await _assignmentsRepository.GetAssignments());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AssignmentDto>> DeleteAssignment([FromRoute] Guid id)
    {
        await _assignmentsRepository.DeleteAssignment(id);
        return Ok(await _assignmentsRepository.GetAssignments());
    }
}