﻿namespace ToDoListAPI.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}