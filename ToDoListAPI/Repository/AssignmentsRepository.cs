using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Db;
using ToDoListAPI.Dtos;
using ToDoListAPI.Entities;
using ToDoListAPI.Interfaces;

namespace ToDoListAPI.Repository;

public class AssignmentsRepository : IAssignmentsRepository
{
    private readonly ToDoListContext _context;
    private readonly IMapper _mapper;

    public AssignmentsRepository(ToDoListContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AssignmentDto> CreateAssignment(AssignmentDto assignmentDto)
    {
        var dbAssignment = new Assignment { };
        _mapper.Map(assignmentDto, dbAssignment);
        await _context.AddAsync(dbAssignment);
        await _context.SaveChangesAsync();

        return _mapper.Map<AssignmentDto>(dbAssignment);
    }

    public async Task DeleteAssignment(Guid id)
    {
        var assignment = await _context.Assignments.FirstOrDefaultAsync(t => t.Id == id);
        if (assignment != null)
        {
            _context.Remove(assignment);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<AssignmentDto>> GetAssignments()
    {
        return _mapper.Map<List<AssignmentDto>>(await _context.Assignments.ToListAsync());
    }

    public async Task<AssignmentDto> GetAssignment(Guid id)
    {
        return _mapper.Map<AssignmentDto>(await _context.Assignments.FirstOrDefaultAsync(t => t.Id == id));
    }

    public async Task<AssignmentDto> UpdateAssignment(AssignmentDto assignmentDto, Guid id)
    {
        assignmentDto.Id = id;
        var assignment = await _context.Assignments.FirstOrDefaultAsync(t => t.Id == id);
        _mapper.Map(assignmentDto, assignment);
        await _context.SaveChangesAsync();

        return _mapper.Map<AssignmentDto>(assignment);
    }
}