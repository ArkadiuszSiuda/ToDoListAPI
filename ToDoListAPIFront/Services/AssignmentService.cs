using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPIFront.Models;

namespace ToDoListAPIFront.Services;

public class AssignmentService : IAssignmentService
{
    private readonly HttpClient _http;
    private readonly NavigationManager _navigationManager;

    public AssignmentService(HttpClient http, NavigationManager navigationManager)
    {
        _http = http;
        _navigationManager = navigationManager;
    }

    public List<Assignment> Assignments { get; set; } = new List<Assignment>();
    public List<Assignment> FilteredAssignments { get; set; } = new List<Assignment>();

    public async Task CreateAssignment(Assignment assignment)
    {
        var result = await _http.PostAsJsonAsync("api/assignments", assignment);
        await SetAssignment(result);
    }

    private async Task SetAssignment(HttpResponseMessage result)
    {
        var response = await result.Content.ReadFromJsonAsync<List<Assignment>>();
        Assignments = response;
        _navigationManager.NavigateTo("mainpage");
    }

    public async Task DeleteAssignment(Guid id)
    {
        var result = await _http.DeleteAsync($"api/assignments/{id}");
        await SetAssignment(result);
    }

    public async Task<Assignment> GetAssignment(Guid id)
    {
        var result = await _http.GetFromJsonAsync<Assignment>($"api/assignments/{id}");
        if (result != null)
        {
            return result;
        }
        throw new Exception("Assignment not found!");
    }

    public async Task GetAssignments(DateTime? day = null)
    {
        if (day == null)
            day = DateTime.UtcNow;

        var result = await _http.GetFromJsonAsync<List<Assignment>>("api/assignments");
        if (result != null)
        {
            Assignments = result;
        }
    }

    public async Task UpdateAssignment(Assignment assignment, Guid id)
    {
        var result = await _http.PutAsJsonAsync($"api/assignments/{id}", assignment);
        await SetAssignment(result);
    }

    public void FilterAssignments(DateTime? day)
    {
        FilteredAssignments = Assignments.Where(a => day.Value.AddDays(1) >= a.Added && day <= a.Deadline).ToList();
    }
}