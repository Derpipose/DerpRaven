﻿namespace DerpRaven.Shared.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string OAuth { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool Active { get; set; } = false;
}
