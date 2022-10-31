using FrontToDoListAPI.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace FrontToDoListAPI.Client.Services;

public class AssignmentsService : IAssignmentsService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigationManager;

    public AssignmentsService(HttpClient http, NavigationManager navigationManager)
    {
        _http = http;
        _navigationManager = navigationManager;
    }

    public List<Assignments> Assignments { get; set; } = new List<Assignments>();

    public async Task CreateAssignment(Assignments assignment)
    {
        var result = await _http.PostAsJsonAsync("api/assignments", assignment);
        await SetAssignments(result);
    }

    private async Task SetAssignments(HttpResponseMessage result)
    {
        var response = await result.Content.ReadFromJsonAsync<List<Assignments>>();
        Assignments = response;
        _navigationManager.NavigateTo("assignments");
    }

    public async Task DeleteAssignment(Guid id)
    {
        var result = await _http.DeleteAsync($"api/assignments/{id}");
        await SetAssignments(result);
    }

    public async Task<Assignments> GetAssignment(Guid id)
    {
        var result = await _http.GetFromJsonAsync<Assignments>($"api/assignments/{id}");
        if (result != null)
        {
            return result;
        }
        throw new Exception("Assignment not found!");
    }

    public async Task GetAssignments()
    {
        var nus = await _http.GetStringAsync("api/assignments");
        Console.WriteLine(nus);
        //var result = await _http.GetFromJsonAsync<List<Assignments>>("api/assignments");
        //if (result != null)
        //    Assignments = result;
    }

    public async Task UpdateAssignment(Assignments assignment, Guid id)
    {
        var result = await _http.PutAsJsonAsync($"api/assignments/{id}", assignment);
        await SetAssignments(result);
    }
}