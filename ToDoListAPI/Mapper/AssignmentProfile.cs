using AutoMapper;
using System.Runtime.InteropServices;
using ToDoListAPI.Dtos;
using ToDoListAPI.Entities;

namespace ToDoListAPI.Mapper;

public class AssignmentProfile : Profile
{
    public AssignmentProfile()
    {
        CreateMap<AssignmentDto, Assignment>().ReverseMap();
    }
}